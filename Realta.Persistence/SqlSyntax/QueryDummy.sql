﻿SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotels';

USE WEBApiDbDemo

SELECT * FROM Hotel.Hotels;

SELECT * FROM Users.users;

SELECT 1 FROM Users.user_roles WHERE usro_user_id = ___ AND usro_role_id IN (2, 4);

SELECT * FROM [Hotel].[Hotels] WHERE hotel_id = 1;


SELECT * FROM Hotel.Facility_Photos;

UPDATE Hotel.Facility_Photos
SET fapho_primary = 1
WHERE fapho_id = 1;
SELECT * FROM Hotel.Facility_Photos;

