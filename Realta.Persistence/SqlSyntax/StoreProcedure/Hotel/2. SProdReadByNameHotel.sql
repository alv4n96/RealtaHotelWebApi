-- DROP PROCEDURE IF EXISTS [Hotel].[spSelectHotelById];

CREATE OR ALTER PROCEDURE Hotel.spSelectHotelByName
	@hotelName NVARCHAR(85)
AS
BEGIN 
	SET NOCOUNT	ON;

	IF EXISTS (SELECT * FROM Hotel.Hotels WHERE hotel_name LIKE '%' + @hotelName + '%')
    BEGIN
        SELECT 
		    hotel_id AS HotelId, 
		    hotel_name AS HotelName, 
		    hotel_description AS HotelDescription, 
		    hotel_status AS HotelStatus, 
		    hotel_reason_status AS HotelReasonStatus, 
		    hotel_rating_star AS HotelRatingStar, 
		    hotel_phonenumber AS HotelPhonenumber, 
		    hotel_modified_date AS HotelModifiedDate, 
		    hotel_addr_id AS HotelAddrId 
		FROM 
		    [Hotel].[Hotels]
		WHERE 
		    hotel_name LIKE '%' + @hotelName + '%'
    END
    -- ELSE
    -- BEGIN
    --     RAISERROR('Hotel with ID %d could not be found', 16, 1, @hotelId)
    -- END
END;
GO

EXEC Hotel.spSelectHotelById @hotelId = 9;
