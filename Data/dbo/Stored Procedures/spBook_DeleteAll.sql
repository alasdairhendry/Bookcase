CREATE PROCEDURE [dbo].[spBook_DeleteAll]
AS
	
begin
	set nocount on;

	delete from [dbo].[Book];	

end
