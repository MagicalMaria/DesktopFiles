// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScreenAPIProject.Models;

namespace ScreenAPIProject.Migrations
{
    [DbContext(typeof(ShowContext))]
    partial class ShowContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScreenAPIProject.Models.Show", b =>
                {
                    b.Property<string>("ScreenName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.HasKey("ScreenName");

                    b.ToTable("Shows");

                    b.HasData(
                        new
                        {
                            ScreenName = "Kite",
                            Capacity = 30
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
