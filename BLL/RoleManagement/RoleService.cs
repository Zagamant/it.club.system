using System.BLL.Helpers;
using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.RoleManagement
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper, DataContext context)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            return await _context.Roles.Select(role => _mapper.Map<RoleModel>(role)).ToListAsync();
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
        
    }
}