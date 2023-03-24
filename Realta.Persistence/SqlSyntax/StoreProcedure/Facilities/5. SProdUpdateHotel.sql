CREATE OR ALTER PROCEDURE [Hotel].[spUpdateHotel]
(
    @hotel_id int,
    @hotel_name nvarchar(85),
    @hotel_phonenumber nvarchar(25),
    @add_id nvarchar(50),
    @hotel_description nvarchar(500) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
        
        DECLARE @addr_id int

        -- Check if add_id exists in Master.Address
        SELECT @addr_id = addr_id
        FROM Master.Address
        WHERE addr_line1 LIKE '%' + @add_id + '%'

        -- If add_id doesn't exist in Master.Address, insert new row
        IF @addr_id IS NULL
        BEGIN
            INSERT INTO Master.Address (addr_line1) 
            VALUES (@add_id)

            SET @addr_id = SCOPE_IDENTITY() -- Get the ID of the newly inserted row
        END

        -- Update the row in Hotel.Hotels
        UPDATE Hotel.Hotels
        SET
            hotel_name = @hotel_name,
            hotel_phonenumber = @hotel_phonenumber,
            hotel_addr_id = @addr_id,
            hotel_description = @hotel_description,
            hotel_modified_date = GETDATE()
        WHERE hotel_id = @hotel_id
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        -- Rollback transaction if any error occurs
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        -- Raise error message
        DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity int = ERROR_SEVERITY()
        DECLARE @ErrorState int = ERROR_STATE()
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO


EXEC [Hotel].[spUpdateHotel] 
@hotel_id = 16,
@hotel_name = 'Alvan Hotel',
@hotel_phonenumber = '081234564365',
@add_id = 'Malang 23',
@hotel_description = 'Hotel berbintang yang sangat indah with your heart and sul'

SELECT * FROM Hotel.Hotels
ORDER BY hotel_id DESC;

  
