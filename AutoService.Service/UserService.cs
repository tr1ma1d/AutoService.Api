using AutoService.Core.Abstractions;
using AutoService.Core.Models;

namespace AutoService.Service
{
    public class UserService : IService<Users>, IUserService
    {
        private readonly IRepository<Users> _repository;
        public UserService(IRepository<Users> repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Add(Users entity)
        {
            return await _repository.Add(entity);
        }

        public Task<Guid> Delete(Users entity)
        {
            return _repository.Delete(entity);
        }

        public Task<List<Users>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Users> GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public Task<Guid> Update(Users entity)
        {
            return _repository.Update(entity);
        }

        public async Task<Users> ValidateUser(Users data)
        {
            var user = await _repository.GetByName(data.Username);
            if(user == null)
            {
                throw new Exception("Error validate");
            }
            if(data.Password == user.Password)
            {
                return user;
            }
            return null;
        }
    }
}
