CREATE PROCEDURE [dbo].[spBook_GetByIsbn]
	@ISBN int
AS
begin
	set nocount on;

	select [Id], [Title], [ISBN], [Author], [Description] from dbo.Book
	where ISBN = @ISBN;
end