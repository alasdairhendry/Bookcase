CREATE PROCEDURE [dbo].[spBook_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [Title], [ISBN], [Author], [Description] from dbo.Book
	where Id = @Id;
end