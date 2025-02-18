using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Reposatories._Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Hosting> Hostings { get; set; }
        public DbSet<HostingProperties> HostingProperties { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectReview> ProjectReviews { get; set; }
        public DbSet<ProjectMedia> projectMedias { get; set; }
        public DbSet<ProjectSteps> ProjectSteps { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceSteps> ServiceSteps { get; set; }

    }
}
