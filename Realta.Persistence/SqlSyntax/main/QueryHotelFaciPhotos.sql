SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Facility_Photos';

-- ALTER TABLE Hotel.Facilities ALTER COLUMN faci_rate_price MONEY NULL;


USE WEBApiDbDemo


SELECT 
fapho_id AS FaphoId
,fapho_thumbnail_filename AS FaphoThumbnailFilename
,fapho_photo_filename AS FaphoPhotoFilename
,fapho_primary AS FaphoPrimary
,fapho_url AS FaphoUrl 
,fapho_modified_date AS FaphoModifiedDate
,fapho_faci_id AS FaphoFaciId
,fapho_original_filename AS FaphoOriginalFilename
,fapho_file_size AS FaphoFileSize 
,fapho_file_type AS FaphoFileType 
from hotel.Facility_Photos WHERE fapho_faci_id = @fapho_faci_id;

ALTER TABLE Hotel.Facility_Photos
ADD fapho_original_filename nvarchar(50) NULL,
    fapho_file_size INT NULL,
    fapho_file_type nvarchar(50) NULL;


SELECT * FROM Hotel.Facility_Photos

SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id;

SELECT * FROM Hotel.Facilities WHERE faci_hotel_id = @faci_hotel_id AND faci_id = @faci_id;

SELECT * FROM [Hotel].Facilities WHERE faci_id = 30;


DELETE FROM Hotel.Facilities WHERE faci_id = 30;

SELECT * FROM Hotel.Facility_Photos;


INSERT INTO Hotel.Facility_Photos 
        (fapho_thumbnail_filename, fapho_photo_filename, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id)
VALUES  (@fapho_thumbnail_filename, @fapho_photo_filename, @fapho_primary, @fapho_url, GETDATE(), @fapho_faci_id);


DELETE FROM Hotel.Facility_Photos WHERE fapho_id = @fapho_id;
DBCC CHECKIDENT ('Hotel.Facility_Photos', RESEED,7 );

SELECT * FROM Hotel.Hotels ORDER BY hotel_id DESC;

UPDATE Hotel.Facility_Photos
SET 
    fapho_thumbnail_filename = @fapho_thumbnail_filename,
    fapho_photo_filename = @fapho_photo_filename,
    fapho_primary = @fapho_primary,
    fapho_url = @fapho_url,
    fapho_modified_date = GETDATE(),
    fapho_faci_id = @fapho_faci_id
WHERE fapho_id = @fapho_id


UPDATE Hotel.Hotels
SET hotel_status = @hotel_status,
    hotel_reason_status = @hotel_reason_status
WHERE hotel_id = @hotel_id;
SELECT * FROM Hotel.Hotels WHERE hotel_id = 10;

SELECT * FROM Hotel.Hotel_Reviews;

SELECT DATA_TYPE, COLUMN_NAME, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Hotel_Reviews';
