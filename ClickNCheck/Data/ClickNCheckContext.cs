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
        public DbSet<Candidate_VerificationRequest> Candidate_VerificationRequest { get; set; }
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
        public DbSet<VerificationRequest> VerificationRequest { get; set; }


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

            modelBuilder.Entity<Candidate_VerificationRequest>()
                .HasKey(t => new { t.CandidateId, t.VerificationRequestId });

            modelBuilder.Entity<Candidate_VerificationRequest>()
            .HasOne(pt => pt.Candidate)
            .WithMany(p => p.Candidate_JobProfile)
            .HasForeignKey(pt => pt.CandidateId);

            modelBuilder.Entity<Candidate_VerificationRequest>()
            .HasOne(pt => pt.VerificationRequest)
            .WithMany(p => p.Candidate_VerificationRequest)
            .HasForeignKey(pt => pt.VerificationRequestId);

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

            modelBuilder.Entity<JobProfile>()
            .HasMany(c => c.VerificationRequest)
            .WithOne(e => e.JobProfile);

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

         /*   modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 1,
                Cost = 100,
                isAvailable = true,
                Name = "A service",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 1
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 2,
                Cost = 100,
                isAvailable = true,
                Name = "Another service",
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
                ServicesID = 2,
                Name = "Experian"
            });

            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 1,
                CheckCategoryId = 5
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 1
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 2
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 3
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 4
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 5
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 6
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 7
            });
            modelBuilder.Entity<Vendor_Category>().HasData(new
            {
                VendorId = 2,
                CheckCategoryId = 8
            });

            modelBuilder.Entity<AccountsPerson>().HasData(new
            {
                ID = 1,
                Name = "ContactPerson1",
                Phone = "1324654978",
                Email = "mail@mail.com"
            });
            modelBuilder.Entity<ContactPerson>().HasData(new
            {
                ID = 1,
                Name = "ContactPerson1",
                Phone = "1324654978",
                Email = "mail@mail.com"
            });

            modelBuilder.Entity<Address>().HasData(new
            {
                ID = 1,
                Building = "Rabbitania",
                Street = "3 Diep in Die Berg",
                Suburb = "Wapadrand",
                City = "Pretoria",
                PostalCode = "0028",
                Province = "Gauteng",
                AddressTypeID = 1
            });
            modelBuilder.Entity<Address>().HasData(new
            {
                ID = 2,
                Building = "Rabbitania",
                Street = "3 Diep in Die Berg",
                Suburb = "Wapadrand",
                City = "Pretoria",
                PostalCode = "0028",
                Province = "Gauteng",
                AddressTypeID = 2
            });
            modelBuilder.Entity<Organisation>().HasData(new
            {
                ID = 1,
                Name = "Retro Rabbit",
                RegistrationNumber = "7522",
                ContractUrl = "www.contract.url",
                TaxNumber = "42757",
                AccountsPersonID = 1,
                ContactPersonID = 1,
                PhysicalAddressID = 1,
                BillingAddressID = 2
            });

            modelBuilder.Entity<LinkCode>().HasData(new
            {
                ID = 5,
                Code = "codecodecode",
                Used = false,
                Admin_ID = 1
            });
            modelBuilder.Entity<LinkCode>().HasData(new
            {
                ID = 1,
                Code = "codecodecode",
                Used = false,
                Admin_ID = 1
            });
            modelBuilder.Entity<LinkCode>().HasData(new
            {
                ID = 2,
                Code = "codecodecode",
                Used = false,
                Admin_ID = 1
            });
            modelBuilder.Entity<LinkCode>().HasData(new
            {
                ID = 3,
                Code = "codecodecode",
                Used = false,
                Admin_ID = 1
            });
            modelBuilder.Entity<LinkCode>().HasData(new
            {
                ID = 4,
                Code = "codecodecode",
                Used = false,
                Admin_ID = 1
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 1,
                Name = "manager",
                Surname = "man",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 65465,
                Otp = 1231,
                OrganisationID = 1,
                LinkCodeID = 1,
                ManagerID = 1,
                UserTypeID = 4
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 2,
                Name = "administrator",
                Surname = "man",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 54646,
                Otp = 0x2_056a,
                OrganisationID = 1,
                LinkCodeID = 2,
                ManagerID = 1,
                UserTypeID = 1
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 3,
                Name = "Super",
                Surname = "administrator",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 54646,
                Otp = 52345,
                OrganisationID = 1,
                LinkCodeID = 3,
                ManagerID = 1,
                UserTypeID = 2
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 4,
                Name = "Recruiter",
                Surname = "man",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 542435,
                Otp = 542223,
                OrganisationID = 1,
                LinkCodeID = 4,
                ManagerID = 1,
                UserTypeID = 3
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 5,
                Name = "Operator",
                Surname = "man",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 2345,
                Otp = 3452,
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 5
            });

            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 1,
                UserId = 2
            });
            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 2,
                UserId = 3
            });
            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 3,
                UserId = 4
            });
            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 4,
                UserId = 1
            });
            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 5,
                UserId = 5
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 1,
                Title = "Sofwtare Developer",
                JobCode = "555",
                isCompleted = true,
                isTemplate = true,
                checksNeedVerification = false,
                OrganisationID = 1
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 2,
                Title = "Sofwtare Developer",
                JobCode = "555",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1
            });*/
        }

  
    }
}
