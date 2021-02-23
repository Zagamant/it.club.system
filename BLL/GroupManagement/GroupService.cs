using System.BLL.Helpers;
using System.BLL.Models.GroupManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.GroupManagement
{
    public class GroupService : Repository<int, Group, GroupModel, GroupModel, GroupModel>, IGroupService
    {
        public GroupService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _table = _context.Groups;
        }


        public override async Task<GroupModel> AddAsync(GroupModel group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            var realGroup = _mapper.Map<Group>(group);

            realGroup.Room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == group.RoomId);
            realGroup.Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == group.CourseId);

            await _context.Groups.AddAsync(realGroup);
            await _context.SaveChangesAsync();

            return _mapper.Map<GroupModel>(realGroup);
        }


        public override async Task<GroupModel> UpdateAsync(int groupId, GroupModel updatedGroup)
        {
            var oldGroup = await _context.Groups
                .FirstOrDefaultAsync(gr => gr.Id == groupId);

            if (oldGroup == null)
                throw new AppException($"Group with id: {groupId} not exist in database");

            if (oldGroup.RoomId != updatedGroup.RoomId)
                oldGroup.Room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == updatedGroup.RoomId);
            
            if (oldGroup.CourseId != updatedGroup.CourseId)
                oldGroup.Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == updatedGroup.CourseId);
            
            var idsToAdd = updatedGroup.UsersIds.Except(oldGroup.Users.Select(u => u.Id));
            
            var IdsToRemove = oldGroup.Users.Select(u => u.Id).Except(updatedGroup.UsersIds);
            
            foreach (var oldGroupUser in oldGroup.Users.Where(user => IdsToRemove.Contains(user.Id)))
            {
                oldGroup.Users.Remove(oldGroupUser);
            }

            foreach (var oldGroupUser in _context.Users.Where(user => idsToAdd.Contains(user.Id)))
            {
                oldGroup.Users.Add(oldGroupUser);
            }

            _context.Groups.Update(oldGroup);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroupModel>(oldGroup);
        }

        public async Task<GroupModel> AddStudentAsync(int groupId, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == userId);
            if (user == null)
                new AppException($"User with id: {userId} not exist");

            var group = await _context.Groups.FirstOrDefaultAsync(gr => gr.Id == groupId);
            if (group == null)
                throw new AppException($"Group with id: {groupId} not exist");

            if (group.Users.Contains(user))
                throw new AppException("User already contained in Group");

            group.Users.Add(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroupModel>(group);
        }

        public async Task<GroupModel> RemoveStudentAsync(int groupId, int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(usr => usr.Id == userId);

            if (user == null) throw new AppException($"User with id: {userId} not exist");

            var group = await _context.Groups
                .FirstOrDefaultAsync(gr => gr.Id == groupId);

            if (group == null) throw new AppException($"Group with id: {groupId} not exist");

            group.Users.Remove(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroupModel>(group);
        }

        public override async Task DeleteAsync(int groupId, bool isDeleted = false)
        {
            var group = await _context.Groups
                .FirstOrDefaultAsync(gr => gr.Id == groupId);

            if (group == null) throw new AppException($"Group with id: {groupId} not exist in database");
            if (isDeleted)
            {
                _context.Groups.Remove(group);
            }
            else
            {
                group.Status = GroupStatus.Canceled;
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}