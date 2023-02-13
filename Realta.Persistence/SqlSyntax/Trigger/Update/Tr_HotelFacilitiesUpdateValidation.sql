-- DROP TRIGGER Hotel.Facilities_Update_validation

CREATE TRIGGER Hotel.Facilities_Update_validation
ON Hotel.Facilities
AFTER UPDATE
AS
BEGIN
    DECLARE @faci_user_id INT

    SELECT @faci_user_id = faci_user_id
    FROM inserted   
    
    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @faci_user_id AND usro_role_id IN (2, 4))
    BEGIN
        RAISERROR ('User does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
    END
END
