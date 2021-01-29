using System.BLL.Helpers;
using System.BLL.Models.CostsManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.CostManagement
{
    public class CostsService : ICostsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CostsService(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CostsModel>> GetAllAsync()
        {
            return await _context.Costs.Select(item => _mapper.Map<CostsModel>(item)).ToListAsync();
        }

        public async Task<CostsModel> GetAsync(int id)
        {
            var result = await _context.Costs
                .SingleOrDefaultAsync(ev => ev.Id == id);

            if (result == null) throw new AppException($"Event with id: {id} not found.");

            var map = _mapper.Map<CostsModel>(result);
            return map;
        }

        public async Task<CostsModel> AddAsync(CostsModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Costs
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Event already exist.");

            var map = _mapper.Map<Costs>(entity);

            await _context.Costs
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }
        
        public async Task<CostsModel> UpdateAsync(int id, CostsModel newEntity)
        {
            if (!_context.Costs.Any(evnt => evnt.Id == id))
            {
                throw new AppException("Not found");
            }

            newEntity.Id = id;

            var map = _mapper.Map<Costs>(newEntity);

            _context.Costs
                .Update(map);

            await _context.SaveChangesAsync();

            return newEntity;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Costs
                .SingleOrDefaultAsync(costs => costs.Id == id);

            if (result == null) throw new AppException($"Costs with id: {id} not found.");

            _context.Costs.Remove(result);

            await _context.SaveChangesAsync();
        }
    }
}