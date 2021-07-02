using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Job.Manager.DataAccess.Database
{
    public partial class JobsContext : DbContext
    {
        public JobsContext()
        {
        }

        public JobsContext(DbContextOptions<JobsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<NumberSortJobs> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Jobs;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<NumberSortJobs>(entity =>
            {
                entity.HasKey("Id");

                entity.ToTable("NumberSortJobs");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.EnqueueDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.InputArray)
                .IsRequired()
                .HasMaxLength(int.MaxValue)
                .IsFixedLength(true);

                entity.Property(e => e.OutputArray)
                .HasMaxLength(int.MaxValue)
                .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
