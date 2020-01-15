using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.Models
{
    public class CoursesSummaryRequest : ApiResponse
    {
        public List<CoursesSummary> Data { get; set; }
    }

    public class CoursesSummary
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Teachers { get; set; }
        public int? Students { get; set; }
        public decimal AverageGrade { get; set; }

        public CoursesSummary()
        {
        }

        public CoursesSummary(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

    public class CourseRequest : ApiResponse
    {
        public CoursesSummary Data { get; set; }
    }
}