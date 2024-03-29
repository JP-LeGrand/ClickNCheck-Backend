﻿using ClickNCheck.Models;
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
                optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = ClickNCheck; Trusted_Connection = True; MultipleActiveResultSets = true");
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
        public DbSet<VerificationCheckChecks> VerificationCheckChecks { get; set; }

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
            .HasForeignKey(pt => pt.VerificationCheckID);


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

            modelBuilder.Entity<CheckStatusType>().HasData(
               new CheckStatusType() { ID = 1, Name = "Cleared" });

            modelBuilder.Entity<CheckStatusType>().HasData(
              new CheckStatusType() { ID = 2, Name = "Possible Issues" });

            modelBuilder.Entity<CheckStatusType>().HasData(
              new CheckStatusType() { ID = 3, Name = "Failed" });

            modelBuilder.Entity<CheckStatusType>().HasData(
              new CheckStatusType() { ID = 4, Name = "In Progress" });

            modelBuilder.Entity<CheckStatusType>().HasData(
              new CheckStatusType() { ID = 5, Name = "Not Started" });

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
                Cost = 100.00,
                isAvailable = true,
                Name = "Compuscan credit report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/SOAPCheck/Umalusi",
                VendorID = 1,
                CheckCategoryID = 1,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 2,
                Cost = 100.00,
                isAvailable = true,
                Name = "Experian employment report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/RestCheck/Experian",
                VendorID = 2,
                CheckCategoryID = 5,
                APIType = 1
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 3,
                Cost = 100.00,
                isAvailable = true,
                Name = "Lexis Nexis criminal report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/mail",
                VendorID = 3,
                CheckCategoryID = 2,
                APIType = 2
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 4,
                Cost = 100.00,
                isAvailable = true,
                Name = "MIE identity report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/longRunningEndpoint/theEndpoint",
                VendorID = 4,
                CheckCategoryID = 3,
                APIType = 3
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 5,
                Cost = 100.00,
                isAvailable = true,
                Name = "PNet driver's report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/SOAPCheck/PNet",
                VendorID = 5,
                CheckCategoryID = 4,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 6,
                Cost = 100.00,
                isAvailable = true,
                Name = "Umalusi academic report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/RestCheck/Experian",
                VendorID = 6,
                CheckCategoryID = 6,
                APIType = 1
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 7,
                Cost = 100.00,
                isAvailable = true,
                Name = "Transunion residency report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/mail",
                VendorID = 7,
                CheckCategoryID = 7,
                APIType = 2
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 8,
                Cost = 100.00,
                isAvailable = true,
                Name = "XDS personal report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/longRunningEndpoint/theEndpoint",
                VendorID = 8,
                CheckCategoryID = 8,
                APIType = 3
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 9,
                Cost = 100.00,
                isAvailable = true,
                Name = "SAPS criminal report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/SOAPCheck/SAPS",
                VendorID = 9,
                CheckCategoryID = 2,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 10,
                Cost = 100.00,
                isAvailable = true,
                Name = "FSCA credit report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/RestCheck/Experian",
                VendorID = 10,
                CheckCategoryID = 1,
                APIType = 1
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 11,
                Cost = 100.00,
                isAvailable = true,
                Name = "INSETA employment report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/mail",
                VendorID = 11,
                CheckCategoryID = 5,
                APIType = 2
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 12,
                Cost = 100.00,
                isAvailable = true,
                Name = "SAQA academic report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/longRunningEndpoint/theEndpoint",
                VendorID = 12,
                CheckCategoryID = 6,
                APIType = 3
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 13,
                Cost = 100.00,
                isAvailable = true,
                Name = "Traffic Department driver's report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/SOAPCheck/TrafficDepartment",
                VendorID = 13,
                CheckCategoryID = 4,
                APIType = 0
            });

            modelBuilder.Entity<Models.Services>().HasData(new
            {
                ID = 14,
                Cost = 100.00,
                isAvailable = true,
                Name = "Home Affairs identity report",
                TurnaroundTime = "2 days",
                URL = "https://checkserviceapi.azurewebsites.net/api/RestCheck/Experian",
                VendorID = 14,
                CheckCategoryID = 5,
                APIType = 1
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
                Name = "AccountsPerson1",
                Phone = "1324654978",
                Email = "kmuranga@retrorabbit.com"
            });
            modelBuilder.Entity<ContactPerson>().HasData(new
            {
                ID = 1,
                Name = "ContactPerson1",
                Phone = "1324654978",
                Email = "kmuranga@retrorabbit.com"
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
                Email = "cmoganedi@retrorabbit.com",
                Phone = "08334419512",
                EmployeeNumber = 65465,
                Otp = "54346546",
                Password = "123",
                OrganisationID = 1,
                LinkCodeID = 1,
                ManagerID = 1,
                UserTypeID = 4,
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 2,
                Name = "administrator",
                Surname = "man",
                Email = "mmohale@retrorabbit.co.za",
                Phone = "+27649019205",
                EmployeeNumber = 54646,
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 2,
                ManagerID = 1,
                UserTypeID = 1,
                Guid = System.Guid.NewGuid(),
                Password = "123",
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 3,
                Name = "Super",
                Surname = "administrator",
                Email = "tvurayayi@retrorabbit.co.za",
                Phone = "+27649019205",
                EmployeeNumber = 54646,
                Otp = "54346546",
                OrganisationID = 1,
                LinkCodeID = 3,
                ManagerID = 1,
                UserTypeID = 2,
                Guid = System.Guid.NewGuid(),
                Password = "123",
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 4,
                Name = "Keoabetswe",
                Surname = "Morake",
                Email = "kmorake@retrorabbit.co.za",
                Phone = "+27649019205",
                EmployeeNumber = 542435,
                Otp = "54346546",
                Password = "123",
                OrganisationID = 1,
                LinkCodeID = 4,
                ManagerID = 1,
                UserTypeID = 3,
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 10,
                Name = "Recruiter2",
                Surname = "man",
                Email = "kmuranga@retrorabbit.co.za",
                Phone = "+27649019205",
                EmployeeNumber = 542435,
                Otp = "54346546",
                Password = "123",
                OrganisationID = 1,
                LinkCodeID = 4,
                ManagerID = 1,
                UserTypeID = 3,
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 9,
                Name = "Recruiter1",
                Surname = "man",
                Email = "u13278012@tuks.co.za",
                Phone = "+27649019205",
                EmployeeNumber = 542435,
                Otp = "54346546",
                Password = "123",
                OrganisationID = 1,
                LinkCodeID = 4,
                ManagerID = 1,
                UserTypeID = 3,
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });
            modelBuilder.Entity<User>().HasData(new
            {
                ID = 5,
                Name = "Operator",
                Surname = "man",
                Email = "xdlamini@retrorabbit.com",
                Phone = "+27649019205",
                EmployeeNumber = 2345,
                Otp = "54346546",
                Password = "123",
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 5,
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 6,
                Name = "Jaco",
                Surname = "van den Heever",
                Email = "jaco@sanddollardesign.co.za",
                Phone = "+27728244888",
                EmployeeNumber = 2345,
                Otp = "0",
                Password = "CR@ZYmanga1",
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 6,
                PictureUrl = "https://clicknchecksite.blob.core.windows.net/ba30e302-fdc5-4f37-8ddf-f53ad8f23a76/Images/8db88b9d-98e1-447e-851b-2989d1302517788720.jpg",
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 7,
                Name = "Admin",
                Surname = "Admin",
                Email = "mpinanemohale@gmail.com",
                Phone = "+27649019205",
                EmployeeNumber = 2345,
                Otp = "0",
                Password = "123",
                OrganisationID = 1,
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 6,
                PictureUrl = "https://clicknchecksite.blob.core.windows.net/ba30e302-fdc5-4f37-8ddf-f53ad8f23a76/Images/8db88b9d-98e1-447e-851b-2989d1302517788720.jpg",
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
            });

            modelBuilder.Entity<User>().HasData(new
            {
                ID = 8,
                Name = "Gerome",
                Surname = "Schutte",
                Email = "gschutte@retrorabbit.co.za",
                Phone = "+27814201753",
                EmployeeNumber = 2345,
                Otp = "0",
                OrganisationID = 1,
                Password = "G3rome$chutte",
                LinkCodeID = 5,
                ManagerID = 1,
                UserTypeID = 6,
                PictureUrl = "https://clicknchecksite.blob.core.windows.net/ba30e302-fdc5-4f37-8ddf-f53ad8f23a76/Images/8db88b9d-98e1-447e-851b-2989d1302517788720.jpg",
                Guid = System.Guid.NewGuid(),
                PasswordCount = 0,
                PasswordExpiryDate = System.DateTime.Now
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
                Title = "Software Developer",
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
                Title = "HR Assistant",
                JobCode = "111",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 3,
                Title = "Project Manager",
                JobCode = "222",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 4,
                Title = "CEO",
                JobCode = "333",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 5,
                Title = "Senior Software Developer",
                JobCode = "444",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<JobProfile>().HasData(new
            {
                ID = 6,
                Title = "Junior Software Developer",
                JobCode = "777",
                isCompleted = true,
                isTemplate = false,
                checksNeedVerification = false,
                OrganisationID = 1,
                authorisationRequired = true
            });

            modelBuilder.Entity<Recruiter_JobProfile>().HasData(new
            {
                JobProfileId = 1,
                RecruiterId = 4
            });

            modelBuilder.Entity<Recruiter_JobProfile>().HasData(new
            {
                JobProfileId = 2,
                RecruiterId = 4
            });

            modelBuilder.Entity<Recruiter_JobProfile>().HasData(new
            {
                JobProfileId = 3,
                RecruiterId = 9
            });

            modelBuilder.Entity<Recruiter_JobProfile>().HasData(new
            {
                JobProfileId = 4,
                RecruiterId = 9
            });

            modelBuilder.Entity<Recruiter_JobProfile>().HasData(new
            {
                JobProfileId = 5,
                RecruiterId = 10
            });

            modelBuilder.Entity<Recruiter_JobProfile>().HasData(new
            {
                JobProfileId = 6,
                RecruiterId = 10
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

            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 3,
                ServicesID = 2,
                Order = 1
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 3,
                ServicesID = 1,
                Order = 2
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 3,
                ServicesID = 7,
                Order = 4
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 3,
                ServicesID = 3,
                Order = 3
            });

            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 4,
                ServicesID = 2,
                Order = 1
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 4,
                ServicesID = 1,
                Order = 2
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 4,
                ServicesID = 7,
                Order = 4
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 4,
                ServicesID = 3,
                Order = 3
            });


            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 5,
                ServicesID = 2,
                Order = 1
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 5,
                ServicesID = 1,
                Order = 2
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 5,
                ServicesID = 7,
                Order = 4
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 5,
                ServicesID = 3,
                Order = 3
            });

            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 6,
                ServicesID = 2,
                Order = 1
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 6,
                ServicesID = 1,
                Order = 2
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 6,
                ServicesID = 7,
                Order = 4
            });
            modelBuilder.Entity<JobProfile_Checks>().HasData(new
            {
                JobProfileID = 6,
                ServicesID = 3,
                Order = 3
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
                Guid = System.Guid.NewGuid(),
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
                Guid = System.Guid.NewGuid(),
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
                Guid = System.Guid.NewGuid(),
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
                Guid = System.Guid.NewGuid(),
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
                Guid = System.Guid.NewGuid(),
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
                Guid = System.Guid.NewGuid(),
                OrganisationID = 1
            });

            modelBuilder.Entity<VerificationCheck>().HasData(new
            {
                ID = 1,
                IsAuthorize = true,
                IsComplete = true,
                JobProfileID = 6,
                RecruiterID = 10,
                Title = "Junior Software Developer"
            });

            modelBuilder.Entity<Candidate_Verification>().HasData(new
            {
                ID = 1,
                CandidateID = 1,
                HasConsented = true,
                VerificationCheckID = 1
            });

            modelBuilder.Entity<Candidate_Verification>().HasData(new
            {
                ID = 2,
                CandidateID = 2,
                HasConsented = true,
                VerificationCheckID = 1
            });

            modelBuilder.Entity<Candidate_Verification>().HasData(new
            {
                ID = 3,
                CandidateID = 3,
                HasConsented = true,
                VerificationCheckID = 1
            });

            modelBuilder.Entity<Candidate_Verification>().HasData(new
            {
                ID = 4,
                CandidateID = 4,
                HasConsented = true,
                VerificationCheckID = 1
            });

            modelBuilder.Entity<Candidate_Verification>().HasData(new
            {
                ID = 5,
                CandidateID = 5,
                HasConsented = true,
                VerificationCheckID = 1
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 1,
                Candidate_VerificationID = 1,
                ServicesID = 1,
                CheckStatusTypeID = 1,
                Order = 1
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 2,
                Candidate_VerificationID = 1,
                ServicesID = 2,
                CheckStatusTypeID = 2,
                Order = 2
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 3,
                Candidate_VerificationID = 1,
                ServicesID = 3,
                CheckStatusTypeID = 3,
                Order = 3
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 4,
                Candidate_VerificationID = 1,
                ServicesID = 7,
                CheckStatusTypeID = 4,
                Order = 4
            });

            ///
            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 5,
                Candidate_VerificationID = 2,
                ServicesID = 1,
                CheckStatusTypeID = 5,
                Order = 1
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 6,
                Candidate_VerificationID = 2,
                ServicesID = 2,
                CheckStatusTypeID = 1,
                Order = 2
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 7,
                Candidate_VerificationID = 2,
                ServicesID = 3,
                CheckStatusTypeID = 2,
                Order = 3
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 8,
                Candidate_VerificationID = 2,
                ServicesID = 7,
                CheckStatusTypeID = 3,
                Order = 4
            });
            ///

            ///
            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 9,
                Candidate_VerificationID = 3,
                ServicesID = 1,
                CheckStatusTypeID = 4,
                Order = 1
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 10,
                Candidate_VerificationID = 3,
                ServicesID = 2,
                CheckStatusTypeID = 5,
                Order = 2
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 11,
                Candidate_VerificationID = 3,
                ServicesID = 3,
                CheckStatusTypeID = 1,
                Order = 3
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 12,
                Candidate_VerificationID = 3,
                ServicesID = 7,
                CheckStatusTypeID = 4,
                Order = 4
            });
            ///

            ///
            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 13,
                Candidate_VerificationID = 4,
                ServicesID = 1,
                CheckStatusTypeID = 5,
                Order = 1
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 14,
                Candidate_VerificationID = 4,
                ServicesID = 2,
                CheckStatusTypeID = 1,
                Order = 2
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 15,
                Candidate_VerificationID = 4,
                ServicesID = 3,
                CheckStatusTypeID = 2,
                Order = 3
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 16,
                Candidate_VerificationID = 4,
                ServicesID = 7,
                CheckStatusTypeID = 3,
                Order = 4
            });
            ///

            ///
            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 17,
                Candidate_VerificationID = 5,
                ServicesID = 1,
                CheckStatusTypeID = 5,
                Order = 1
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 18,
                Candidate_VerificationID = 5,
                ServicesID = 2,
                CheckStatusTypeID = 1,
                Order = 2
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 19,
                Candidate_VerificationID = 5,
                ServicesID = 3,
                CheckStatusTypeID = 2,
                Order = 3
            });

            modelBuilder.Entity<Candidate_Verification_Check>().HasData(new
            {
                ID = 20,
                Candidate_VerificationID = 5,
                ServicesID = 7,
                CheckStatusTypeID = 3,
                Order = 4
            });
            ///

            modelBuilder.Entity<VerificationCheckChecks>().HasData(new
            {
                ID = 1,
                VerificationCheckID = 1,
                ServicesID = 1,
                Order = 1
            });

            modelBuilder.Entity<VerificationCheckChecks>().HasData(new
            {
                ID = 2,
                VerificationCheckID = 1,
                ServicesID = 2,
                Order = 2
            });

            modelBuilder.Entity<VerificationCheckChecks>().HasData(new
            {
                ID = 3,
                VerificationCheckID = 1,
                ServicesID = 3,
                Order = 3
            });

            modelBuilder.Entity<VerificationCheckChecks>().HasData(new
            {
                ID = 4,
                VerificationCheckID = 1,
                ServicesID = 7,
                Order = 4
            });
        }
    }
}
