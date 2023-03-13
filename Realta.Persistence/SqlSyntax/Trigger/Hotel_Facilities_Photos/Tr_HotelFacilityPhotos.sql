--DROP TRIGGER Hotel.tr_hotel_facilities_price_history

CREATE OR ALTER TRIGGER Hotel.tr_facility_photos_fapho_primary
ON Hotel.Facility_Photos
AFTER INSERT 
AS
BEGIN
  SET NOCOUNT ON;

  BEGIN TRY
    -- Start transaction
    BEGIN TRANSACTION
    DECLARE @fapho_faci_id INT
    DECLARE @fapho_id INT
    DECLARE @fapho_primary INT

    SELECT 
        @fapho_id = fapho_id,
        @fapho_faci_id = fapho_faci_id,
        @fapho_primary = fapho_primary
    FROM inserted

    -- If any row is updated, check if the value of fapho_primary is changed to 1
    IF NOT EXISTS (
    SELECT * FROM Hotel.Facility_Photos WHERE fapho_primary = 1 AND fapho_faci_id = @fapho_faci_id) AND (@fapho_primary = 0)
    BEGIN
      -- Only allow one record with fapho_primary = 1 for each faci_id
      UPDATE Hotel.Facility_Photos
      SET 
        fapho_primary = 1,
        fapho_original_filename = 'ini dari insert bukan ya'
      WHERE fapho_id = @fapho_id
    END

    -- Commit transaction
    COMMIT TRANSACTION

  END TRY

  BEGIN CATCH
    -- Rollback transaction in case of any errors
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
  END CATCH;
END;

