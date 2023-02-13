-- DROP TRIGGER Hotel.tr_Hotel_Facilities


CREATE TRIGGER Hotel.tr_Hotel_Facilities
ON Hotel.Facilities
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Hotel.Facilities
    SET faci_modified_date = GETDATE()
    FROM Hotel.Facilities;
END;
