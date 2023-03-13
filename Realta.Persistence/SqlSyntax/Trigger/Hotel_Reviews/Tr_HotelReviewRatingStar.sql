/* 
    Menggunakan trigger ini beresiko!
    NOTE : anda harus mengubah pada tabel Hotel.Hotels field hotel_rating_star 
    dari tipe data int, menjadi float untuk menampung hasil dari trigger ini
*/


CREATE OR ALTER TRIGGER Hotel.tr_Hotel_Reviews_Rating_Star
ON Hotel.Hotel_Reviews
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @hotel_id INT

    IF EXISTS(SELECT 1 FROM inserted)
        SET @hotel_id = (SELECT TOP 1 hore_hotel_id FROM inserted)
    ELSE IF EXISTS(SELECT 1 FROM deleted)
        SET @hotel_id = (SELECT TOP 1 hore_hotel_id FROM deleted)
    ELSE
        RETURN;

    UPDATE Hotel.Hotels
    SET hotel_rating_star = (
        SELECT CAST(FORMAT(AVG(cast(hore_rating AS numeric(2,1))), 'N1') AS numeric (2,1))
        FROM Hotel.Hotel_Reviews
        WHERE hore_hotel_id = @hotel_id )
    WHERE hotel_id = @hotel_id;

END;

