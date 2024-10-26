using AutoService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Core.Abstractions
{
    public interface IUserService
    {
        Task<Users> ValidateUser(Users data);
    }
}
