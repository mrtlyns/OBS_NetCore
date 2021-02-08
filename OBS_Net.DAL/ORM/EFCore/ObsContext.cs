using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OBS_Net.Entities.Tables;
using Microsoft.Extensions.Configuration;

namespace OBS_Net.DAL.ORM.EFCore
{
    public class ObsContext : DbContext
    {
        private readonly IConfiguration _configration;
        public ObsContext(IConfiguration configuration){
            _configration = configuration;
            }
             public ObsContext(DbContextOptions<ObsContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configration.GetConnectionString("ObsDB"));
            }
        }
      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonForStudent>()
                 .HasOne(sa => sa.Student)
                 .WithMany(sa => sa.MyLessons)
                 .HasForeignKey(sa => sa.StudentId);
            modelBuilder.Entity<Lesson>()
                .HasOne(sa => sa.Teacher)
                .WithMany(sa => sa.MeLessons)
                .HasForeignKey(sa => sa.TeacherId);

        }

        
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonForStudent> LessonForStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

    }
}
