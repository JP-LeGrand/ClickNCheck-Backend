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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClickNCheck;Integrated Security=True");
            }
        }

        public DbSet<User> User { get; set; }
        public DbSet<Candidate_Verification> Candidate_Verification { get; set; }
        public DbSet<Candidate_JobProfile> Candidate_JobProfile { get; set; }
        public DbSet<Candidate_Verification_Check> Candidate_Verification_Check { get; set; }
        public DbSet<CheckStatusType> CheckStatusType { get; set; }
        public DbSet<VerificationCheck> VerificationCheck { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<Recruiter_JobProfile> Recruiter_JobProfile { get; set; }
        public DbSet<JobProfile> JobProfile { get; set; }
        public DbSet<Vendor> Checks { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<JobProfile_Checks> JobProfile_Check { get; set; }
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
        public DbSet<Results> Result { get; set; }

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

            modelBuilder.Entity<JobProfile_Checks>()
                .HasKey(t => new { t.JobProfileID, t.ServicesID });

            modelBuilder.Entity<JobProfile_Checks>()
            .HasOne(pt => pt.Services)
            .WithMany(p => p.JobProfile_Check)
            .HasForeignKey(pt => pt.ServicesID);

            modelBuilder.Entity<JobProfile_Checks>()
            .HasOne(pt => pt.JobProfile)
            .WithMany(p => p.JobProfile_Check)
            .HasForeignKey(pt => pt.JobProfileID);


            modelBuilder.Entity<Candidate_JobProfile>()
                .HasKey(t => new { t.JobProfileID, t.CandidateID });

            modelBuilder.Entity<Candidate_JobProfile>()
            .HasOne(pt => pt.Candidate)
            .WithMany(p => p.Candidate_JobProfile)
            .HasForeignKey(pt => pt.CandidateID);

            modelBuilder.Entity<Candidate_JobProfile>()
            .HasOne(pt => pt.JobProfile)
            .WithMany(p => p.Candidate_JobProfile)
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


            modelBuilder.Entity<Candidate_Verification>()
            .HasOne(pt => pt.Candidate)
            .WithMany(p => p.Candidate_Verification)
            .HasForeignKey(pt => pt.CandidateID);

            modelBuilder.Entity<Candidate_Verification>()
            .HasOne(pt => pt.VerificationCheck)
            .WithMany(p => p.Candidate_Verification)
            .HasForeignKey(pt => pt.VerificationID);


            modelBuilder.Entity<UserType>().HasData(
                new UserType() { ID = 1, Type = "Administrator" });

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
                Name = "Compuscan credit report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 1,
                CheckCategoryID = 1,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 2,
                Cost = 100,
                isAvailable = true,
                Name = "Experian employment report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 2,
                CheckCategoryID = 5,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 3,
                Cost = 100,
                isAvailable = true,
                Name = "Lexis Nexis criminal report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 3,
                CheckCategoryID = 2,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 4,
                Cost = 100,
                isAvailable = true,
                Name = "MIE identity report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 4,
                CheckCategoryID = 3,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 5,
                Cost = 100,
                isAvailable = true,
                Name = "PNet driver's report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 5,
                CheckCategoryID = 4,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 6,
                Cost = 100,
                isAvailable = true,
                Name = "Umalusi academic report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 6,
                CheckCategoryID = 6,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 7,
                Cost = 100,
                isAvailable = true,
                Name = "Transunion residency report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 7,
                CheckCategoryID = 7,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 8,
                Cost = 100,
                isAvailable = true,
                Name = "XDS personal report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 8,
                CheckCategoryID = 8,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 9,
                Cost = 100,
                isAvailable = true,
                Name = "SAPS criminal report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 9,
                CheckCategoryID = 2,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 10,
                Cost = 100,
                isAvailable = true,
                Name = "FSCA credit report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 10,
                CheckCategoryID = 1,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 11,
                Cost = 100,
                isAvailable = true,
                Name = "INSETA employment report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 11,
                CheckCategoryID = 5,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 12,
                Cost = 100,
                isAvailable = true,
                Name = "SAQA academic report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 12,
                CheckCategoryID = 6,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 13,
                Cost = 100,
                isAvailable = true,
                Name = "Traffic Department driver's report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 13,
                CheckCategoryID = 4,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 14,
                Cost = 100,
                isAvailable = true,
                Name = "Home Affairs identity report",
                TurnaroundTime = "2 days",
                URL = "www.google.com",
                VendorID = 14,
                CheckCategoryID = 5,
                APIType = 0
            });


            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 1,
                Name = "Compuscan"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 2,
                Name = "Experian"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 3,
                Name = "Lexis Nexis"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 4,
                Name = "MIE"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 5,
                Name = "PNet"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 6,
                Name = "Umalusi"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 7,
                Name = "Transunion"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 8,
                Name = "XDS"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 9,
                Name = "SAPS"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 10,
                Name = "FSCA"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 11,
                Name = "INSETA"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 12,
                Name = "SAQA "
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 13,
                Name = "Traffic Department"
            });

            modelBuilder.Entity<Vendor>().HasData(new
            {
                ID = 14,
                Name = "Home Affairs"
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
                BillingAddressID = 2,
                Guid = System.Guid.NewGuid()
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
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 1,
                ManagerID = 1,
                UserTypeID = 4,
                Guid = System.Guid.NewGuid()
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 2,
                Name = "administrator",
                Surname = "man",
                Email = "mmohale@retrorabbit.co.za",
                Phone = "08334419512",
                EmployeeNumber = 54646,
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 2,
                ManagerID = 1,
                UserTypeID = 1,
                Guid = System.Guid.NewGuid(),
                Password = "123456789"
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 3,
                Name = "Super",
                Surname = "administrator",
                Email = "mmohale@retrorabbit.co.za",
                Phone = "08334419512",
                EmployeeNumber = 54646,
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 3,
                ManagerID = 1,
                UserTypeID = 2,
                Guid = System.Guid.NewGuid(),
                Password = "123456789"
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 4,
                Name = "Recruiter1",
                Surname = "man",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 542435,
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 4,
                ManagerID = 1,
                UserTypeID = 3,
                Guid = System.Guid.NewGuid()
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 5,
                Name = "Operator",
                Surname = "man",
                Email = "me@mail.com",
                Phone = "08334419512",
                EmployeeNumber = 2345,
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 5,
                Guid = System.Guid.NewGuid()
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 6,
                Name = "Recruiter",
                Surname = "Recruiter",
                Email = "mmohale@retrotabbit.co.za",
                Phone = "08334419512",
                EmployeeNumber = 2345,
                Otp = "0",
                Password = "rec",
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 6,
                PictureUrl = "https://clicknchecksite.blob.core.windows.net/ba30e302-fdc5-4f37-8ddf-f53ad8f23a76/Images/8db88b9d-98e1-447e-851b-2989d1302517788720.jpg",
                Guid = System.Guid.NewGuid()
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 7,
                Name = "Admin",
                Surname = "Admin",
                Email = "mpinanemohale@gmail.com",
                Phone = "08334419512",
                EmployeeNumber = 2345,
                Otp = "0",
                Password = "admin",
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 6,
                PictureUrl = "https://clicknchecksite.blob.core.windows.net/ba30e302-fdc5-4f37-8ddf-f53ad8f23a76/Images/8db88b9d-98e1-447e-851b-2989d1302517788720.jpg",
                Guid = System.Guid.NewGuid()
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 8,
                Name = "Admin",
                Surname = "Recruiter",
                Email = "1363679@students.wits.ac.za",
                Phone = "08334419512",
                EmployeeNumber = 2345,
                Otp = "0",
                OrganisationID = 1,
                Password = "admin_rec",
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 6,
                PictureUrl = "https://clicknchecksite.blob.core.windows.net/ba30e302-fdc5-4f37-8ddf-f53ad8f23a76/Images/8db88b9d-98e1-447e-851b-2989d1302517788720.jpg",
                Guid = System.Guid.NewGuid()
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
                UserTypeId = 3,
                UserId = 7
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

            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 3,
                UserId = 6
            });

            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 1,
                UserId = 7
            });

            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 1,
                UserId = 8
            });

            modelBuilder.Entity<Roles>().HasData(new
            {
                UserTypeId = 3,
                UserId = 8
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 1,
                Title = "Sofwtare Developer",
                JobCode = "555",
                isCompleted = true,
                isTemplate = true,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 2,
                Title = "Sofwtare Developer",
                JobCode = "555",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 1,
                ServicesID = 1,
                Order = 1
            });

            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 1,
                ServicesID = 2,
                Order = 2
            });

            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 2,
                ServicesID = 2,
                Order = 1
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 2,
                ServicesID = 1,
                Order = 2
            });

            modelBuilder.Entity<Candidate>().HasData(new
            {
                ID = 1,
                Name = "kudzai",
                Surname = "mur",
                ID_Passport = "fd54545",
                ID_Type = "passport",
                PictureUrl = "www.pic.com",
                isVerified = true,
                Maiden_Surname = "asdddd",
                passwordChanged = true,
                Email = "me@mail.com",
                HasConsented = true,
                Phone = "2342423423",
                Password = "dgsfhsf",
                OrganisationID = 1
            });
            modelBuilder.Entity<Candidate>().HasData(new
            {
                ID = 6,
                Name = "kudzai6",
                Surname = "mur",
                ID_Passport = "fd54545",
                ID_Type = "passport",
                PictureUrl = "www.pic.com",
                isVerified = true,
                Maiden_Surname = "asdddd",
                passwordChanged = true,
                Email = "me@mail.com",
                HasConsented = true,
                Phone = "2342423423",
                Password = "dgsfhsf",
                OrganisationID = 1
            });
            modelBuilder.Entity<Candidate>().HasData(new
            {
                ID = 2,
                Name = "kudzai2",
                Surname = "mur",
                ID_Passport = "fd54545",
                ID_Type = "passport",
                PictureUrl = "www.pic.com",
                isVerified = true,
                Maiden_Surname = "asdddd",
                passwordChanged = true,
                Email = "me@mail.com",
                HasConsented = true,
                Phone = "2342423423",
                Password = "dgsfhsf",
                OrganisationID = 1
            });
            modelBuilder.Entity<Candidate>().HasData(new
            {
                ID = 3,
                Name = "kudzai3",
                Surname = "mur",
                ID_Passport = "fd54545",
                ID_Type = "passport",
                PictureUrl = "www.pic.com",
                isVerified = true,
                Maiden_Surname = "asdddd",
                passwordChanged = true,
                Email = "me@mail.com",
                HasConsented = true,
                Phone = "2342423423",
                Password = "dgsfhsf",
                OrganisationID = 1
            });
            modelBuilder.Entity<Candidate>().HasData(new
            {
                ID = 4,
                Name = "kudzai4",
                Surname = "mur",
                ID_Passport = "fd54545",
                ID_Type = "passport",
                PictureUrl = "www.pic.com",
                isVerified = true,
                Maiden_Surname = "asdddd",
                passwordChanged = true,
                Email = "me@mail.com",
                HasConsented = true,
                Phone = "2342423423",
                Password = "dgsfhsf",
                OrganisationID = 1
            });
            modelBuilder.Entity<Candidate>().HasData(new
            {
                ID = 5,
                Name = "kudzai5",
                Surname = "mur",
                ID_Passport = "fd54545",
                ID_Type = "passport",
                PictureUrl = "www.pic.com",
                isVerified = true,
                Maiden_Surname = "asdddd",
                passwordChanged = true,
                Email = "me@mail.com",
                HasConsented = true,
                Phone = "2342423423",
                Password = "dgsfhsf",
                OrganisationID = 1
            });
        }


    }
}
