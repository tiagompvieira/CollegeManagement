using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static CollegeManagement.Models.SubjectsModels;

namespace CollegeManagement.Controllers
{
    public class SubjectsController : ApiController
    {
        [HttpGet]
        [Route("api/Subjects/GetSubjectsSummary")]
        public SubjectsSummaryRequest GetSubjectsSummary()
        {
            SubjectsSummaryRequest response = new SubjectsSummaryRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    response.Data = entities.Subjects
                        .ToList()
                        .Select(subject => new SubjectSummary()
                        {
                            Id = subject.Id,
                            Name = subject.Name,
                            Teacher = subject?.Teacher?.Name,
                            TeacherId = subject.TeacherId
                        })
                        .OrderBy(subject => subject.Name)
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
        [Route("api/Subjects/GetStudentSubjects/{courseId}/{studentId}")]
        public SubjectsSummaryRequest GetSubjects(int? courseId, int? studentId)
        {
            SubjectsSummaryRequest response = new SubjectsSummaryRequest();

            if (!studentId.HasValue)
            {
                response.Success = true;
                return response;
            }

            try
            {
                List<SubjectSummary> studentSubjects = null;

                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    // Obtain course associated subjects
                    response.Data = entities.Courses
                        .Where(course => course.Id == courseId.Value)
                        .First()
                        .Subjects
                        .ToList()
                        .Select(subject => new SubjectSummary()
                        {
                            Id = subject.Id,
                            Name = subject.Name
                        })
                        .ToList();

                    // Obtain student subjects
                    if (studentId.HasValue)
                    {
                        studentSubjects = entities.Subjects
                            .Where(subject => subject.Students.Any(student => student.RegistrationNumber == studentId.Value))
                            .ToList()
                            .Select(subject => new SubjectSummary()
                            {
                                Id = subject.Id,
                                Name = subject.Name
                            })
                            .OrderBy(subject => subject.Name)
                            .ToList();
                    }

                    // mark student subjects
                    if (studentSubjects != null)
                    {
                        foreach (var item in response.Data)
                        {
                            var studentSubject = studentSubjects
                                .Where(subject => subject.Id == item.Id)
                                .FirstOrDefault();

                            if (studentSubject == null)
                            {
                                continue;
                            }

                            item.Selected = true;
                        }
                    }
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
        [Route("api/Subjects/GetSubject/{id}")]
        public SubjectRequest GetSubjectsSummary(int id)
        {
            SubjectRequest response = new SubjectRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var subject = entities.Subjects.Find(id);

                    if (subject != null)
                    {
                        response.Data = new SubjectSummary()
                        {
                            Id = subject.Id,
                            Name = subject.Name,
                            Teacher = subject?.Teacher?.Name,
                            TeacherId = subject.TeacherId
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
        [Route("api/Subjects/SaveSubject")]
        public ApiResponse SaveCourse(SubjectSummary subjectData)
        {
            bool isNew = false;
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    CollegeManagement.DataAccess.Subject bdSubject = null;

                    if (subjectData.Id.HasValue)
                    {
                        bdSubject = entities.Subjects.Find(subjectData.Id);
                    }

                    if (bdSubject == null)
                    {
                        isNew = true;
                        bdSubject = new DataAccess.Subject();
                    }
                    else
                    {
                        bdSubject.Id = subjectData.Id.Value;
                    }

                    bdSubject.Name = subjectData.Name;
                    bdSubject.TeacherId = subjectData.TeacherId;

                    if (isNew)
                    {
                        entities.Subjects.Add(bdSubject);
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
        [Route("api/Subjects/DeleteSubject/{id}")]
        public ApiResponse DeleteCourse(int id)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var subject = entities.Subjects.Find(id);

                    subject.StudentGrades.Clear();
                    subject.Students.Clear();
                    subject.Courses.Clear();

                    if (subject != null)
                    {
                        entities.Subjects.Remove(subject);
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
