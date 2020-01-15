﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CollegeManagement.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
    		if (!Database.Exists())
            {
                // https://www.entityframeworktutorial.net/code-first/seed-database-in-code-first.aspx
                Database.SetInitializer(new DataInitializer());
            }
    
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
    
        public virtual ObjectResult<CountCourseTeachers_Result> CountCourseTeachers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CountCourseTeachers_Result>("CountCourseTeachers");
        }
    
        public virtual ObjectResult<CountCourseStudents_Result> CountCourseStudents()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CountCourseStudents_Result>("CountCourseStudents");
        }
    
        public virtual ObjectResult<StudentsAverageGrades_Result> StudentsAverageGrades()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StudentsAverageGrades_Result>("StudentsAverageGrades");
        }
    }
}