
CREATE PROCEDURE [dbo].[CountCourseStudents]
AS
BEGIN
	select CourseId AS Id, COUNT(1) AS 'StudentsCount'
	from student
	group by CourseId
END