using BatePapo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.Data.Mappings
{

    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.Ignore(x => x.NotificacoesErros);

            builder.ToTable("Customer");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.NickName)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.SurName)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            builder.OwnsOne(x=>x.CPF)
                    .Property(c => c.Doc)
                   .HasColumnType("varchar(20)")
                   .HasMaxLength(20)
                   .IsRequired();

            

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.Addresses)
                    .WithOne(c => c.Customer)
                    .HasForeignKey(c => c.CustomerId);

        }


    }
}
