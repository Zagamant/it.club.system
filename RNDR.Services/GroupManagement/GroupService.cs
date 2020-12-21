using System.BLL.Helpers;
using System.BLL.Models.GroupManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.GroupManagement
{
	public class GroupService : IGroupService
	{
		private IMapper _mapper;
		private DataContext _context;

		public GroupService(DataContext context, IMapper mapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task CreateAsync(GroupRegisterModel group)
		{
			if (group == null) throw new ArgumentNullException(nameof(group));

			var result = _mapper.Map<Group>(group);

			await _context.Groups.AddAsync(result);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Group>> GetAllGroupAsync()
		{
			var result = await _context.Groups
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<Group> GetCourseByIdAsync(int groupId)
		{
			var result = await _context.Groups
				.AsNoTracking()
				.FirstOrDefaultAsync(gr => gr.Id == groupId);

			if (result == null) throw new ArgumentNullException(nameof(result));

			return result;
		}

		public async Task UpdateAsync(int groupId, Group newGroup)
		{
			var result = await _context.Groups.FirstOrDefaultAsync(gr => gr.Id == groupId);

			if (result == null) throw new AppException($"Group with id: {groupId} not exist in database");

			newGroup.Id = groupId;

			_context.Groups.Update(newGroup);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Group group, Group newGroup)
		{
			await UpdateAsync(group.Id, newGroup);
		}

		public async Task AddStudentAsync(int groupId, User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			var result = await _context.Groups.FirstOrDefaultAsync(gr => gr.Id == groupId);

			if (result == null) throw new AppException($"Group with id: {groupId} not exist in database");

			result.Users.Add(user);

			await _context.SaveChangesAsync();
		}

		public async Task AddStudentAsync(Group group, User user)
		{
			await AddStudentAsync(group.Id, user);
		}

		public async Task RemoveStudentAsync(int groupId, User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			var result = await _context.Groups.FirstOrDefaultAsync(gr => gr.Id == groupId);

			if (result == null) throw new AppException($"Group with id: {groupId} not exist in database");
			
			result.Users.Remove(user);

			await _context.SaveChangesAsync();
		}

		public async Task RemoveStudentAsync(Group group, User user)
		{
			await RemoveStudentAsync(group.Id, user);
		}

		public async Task RemoveAsync(int groupId)
		{
			var result = await _context.Groups.FirstOrDefaultAsync(gr => gr.Id == groupId);

			if (result == null) throw new AppException($"Group with id: {groupId} not exist in database");

			_context.Groups.Remove(result);
		}

		public async Task RemoveAsync(Group group)
		{
			await RemoveAsync(group.Id);
		}
	}
}