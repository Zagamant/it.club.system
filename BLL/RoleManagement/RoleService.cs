using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.RoleManagement
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper,
            DataContext context)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync(string page = "",
            string pageSize = "", string sort = "", string filter = "")
        {
            if (string.IsNullOrEmpty(page) || string.IsNullOrEmpty(pageSize))
            {
                return await _context.Roles.Select(ent => _mapper.Map<RoleModel>(ent))
                    .ToListAsync();
            }
            
            var entityQuery = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(sort))
            {
                var sortVal = sort.Split('+');
                var condition = sortVal.First();
                var order = sortVal.Last() == "ASC" ? "" : "descending";
                entityQuery = entityQuery.OrderBy($"{condition} {order}");
            }

            if (!string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(pageSize))
            {
                var pageNumber = int.Parse(page);
                var pageSizeNumber = int.Parse(pageSize);

                var from = (pageNumber - 1) * pageSizeNumber;

                entityQuery = entityQuery.Skip(from).Take(pageSizeNumber);
            }

            return await entityQuery
                .Select(user => _mapper.Map<RoleModel>(user))
                .ToListAsync();
            
        }

        public async Task<RoleModel> GetAsync(int id)
        {
            var realRole = await _context.Roles.FirstOrDefaultAsync(role => role.Id == id);

            if (realRole == null) throw new ArgumentNullException($"Role with roleId: {id} not found");

            return _mapper.Map<RoleModel>(realRole);
        }

        public async Task<RoleModel> AddAsync(RoleModel role)
        {
            var realRole = await _roleManager.FindByNameAsync(role.Name);

            if (realRole != null) throw new ArgumentNullException($"Role with name {role.Name} already exist");

            return _mapper.Map<RoleModel>(await _roleManager.CreateAsync(new Role(role.Name)));
        }


        public async Task<RoleModel> UpdateAsync(int roleId, RoleModel role)
        {
            var realRole = await _roleManager.FindByIdAsync(roleId.ToString());

            if (realRole == null) throw new ArgumentNullException($"Role with name {role.Name} not found");

            realRole.Name = role.Name;
            return _mapper.Map<RoleModel>(await _roleManager.UpdateAsync(realRole));
        }

        public async Task RemoveAsync(int roleId)
        {
            var realRole = await _roleManager.FindByIdAsync(roleId.ToString());

            if (realRole == null) throw new ArgumentNullException($"Role with Id: {roleId} not found");

            await _roleManager.DeleteAsync(realRole);
        }

        public async Task<int> Count()
        {
            return await _context.Roles.CountAsync();
        }
    }
}