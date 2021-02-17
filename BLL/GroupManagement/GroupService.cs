﻿using System.BLL.Helpers;
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


        public override async Task<GroupModel> UpdateAsync(int groupId, GroupModel newGroup)
        {
            if (!await _context.Groups
                .AnyAsync(gr => gr.Id == groupId))
                throw new AppException($"Group with id: {groupId} not exist in database");

            newGroup.Id = groupId;

            var realGroup = _mapper.Map<Group>(newGroup);
            realGroup.Room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == newGroup.RoomId);
            realGroup.Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == newGroup.CourseId);

            _context.Groups.Update(realGroup);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroupModel>(realGroup);
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