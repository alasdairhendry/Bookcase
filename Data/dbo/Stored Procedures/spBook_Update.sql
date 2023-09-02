CREATE PROCEDURE [dbo].[spBook_Update]
	@Id int,
	@Title nvarchar(128),
	@ISBN numeric(13,0),
	@Author nvarchar(128),
	@Description nvarchar(MAX)
AS
	
begin
	set nocount on;

	update [dbo].[Book]
	set Title = @Title, ISBN = @ISBN, Author = @Author, [Description] = @Description
	where Id = @Id;

end
