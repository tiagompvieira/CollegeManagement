using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.Models
{
    public class TeachersSummaryRequest : ApiResponse
    {
        public List<TeacherSummary> Data { get; set; }
    }

    public class TeacherSummary
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal? Salary { get; set; }

        public TeacherSummary()
        {
        }
    }

    public class TeacherRequest : ApiResponse
    {
        public TeacherSummary Data { get; set; }
    }
}