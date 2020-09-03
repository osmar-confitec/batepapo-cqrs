using BatePapo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.Data.Mappings
{
   public class AdressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.Ignore(x => x.NotificacoesErros);



            builder.ToTable("Address");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id");


            builder.Property(c => c.Neighborhood)
            .HasColumnType("varchar(150)")
            .HasMaxLength(150)
            .IsRequired();


            builder.Property(c => c.PublicPlace)
            .HasColumnType("varchar(150)")
            .HasMaxLength(150)
            .IsRequired();


            builder.Property(c => c.Number)
               .HasColumnType("varchar(30)")
               .HasMaxLength(30)
               .IsRequired();

            // 1 : N => Pedido : Pagamento
            builder.HasOne(c => c.Customer)
                .WithMany(c => c.Addresses );

            builder.ToTable("Address");
        }
    }
}
