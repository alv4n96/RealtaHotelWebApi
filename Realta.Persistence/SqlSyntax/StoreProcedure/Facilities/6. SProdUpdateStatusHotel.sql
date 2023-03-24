CREATE OR ALTER PROCEDURE [Hotel].[spUpdateStatusHotel]
(
    @hotel_id int,
    @hotel_status BIT,
    @hotel_reason_status nvarchar(500)
)
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE Hotel.Hotels 
		SET hotel_status = @hotel_status, 
			hotel_reason_status = @hotel_reason_status 
		WHERE hotel_id = @hotel_id;
    COMMIT TRANSACTION;
END;
GO

EXEC [Hotel].[spUpdateStatusHotel] 
	@hotel_id = 16, 
	@hotel_status = 1, 
	@hotel_reason_status = 'Working on it';

SELECT * FROM Hotel.Hotels WHERE hotel_id = 16;


