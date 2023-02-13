
CREATE PROCEDURE [Hotel].[spSelectHotel]
AS
BEGIN 
	SET NOCOUNT	ON;

SELECT	hotel_id
		,hotel_name
		,hotel_description
		,hotel_rating_star
		,hotel_phonenumber
		,hotel_modified_date
		,hotel_addr_id
FROM [Hotel].[Hotels]

END;
GO;


EXEC [Hotel].[spSelectHotel];
