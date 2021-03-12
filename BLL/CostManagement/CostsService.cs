using System.BLL.Helpers;
using System.BLL.Models.CostsManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.CostManagement
{
    public class CostsService :  Repository<int, Costs, CostsModel,CostsModel,CostsModel>, ICostsService
    {
        public CostsService(DataContext context, IMapper mapper, ILogger<CostsService> logger) : base(context, mapper, logger)
        {
            _table = _context.Costs;
        }
        
        public override async Task<CostsModel> AddAsync(CostsModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Costs
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Event already exist.");

            var map = _mapper.Map<Costs>(entity);

            map.Club = await _context.Clubs.SingleOrDefaultAsync(e => e.Id == map.ClubId);
            
            await _context.Costs
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public override async Task<CostsModel> UpdateAsync(int id, CostsModel updatedGroup)
        {
            if (!_context.Events.Any(evnt => evnt.Id == id))
            {
                throw new AppException("Not found");
            }

            updatedGroup.Id = id;

            var map = _mapper.Map<Costs>(updatedGroup);

            map.Club = await _context.Clubs.SingleOrDefaultAsync(e => e.Id == map.ClubId);

            _context.Costs
                .Update(map);

            await _context.SaveChangesAsync();

            return updatedGroup;
        }
    }
}