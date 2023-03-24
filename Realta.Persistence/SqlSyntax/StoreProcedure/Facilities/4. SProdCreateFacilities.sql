CREATE OR ALTER PROCEDURE [Hotel].[SpInsertFacility]
(
    @faci_name nvarchar(125),
    @faci_description nvarchar(255),
    @faci_max_number INT,
    @faci_room_number nvarchar(15),
    @faci_startdate DATETIME,
    @faci_enddate DATETIME,
    @faci_low_price MONEY,
    @faci_high_price MONEY,
    @faci_expose_price TINYINT,
    @faci_discount SMALLMONEY,
    @faci_tax_rate SMALLMONEY,
    @faci_cagro_id INT,
    @faci_hotel_id INT,
    @faci_user_id INT

)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN
        BEGIN TRANSACTION
            INSERT INTO Hotel.Facilities (
                faci_name, 
                faci_description, 
                faci_max_number, 
                faci_room_number, 
                faci_startdate, 
                faci_enddate, 
                faci_low_price, 
                faci_high_price, 
                faci_expose_price, 
                faci_discount, 
                faci_tax_rate, 
                faci_modified_date, 
                faci_cagro_id, 
                faci_hotel_id, 
                faci_user_id)
            VALUES (
                @faci_name, 
                @faci_description, 
                @faci_max_number, 
                @faci_room_number, 
                @faci_startdate, 
                @faci_enddate, 
                @faci_low_price, 
                @faci_high_price, 
                @faci_expose_price, 
                @faci_discount, 
                @faci_tax_rate, 
                GETDATE(), 
                @faci_cagro_id, 
                @faci_hotel_id, 
                @faci_user_id);

            SELECT CAST(scope_identity() as int);
        COMMIT TRANSACTION
    END
END;
GO



EXEC [Hotel].[spInsertHotel]
    @hotel_name = 'hotel xaaxax',
    @hotel_phonenumber = '0857 0718 0167',
    @hotel_status = 0, -- atau 0, tergantung status hotel
    @add_id = 'abu ali 29',
    @hotel_description = 'test input bos ku'

SELECT * FROM Hotel.Facilities ORDER BY faci_id DESC;


