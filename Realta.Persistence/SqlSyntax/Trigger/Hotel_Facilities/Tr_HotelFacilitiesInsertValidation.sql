CREATE OR ALTER TRIGGER Hotel.Facilities_insert_validation
ON Hotel.Facilities
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @faci_user_id INT
    DECLARE @faci_hotel_id INT
    DECLARE @faci_rate_price MONEY
    DECLARE @faci_low_price MONEY
    DECLARE @faci_high_price MONEY
    DECLARE @faci_startdate DATETIME
    DECLARE @faci_enddate DATETIME
    DECLARE @faci_measure_unit VARCHAR(15)

    SELECT 
        @faci_user_id = faci_user_id,
        @faci_hotel_id = faci_hotel_id,
        @faci_low_price = faci_low_price,
        @faci_high_price = faci_high_price,
        @faci_startdate = faci_startdate,
        @faci_enddate = faci_enddate,
        @faci_measure_unit = (
        CASE
            WHEN faci_cagro_id = 1 THEN 'beds'
            ELSE 'people'
        END
        ),
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
    
    IF (@faci_enddate < @faci_startdate) 
    BEGIN 
        RAISERROR ('End date cannot be earlier than start date', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 

    IF (@faci_rate_price > @faci_high_price OR @faci_rate_price < @faci_low_price) 
    BEGIN 
        RAISERROR ('Rate price cannot be lower than low price OR cannot be higher than high price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 


    BEGIN TRY
        INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, 
                    faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_discount, faci_tax_rate, faci_modified_date, 
                    faci_cagro_id, faci_hotel_id, faci_user_id, faci_expose_price) 
        SELECT 
            i.faci_name, 
            i.faci_description, 
            i.faci_max_number,
            @faci_measure_unit,
            i.faci_room_number,
            i.faci_startdate,
            i.faci_enddate,
            i.faci_low_price, 
            i.faci_high_price, 
            @faci_rate_price,
            i.faci_discount,
            i.faci_tax_rate,
            GETDATE(),
            i.faci_cagro_id,
            i.faci_hotel_id,
            i.faci_user_id,
            i.faci_expose_price
        FROM inserted i
    END TRY

    
BEGIN CATCH
        ROLLBACK TRANSACTION
    -- Handle the exception here, for example by logging the error
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;
		
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
			
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
END;
GO
