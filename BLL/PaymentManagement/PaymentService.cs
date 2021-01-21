using System;
using System.BLL.Helpers;
using System.BLL.Models.PaymentManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.PaymentManagement
{
	public class PaymentService : IPaymentService
	{
		private readonly DataContext _context;

		public PaymentService(DataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<IEnumerable<Payment>> GetAllAsync()
		{
			var result = await _context.Payments
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<Payment> GetAsync(int id)
		{
			var result = await _context.Payments
				.AsNoTracking()
				.SingleOrDefaultAsync(ev => ev.Id == id);

			if (result == null) throw new AppException($"Event with id: {id} not found.");

			return result;
		}

		public async Task AddAsync(Payment entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (entity.Id != 0)
			{
				var result = await _context.Payments
					.AsNoTracking()
					.SingleOrDefaultAsync(ev => ev.Id == entity.Id);

				if (result != null) throw new AppException($"Event already exist.");
			}

			await _context.Payments
				.AddAsync(entity);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Payment dbEntity, Payment newEntity)
		{
			if (newEntity == null) throw new ArgumentNullException(nameof(newEntity));
			if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));


			var result = await _context.Payments
				.SingleOrDefaultAsync(ev => ev.Id == dbEntity.Id);

			if (result == null) throw new AppException($"Event not found.");

			newEntity.Id = dbEntity.Id;
			_context.Payments
				.Update(newEntity);

			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Payment entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var result = await _context.Payments
				.SingleOrDefaultAsync(cos => cos.Id == entity.Id);

			if (result == null) throw new AppException($"Costs with id: {entity.Id} not found.");

			_context.Payments.Remove(entity);

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