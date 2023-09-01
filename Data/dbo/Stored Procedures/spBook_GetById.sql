CREATE PROCEDURE [dbo].[spBook_GetById]
	@Id int
AS
begin
	set nocount on;

	select [Id], [Title], [Description], [ISBN], [Author] from dbo.Book
	where Id = @Id;
end