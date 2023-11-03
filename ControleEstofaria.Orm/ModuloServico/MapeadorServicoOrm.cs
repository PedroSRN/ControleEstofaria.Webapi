using ControleEstofaria.Dominio.ModuloServico;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstofaria.Orm.ModuloServico
{
    public class MapeadorServico : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.ToTable("TBServico");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.NomeServico).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("varchar(2000)").IsRequired();
            builder.Property(x => x.DataEntradaServico).IsRequired();
            builder.Property(x => x.DataSaidaServico);
            builder.Property(x => x.ValorServico).IsRequired();
            builder.Property(x => x.FormaPagamento).HasConversion<int>().IsRequired();
            builder.Property(x => x.StatusServico).HasConversion<int>().IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Servicos)
                .IsRequired()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
