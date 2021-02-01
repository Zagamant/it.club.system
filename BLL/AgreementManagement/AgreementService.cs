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
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AgreementService(DataContext context, IMapper mapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<IEnumerable<AgreementModel>> GetAllAsync()
		{
			return await _context.Agreements
				.Select(agr =>  _mapper.Map<AgreementModel>(agr))
				.ToListAsync();
		}

		public async Task<AgreementModel> GetAsync(int agreementId)
		{
			var agreement = await _context.Agreements
				.FirstOrDefaultAsync(agr => agr.Id == agreementId);

			if (agreement == null) throw new AppException($"Agreement with id: {agreementId} not found.");

			var agrem = _mapper.Map<AgreementModel>(agreement);

			return agrem;
		}
		
		public async Task<AgreementModel> AddAsync(AgreementModel agreement)
		{
			var agreementOrig = _mapper.Map<Agreement>(agreement);
			await _context.Agreements.AddAsync(agreementOrig);
			await _context.SaveChangesAsync();

			return agreement;
		}
		
		public async Task<AgreementModel> UpdateAsync(int agreementId, AgreementModel agreementNew)
		{
			var newAgr = _mapper.Map<Agreement>(agreementNew);
			newAgr.Id = agreementId;

			_context.Agreements.Update(newAgr);
			await _context.SaveChangesAsync();

			return agreementNew;
		}

		public async Task DeleteAsync(int agreementId, bool isDelete = false)
		{
			var agreement = await _context.Agreements
				.FirstOrDefaultAsync(agr => agr.Id == agreementId);

			if (agreement == null) throw new AppException($"Agreement with id: {agreementId} not found.");

			_context.Agreements.Remove(agreement);
			await _context.SaveChangesAsync();
		}
	}
}