CREATE PROCEDURE [dbo].[AverageCourseGrade]
AS
BEGIN
	select CourseId, AVG(grade) AS 'Average'
	from Student
	inner join StudentGrade sgrade
	on sgrade.StudentId = student.RegistrationNumber
	group by CourseId
END
