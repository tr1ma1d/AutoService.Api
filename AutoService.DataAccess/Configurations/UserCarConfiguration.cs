using AutoService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.DataAccess.Configurations
{
    public class UserCarConfiguration : IEntityTypeConfiguration<UserCarEntity>
    {
        public void Configure(EntityTypeBuilder<UserCarEntity> builder)
        {
            // Настройка составного первичного ключа
            builder.HasKey(uc => new { uc.UserId, uc.CarId });

            // Настройка внешнего ключа для связи с UserEntity
            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserCars)
                .HasForeignKey(uc => uc.UserId);

            // Настройка внешнего ключа для связи с CarEntity
            builder.HasOne(uc => uc.Car)
                .WithMany(c => c.UserCars)
                .HasForeignKey(uc => uc.CarId);

       
        }
    }
}
