-- DROP TRIGGER Hotel.Facilities_insert_validation

CREATE TRIGGER Hotel.Facilities_insert_validation
ON Hotel.Facilities
BEFORE INSERT
AS
BEGIN
    DECLARE @faci_user_id INT
    DECLARE @resetIndex INT

    SELECT @faci_user_id = faci_user_id
    FROM inserted   
    
    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @faci_user_id AND usro_role_id IN (2, 4))
    BEGIN
        SELECT @resetIndex = (IDENT_CURRENT('Hotel.Facilities') - 1);
        RAISERROR ('User does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        DBCC CHECKIDENT ('Hotel.Facilities', RESEED, @resetIndex );
    END
    
    IF EXISTS (SELECT faci_low_price, faci_high_price 
               FROM inserted 
               WHERE faci_high_price < faci_low_price) 
    BEGIN 
        RAISERROR ('High price cannot be lower than low price', 16, 1) 
        ROLLBACK TRANSACTION
        DBCC CHECKIDENT ('Hotel.Facilities', RESEED, @resetIndex ); 
    END 
END
