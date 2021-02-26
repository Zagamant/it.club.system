using System.BLL.Models.Helpers;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.BLL.Helpers
{
    public abstract class Repository<TId, T, TAddModel, TUpdateModel, TModel> : IRepository<TId, TAddModel, TUpdateModel, TModel>
        where TAddModel : BaseModel
        where TUpdateModel : BaseModel
        where TModel : BaseModel
        where T : BaseEntity, new()
    {
        protected DbSet<T> _table { get; init; }
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
            
        public Repository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));       
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(string sort = "",string page = "",string pageSize = "", string filter = "")
        {
            var entityQuery = _table.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                var filterVal = (JObject) JsonConvert.DeserializeObject(filter);
                var t = new T();
                foreach (var (key, value) in filterVal)
                {
                    if (key == "q")
                    {
                        //TODO: Logic for non correct filters
                        continue;
                    }
                    entityQuery = entityQuery
                        .Where(t.GetType().GetProperty(key.FirstCharToUpper())?.PropertyType == typeof(string)
                            ? $"{key}.Contains(@0)"
                            : $"{key} == @0", value.ToString());
                }
            }

            // if (!string.IsNullOrEmpty(search))
            // {
            //     var t = new T();
            //
            //     entityQuery = entityQuery
            //         .Where(t.GetType().GetProperty(key.FirstCharToUpper()).PropertyType == typeof(string)
            //             ? $"{key}.Contains(@0)"
            //             : $"{key} == @0", value.ToString());
            //
            // }


            if (!string.IsNullOrEmpty(sort))
            {
                var sortVal = sort.Split('+');
                var condition = sortVal.First();
                var order = sortVal.Last() == "ASC" ? "" : "descending";
                entityQuery = entityQuery.OrderBy($"{condition} {order}");
            }

            if (!string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(pageSize))
            {
                
                var pageNumber = int.Parse(page);
                var pageSizeNumber = int.Parse(pageSize);
            
                var from = (pageNumber-1)*pageSizeNumber;
                var to = (pageNumber)*pageSizeNumber;
                
                entityQuery = entityQuery.Skip(from).Take(to - from + 1);
            }

            return await entityQuery
                .Select(item => _mapper.Map<TModel>(item))
                .ToListAsync();
        }

        public virtual async Task<TModel> GetAsync(TId id)
        {
            var entity = await _table.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"{typeof(T)} not found");
            }

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> AddAsync(TAddModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _table
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Event already exist.");

            var map = _mapper.Map<T>(entity);

            await _table
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> UpdateAsync(TId id, TUpdateModel updatedGroup)
        {
            var realItem = _mapper.Map<T>(updatedGroup);
            var entityId = (TId)typeof(T).GetProperty("Id").GetValue(realItem);
             if (!id.Equals( entityId))
             {
                 throw new ArgumentException();
             }

             _context.Entry(realItem).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!EntityExists(id))
                 {
                     throw;
                 }
                 else
                 {
                     throw;
                 }
             }

             return _mapper.Map<TModel>(await _table.FindAsync(entityId));
        }

        public virtual async Task DeleteAsync(TId id, bool isDelete = false)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
                throw new ArgumentException($"{typeof(T)} not found");

            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        protected bool EntityExists(TId id)
        {
            return _table.Any(e => ((TId)typeof(T).GetProperty("Id").GetValue(e)).Equals(id));
        }
    }
}