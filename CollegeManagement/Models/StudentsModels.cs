using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CollegeManagement.Models.SubjectsModels;

namespace CollegeManagement.Models
{
    public class StudentsModels
    {
        public class StudentsSummaryRequest : ApiResponse
        {
            public List<StudentSummary> Data { get; set; }
        }

        public class StudentSummary
        {
            public int? RegistrationNumber { get; set; }
            public string Name { get; set; }
            public int? CourseId { get; set; }
            public List<SubjectSummary> Subjects { get; set; }
            public decimal? GradeAverage { get; set; }

            public StudentSummary()
            {
                this.Subjects = new List<SubjectSummary>();
            }
        }

        public class StudentRequest : ApiResponse
        {
            public StudentSummary Data { get; set; }
            public int? CourseId { get; set; }
        }

        public class StudentEvaluationRequest : ApiResponse
        {
            public int Id { get; set; }
            public List<StudentEvaluation> Data { get; set; }
        }

        public class StudentEvaluation
        {
            public string Name { get; set; }
            public int? SubjectId { get; set; }
            public int? Grade { get; set; }
        }
    }
}