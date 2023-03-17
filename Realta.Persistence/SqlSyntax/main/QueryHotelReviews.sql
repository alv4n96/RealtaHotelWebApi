SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotel_Reviews';

USE WEBApiDbDemo

UPDATE hotel.Hotel_Reviews
SET hore_rating = 3
WHERE hore_id = 6

-- 2023-03-13 03:01:54.983
-- 2023-03-13 03:01:54.983 3.8

SELECT * FROM Hotel.Hotel_Reviews;
SELECT * FROM Hotel.Hotels WHERE hotel_id=1;
SELECT * FROM Hotel.Hotel_Reviews

INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES ('Great hotel, loved the amenities and friendly staff!', 5, GETDATE(), 5, 1);


SELECT 
hore_id AS HoreId 
,hore_user_review AS HoreUserReview 
,hore_rating AS HoreRating 
,hore_created_on AS HoreCreatedOn 
,hore_user_id AS HoreUserId 
,hore_hotel_id AS HoreHotelId 
FROM Hotel.Hotel_Reviews 
WHERE hore_hotel_id = @hore_hotel_id;

SELECT 
hore_id AS HoreId 
,hore_user_review AS HoreUserReview 
,hore_rating AS HoreRating 
,hore_created_on AS HoreCreatedOn 
,hore_user_id AS HoreUserId 
,hore_hotel_id AS HoreHotelId 
FROM Hotel.Hotel_Reviews 
WHERE hore_hotel_id = 2;

SELECT * FROM Hotel.Hotel_Reviews WHERE hore_hotel_id = 2 AND hore_id = 1;

SELECT * FROM Hotel.Hotel_Reviews WHERE hore_hotel_id = 2

DELETE FROM Hotel.Hotel_Reviews
WHERE hore_id = 7;

-- DBCC CHECKIDENT ('Hotel.Hotel_Reviews', RESEED, 5);

INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES (@hore_user_review, @hore_rating, GETDATE(), @hore_user_id, @hore_hotel_id);

SELECT * FROM Hotel.Hotel_Reviews;

UPDATE Hotel.Hotel_Reviews
SET hore_user_review = @hore_user_review, 
    hore_rating = @hore_rating,
    hore_created_on = GETDATE(),
    hore_user_id = @hore_user_id,
    hore_hotel_id = @hore_hotel_id
WHERE hore_id = @hore_id



DBCC CHECKIDENT ('Hotel.Hotels', RESEED,10 );

SELECT * FROM Hotel.Hotels ORDER BY hotel_id DESC;

SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotel_Reviews';
