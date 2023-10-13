using DataLayer.Entities;
using DataLayer.Interfaces;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UsersService
    {
        private readonly IGenericRepository<AspNetUser> _usersRepository;
        public UsersService(IUnitOfWorkRepositories unitofworkRepository)
        {
            _usersRepository = unitofworkRepository.UsersRepository;
        }

        public async Task<(IEnumerable<UserDTO> EntityData, int Count)> ListWithPaging(
string orderBy, int? page, int? pageSize, bool isDescending)
        {
            Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>> orderByExpression;

            switch (orderBy)
            {
                case "Name":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.FirstName) : query.OrderBy(entity => entity.FirstName);
                    break;
                case "Email":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.Email) : query.OrderBy(entity => entity.Email);
                    break;
                case "PhoneNumber":
                    orderByExpression = query => isDescending ? query.OrderByDescending(entity => entity.PhoneNumber) : query.OrderBy(entity => entity.PhoneNumber);
                    break;
                default:
                    orderByExpression = query => query.OrderBy(entity => entity.FirstName);
                    break;
            }

            (IList<AspNetUser> EntityData, int Count) = await _usersRepository.ListWithPaging(
                orderBy: orderByExpression,
                page: page,
                pageSize: pageSize,
                filter: null
            );

            IEnumerable<UserDTO> results = EntityData.Select(x => new UserDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Role = x.AspNetRole.Where(id => id.Id == x.Id).Select(x => x.Name).FirstOrDefault()
            }).ToList();

            return (results, Count);
        }
        public async Task<UserDTO> GetById(int Id)
        {
            var entity = await _usersRepository.GetById(Id);

            var data = new UserDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                NormalizedEmail = entity.NormalizedEmail,
                PhoneNumber = entity.PhoneNumber,
                UserName = entity.UserName,
                NormalizedUserName = entity.NormalizedUserName,
                PasswordHash = entity.PasswordHash,
                SecurityStamp = entity.SecurityStamp,
                ConcurrencyStamp = entity.ConcurrencyStamp,
                LockoutEnd = entity.LockoutEnd,
                LockoutEnabled = entity.LockoutEnabled,
                AccessFailedCount = entity.AccessFailedCount,
                TwoFactorEnabled = entity.TwoFactorEnabled,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                EmailConfirmed = entity.EmailConfirmed,
                Role = entity.AspNetRole.Where(id => id.Id == entity.Id).Select(x => x.Name).FirstOrDefault()
            };
            return data;
        }
    }
}
