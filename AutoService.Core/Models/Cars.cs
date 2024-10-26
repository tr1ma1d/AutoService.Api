using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Core.Models
{
    public class Cars
    {
        public Guid Id { get; } 
        public string Name { get; } = string.Empty;
        public string Price { get; } = string.Empty;
        public bool IsAvailable { get; }


        private Cars(Guid id, string name, string price, bool isAvailable)
        {
            Id = id; 
            Name = name; 
            Price = price;
            IsAvailable = isAvailable;
        }
        private Cars(string name, string price, bool isAvailable)
        {
            Name = name;
            Price = price;
            IsAvailable = isAvailable;
        }
        public static (Cars Car, string error) Create(Guid id, string name, string price, bool isAvailable)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(name)) {
                error = "Name of car is null | is empty";
            }
            var car = new Cars(id, name, price, isAvailable);
            return(car,  error);
        }
        public static (Cars Car, string error) Create(string name, string price, bool isAvailable)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                error = "Name of car is null | is empty";
            }
            var car = new Cars(name, price, isAvailable);
            return (car, error);
        }
    }
}
