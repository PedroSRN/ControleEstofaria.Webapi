using ControleEstofaria.Dominio.ModuloFinanca;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.ModuloFinanca
{
    public class MapeadorFinancaOrm : IEntityTypeConfiguration<Financa>
    {
        public void Configure(EntityTypeBuilder<Financa> builder)
        {
            builder.ToTable("TBFinanca");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Saldo).HasColumnType("decimal(10, 2)").IsRequired();


            builder.HasOne(x => x.Servico)
                .WithMany(x => x.Financas)
                .IsRequired()
                .HasForeignKey(x => x.ServicoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
