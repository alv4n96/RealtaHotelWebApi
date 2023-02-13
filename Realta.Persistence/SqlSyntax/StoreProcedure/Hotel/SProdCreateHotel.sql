CREATE PROCEDURE [Hotel].[spInsertHotel]
(
  @hotel_name nvarchar(85),
  @hotel_description nvarchar(500) = NULL,
  @hotel_rating_star smallint = NULL,
  @hotel_phonenumber nvarchar(25),
  @hotel_addr_id INT
)
AS
BEGIN
  BEGIN TRY
    BEGIN TRANSACTION

    INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_rating_star, hotel_phonenumber, hotel_modified_date, hotel_addr_id)
    VALUES (@hotel_name, @hotel_description, @hotel_rating_star, @hotel_phonenumber, GETDATE(), @hotel_addr_id)

    COMMIT TRANSACTION
  END TRY
  BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
      ROLLBACK TRANSACTION
    END
    DECLARE @errorMessage NVARCHAR(4000);
    DECLARE @errorSeverity INT;
    DECLARE @errorState INT;

    SELECT 
      @errorMessage = ERROR_MESSAGE(),
      @errorSeverity = ERROR_SEVERITY(),
      @errorState = ERROR_STATE();

    RAISERROR (@errorMessage, @errorSeverity, @errorState);
  END CATCH
END;


EXECUTE Hotel.spInsertHotel
@hotel_name = 'Hotel Alvan',
@hotel_description = 'Hotel terbaru dan bintang 5',
@hotel_rating_star = 4,
@hotel_phonenumber = '+62 812 3456 7890',
@hotel_addr_id = 1;

