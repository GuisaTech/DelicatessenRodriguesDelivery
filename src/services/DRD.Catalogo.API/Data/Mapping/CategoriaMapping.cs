using DRD.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Catalogo.API.Data.Mapping
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            //builder.HasIndex(x=> x.Nome).IsUnique();

            builder.Property(x => x.ImagemUrl)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(x => x.Disponivel)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(x => x.Removido)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.HasMany(x => x.Produtos)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Categorias");
        }
    }
}
