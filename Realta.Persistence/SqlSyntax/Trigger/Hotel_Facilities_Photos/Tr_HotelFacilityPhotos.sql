--DROP TRIGGER Hotel.tr_hotel_facilities_price_history

CREATE TRIGGER Hotel.tr_facility_photos_fapho_primary
ON Hotel.Facility_Photos
AFTER INSERT, UPDATE
AS
BEGIN
  SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM inserted) RETURN; -- return if no rows inserted or updated
    -- update other records with the same fapho_faci_id to have fapho_primary = 0
    UPDATE p
    SET fapho_primary = 0
    FROM Hotel.Facility_Photos p
    JOIN inserted i ON p.fapho_faci_id = i.fapho_faci_id
    WHERE p.fapho_id <> i.fapho_id;

    -- set inserted records with fapho_primary = 1
    UPDATE p
    SET fapho_primary = 1
    FROM Hotel.Facility_Photos p
    JOIN inserted i ON p.fapho_id = i.fapho_id
    WHERE i.fapho_primary = 1;
END;
