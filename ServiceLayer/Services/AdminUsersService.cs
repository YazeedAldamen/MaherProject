using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public class AdminUsersService
    {
        private readonly IGenericRepository<AspNetUser> _usersRepository;
        private readonly IGenericRepository<AspNetUserRole> _userRolesRepository;
        private readonly IGenericRepository<AspNetRole> _roleRepository;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;

        public AdminUsersService(IUnitOfWorkRepositories unitOfWorkRepositories, UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager)
        {
            _usersRepository = unitOfWorkRepositories.UsersRepository;
            _userRolesRepository = unitOfWorkRepositories.UserRoleRepository;
            _roleRepository = unitOfWorkRepositories.RoleRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #region ListWithPaging
        public async Task<(IEnumerable<AdminUsersDTO> EntityData, int Count, IList<AspNetRole> roles)> ListWithPaging(
        string orderBy, int? page, int? pageSize, bool isDescending)
        {
            Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>> orderByExpression;

            switch (orderBy)
            {
                case "Id":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Id) : query.OrderBy(entity => entity.Id);
                    break;
                case "FirstName":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.FirstName) : query.OrderBy(entity => entity.FirstName);
                    break;
                case "Email":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Email) : query.OrderBy(entity => entity.Email);
                    break;
                case "PhoneNumber":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.PhoneNumber) : query.OrderBy(entity => entity.PhoneNumber);
                    break;
                default:
                    orderByExpression = query => query.OrderBy(entity => entity.Id);
                    break;
            }

            (IList<AspNetUser> EntityData, int Count) = await _usersRepository.ListWithPaging(
                orderBy: orderByExpression,
                page: page,
                pageSize: pageSize,
                filter: null
            );

            IEnumerable<AdminUsersDTO> results = EntityData.Select((x) => new AdminUsersDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            }).ToList();

            // write me a method that loops through the results and gets the role for each user
            int count = 0;
            foreach (var user in results)
            {
                var roleName = await _userManager.GetRolesAsync(EntityData[count]);
                user.UserType = roleName.FirstOrDefault();
                count++;
            }
            var allRoles =  _roleRepository.GetAll();

            return (results, Count, allRoles);
        }
        #endregion

        // write me a method that updates the user's role
        public async Task UpdateUserRole(string userId, string role)
        {
            var newRole = await _roleManager.FindByNameAsync(role);
            var user = await _userManager.FindByIdAsync(userId);
            var oldRole = await _userManager.GetRolesAsync(user);
            if (oldRole.Count > 0)
            {
                await _userManager.RemoveFromRoleAsync(user, oldRole.FirstOrDefault());
            }
            try
            {
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }
            catch (Exception e)
            {
            }
        }

        public async Task<bool> CheckUserRoleByEmail(string Email)
        {
            var user=await _usersRepository.GetBy(x=>x.Email==Email);
            var userRoles=await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles) 
            {
                if (role == "Admin" || role == "Service Provider")
                {
                    return true;
                }
            }
            return false;
        }

    }


}
