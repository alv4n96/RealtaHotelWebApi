CREATE TRIGGER tr_update_hotel_modified_date
ON Hotel.Hotels
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Hotel.Hotels
    SET hotel_modified_date = GETDATE()
    FROM inserted
    WHERE inserted.hotel_id = Hotel.Hotels.hotel_id
END