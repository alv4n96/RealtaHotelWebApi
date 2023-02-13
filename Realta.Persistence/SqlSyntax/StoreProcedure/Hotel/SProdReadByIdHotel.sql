-- DROP PROCEDURE IF EXISTS [Hotel].[spSelectHotelById];

CREATE PROCEDURE [Hotel].[spSelectHotelById]
	@Id int
	
AS
BEGIN 
	SET NOCOUNT	ON;

	IF EXISTS (SELECT * FROM Hotel.Hotels WHERE hotel_id = @Id)
    BEGIN
        SELECT	hotel_id
			,hotel_name
			,hotel_description
			,hotel_rating_star
			,hotel_phonenumber
			,hotel_modified_date
			,hotel_addr_id
		FROM [Hotel].[Hotels]
		WHERE hotel_id = @Id;
    END
    ELSE
    BEGIN
        RAISERROR('Hotel with ID %d could not be found', 16, 1, @Id)
    END
END;
GO;

EXEC [Hotel].[spSelectHotelById] @id = 100;
