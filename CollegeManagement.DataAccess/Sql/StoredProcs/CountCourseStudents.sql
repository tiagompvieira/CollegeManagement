
CREATE PROCEDURE [dbo].[CountCourseStudents]
AS
BEGIN
	select Course.Id, COUNT(1) AS 'StudentsCount'
	from Course
	inner join CourseSubject courseSubject
		on course.Id = courseSubject.CourseId
	inner join Subject
		on Subject.Id = courseSubject.SubjectId
	inner join [StudentSubject] studentSubjects
		on studentSubjects.StudentId = Subject.Id
	inner join Student
		on student.RegistrationNumber = studentSubjects.StudentId
	group by Course.Id
END