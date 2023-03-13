-- DROP TRIGGER Hotel.Hotel_Facility_Photos_insert_validation

CREATE OR ALTER TRIGGER Hotel.Hotel_Facility_Photos_insert_validation
ON Hotel.Facility_Photos
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @fapho_faci_id INT

    SELECT 
        @fapho_faci_id = fapho_faci_id
    FROM inserted

 

    IF NOT EXISTS (SELECT 1 FROM Hotel.Facilities WHERE faci_id = @fapho_faci_id)
    BEGIN
        RAISERROR ('Facility does not exist!', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        INSERT INTO Hotel.Facility_Photos (fapho_photo_filename, fapho_thumbnail_filename, fapho_original_filename, fapho_file_size, fapho_file_type, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id)
		SELECT fapho_photo_filename, fapho_thumbnail_filename, fapho_original_filename, fapho_file_size, fapho_file_type, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id
	    FROM inserted;
    END
END

