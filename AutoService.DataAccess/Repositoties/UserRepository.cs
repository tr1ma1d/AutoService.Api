using AutoService.Core.Abstractions;
using AutoService.Core.Models;
using AutoService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.DataAccess.Repositoties
{
    public class UserRepository : IRepository<Users>
    {
        private readonly AutoServiceDataContext _context;
        public UserRepository(AutoServiceDataContext context)
        {
            _context = context; 
        }
        public async Task<Guid> Add(Users entity)
        {
            var userEntity = new UserEntity
            {
                Username = entity.Username,
                Password = entity.Password,
                Email = entity.Email,
            };
            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return userEntity.Id;
        }

        public async Task<Guid> Delete(Users entity)
        {
            await _context.users.Where(x => x.Username == entity.Username)
                                .ExecuteDeleteAsync();
            
            return entity.Id;

        }

        public async Task<List<Users>> GetAll()
        {
            var userEntity = await _context.users.AsNoTracking()
                .ToListAsync();

            var user = userEntity.Select(u => Users.Create(u.Id, u.Username, u.Password, u.Email).User).ToList();


            return user;
        }

        public async Task<Users> GetByName(string name)
        {
            var userEntity = await _context.users.Where(x => x.Username == name).FirstOrDefaultAsync();
            var user = Users.Create(userEntity.Id, userEntity.Username, userEntity.Password, userEntity.Email).User;
            return user;


        }

        public async Task<Guid> Update(Users entity)
        {
            var userEntity = await _context.users.Where(x => x.Username == entity.Username || x.Email == entity.Email).FirstOrDefaultAsync();

            // Если сущность не найдена, выбрасываем исключение или обрабатываем ошибку
            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {entity.Id} not found.");
            }

            // Обновляем свойства сущности
            userEntity.Username = entity.Username;
            userEntity.Password = entity.Password;
            userEntity.Email = entity.Email;

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return entity.Id;

        }
    }
}
