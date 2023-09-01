CREATE PROCEDURE [dbo].[spBook_Insert]
	@Title nvarchar,
	@Description nvarchar,
	@ISBN numeric,
	@Author nvarchar
AS
	
begin
	set nocount on;

	insert into dbo.Book(Title, [Description], ISBN, Author)
	values (@Title, @Description, @ISBN, @Author);
end
