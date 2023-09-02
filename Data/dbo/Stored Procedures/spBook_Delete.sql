CREATE PROCEDURE [dbo].[spBook_Delete]
	@Id int
AS
	
begin
	set nocount on;

	delete from [dbo].[Book]
	where Id = @Id;

end
