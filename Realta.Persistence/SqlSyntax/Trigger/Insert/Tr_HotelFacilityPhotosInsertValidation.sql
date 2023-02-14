-- DROP TRIGGER Hotel.Hotel_Facility_Photos_insert_validation

CREATE TRIGGER Hotel.Hotel_Facility_Photos_insert_validation
ON Hotel.Facility_Photos
AFTER INSERT
AS
BEGIN
    DECLARE @fapho_faci_id INT
    DECLARE @resetIndex INT

    SELECT @fapho_faci_id = fapho_faci_id
    FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Hotel.Facilities WHERE faci_id = @fapho_faci_id)
    BEGIN
        SELECT @resetIndex = (IDENT_CURRENT('Hotel.Facility_Photos') - 1);
        RAISERROR ('Facility does not exist!', 16, 1);
        ROLLBACK TRANSACTION;
        DBCC CHECKIDENT ('Hotel.Facility_Photos', RESEED, @resetIndex );
    END
END