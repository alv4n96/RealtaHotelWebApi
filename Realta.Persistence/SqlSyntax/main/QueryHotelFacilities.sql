SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Facilities';

-- ALTER TABLE Hotel.Facilities ALTER COLUMN faci_rate_price MONEY NULL;


USE WEBApiDbDemo

SELECT faci_endate FROM Hotel.Facilities;

SELECT * FROM Hotel.Facility_Price_History where faph_faci_id = 43; SELECT * FROM Hotel.Facilities WHERE faci_id = 43; 

SELECT faph.*
FROM Hotel.Facility_Price_History faph
INNER JOIN Hotel.Facilities faci ON faph.faph_faci_id = faci.faci_id
WHERE faci.faci_hotel_id = 4;

UPDATE Hotel.Facilities
SET faci_name = 'Swimswum Pool'
--SET faci_discount = 20
WHERE faci_id = 43;

INSERT INTO Hotel.Facilities
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, 
faci_startdate, faci_endate, faci_low_price, faci_high_price, 
faci_discount, faci_tax_rate, 
faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Swimming Pool', 'An outdoor swimming pool', 50, 'people', 'COBAC1',
'2022-01-01', '2022-12-31', 100000, 200000, 10, 10,
1, 4, 4);


SELECT 1 FROM Users.user_roles WHERE usro_user_id = 4 AND usro_role_id IN (2, 4)


SELECT 
faci_id AS FaciId
,faci_name AS FaciName
,faci_description AS FaciDescription
,faci_max_number AS FaciMaxNumber
,faci_measure_unit AS FaciMeasureUnit
,faci_room_number AS FaciRoomNumber
,faci_startdate AS FaciStartdate
,faci_endate AS FaciEndate
,faci_low_price AS FaciLowPrice
,faci_high_price AS FaciHighPrice
,faci_rate_price AS FaciRatePrice
,faci_discount AS FaciDiscount
,faci_tax_rate AS FaciTaxRate
,faci_modified_date AS FaciModifiedDate
,faci_cagro_id AS FaciCagroId
,faci_hotel_id AS FaciHotelId
,faci_user_id AS FaciUserId
from hotel.Facilities

SELECT 
faci_id AS FaciId
,faci_name AS FaciName
,faci_description AS FaciDescription
,faci_max_number AS FaciMaxNumber
,faci_measure_unit AS FaciMeasureUnit
,faci_room_number AS FaciRoomNumber
,faci_startdate AS FaciStartdate
,faci_endate AS FaciEndate
,faci_low_price AS FaciLowPrice
,faci_high_price AS FaciHighPrice
,faci_rate_price AS FaciRatePrice
,faci_discount AS FaciDiscount
,faci_tax_rate AS FaciTaxRate
,faci_modified_date AS FaciModifiedDate
,faci_cagro_id AS FaciCagroId
,faci_hotel_id AS FaciHotelId
,faci_user_id AS FaciUserId
FROM Hotel.Facilities WHERE faci_hotel_id = 1 AND faci_id = 2;

SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id AND faci_id = @faci_id;

SELECT * FROM [Hotel].Facilities WHERE faci_id = 30;

DBCC CHECKIDENT ('Hotel.Facilities', RESEED,29 );

DELETE FROM Hotel.Facilities WHERE faci_id = 30;

SELECT * FROM Hotel.Facility_Photos;


INSERT INTO Hotel.Facilities 
        (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES (@faci_name, @faci_description, @faci_max_number, faci_measure_unit, @faci_room_number, @faci_startdate, @faci_endate, @faci_low_price, @faci_high_price, @faci_discount, @faci_tax_rate, @faci_cagro_id, @faci_hotel_id, @faci_user_id);



SELECT * FROM Hotel.Hotels ORDER BY hotel_id DESC;

UPDATE Hotel.Facilities
SET 
  faci_name = @faci_name,
  faci_description = @faci_description,
  faci_max_number = @faci_max_number,
  faci_measure_unit = @faci_measure_unit,
  faci_room_number = @faci_room_number,
  faci_startdate = @faci_startdate,
  faci_endate = @faci_endate,
  faci_low_price = @faci_low_price,
  faci_high_price = @faci_high_price,
  faci_discount = @faci_discount,
  faci_tax_rate = @faci_tax_rate,
  faci_cagro_id = @faci_cagro_id,
  faci_hotel_id = @faci_hotel_id,
  faci_user_id = @faci_user_id
WHERE faci_id = @faci_id;



UPDATE Hotel.Hotels
SET hotel_status = @hotel_status,
    hotel_reason_status = @hotel_reason_status
WHERE hotel_id = @hotel_id;
SELECT * FROM Hotel.Hotels WHERE hotel_id = 10;

SELECT * FROM Hotel.Hotel_Reviews;

SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotel_Reviews';
