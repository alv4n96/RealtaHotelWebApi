-- DROP TRIGGER Hotel.Hotel_Reviews_insert_validation

CREATE TRIGGER Hotel.Hotel_Reviews_insert_validation
ON Hotel.Hotel_Reviews
AFTER INSERT
AS
BEGIN
    DECLARE @hore_user_id INT
    DECLARE @resetIndex INT

    SELECT @hore_user_id = hore_user_id
    FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @hore_user_id AND usro_role_id IN (1, 5))
    BEGIN
        SELECT @resetIndex = (IDENT_CURRENT('Hotel.Hotel_Reviews') - 1);
        RAISERROR ('User does not exist or you do not have permission', 16, 1);
        ROLLBACK TRANSACTION;
        DBCC CHECKIDENT ('Hotel.Hotel_Reviews', RESEED, @resetIndex );
    END
END