CREATE PROCEDURE [dbo].[spBook_GetAll]

AS
begin
	set nocount on;

	select [Id], [Title], [Description], [ISBN], [Author] from dbo.Book;
end
