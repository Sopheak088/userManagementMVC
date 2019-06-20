using iThinking.UserCenter.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using System.Data.Entity;

namespace iThinking.UserCenter
{
    public class UserCenterDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IDataContextAsync
    {
        public UserCenterDbContext()
            : base("iThinkingUserCenterConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        static UserCenterDbContext()
        {
        }

        #region Identity

        public virtual DbSet<ApplicationUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<ApplicationGroupRole> AspNetGroupRoles { get; set; }
        public virtual DbSet<ApplicationGroup> AspNetGroups { get; set; }
        public virtual DbSet<ApplicationUserGroup> AspNetUserGroups { get; set; }
        public virtual DbSet<ApplicationUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<ApplicationUserClaim> AspNetUserClaims { get; set; }

        public virtual DbSet<ApplicationProject> AspNetProjects { get; set; }
        public virtual DbSet<ApplicationUserChange> AspNetUserChanges { get; set; }
        public virtual DbSet<ApplicationUserHistory> AspNetUserHistories { get; set; }
        public virtual DbSet<ApplicationUserGroupChange> AspNetUserGroupChanges { get; set; }
        public virtual DbSet<ApplicationUserGroupHistory> AspNetUserGroupHistories { get; set; }

        #endregion Identity

        #region System

        public DbSet<ApplicationError> ApplicationErrors { set; get; }

        #endregion System



        #region UnitOfWork

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        #endregion UnitOfWork

        public static UserCenterDbContext Create()
        {
            return new UserCenterDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                .WithRequired().HasForeignKey<string>((ApplicationUserGroup ag) => ag.ApplicationGroupId);
            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        ApplicationUserId = r.ApplicationUserId,
                        ApplicationGroupId = r.ApplicationGroupId
                    }).ToTable("AspNetUserGroups");

            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                .WithRequired().HasForeignKey<string>((ApplicationGroupRole ap) => ap.ApplicationGroupId);
            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    ApplicationRoleId = gr.ApplicationRoleId,
                    ApplicationGroupId = gr.ApplicationGroupId
                }).ToTable("AspNetGroupRoles");
        }

        //public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
    }
}