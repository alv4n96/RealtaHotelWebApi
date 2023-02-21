SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Facility_Photos';

-- ALTER TABLE Hotel.Facilities ALTER COLUMN faci_rate_price MONEY NULL;


USE WEBApiDbDemo

SELECT faci_measure_unit FROM Hotel.Facilities;

SELECT * from hotel.Facility_Photos WHERE fapho_faci_id = @fapho_faci_id AND fapho_id = @fapho_id;

SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id;

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
