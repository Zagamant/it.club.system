using System.BLL.Helpers;
using System.BLL.Models.AgreementManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.AgreementManagement
{
	public class AgreementService : IAgreementService
	{
		private DataContext _context;
		private IMapper _mapper;

		public AgreementService(DataContext context, IMapper mapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task CreateAsync(AgreementModel agreement)
		{
			var agreementOrig = _mapper.Map<Agreement>(agreement);
			await _context.Agreements.AddAsync(agreementOrig);
			await _context.SaveChangesAsync();
		}

		public async Task<AgreementModel> GetById(int agreementId)
		{
			var agreement = await _context.Agreements.FirstOrDefaultAsync(agr => agr.Id == agreementId);

			if (agreement == null) throw new AppException($"Agreement with id: {agreementId} not found.");

			var agrem = _mapper.Map<AgreementModel>(agreement);

			return agrem;
		}

		public async Task<IEnumerable<AgreementModel>> GetByUser(User user)
		{
			var models = await _context.Agreements
				.AsNoTracking()
				.Where(agr => agr.User == user)
				.Select(agr => new AgreementModel
			{
				User = agr.User,
				Payment = agr.Payment
			}).ToListAsync();
			return models;
		}

		public async Task Update(int agreementId, AgreementModel agreementNew)
		{
			var newAgr = _mapper.Map<Agreement>(agreementNew);
			newAgr.Id = agreementId;

			_context.Agreements.Update(newAgr);
			await _context.SaveChangesAsync();
		}

		public async Task Update(AgreementModel agreement, AgreementModel agreementNew)
		{
			var oldAgr = await _context.Agreements
				.SingleOrDefaultAsync(agr => agr.User == agreement.User && agr.Course == agreement.Course);

			if (agreement == null) throw new AppException("Agreement not found.");

			await Update(oldAgr.Id, agreementNew);
		}

		public async Task Delete(int agreementId)
		{
			var agreement = await _context.Agreements.FirstOrDefaultAsync(agr => agr.Id == agreementId);

			if (agreement == null) throw new AppException($"Agreement with id: {agreementId} not found.");

			_context.Agreements.Remove(agreement);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(AgreementModel agreement)
		{
			var oldAgr = await _context.Agreements
				.SingleOrDefaultAsync(agr => agr.User == agreement.User && agr.Course == agreement.Course);

			if (agreement == null) throw new AppException("Agreement not found.");

			await Delete(oldAgr.Id);
		}
	}
}
