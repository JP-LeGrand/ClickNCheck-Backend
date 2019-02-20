using ClickNCheck.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNCheck.Data
{
    public class ClickNCheckContext : DbContext
    {
        public ClickNCheckContext(DbContextOptions<ClickNCheckContext> options) : base(options)
        {
        }

        public ClickNCheckContext()
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<Recruiter_JobProfile> Recruiter_JobProfile { get; set; }
        public DbSet<JobProfile> JobProfile { get; set; }
        public DbSet<Vendor> Checks { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Candidate_JobProfile> Candidate_JobProfile { get; set; }
        public DbSet<JobProfile_Vendor> JobProfile_Checks { get; set; }
        public DbSet<AccountsPerson> AccountsPerson { get; set; }
        public DbSet<AddressType> AddressType { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CheckCategory> CheckCategory { get; set; }
        public DbSet<Recruiter_Candidate> Recruiter_Candidate { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Models.Services> Services { get; set; }
        public DbSet<LinkCode> LinkCodes { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Vendor_Category> Vendor_Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recruiter_JobProfile>()
                .HasKey(t => new { t.RecruiterId, t.JobProfileId });

            modelBuilder.Entity<Recruiter_JobProfile>()
            .HasOne(pt => pt.Recruiter)
            .WithMany(p => p.Recruiter_JobProfile)
            .HasForeignKey(pt => pt.RecruiterId);

            modelBuilder.Entity<Recruiter_JobProfile>()
            .HasOne(pt => pt.JobProfile)
            .WithMany(p => p.Recruiter_JobProfile)
            .HasForeignKey(pt => pt.JobProfileId);


            modelBuilder.Entity<Recruiter_Candidate>()
                .HasKey(t => new { t.RecruiterId, t.CandidateId });

            modelBuilder.Entity<Recruiter_Candidate>()
            .HasOne(pt => pt.Recruiter)
            .WithMany(p => p.Recruiter_Candidate)
            .HasForeignKey(pt => pt.RecruiterId);

            modelBuilder.Entity<Recruiter_Candidate>()
            .HasOne(pt => pt.Candidate)
            .WithMany(p => p.Recruiter_Candidate)
            .HasForeignKey(pt => pt.CandidateId);

            modelBuilder.Entity<Candidate_JobProfile>()
                .HasKey(t => new { t.CandidateId, t.JobProfileId });

            modelBuilder.Entity<Candidate_JobProfile>()
            .HasOne(pt => pt.Candidate)
            .WithMany(p => p.Candidate_JobProfile)
            .HasForeignKey(pt => pt.CandidateId);

            modelBuilder.Entity<Candidate_JobProfile>()
            .HasOne(pt => pt.JobProfile)
            .WithMany(p => p.Candidate_JobProfile)
            .HasForeignKey(pt => pt.JobProfileId);

            modelBuilder.Entity<Vendor_Category>()
                .HasKey(t => new { t.CheckCategoryId, t.VendorId });

            modelBuilder.Entity<Vendor_Category>()
            .HasOne(pt => pt.Vendor)
            .WithMany(p => p.Vendor_Category)
            .HasForeignKey(pt => pt.VendorId);

            modelBuilder.Entity<Vendor_Category>()
            .HasOne(pt => pt.CheckCategory)
            .WithMany(p => p.Vendor_Category)
            .HasForeignKey(pt => pt.CheckCategoryId);

            modelBuilder.Entity<JobProfile_Vendor>()
                .HasKey(t => new { t.JobProfileID, t.VendorId });

            modelBuilder.Entity<JobProfile_Vendor>()
            .HasOne(pt => pt.Vendor)
            .WithMany(p => p.JobProfile_Vendor)
            .HasForeignKey(pt => pt.VendorId);

            modelBuilder.Entity<JobProfile_Vendor>()
            .HasOne(pt => pt.JobProfile)
            .WithMany(p => p.JobProfile_Vendor)
            .HasForeignKey(pt => pt.JobProfileID);

            modelBuilder.Entity<Roles>()
               .HasKey(t => new { t.UserId, t.UserTypeId });

            modelBuilder.Entity<Roles>()
            .HasOne(pt => pt.User)
            .WithMany(p => p.Roles)
            .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<Roles>()
            .HasOne(pt => pt.UserType)
            .WithMany(p => p.Roles)
            .HasForeignKey(pt => pt.UserTypeId);

            modelBuilder.Entity<UserType>().HasData(
                new UserType() { ID = 1, Type = "Administrator"});

            modelBuilder.Entity<UserType>().HasData(
                new UserType() { ID = 2, Type = "SuperAdmin" });

            modelBuilder.Entity<UserType>().HasData(
                new UserType() { ID = 3, Type = "Recruiter" });

            modelBuilder.Entity<UserType>().HasData(
                new UserType() { ID = 4, Type = "Manager" });

            modelBuilder.Entity<UserType>().HasData(
                new UserType() { ID = 5, Type = "Operator" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 1, Category = "Credit" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 2, Category = "Criminal" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 3, Category = "Identity" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 4, Category = "Drivers" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 5, Category = "Employment" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 6, Category = "Academic" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 7, Category = "Residency" });

            modelBuilder.Entity<CheckCategory>().HasData(
                new CheckCategory() { ID = 8, Category = "Personal" });

            modelBuilder.Entity<AddressType>().HasData(
                new AddressType() { ID = 1, Type = "Physical" });

            modelBuilder.Entity<AddressType>().HasData(
               new AddressType() { ID = 2, Type = "Billing" });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 1,
                Cost = 100,
                isAvailable = true,
                Name = "A service",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 1
            });



            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 1,
                ServicesID = 1,
                Name = "Compuscan"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 2,
                ServicesID = 1,
                Name = "Experian"
            });

           
        }

  
    }
}
