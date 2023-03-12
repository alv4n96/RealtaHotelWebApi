-- DROP TRIGGER Hotel.Hotel_Reviews_insert_validation
CREATE OR ALTER TRIGGER Hotel.Hotel_Reviews_insert_validation
ON Hotel.Hotel_Reviews
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @hore_user_id INT

    SELECT @hore_user_id = hore_user_id
    FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @hore_user_id AND usro_role_id IN (1, 5))
    BEGIN
        RAISERROR ('User does not exist or you do not have permission', 16, 1);
        RETURN;
    END

    INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
    SELECT hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id
    FROM inserted;
END
