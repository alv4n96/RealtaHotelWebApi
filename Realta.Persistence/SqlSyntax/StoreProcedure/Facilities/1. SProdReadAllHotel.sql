CREATE OR ALTER PROCEDURE [Hotel].[spSelectHotel]
AS
BEGIN 
	SET NOCOUNT	ON;

	SELECT 
		hotel_id AS HotelId
		,hotel_name AS HotelName
		,hotel_description AS HotelDescription
		,hotel_status AS HotelStatus
		,hotel_reason_status AS HotelReasonStatus
		,hotel_rating_star AS HotelRatingStar
		,hotel_phonenumber AS HotelPhonenumber
		,hotel_modified_date AS HotelModifiedDate
		,hotel_addr_id AS HotelAddrId
		,hotel_addr_description AS HotelAddrDescription
	FROM Hotel.Hotels;
	END;
GO


EXEC [Hotel].[spSelectHotel];

