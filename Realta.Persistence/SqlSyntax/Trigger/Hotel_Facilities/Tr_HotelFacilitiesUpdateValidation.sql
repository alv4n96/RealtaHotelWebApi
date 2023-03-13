-- DROP TRIGGER Hotel.Facilities_Update_validation

CREATE OR ALTER TRIGGER Hotel.Facilities_update_validation
ON Hotel.Facilities
AFTER UPDATE 
AS
BEGIN
    DECLARE @faci_id INT;
    DECLARE @faci_user_id INT;
    DECLARE @faci_hotel_id INT;
	DECLARE @faci_startdate DATETIME;
	DECLARE @faci_enddate DATETIME;
	DECLARE @faci_discount SMALLMONEY;
	DECLARE @faci_tax_rate SMALLMONEY;
    DECLARE @faci_low_price MONEY;
    DECLARE @faci_high_price MONEY;
    DECLARE @faci_rate_price MONEY;

    SELECT 
        @faci_id = faci_id,
        @faci_user_id = faci_user_id,
        @faci_hotel_id = faci_hotel_id,
        @faci_startdate = faci_startdate,
        @faci_enddate = faci_enddate,
        @faci_discount = faci_discount,
        @faci_tax_rate = faci_tax_rate,
        @faci_low_price = faci_low_price,
        @faci_high_price = faci_high_price,
        @faci_rate_price = 
        (
		CASE
			WHEN faci_discount IS NULL AND faci_tax_rate IS NULL THEN (faci_high_price + faci_low_price) / 2
			WHEN faci_discount IS NULL THEN (((faci_high_price + faci_low_price) / 2) + (((faci_high_price + faci_low_price) / 2) * (faci_tax_rate/100)))
			WHEN faci_tax_rate IS NULL THEN (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * (faci_discount/100)))
			ELSE (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * faci_discount/100)) + (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * faci_discount/100)) * (faci_tax_rate/100)
		END
	)
    FROM inserted   

    IF NOT EXISTS (SELECT 1 FROM Hotel.Hotels WHERE hotel_id = @faci_hotel_id)
    BEGIN
        RAISERROR ('Hotel does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        RETURN;
    END
    
    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @faci_user_id AND usro_role_id IN (2, 4))
    BEGIN
        RAISERROR ('User does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        RETURN;
    END
    
    IF EXISTS (SELECT faci_low_price, faci_high_price 
               FROM inserted 
               WHERE faci_high_price < faci_low_price) 
    BEGIN 
        RAISERROR ('High price cannot be lower than low price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 

    IF (@faci_rate_price > @faci_high_price OR @faci_rate_price < @faci_low_price) 
    BEGIN 
        RAISERROR ('Rate price cannot be lower than low price OR cannot be higher than high price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 
    

    IF UPDATE(faci_startdate)
		OR UPDATE(faci_enddate)
		OR UPDATE(faci_low_price)
		OR UPDATE(faci_high_price) 
		OR UPDATE(faci_rate_price) 
		OR UPDATE(faci_discount) 
		OR UPDATE(faci_tax_rate)
    BEGIN
        BEGIN TRANSACTION
			UPDATE Hotel.Facilities 
			SET 
				faci_rate_price = @faci_rate_price
			WHERE 
				faci_id = @faci_id

			INSERT INTO Hotel.Facility_Price_History (faph_startdate, faph_enddate, faph_low_price, faph_high_price, faph_rate_price, faph_discount, faph_tax_rate, faph_modified_date, faph_faci_id, faph_user_id)
			VALUES (@faci_startdate, @faci_enddate, @faci_low_price, @faci_high_price, @faci_rate_price, @faci_discount, @faci_tax_rate, GETDATE(), @faci_id, @faci_user_id);
        COMMIT TRANSACTION
    END
END
