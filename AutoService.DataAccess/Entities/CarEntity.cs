using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.DataAccess.Entities
{
    public class CarEntity
    {
        [Column("car_id")]
        public Guid Id { get; set; }
        [Column("car_name")]
        public string Name { get; set; }
        [Column("price")]
        public string Price { get; set; }
        [Column("available")]
        public bool isAvailable { get; set; }


        public ICollection<UserCarEntity> UserCars { get; set; }
    }
}
