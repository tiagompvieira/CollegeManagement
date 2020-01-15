
CREATE PROCEDURE [dbo].[CountCourseTeachers]
AS
BEGIN
	-- each subject has one teacher!
	select CourseId, count(1) AS 'TeachersCount'
	from CourseSubject
	group by CourseId
END