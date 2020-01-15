using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CollegeManagement.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpGet]
        [Route("api/Courses/GetCoursesSummary")]
        public CoursesSummaryRequest GetCoursesSummary()
        {
            CoursesSummaryRequest response = new CoursesSummaryRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    response.Data = entities.Courses
                        .ToList()
                        .OrderBy(subject => subject.Name)
                        .Select(course => new CoursesSummary(course.Id, course.Name))
                        .ToList();

                    var coursesTeachers = entities.CountCourseTeachers().ToList();
                    var coursesStudents = entities.CountCourseStudents().ToList();


                    foreach (var course in response.Data)
                    {
                        course.Teachers = coursesTeachers
                            .Where(ct => ct.CourseId == course.Id).FirstOrDefault()
                            ?.TeachersCount;

                        course.Students = coursesStudents
                            .Where(ct => ct.Id == course.Id).FirstOrDefault()
                            ?.StudentsCount;
                    }
                }

                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Error = true;
                response.Message = "MsgUnkownError";
            }

            return response;
        }

        [HttpGet]
        [Route("api/Courses/GetCourses")]
        public CoursesSummaryRequest GetCourses()
        {
            CoursesSummaryRequest response = new CoursesSummaryRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    response.Data = entities.Courses
                        .ToList()
                        .OrderBy(subject => subject.Name)
                        .Select(course => new CoursesSummary(course.Id, course.Name))
                        .ToList();
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = "MsgUnkownError";
            }

            return response;
        }

        [HttpGet]
        [Route("api/Courses/GetCourse/{id}")]
        public CourseRequest GetCoursesSummary(int id)
        {
            CourseRequest response = new CourseRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var course = entities.Courses.Find(id);

                    if(course != null)
                    {
                        response.Data = new CoursesSummary(course.Id, course.Name);
                        response.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
            }

            return response;
        }

        [HttpPost]
        [Route("api/Courses/SaveCourse")]
        public ApiResponse SaveCourse(CoursesSummary courseData)
        {
            bool isNew = false;
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    CollegeManagement.DataAccess.Course bdCourse = null;

                    if (courseData.Id.HasValue)
                    {
                        bdCourse = entities.Courses.Find(courseData.Id);
                    }

                    if (bdCourse == null)
                    {
                        isNew = true;
                        bdCourse = new DataAccess.Course();
                    }
                    else
                    {
                        bdCourse.Id = courseData.Id.Value;
                    }
                    
                    bdCourse.Name = courseData.Name;

                    if (isNew)
                    {
                        entities.Courses.Add(bdCourse);
                    }

                    entities.SaveChanges();

                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
            }

            return response;
        }

        [HttpPost]
        [Route("api/Courses/DeleteCourse/{id}")]
        public ApiResponse DeleteCourse(int id)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var course = entities.Courses.Find(id);

                    var courseSubjects = course.Subjects.ToList();

                    foreach (var subjects in courseSubjects)
                    {
                        subjects.StudentGrades.Clear();
                        subjects.Students.Clear();
                    }

                    if (course != null)
                    {
                        entities.Courses.Remove(course);
                        entities.SaveChanges();
                    }

                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
            }

            return response;
        }
    }
}
