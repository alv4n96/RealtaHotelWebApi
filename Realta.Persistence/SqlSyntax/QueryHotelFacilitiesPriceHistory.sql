SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Facility_Price_History';

-- ALTER TABLE Hotel.Facilities ALTER COLUMN faci_rate_price MONEY NULL;


USE WEBApiDbDemo

SELECT faci_measure_unit FROM Hotel.Facilities;

SELECT * from hotel.Facility_Price_History;
SELECT * from hotel.Facility_Price_History WHERE faph_faci_id = @faph_faci_id;
SELECT * from hotel.Facility_Price_History WHERE faph_faci_id = @faph_faci_id AND faph_id = @faph_id;


SELECT faph.*
FROM Hotel.Facility_Price_History faph
INNER JOIN Hotel.Facilities faci ON faph.faph_faci_id = faci.faci_id
WHERE faci.faci_hotel_id = @faci_hotel_id;

SELECT faph.*
FROM Hotel.Facility_Price_History faph
INNER JOIN Hotel.Facilities faci ON faph.faph_faci_id = faci.faci_id
WHERE faci.faci_hotel_id = @faci_hotel_id AND faph.faph_faci_id = @faph_faci_id;

SELECT * FROM Hotel.Facility_Price_History WHERE faph_id = @faph_id AND faph_faci_id = @faph_faci_id;


-- GROUP BY faph.faph_id, faph.faph_startdate, faph.faph_enddate, faph.faph_low_price, faph.faph_high_price, faph.faph_rate_price, faph.faph_discount, faph.faph_tax_rate, faph.faph_modified_date, faph.faph_faci_id, faph.faph_user_id;

SELECT * FROM [Hotel].Facilities WHERE faci_id = 30;

DBCC CHECKIDENT ('Hotel.Facility_Price_History', RESEED,0 );

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
