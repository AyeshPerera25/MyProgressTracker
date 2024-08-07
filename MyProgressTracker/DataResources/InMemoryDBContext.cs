﻿using Microsoft.EntityFrameworkCore;
using MyProgressTrackerDependanciesLib.Models.Entities;

namespace MyProgressTracker.DataResources
{
    public class InMemoryDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }

        public InMemoryDBContext(DbContextOptions<InMemoryDBContext> options)
            : base(options)
        {

        }

    }
}
