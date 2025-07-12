using LogisticsSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace LogisticsSolution.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<MoveRequest> MoveRequests  { get; set; }
        public DbSet<MoveItem> MoveItems { get; set; }
        public DbSet<MovingAgent> MovingAgents { get; set; }
        public DbSet<AgentProvince> AgentProvinces { get; set; }
        public DbSet<MoveHistory> MoveHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // db table definition

            // province table design...
            var provinceEntity = modelBuilder.Entity<Province>();

            provinceEntity.ToTable("ZN_Province");
            provinceEntity.HasKey(x => x.Id);
            provinceEntity.Property(x => x.Name).HasMaxLength(30);
            provinceEntity.HasData(InsertedData.Provinces);

            //Move request table design 

            var moveRequestEntity = modelBuilder.Entity<MoveRequest>();

            moveRequestEntity.ToTable("ZN_Move_Requests");
            moveRequestEntity.HasKey(x => x.Id);
            moveRequestEntity.Property(x => x.FullName).HasMaxLength(100);
            moveRequestEntity.Property(x => x.PhoneNumber).HasMaxLength(15);
            moveRequestEntity.Property(x => x.MoveCode).HasMaxLength(7);
            moveRequestEntity.Property(x => x.PickUpAddressNumber).HasMaxLength(7);
            moveRequestEntity.Property(x => x.DropOffAddressNumber).HasMaxLength(7);
            moveRequestEntity.HasIndex(x => x.MoveCode).IsUnique();
            moveRequestEntity.Property(x => x.Email).HasMaxLength(150);
            moveRequestEntity.Property(x => x.ToRemark).HasMaxLength(300);
            moveRequestEntity.Property(x => x.FromRemark).HasMaxLength(300);
            moveRequestEntity.Property(x => x.PickUpAddress).HasMaxLength(250);
            moveRequestEntity.Property(x => x.DropOffAddress).HasMaxLength(250);
            moveRequestEntity.Property(x => x.FromLongCarry).HasMaxLength(50);
            moveRequestEntity.Property(x => x.ToLongCarry).HasMaxLength(50);
            moveRequestEntity.Property(x => x.PickUpLatitude).HasMaxLength(50);
            moveRequestEntity.Property(x => x.PickUpLongitude).HasMaxLength(50);
            moveRequestEntity.Property(x => x.DropOffLatitude).HasMaxLength(50);
            moveRequestEntity.Property(x => x.DropOffLongitude).HasMaxLength(50);
            moveRequestEntity.HasOne(x => x.Province).WithMany(x => x.MoveRequests).HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.Restrict);

            //Move Items table design..

            var moveItemEntity = modelBuilder.Entity<MoveItem>();

            moveItemEntity.ToTable("ZN_Move_Items");
            moveItemEntity.Property(x => x.ItemName).HasMaxLength(30);
            moveItemEntity.Property(x => x.RoomName).HasMaxLength(30);
            moveItemEntity.HasOne(x => x.MoveRequest).WithMany(x => x.MoveItems).HasForeignKey(x => x.MoveRequestId).OnDelete(DeleteBehavior.Restrict);

            //MoveAgents table design ...

            var movingAgentsEntity = modelBuilder.Entity<MovingAgent>();

            movingAgentsEntity.ToTable("ZN_Moving_Agents");
            movingAgentsEntity.Property(x => x.CompanyName).HasMaxLength(50);
            movingAgentsEntity.Property(x => x.Email).HasMaxLength(100);
            movingAgentsEntity.Property(x => x.KvkNumber).HasMaxLength(20);
            movingAgentsEntity.HasIndex(x => x.KvkNumber).IsUnique();
            movingAgentsEntity.Property(x => x.CompanyOverView).HasMaxLength(500);
            movingAgentsEntity.HasIndex(x => x.Email).IsUnique();

            //agent province table design 
            var agentProvinceEnity = modelBuilder.Entity<AgentProvince>();

            agentProvinceEnity.ToTable("ZN_AgentsProvince");
            agentProvinceEnity.HasOne(x => x.Agent).WithMany(x => x.ProvincesCovered).HasForeignKey(x => x.AgentId).OnDelete(DeleteBehavior.Restrict);
            agentProvinceEnity.HasOne(x => x.Province).WithMany(x => x.MovingAgents).HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.Restrict);

            //Move History table design

            var moveHistoryEntity = modelBuilder.Entity<MoveHistory>();

            moveHistoryEntity.ToTable("ZN_MoveHostories");
            moveHistoryEntity.HasOne(x => x.MoveRequest).WithMany(x => x.MoveHistories).HasForeignKey(x => x.MoveRequestId).OnDelete(DeleteBehavior.Restrict);
            moveHistoryEntity.HasOne(x => x.MovingAgent).WithMany(x => x.MoveHistories).HasForeignKey(x => x.MoveAgentId).OnDelete(DeleteBehavior.Restrict);

            //Move History table design

            var quotesEntity = modelBuilder.Entity<Quote>();

            quotesEntity.ToTable("ZN_Quotes");
            quotesEntity.Property(x => x.AdditonalInformation).HasMaxLength(250);
            quotesEntity.HasOne(x => x.MoveRequest).WithMany(x => x.Quotes).HasForeignKey(x => x.MoveRequestId).OnDelete(DeleteBehavior.Restrict);
            quotesEntity.HasOne(x => x.MovingAgent).WithMany(x => x.Quotes).HasForeignKey(x => x.MovingAgentId).OnDelete(DeleteBehavior.Restrict);

            //mailing List table design

            var mailingListEntity = modelBuilder.Entity<Mailing>();

            mailingListEntity.ToTable("ZN_MailingList");
            mailingListEntity.Property(x => x.Email).HasMaxLength(150);
        }
    }
}
