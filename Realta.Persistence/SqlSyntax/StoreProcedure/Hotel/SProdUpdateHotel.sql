CREATE PROCEDURE [Hotel].[spUpdateHotel]
  @hotel_id int,
  @hotel_name nvarchar(85),
  @hotel_description nvarchar(500),
  @hotel_rating_star smallint,
  @hotel_phonenumber nvarchar(25),
  @hotel_addr_id int
AS
BEGIN
  BEGIN TRY
    BEGIN TRANSACTION
      UPDATE Hotel.Hotels 
      SET hotel_name = @hotel_name, 
        hotel_description = @hotel_description, 
        hotel_rating_star = @hotel_rating_star, 
        hotel_phonenumber = @hotel_phonenumber, 
        hotel_modified_date = GETDATE(), 
        hotel_addr_id = @hotel_addr_id 
      WHERE hotel_id = @hotel_id;

      COMMIT TRANSACTION
  END TRY
  BEGIN CATCH
    ROLLBACK TRANSACTION
    PRINT ERROR_MESSAGE()
  END CATCH
END


EXEC [Hotel].[spUpdateHotel] 
  @hotel_id = 13, 
  @hotel_name = 'Hotel Bintang Update', 
  @hotel_description = 'Hotel terbaru dan bintang 5', 
  @hotel_rating_star = 5, 
  @hotel_phonenumber = '+62 812 3456 7890', 
  @hotel_addr_id = 2

  
