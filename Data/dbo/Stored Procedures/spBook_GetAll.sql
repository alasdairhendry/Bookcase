CREATE PROCEDURE [dbo].[spBook_GetAll]

AS
begin
	set nocount on;

	select [Id], [Title], [ISBN], [Author], [Description] from dbo.Book;
end
