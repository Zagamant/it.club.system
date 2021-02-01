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
			var agreementOrig = new Agreement
			{
				Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == agreement.CourseId)?? throw new ArgumentException("CourseId wasn't correct"),
				User = await _context.Users.SingleOrDefaultAsync(c => c.Id == agreement.UserId) ?? throw new ArgumentException("UserId wasn't correct"),
				Payment = agreement.Payment
			};
			
			await _context.Agreements.AddAsync(agreementOrig);
			await _context.SaveChangesAsync();

			return agreement;
		}
		
		public async Task<AgreementModel> UpdateAsync(int agreementId, AgreementModel agreementNew)
		{
			var newAgr = await _context.Agreements.SingleOrDefaultAsync(a => a.Id == agreementId);

			newAgr.Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == agreementNew.CourseId) ??
			                throw new ArgumentException("CourseId wasn't correct");
			newAgr.User = await _context.Users.SingleOrDefaultAsync(c => c.Id == agreementNew.UserId) ??
			             throw new ArgumentException("UserId wasn't correct");

			newAgr.Payment = agreementNew.Payment;
			
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