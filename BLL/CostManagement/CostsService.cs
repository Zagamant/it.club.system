using System.BLL.Helpers;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.CostManagement
{
	public class CostsService : ICostsService
	{
		private readonly DataContext _context;

		public CostsService(DataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<IEnumerable<Costs>> GetAllAsync()
		{
			var result = await _context.Costs
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<Costs> GetAsync(int id)
		{
			var result = await _context.Costs
				.AsNoTracking()
				.SingleOrDefaultAsync(cos => cos.Id == id);

			if (result == null) throw new AppException($"Costs with id: {id} not found.");

			return result;
		}

		public async Task AddAsync(Costs entity)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));

			await _context.Costs.AddAsync(entity);
			
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Costs dbEntity, Costs newEntity)
		{
			if (newEntity == null) throw new ArgumentNullException(nameof(newEntity));
			if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

			var result = await _context.Costs
				.SingleOrDefaultAsync(cos => cos.Id == dbEntity.Id);

			if (result == null) throw new AppException($"Costs with id: {dbEntity.Id} not found.");

			newEntity.Id = dbEntity.Id;

			_context.Costs.Update(newEntity);

			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Costs entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var result = await _context.Costs
				.SingleOrDefaultAsync(cos => cos.Id == entity.Id);

			if (result == null) throw new AppException($"Costs with id: {entity.Id} not found.");

			_context.Costs.Remove(entity);

			await _context.SaveChangesAsync();
		}
	}
}
