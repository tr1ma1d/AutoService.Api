using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.DataAccess.Entities
{
    public class UserEntity
    {
        [Column("user_id")]
        public Guid Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password_hash")]
        public string Password { get; set; }
        [Column("email")]
        public string Email { get; set; }   

        public ICollection<UserCarEntity> UserCars { get; set; }    

        
    }
}
