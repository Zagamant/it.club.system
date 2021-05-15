using System.BLL.Helpers;
using System.BLL.Models.PaymentManagement;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.PaymentManagement
{
    public class PaymentService : Repository<int, Payment, PaymentModel, PaymentModel, PaymentModel>, IPaymentService
    {
        public PaymentService(DataContext context, IMapper mapper, ILogger<PaymentService> logger) : base(context, mapper, logger)
        {
            _table = _context.Payments;
        }

        public override async Task<PaymentModel> AddAsync(PaymentModel entity)
        {
            _logger.LogInformation($"{DateTime.Now}: Adding new payment");
            
            if (entity == null)
            {
                _logger.LogError($"{DateTime.Now}: payment model was null");
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id != 0 && _context.Payments.Any(ev => ev.Id == entity.Id))
            {
                _logger.LogError($"{DateTime.Now}: Payment already exist");
                throw new AppException("Payment already exist.");
            }

            var map = _mapper.Map<Payment>(entity);

            map.User = entity.UserId == 0 ? null : _context.Users.FirstOrDefault(user => user.Id == entity.UserId);
            map.Club = await _context.Clubs.SingleOrDefaultAsync(c => c.Id == map.ClubId);

            await _context.Payments
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public override async Task<PaymentModel> UpdateAsync(int id, PaymentModel updatedGroup)
        {
            if (_context.Events.SingleOrDefaultAsync(evnt => evnt.Id == id) == null)
            {
                throw new AppException("Not found");
            }

            updatedGroup.Id = id;

            var map = _mapper.Map<Event>(updatedGroup);

            map.Club = await _context.Clubs.SingleOrDefaultAsync(c => c.Id == map.ClubId);

            _context.Events
                .Update(map);

            await _context.SaveChangesAsync();

            return updatedGroup;
        }

        public async Task<PaymentModel> UpdatePaymentToUserAsync(int userId, DateTime month, decimal sum)
        {
            var userCheck = await _context.Users.FindAsync(userId);
            if (userCheck == null) throw new AppException("User not found");

            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.User == userCheck);

            switch ((Month) month.Month)
            {
                case Month.January:
                    payment.January = sum;
                    break;
                case Month.February:
                    payment.February = sum;
                    break;
                case Month.March:
                    payment.March = sum;
                    break;
                case Month.April:
                    payment.April = sum;
                    break;
                case Month.May:
                    payment.May = sum;
                    break;
                case Month.June:
                    payment.June = sum;
                    break;
                case Month.July:
                    payment.July = sum;
                    break;
                case Month.August:
                    payment.August = sum;
                    break;
                case Month.September:
                    payment.September = sum;
                    break;
                case Month.October:
                    payment.October = sum;
                    break;
                case Month.November:
                    payment.November = sum;
                    break;
                case Month.December:
                    payment.December = sum;
                    break;
                default:
                    throw new AppException("Month was provided in incorrect format");
            }

            return _mapper.Map<PaymentModel>(payment);
        }
    }
}