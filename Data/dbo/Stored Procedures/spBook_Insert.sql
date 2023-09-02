CREATE PROCEDURE [dbo].[spBook_Insert]
	@Title nvarchar(128),
	@ISBN numeric(13,0),
	@Author nvarchar(128),
	@Description nvarchar(MAX)
AS
	
begin
	set nocount on;

	insert into dbo.Book(Title, ISBN, Author, [Description])
	values (@Title, @ISBN, @Author, @Description);
end
