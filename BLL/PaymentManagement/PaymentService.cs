using System.BLL.Helpers;
using System.BLL.Models.PaymentManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.PaymentManagement
{
    public class PaymentService : IPaymentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentService(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PaymentModel>> GetAllAsync()
        {
            var result = await _context.Payments
                .ToListAsync();

            return result.Select(item => _mapper.Map<PaymentModel>(item)).ToList();
        }

        public async Task<PaymentModel> GetAsync(int id)
        {
            var result = await _context.Payments
                .AsNoTracking()
                .SingleOrDefaultAsync(ev => ev.Id == id);

            if (result == null) throw new AppException($"Event with id: {id} not found.");

            var map = _mapper.Map<PaymentModel>(result);
            return map;
        }

        public async Task<PaymentModel> AddAsync(PaymentModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id != 0)
            {
                var result = await _context.Payments
                    .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

                if (result != null)
                    throw new AppException("Payment" +
                                           " already exist.");
            }

            var map = _mapper.Map<Payment>(entity);

            map.User = entity.UserId == 0 ? null : _context.Users.FirstOrDefault(user => user.Id == entity.UserId);

            await _context.Payments
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PaymentModel> UpdateAsync(PaymentModel dbEntity, PaymentModel newEntity)
        {
            if (newEntity == null) throw new ArgumentNullException(nameof(newEntity));
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            if (await _context.Payments
                .SingleOrDefaultAsync(ev => ev.Id == dbEntity.Id) == null) throw new AppException($"Event not found.");

            newEntity.Id = dbEntity.Id;

            var result = _mapper.Map<Event>(newEntity);
            _context.Events
                .Update(result);

            await _context.SaveChangesAsync();

            return newEntity;
        }

        public async Task<PaymentModel> UpdateAsync(int id, PaymentModel newEntity)
        {
            if (_context.Events.SingleOrDefaultAsync(evnt => evnt.Id == id) == null)
            {
                throw new AppException("Not found");
            }

            newEntity.Id = id;

            var map = _mapper.Map<Event>(newEntity);

            _context.Events
                .Update(map);

            await _context.SaveChangesAsync();

            return newEntity;
        }

        public async Task DeleteAsync(PaymentModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Payments
                .SingleOrDefaultAsync(cos => cos.Id == entity.Id);

            if (result == null) throw new AppException($"Costs with id: {entity.Id} not found.");

            _context.Payments.Remove(result);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Payments
                .SingleOrDefaultAsync(Event => Event.Id == id);

            if (result == null) throw new AppException($"Costs with id: {id} not found.");

            _context.Payments.Remove(result);

            await _context.SaveChangesAsync();
        }

        public async Task AddPaymentToUserAsync(User user, DateTime month, decimal sum)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userCheck = await _context.Users.FindAsync(user);
            if (userCheck == null) throw new AppException("User not found");

            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.User == user);

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
        }
    }
}