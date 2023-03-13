-- DROP TRIGGER Hotel.Hotel_Facility_Photos_update_validation

CREATE OR ALTER TRIGGER Hotel.Hotel_Facility_Photos_update_primary_validation
ON Hotel.Facility_Photos
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @fapho_faci_id INT
    DECLARE @fapho_primary INT

    SELECT 
        @fapho_faci_id = fapho_faci_id,
        @fapho_primary =  fapho_primary
    FROM inserted

    IF (@fapho_primary = 1)
        BEGIN
			-- update other records with the same fapho_faci_id to have fapho_primary = 0
			UPDATE p
			SET fapho_primary = 0
			FROM Hotel.Facility_Photos p
			JOIN inserted i ON p.fapho_faci_id = i.fapho_faci_id
			WHERE p.fapho_id <> i.fapho_id
			AND (i.fapho_primary = 1 OR (i.fapho_primary IS NULL AND p.fapho_primary = 1));

			-- set inserted records with fapho_primary = 1
			UPDATE p
			SET fapho_primary = 1
			FROM Hotel.Facility_Photos p
			JOIN inserted i ON p.fapho_id = i.fapho_id
			WHERE i.fapho_primary = 1
			AND (p.fapho_primary IS NULL OR p.fapho_primary = 0);
        END

    IF NOT EXISTS (SELECT * FROM inserted WHERE fapho_primary = 1 AND fapho_faci_id = @fapho_faci_id) 
        BEGIN
            UPDATE Hotel.Facility_Photos
            SET fapho_primary = 1
            WHERE fapho_id = (
            SELECT MIN(fapho_id) FROM hotel.Facility_Photos
            WHERE fapho_faci_id = @fapho_faci_id 
            )
        END
END

