CREATE PROCEDURE [dbo].[StudentsAverageGrades]
AS
BEGIN
	select StudentId, AVG(grade) as 'Average'
	from [dbo].[StudentGrade]
	group by StudentId
END