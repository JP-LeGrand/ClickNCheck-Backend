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
        public DbSet<Checks> Checks { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Candidate_JobProfile> Candidate_JobProfile { get; set; }
        public DbSet<JobProfile_Checks> JobProfile_Checks { get; set; }

        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<PhysicalAddress> PhysicalAddress { get; set; }
        public DbSet<BillingAddress> BillingAddress { get; set; }
        public DbSet<CheckCategory> CheckCategory { get; set; }

        public virtual DbSet<LinkCode> LinkCodes { get; set; }


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

            modelBuilder.Entity<JobProfile_Checks>()
                .HasKey(t => new { t.ChecksId, t.JobProfileId });

            modelBuilder.Entity<JobProfile_Checks>()
            .HasOne(pt => pt.Checks)
            .WithMany(p => p.JobProfile_Checks)
            .HasForeignKey(pt => pt.ChecksId);

            modelBuilder.Entity<JobProfile_Checks>()
            .HasOne(pt => pt.JobProfile)
            .WithMany(p => p.JobProfile_Checks)
            .HasForeignKey(pt => pt.JobProfileId);

        }

  
    }
}
