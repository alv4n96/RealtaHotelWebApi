/* 
    Menggunakan trigger ini beresiko!
    NOTE : anda harus mengubah pada tabel Hotel.Hotels field hotel_rating_star 
    dari tipe data int, menjadi float untuk menampung hasil dari trigger ini
*/


CREATE TRIGGER tr_update_hotel_rating_star
ON Hotel.Hotel_Reviews
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
  SET NOCOUNT ON;

  UPDATE Hotel.Hotels
  SET hotel_rating_star = (
    SELECT AVG(hore_rating)
    FROM Hotel.Hotel_Reviews
    WHERE hore_hotel_id = inserted.hore_hotel_id
  )
  FROM Hotel.Hotels
  JOIN inserted ON Hotel.Hotels.hotel_id = inserted.hore_hotel_id
  WHERE inserted.hore_rating IS NOT NULL
END
