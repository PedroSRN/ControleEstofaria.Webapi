using ControleEstofaria.Dominio.ModuloCliente;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.ModuloCliente
{
    public class MapeadorClienteOrm : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //nome da tabela
            builder.ToTable("TBCliente");

            //propriedades
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("varchar(15)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.CNPJ).HasColumnType("varchar(20)").IsRequired();
        }
    }
}
