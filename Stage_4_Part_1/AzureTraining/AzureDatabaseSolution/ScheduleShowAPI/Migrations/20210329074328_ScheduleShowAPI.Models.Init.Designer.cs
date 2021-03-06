// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleShowAPI.Models;

namespace ScheduleShowAPI.Migrations
{
    [DbContext(typeof(ShowContext))]
    [Migration("20210329074328_ScheduleShowAPI.Models.Init")]
    partial class ScheduleShowAPIModelsInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScheduleShowAPI.Models.ScheduleShow", b =>
                {
                    b.Property<int>("ShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<string>("ScreenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShowId");

                    b.ToTable("Schedules");

                    b.HasData(
                        new
                        {
                            ShowId = 1,
                            MovieID = 101,
                            ScreenName = "Kite",
                            Time = "15:00"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
