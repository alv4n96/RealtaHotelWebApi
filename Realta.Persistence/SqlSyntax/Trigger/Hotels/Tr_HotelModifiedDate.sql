CREATE OR ALTER TRIGGER Hotel.tr_Hotels_ModifiedDate
ON Hotel.Hotels
AFTER INSERT, UPDATE 
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT UPDATE(hotel_rating_star)
    BEGIN
        UPDATE Hotel.Hotels 
        SET hotel_modified_date = GETDATE()
        FROM inserted
        WHERE Hotels.hotel_id = inserted.hotel_id
    END
END;

