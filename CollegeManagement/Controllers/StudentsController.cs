using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using static CollegeManagement.Models.StudentsModels;
using CollegeManagement.DataAccess;

namespace CollegeManagement.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        [Route("api/Students/GetStudentsSummary")]
        public StudentsSummaryRequest GetStudentsSummary()
        {
            StudentsSummaryRequest response = new StudentsSummaryRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    response.Data = entities.Students
                        .Select(student => new StudentSummary()
                        {
                            RegistrationNumber = student.RegistrationNumber,
                            Name = student.Name
                        })
                        .OrderBy(student => student.Name)
                        .ToList();

                    var studentsAverage = entities
                        .StudentsAverageGrades()
                        .ToList();

                    foreach (var item in response.Data)
                    {
                        var average = studentsAverage
                            .Where(student => student.StudentId == item.RegistrationNumber)
                            .FirstOrDefault();

                        if(average == null)
                        {
                            continue;
                        }

                        item.GradeAverage = average.Average;
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
        [Route("api/Students/GetStudent/{id}")]
        public StudentRequest GetStudent(int id)
        {
            StudentRequest response = new StudentRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var student = entities.Students.Find(id);

                    if (student != null)
                    {
                        response.Data = new StudentSummary()
                        {
                            RegistrationNumber = student.RegistrationNumber,
                            Name = student.Name,
                            Subjects = student.Subjects.Select(subject => new SubjectsModels.SubjectSummary()
                            {
                                Id = subject.Id,
                                Name = subject.Name
                            })
                            .ToList(),
                            CourseId = student.CourseId
                        };

                        response.CourseId = response.Data.CourseId;

                        

                        //if(response.Data.Subjects.Any())
                        //{
                        //    response.Data.CourseId = response.Data.Subjects.First().CourseId;
                        //}

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

        [HttpGet]
        [Route("api/Students/GetStudentEvaluation/{id}")]
        public StudentEvaluationRequest GetStudentEvaluation(int id)
        {
            StudentEvaluationRequest response = new StudentEvaluationRequest();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var student = entities.Students.Include("StudentGrades").Where(s => s.RegistrationNumber == id).FirstOrDefault();

                    if (student != null)
                    {
                        var studentGrades = student.StudentGrades.ToList();

                        response.Data = student.Subjects
                            .Select(subject => new StudentEvaluation()
                            {
                                Name = subject.Name,
                                SubjectId = subject.Id,
                            })
                            .ToList();

                        foreach (var item in response.Data)
                        {
                            var grade = studentGrades.Where(g => g.SubjectId == item.SubjectId).FirstOrDefault();

                            if(grade == null)
                            {
                                continue;
                            }

                            item.Grade = grade.Grade;
                        }
                    }
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Error = true;
            }

            return response;
        }

        [HttpPost]
        [Route("api/Students/SaveStudent")]
        public ApiResponse SaveStudent(StudentSummary studentData)
        {
            bool isNew = false;
            ApiResponse response = new ApiResponse();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var entities = new CollegeManagement.DataAccess.Entities())
                    {
                        CollegeManagement.DataAccess.Student bdStudent = null;

                        if (studentData.RegistrationNumber.HasValue)
                        {
                            bdStudent = entities.Students.Find(studentData.RegistrationNumber);
                        }

                        if (bdStudent == null)
                        {
                            isNew = true;
                            bdStudent = new DataAccess.Student();
                        }
                        else
                        {
                            bdStudent.RegistrationNumber = studentData.RegistrationNumber.Value;

                            bdStudent.Subjects.Clear();
                            entities.SaveChanges();
                        }

                        bdStudent.Name = studentData.Name;
                        bdStudent.CourseId = studentData.CourseId;

                        SetStudentSubjects(entities, bdStudent, studentData);

                        if (isNew)
                        {
                            entities.Students.Add(bdStudent);
                        }

                        entities.SaveChanges();

                        scope.Complete();

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
        [Route("api/Students/SaveStudentEvaluation")]
        public ApiResponse SaveStudentEvaluation(StudentEvaluationRequest evaluation)
        {
            var response = new ApiResponse();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var entities = new CollegeManagement.DataAccess.Entities())
                    {
                        var student = entities.Students.Find(evaluation.Id);

                        student.StudentGrades.Clear();

                        foreach (var item in evaluation.Data)
                        {
                            student.StudentGrades.Add(new StudentGrade()
                            {
                                StudentId = evaluation.Id,
                                SubjectId = item.SubjectId.Value,
                                Grade = item.Grade.Value,
                                Year = DateTime.Now.Year
                            });
                        }

                        entities.SaveChanges();

                        scope.Complete();
                    }
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Error = true;
            }

            return response;
        }

        [HttpPost]
        [Route("api/Students/DeleteStudent/{id}")]
        public ApiResponse DeleteStudent(int id)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                using (var entities = new CollegeManagement.DataAccess.Entities())
                {
                    var student = entities.Students.Find(id);

                    student.StudentGrades.Clear();
                    student.Subjects.Clear();

                    if (student != null)
                    {
                        entities.Students.Remove(student);
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


        private void SetStudentSubjects(Entities entities, Student bdStudent, StudentSummary studentData)
        {
            if (studentData.Subjects.Any())
            {
                var studentIds = studentData.Subjects
                    .Where(s => s.Selected)
                    .Select(s => s.Id)
                    .ToArray();

                var studentSubjects = entities.Subjects.Where(subject => studentIds.Contains(subject.Id)).ToList();

                studentSubjects.ForEach(subject => bdStudent.Subjects.Add(subject));
            }
        }
    }
}
