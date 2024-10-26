using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.DataAccess.Entities
{
    public class UserCarEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        [Column("car_id")]
        public Guid CarId { get; set; }
        public CarEntity Car { get; set; }
    }
}
