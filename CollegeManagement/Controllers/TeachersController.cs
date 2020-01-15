using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CollegeManagement.Controllers
{
    public class TeachersController : ApiController
    {
        [HttpGet]
        [Route("api/Teachers/GetTeachers")]
        public TeachersSummaryRequest GetTeachers()
        {
            TeachersSummaryRequest response = new TeachersSummaryRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    response.Data = entities.Teachers
                        .ToList()
                        .OrderBy(subject => subject.Name)
                        .Select(teacher => new TeacherSummary()
                        {
                            Id = teacher.Id,
                            Name = teacher.Name,
                        })
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
        [Route("api/Teachers/GetTeachersSummary")]
        public TeachersSummaryRequest GetTeachersSummary()
        {
            TeachersSummaryRequest response = new TeachersSummaryRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    response.Data = entities.Teachers
                        .ToList()
                        .OrderBy(subject => subject.Name)
                        .Select(teacher => new TeacherSummary()
                        {
                            Id = teacher.Id,
                            DateOfBirth = teacher.DateOfBirth,
                            Name = teacher.Name,
                            Salary = teacher.Salary
                        })
                        .ToList();
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
        [Route("api/Teachers/GetTeacher/{id}")]
        public TeacherRequest GetCoursesSummary(int id)
        {
            TeacherRequest response = new TeacherRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var teacher = entities.Teachers.Find(id);

                    if (teacher != null)
                    {
                        response.Data = new TeacherSummary()
                        {
                            Id = teacher.Id,
                            DateOfBirth = teacher.DateOfBirth,
                            Name = teacher.Name,
                            Salary = teacher.Salary
                        };

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
        [Route("api/Teachers/SaveTeacher")]
        public ApiResponse SaveCourse(TeacherSummary teacherData)
        {
            bool isNew = false;
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    CollegeManagement.DataAccess.Teacher bdTeacher = null;

                    if (teacherData.Id.HasValue)
                    {
                        bdTeacher = entities.Teachers.Find(teacherData.Id);
                    }

                    if (bdTeacher == null)
                    {
                        isNew = true;
                        bdTeacher = new DataAccess.Teacher();
                    }
                    else
                    {
                        bdTeacher.Id = teacherData.Id.Value;
                    }

                    bdTeacher.Name = teacherData.Name;
                    bdTeacher.DateOfBirth = teacherData.DateOfBirth;
                    bdTeacher.Salary = teacherData.Salary.Value;

                    if (isNew)
                    {
                        entities.Teachers.Add(bdTeacher);
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
        [Route("api/Teachers/DeleteTeacher/{id}")]
        public ApiResponse DeleteCourse(int id)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var teacher = entities.Teachers.Find(id);

                    teacher.Subjects.Clear();

                    if (teacher != null)
                    {
                        entities.Teachers.Remove(teacher);
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
