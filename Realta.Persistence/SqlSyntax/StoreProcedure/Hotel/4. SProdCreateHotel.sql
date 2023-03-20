CREATE OR ALTER PROCEDURE [Hotel].[spInsertHotel]
(
    @hotel_name nvarchar(85),
    @hotel_phonenumber nvarchar(25),
    @hotel_status bit,
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

        -- Insert new row into Hotel.Hotels
        INSERT INTO Hotel.Hotels 
        (
            hotel_name, 
            hotel_phonenumber, 
            hotel_status, 
            hotel_addr_id,
            hotel_description,
            hotel_modified_date
        )
        VALUES 
        (
            @hotel_name,
            @hotel_phonenumber,
            @hotel_status,
            @addr_id,
            @hotel_description,
            GETDATE()
        )
        
        COMMIT TRANSACTION;
        
        SELECT CAST(scope_identity() as int);
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
END



EXEC [Hotel].[spInsertHotel]
    @hotel_name = 'hotel xaaxax',
    @hotel_phonenumber = '0857 0718 0167',
    @hotel_status = 0, -- atau 0, tergantung status hotel
    @add_id = 'abu ali 29',
    @hotel_description = 'test input bos ku'

SELECT * FROM Hotel.Hotels ORDER BY hotel_id DESC;


