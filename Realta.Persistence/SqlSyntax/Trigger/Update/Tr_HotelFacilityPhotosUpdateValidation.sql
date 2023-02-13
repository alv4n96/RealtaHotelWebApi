-- DROP TRIGGER Hotel.Hotel_Facility_Photos_update_validation

CREATE TRIGGER Hotel.Hotel_Facility_Photos_update_validation
ON Hotel.Facility_Photos
AFTER UPDATE
AS
BEGIN
    DECLARE @fapho_faci_id INT

    SELECT @fapho_faci_id = fapho_faci_id
    FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Hotel.Facilities WHERE faci_id = @fapho_faci_id)
    BEGIN
        RAISERROR ('Facility does not exist!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END