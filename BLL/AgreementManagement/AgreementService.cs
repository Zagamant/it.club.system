﻿using System.BLL.Helpers;
using System.BLL.Models.AgreementManagement;
using System.DAL;
using System.DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.AgreementManagement
{
    public class AgreementService : BaseService<int, Agreement, AgreementModel, AgreementModel, AgreementModel>,
        IAgreementService
    {
        public AgreementService(DataContext context, IMapper mapper, ILogger<AgreementService> logger) : base(context, mapper, logger)
        {
            _table = _context.Agreements;
        }
        
       public override async Task<AgreementModel> AddAsync(AgreementModel agreement)
        {
            var agreementOrig = new Agreement
            {
                Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == agreement.CourseId) ??
                         throw new ArgumentException("CourseId wasn't correct"),
                User = await _context.Users.SingleOrDefaultAsync(c => c.Id == agreement.UserId) ??
                       throw new ArgumentException("UserId wasn't correct"),
                Payment = agreement.Payment
            };

            await _context.Agreements.AddAsync(agreementOrig);
            await _context.SaveChangesAsync();

            return agreement;
        }

        public override async Task<AgreementModel> UpdateAsync(int agreementId, AgreementModel updated)
        {
            var newAgr = await _context.Agreements.SingleOrDefaultAsync(a => a.Id == agreementId);

            newAgr.Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == updated.CourseId) ??
                            throw new ArgumentException("CourseId wasn't correct");
            newAgr.User = await _context.Users.SingleOrDefaultAsync(c => c.Id == updated.UserId) ??
                          throw new ArgumentException("UserId wasn't correct");

            newAgr.Payment = updated.Payment;

            _context.Agreements.Update(newAgr);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}