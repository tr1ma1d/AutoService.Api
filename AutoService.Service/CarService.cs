using AutoService.Core.Abstractions;
using AutoService.Core.Models;

namespace AutoService.Service
{
    public class CarService : IService<Cars>
    {
        private readonly IRepository<Cars> _service;
        public CarService(IRepository<Cars> service)
        {
            _service = service;
        }
        public async Task<Guid> Add(Cars entity)
        {
            return await _service.Add(entity);    
        }

        public async Task<Guid> Delete(Cars entity)
        {
            return await _service.Delete(entity);   
        }

        public async Task<List<Cars>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Cars> GetByName(string name)
        {
            return await _service.GetByName(name);
        }

        public Task<Guid> Update(Cars entity)
        {
            return _service.Update(entity); 
        }
    }
}
