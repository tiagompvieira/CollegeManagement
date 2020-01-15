using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.Models
{
    public class SubjectsModels
    {
        public class SubjectsSummaryRequest : ApiResponse
        {
            public List<SubjectSummary> Data { get; set; }
        }

        public class SubjectSummary
        {
            public int? Id { get; set; }
            public string Name { get; set; }
            public string Teacher { get; set; }
            public int? TeacherId { get; set; }
            public int? CourseId { get; set; }
            public bool Selected { get; set; }

            public SubjectSummary()
            {
            }
        }

        public class SubjectRequest : ApiResponse
        {
            public SubjectSummary Data { get; set; }
        }
    }
}