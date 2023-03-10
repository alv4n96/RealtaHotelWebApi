SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotels';

USE WEBApiDbDemo

SELECT * FROM Hotel.Hotels;

UPDATE Hotel.Hotels
SET hotel_name = 'Hotel Nyoba Palembang'
WHERE hotel_id = 1;

-- 2023-03-13 03:00:06.273
-- 2023-03-13 03:00:06.273
-- 2023-03-13 03:01:54.983
-- 2023-03-13 03:01:54.983ZZ

UPDATE Hotel.Hotels
SET hotel_rating_star = 5
WHERE hotel_id = 1;

SELECT * FROM [Hotel].[Hotels] WHERE hotel_name LIKE '%Hotel%';

SELECT * FROM [Hotel].[Hotels] WHERE hotel_name LIKE '%am%';



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
