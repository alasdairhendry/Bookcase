CREATE PROCEDURE [dbo].[spBook_GetByIsbn]
	@ISBN int
AS
begin
	set nocount on;

	select [Id], [Title], [Description], [ISBN], [Author] from dbo.Book
	where ISBN = @ISBN;
end