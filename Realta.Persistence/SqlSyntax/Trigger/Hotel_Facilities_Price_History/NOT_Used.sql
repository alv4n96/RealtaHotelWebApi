--DROP TRIGGER Hotel.tr_hotel_facilities_price_history

-- THIS IS NOT USED!

/*
CREATE OR ALTER TRIGGER Hotel.tr_hotel_facilities_price_history
ON Hotel.Facilities
AFTER INSERT 
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @faph_startdate DATETIME
  DECLARE @faph_enddate DATETIME  
  DECLARE @faph_modified_date DATETIME
  DECLARE @faph_low_price MONEY;
  DECLARE @faph_high_price MONEY;
  DECLARE @faph_rate_price MONEY;
  DECLARE @faph_discount SMALLMONEY;
  DECLARE @faph_tax_rate SMALLMONEY;
  DECLARE @faph_faci_id INT;
  DECLARE @faph_user_id INT;

  SELECT @faph_startdate = faci_startdate, @faph_enddate = faci_enddate, @faph_low_price = faci_low_price, 
  @faph_high_price = faci_high_price, @faph_rate_price = faci_rate_price, @faph_discount = faci_discount, 
  @faph_modified_date = faci_modified_date, @faph_tax_rate = faci_tax_rate, @faph_faci_id = faci_id, @faph_user_id = faci_user_id 
  FROM inserted;

  INSERT INTO Hotel.Facility_Price_History (faph_startdate, faph_enddate, faph_low_price, faph_high_price, faph_rate_price, faph_discount, faph_tax_rate, faph_modified_date, faph_faci_id, faph_user_id)
  VALUES (@faph_startdate, @faph_enddate, @faph_low_price, @faph_high_price, @faph_rate_price, @faph_discount, @faph_tax_rate, @faph_modified_date, @faph_faci_id, @faph_user_id);
END;
*/
