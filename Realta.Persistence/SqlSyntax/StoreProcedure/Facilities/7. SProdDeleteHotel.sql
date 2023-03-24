-- DROP PROCEDURE [Hotel].[spDeleteHotel]

CREATE OR ALTER PROCEDURE [Hotel].[spDeleteHotel]
(
    @hotel_id int
)
AS
BEGIN
    
    BEGIN TRY
        BEGIN TRANSACTION
            DELETE FROM [Hotel].[Hotels]
            WHERE [hotel_id] = @hotel_id;
            

            -- this is important, dont do this for your project, 
            -- take this just for clean indexing while testing

            COMMIT TRANSACTION
        END TRY
        BEGIN CATCH
            ROLLBACK TRANSACTION
            PRINT 'Data tidak dapat dihapus.'
        END CATCH
END;

DBCC CHECKIDENT ('[Hotel].[Hotels]', RESEED, 10);

SELECT * FROM hotel.hotels

EXEC [Hotel].[spDeleteHotel] @hotel_id = 18
