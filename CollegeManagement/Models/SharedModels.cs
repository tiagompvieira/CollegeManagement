using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManagement.Models
{
    public class ApiResponse
    {
        public bool Error { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}