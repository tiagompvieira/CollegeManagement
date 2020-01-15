using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.DataAccess
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<Entities>
    {
        protected override void Seed(Entities context)
        {
            List<Course> courses = new List<Course>()
            {
                new Course() { Name = "Engenharia Informática" },
                new Course() { Name = "Informática e Gestão" },
                new Course() { Name = "Ciência dos Dados" },
                new Course() { Name = "Programação" },
                new Course() { Name = "Biologia" },
                new Course() { Name = "Filosofia" }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher() { Name = "Joaquim Gonçalves", DateOfBirth = DateTime.Now.AddDays(38 * 400), Salary = 3600 },
                new Teacher() { Name = "José Quadrado", DateOfBirth = DateTime.Now.AddDays(48 * 400), Salary = 1600 },
                new Teacher() { Name = "Susana Ferreira", DateOfBirth = DateTime.Now.AddDays(42 * 400), Salary = 900 },
                new Teacher() { Name = "Sérgio Borges", DateOfBirth = DateTime.Now.AddDays(29 * 400), Salary = 2400 },
                new Teacher() { Name = "Ana Cunha", DateOfBirth = DateTime.Now.AddDays(47 * 400), Salary = 1570 },

                new Teacher() { Name = "Rosa Pinto", DateOfBirth = DateTime.Now.AddDays(69 * 400), Salary = 5050},
                new Teacher() { Name = "Vanda Marques", DateOfBirth = DateTime.Now.AddDays(32 * 400), Salary = 1750},
                new Teacher() { Name = "Ana Sá", DateOfBirth = DateTime.Now.AddDays(28 * 400), Salary = 1800},
                new Teacher() { Name = "Helena Alves", DateOfBirth = DateTime.Now.AddDays(43 * 400), Salary = 1600},
                new Teacher() { Name = "Nuno Marques", DateOfBirth = DateTime.Now.AddDays(42 * 400), Salary = 2100},

                new Teacher() { Name = "Emanuel Correia", DateOfBirth = DateTime.Now.AddDays(39 * 400), Salary = 2300},
                new Teacher() { Name = "Maria Ventura", DateOfBirth = DateTime.Now.AddDays(37 * 400), Salary = 3010},
                new Teacher() { Name = "Sandra Fernandes", DateOfBirth = DateTime.Now.AddDays(63 * 400), Salary = 2050},
                new Teacher() { Name = "Artur Delgado", DateOfBirth = DateTime.Now.AddDays(54 * 400), Salary = 2570},
                new Teacher() { Name = "Telma André", DateOfBirth = DateTime.Now.AddDays(52 * 400), Salary = 2600},

                new Teacher() { Name = "Hugo Gomes", DateOfBirth = DateTime.Now.AddDays(41 * 400), Salary = 1800 },
                new Teacher() { Name = "João Vargas", DateOfBirth = DateTime.Now.AddDays(46 * 400), Salary = 1900 },
                new Teacher() { Name = "José Barradas", DateOfBirth = DateTime.Now.AddDays(27 * 400), Salary = 1900 },
                new Teacher() { Name = "Paulo Cardoso", DateOfBirth = DateTime.Now.AddDays(60 * 400), Salary = 1900 },
            };

            context.Teachers.AddRange(teachers);
            context.SaveChanges();

            int teacherId = 1;
            List<Subject> subjects = new List<Subject>()
            {
                 new Subject() { Name = "Programação I", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "Análise Matemática", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "Inteligência Artificial", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "SIAD", TeacherId = teachers[teacherId++].Id },

                 new Subject() { Name = "Finanças I", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "Segurança Informática", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "Auditoria e Qualidade", TeacherId = teachers[teacherId++].Id },

                 new Subject() { Name = "Marketing", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "Padrões de Desenvolvimento", TeacherId = teachers[teacherId++].Id },
                 new Subject() { Name = "Gestão de Projetos", TeacherId = teachers[teacherId++].Id },

                new Subject() { Name = "Programação II", TeacherId = teachers[teacherId++].Id },
                new Subject() { Name = "Programação III", TeacherId = teachers[teacherId++].Id },
                new Subject() { Name = "Biologia I", TeacherId = teachers[teacherId++].Id },
                new Subject() { Name = "Biologia II", TeacherId = teachers[teacherId++].Id },
                new Subject() { Name = "Física Aplicada", TeacherId = teachers[teacherId++].Id },

                new Subject() { Name = "Pensadores Livres", TeacherId = teachers[teacherId++].Id },
                new Subject() { Name = "Psicanálise", TeacherId = teachers[teacherId++].Id },
                new Subject() { Name = "Profiling", TeacherId = teachers[teacherId++].Id }
            };

            int subjectId = 1;
            // Engenharia Informática
            subjects[subjectId++].Courses.Add(courses[0]);
            subjects[subjectId++].Courses.Add(courses[0]);
            subjects[subjectId++].Courses.Add(courses[0]);
            subjects[subjectId].Courses.Add(courses[0]);
            // Informática e Gestão
            subjects[subjectId++].Courses.Add(courses[1]);
            subjects[subjectId++].Courses.Add(courses[1]);
            subjects[subjectId].Courses.Add(courses[1]);
            // Ciência dos Dados
            subjects[subjectId++].Courses.Add(courses[2]);
            subjects[subjectId++].Courses.Add(courses[2]);
            subjects[subjectId++].Courses.Add(courses[2]);
            subjects[subjectId++].Courses.Add(courses[2]);
            // Programação
            subjects[1].Courses.Add(courses[3]);
            subjects[2].Courses.Add(courses[3]);
            subjects[subjectId++].Courses.Add(courses[3]);
            subjects[subjectId++].Courses.Add(courses[3]);
            // Biologia
            subjects[subjectId++].Courses.Add(courses[4]);
            subjects[subjectId++].Courses.Add(courses[4]);
            subjects[subjectId++].Courses.Add(courses[4]);

            subjects[subjectId++].Courses.Add(courses[5]);
            subjects[subjectId++].Courses.Add(courses[5]);
            subjects[subjectId++].Courses.Add(courses[5]);

            context.Subjects.AddRange(subjects);
            context.SaveChanges();

            Random random = new Random(DateTime.Now.Second);
            var newCourses = context.Courses.Include("Subjects").ToList();

            List<Student> students = new List<Student>() {
                new Student() { Name = "Francisco Lopes", DateOfBirth = DateTime.Now.AddDays(18 * 380), CourseId = 1,
                    Subjects = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Teresa Isabel", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 1,
                    Subjects = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Tiago Maia", DateOfBirth = DateTime.Now.AddDays(20 * 380), CourseId = 1,
                    Subjects = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Vânia Guerra", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 1,
                    Subjects = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "João Sales", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 1,
                    Subjects = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects ,
                    StudentGrades = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Maria Correia", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 1,
                    Subjects = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects ,
                    StudentGrades = newCourses.Where(c => c.Id == 1).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },

                new Student() { Name = "Ana Rodrigues", DateOfBirth = DateTime.Now.AddDays(18 * 380), CourseId = 2,
                    Subjects = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Laura SAntos", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 2,
                    Subjects = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Susana Garrido", DateOfBirth = DateTime.Now.AddDays(20 * 380), CourseId = 2,
                    Subjects = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Ana Rita", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 2,
                    Subjects = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Rita Maciel", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 2,
                    Subjects = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects ,
                    StudentGrades = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Nuno Cameiro", DateOfBirth = DateTime.Now.AddDays(19 * 380), CourseId = 2,
                    Subjects = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 2).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },

                new Student() { Name = "Mariana Mendes", DateOfBirth = DateTime.Now.AddDays(18 * 380), CourseId = 4,
                    Subjects = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects ,
                    StudentGrades = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Manuel Adão", DateOfBirth = DateTime.Now.AddDays(19 * 370), CourseId = 4,
                    Subjects = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects ,
                    StudentGrades = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Franciso Lopes", DateOfBirth = DateTime.Now.AddDays(19 * 360), CourseId = 4,
                    Subjects = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Capelo Gouveia", DateOfBirth = DateTime.Now.AddDays(19 * 365), CourseId = 4,
                    Subjects = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Isabel Moreira", DateOfBirth = DateTime.Now.AddDays(19 * 375), CourseId = 4,
                    Subjects = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 4).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },

                new Student() { Name = "Maria Elisabete", DateOfBirth = DateTime.Now.AddDays(18 * 380), CourseId = 5,
                    Subjects = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Fabiana Silva", DateOfBirth = DateTime.Now.AddDays(19 * 370), CourseId = 5, 
                    Subjects = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Vânia Silva", DateOfBirth = DateTime.Now.AddDays(19 * 360), CourseId = 5,
                    Subjects = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Catarina Ferreira", DateOfBirth = DateTime.Now.AddDays(19 * 365), CourseId = 5,
                    Subjects = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() },
                new Student() { Name = "Nuno Rodrigues", DateOfBirth = DateTime.Now.AddDays(19 * 375), CourseId = 5,
                    Subjects = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects,
                    StudentGrades = newCourses.Where(c => c.Id == 5).FirstOrDefault().Subjects.Select(s => new StudentGrade() { SubjectId = s.Id, Grade = random.Next(12, 20) }).ToList() }
            };

            context.Students.AddRange(students);

            // Add Stored Procedures
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\CollegeManagement.DataAccess", "Sql\\StoredProcs"), "*.sql"))
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            }
        }
    }
}