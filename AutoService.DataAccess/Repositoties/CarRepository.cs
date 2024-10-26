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
    public class CarRepository : IRepository<Cars>
    {
        AutoServiceDataContext _context;
        public CarRepository(AutoServiceDataContext context)
        {
            _context = context;
        }
        public async Task<Guid> Add(Cars entity)
        {
            var userEntity = new CarEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                isAvailable = entity.IsAvailable
            };
            await _context.cars.AddAsync(userEntity);    
            await _context.SaveChangesAsync();
            return entity.Id;
           
        }

        public async Task<Guid> Delete(Cars entity)
        {
            await _context.cars.Where(x => x.Name == entity.Name || x.Id == entity.Id)
                .ExecuteDeleteAsync();

            return entity.Id;
        }

        public async Task<List<Cars>> GetAll()
        {
            var carEntity = await _context.cars.AsNoTracking()
                .ToListAsync();

            var cars = carEntity.Select(x => Cars.Create(x.Id, x.Name, x.Price, x.isAvailable).Car).ToList();

            return cars;
        }

        public async Task<Cars> GetByName(string name)
        {
            var carEntity = await _context.cars.Where(x => x.Name == name).FirstOrDefaultAsync();
            var car = Cars.Create(carEntity.Id, carEntity.Name, carEntity.Price, carEntity.isAvailable).Car;

            return car;
        }

        public async Task<Guid> Update(Cars entity)
        {
            var carEntity = await _context.cars.Where(x => x.Name == entity.Name).FirstOrDefaultAsync();

            // Если сущность не найдена, выбрасываем исключение или обрабатываем ошибку
            if (carEntity == null)
            {
                throw new KeyNotFoundException($"Car with ID {entity} not found.");
            }

            // Обновляем свойства сущности
            carEntity.Name = entity.Name;
            carEntity.Price = entity.Price;
            carEntity.isAvailable = entity.IsAvailable;

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return carEntity.Id;
        }
    }
}
