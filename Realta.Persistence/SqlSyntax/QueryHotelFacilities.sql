SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Facilities';

USE WEBApiDbDemo

SELECT faci_measure_unit FROM Hotel.Facilities;

SELECT NULL / 100

SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id;

SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id AND faci_id = @faci_id;

SELECT * FROM [Hotel].[Hotels] WHERE hotel_name LIKE '%am%';

SELECT * FROM Hotel.Facility_Photos;

UPDATE Hotel.Facility_Photos
SET fapho_primary = 0
WHERE fapho_id = 1; 
SELECT * FROM Hotel.Facility_Photos;


DBCC CHECKIDENT ('Hotel.Hotels', RESEED,10 );

SELECT * FROM Hotel.Hotels ORDER BY hotel_id DESC;

UPDATE Hotel.Hotels  
SET hotel_name = 'TestEdit Hotel Malang',
    hotel_description = 'Hotel dengan percobaan test post di Malang',
    hotel_status = 1,
    hotel_rating_star = 5,  
    hotel_phonenumber = '+62 823 1234 5680',  
    hotel_modified_date = GETDATE(),  
    hotel_addr_id = 4  
WHERE hotel_id = 11;


UPDATE Hotel.Hotels
SET hotel_status = @hotel_status,
    hotel_reason_status = @hotel_reason_status
WHERE hotel_id = @hotel_id;
SELECT * FROM Hotel.Hotels WHERE hotel_id = 10;

SELECT * FROM Hotel.Hotel_Reviews;

SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotel_Reviews';
