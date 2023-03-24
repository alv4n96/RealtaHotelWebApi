USE DB_HR;
GO

DROP DATABASE IF EXISTS WEBApiDbDemo;
GO

CREATE DATABASE WEBApiDbDemo;
GO

-- menggunakan db WebApiDemo
USE WEBApiDbDemo;
GO

CREATE SCHEMA Master;
GO

CREATE SCHEMA Users;
GO

CREATE SCHEMA Hotel;
GO

-- Dummy Table
CREATE TABLE Users.users (
  user_id int IDENTITY(1,1) NOT NULL,
  user_full_name nvarchar (55) DEFAULT 'guest' NOT NULL,
  user_type nvarchar (15) CHECK(user_type IN('T','C','I')),
  user_company_name nvarchar (255),
  user_email nvarchar(256),
  user_phone_number nvarchar (25) UNIQUE NOT NULL,
  user_modified_date datetime,
  CONSTRAINT pk_user_id PRIMARY KEY(user_id)
);


CREATE TABLE Users.roles (
  role_id int IDENTITY(1,1),
  role_name nvarchar (35) NOT NULL,
  CONSTRAINT pk_role_id PRIMARY KEY(role_id)
);

CREATE TABLE Users.user_roles (
  usro_user_id int,
  usro_role_id int,
  CONSTRAINT pk_usro_user_id PRIMARY KEY(usro_user_id),
  CONSTRAINT fk_usro_user_id FOREIGN KEY (usro_user_id) REFERENCES Users.users(user_id) 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
  CONSTRAINT fk_usro_role_id FOREIGN KEY (usro_role_id) REFERENCES Users.roles(role_id) 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);


--FOR USERS END



CREATE TABLE Master.address (
  addr_id int IDENTITY(1, 1),
  addr_line1 nvarchar(255) NOT NULL,
  addr_line2 nvarchar(255),
  addr_postal_code nvarchar(5),
  addr_spatial_location geography,
  addr_prov_id int,
  CONSTRAINT pk_addr_id PRIMARY KEY(addr_id),
);

--insert from master data
CREATE TABLE Master.category_group (
  cagro_id int IDENTITY(1, 1),
  cagro_name nvarchar(25) UNIQUE NOT NULL,
  cagro_description nvarchar(255),
  cagro_type nvarchar(25) NOT NULL CHECK (cagro_type IN('category', 'service', 'facility')),
  cagro_icon nvarchar(255),
  cagro_icon_url nvarchar(255),
  CONSTRAINT pk_cagro_id PRIMARY KEY(cagro_id)
);

-- Create a new table called 'Hotels' in schema 'Hotel'
-- Drop the table if it already exists
IF OBJECT_ID('Hotel.Hotels', 'U') IS NOT NULL
DROP TABLE Hotel.Hotels
-- Create the table in the specified schema
CREATE TABLE Hotel.Hotels
(
  hotel_id int IDENTITY(1,1) NOT NULL CONSTRAINT hotel_id_pk PRIMARY KEY, -- primary key column
  hotel_name nvarchar(85) NOT NULL,
  hotel_description nvarchar(500) NULL,
  -- BEGIN UPDATE
  hotel_status BIT NOT NULL CHECK(hotel_status IN(0,1)),
  hotel_reason_status nvarchar(500) NULL,
  -- END UPDATE
  hotel_rating_star numeric(2,1) NULL,
  hotel_phonenumber nvarchar(25) NOT NULL,
  hotel_modified_date datetime NULL, 
  -- Primary Key
  hotel_addr_id INT NOT NULL,
  hotel_addr_description nvarchar(500) NULL,
  -- Add this later, on production
  CONSTRAINT hotel_addr_id_fk FOREIGN KEY (hotel_addr_id) REFERENCES Master.Address(addr_id)
);

-- Create a new table called 'Hotel_Reviews' in schema 'Hotel'
-- Drop the table if it already exists
IF OBJECT_ID('Hotel.Hotel_Reviews', 'U') IS NOT NULL
DROP TABLE Hotel.Hotel_Reviews

-- Create the table in the specified schema
CREATE TABLE Hotel.Hotel_Reviews
(
  hore_id INT IDENTITY(1,1) NOT NULL CONSTRAINT hore_id_pk PRIMARY KEY, -- primary key column
  hore_user_review nvarchar(125) NOT NULL,
  hore_rating TINYINT NOT NULL CHECK(hore_rating IN(1,2,3,4,5)) DEFAULT 5,
  hore_created_on datetime NULL,
  -- FOREIGN KEY
  hore_user_id INT NOT NULL,
  hore_hotel_id INT NOT NULL,
  -- Add this later, on production
  CONSTRAINT hore_user_id_pk FOREIGN KEY (hore_user_id) REFERENCES Users.Users(user_id),
  CONSTRAINT hore_hotel_id_fk FOREIGN KEY (hore_hotel_id) REFERENCES Hotel.Hotels(hotel_id) ON DELETE CASCADE ON UPDATE CASCADE
);



-- Create a new table called 'Facilities' in schema 'Hotel'
-- Drop the table if it already exists
IF OBJECT_ID('Hotel.Facilities', 'U') IS NOT NULL
DROP TABLE Hotel.Facilities

-- Create the table in the specified schema
CREATE TABLE Hotel.Facilities
(
  faci_id INT IDENTITY(1,1) NOT NULL CONSTRAINT faci_id_pk PRIMARY KEY, -- primary key column
  faci_name nvarchar(125) NOT NULL,
  faci_description nvarchar(255) NULL,
  faci_max_number INT NULL,
  faci_measure_unit VARCHAR(15) NULL CHECK(faci_measure_unit IN('people','beds')),
  faci_room_number nvarchar(15) NOT NULL,
  faci_startdate datetime NOT NULL,
  faci_enddate datetime NOT NULL,
  faci_low_price MONEY NOT NULL,
  faci_high_price MONEY NOT NULL,
  faci_rate_price MONEY NULL,
  faci_expose_price TINYINT NOT NULL CHECK(faci_expose_price IN(1,2,3)),
  faci_discount SMALLMONEY NULL,
  faci_tax_rate SMALLMONEY NULL,
  faci_modified_date datetime NULL,
  --FOREIGN KEY
  faci_cagro_id INTEGER NOT NULL,
  faci_hotel_id INT NOT NULL,
  faci_user_id INT NOT NULL,
  -- UNIQUE ID
  CONSTRAINT faci_room_number_uq UNIQUE (faci_room_number),
  -- Add this later, on production
  CONSTRAINT faci_cagro_id_fk FOREIGN KEY (faci_cagro_id) REFERENCES Master.Category_Group(cagro_id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT faci_hotel_id_fk FOREIGN KEY (faci_hotel_id) REFERENCES Hotel.Hotels(hotel_id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT faci_user_id_fk FOREIGN KEY (faci_user_id) REFERENCES Users.users(user_id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- Create a new table called 'Facility_Price_History' in schema 'Hotel'
-- Drop the table if it already exists
IF OBJECT_ID('Hotel.Facility_Price_History', 'U') IS NOT NULL
DROP TABLE Hotel.Facility_Price_History

-- Create the table in the specified schema
create table hotel.facility_price_history
(
  faph_id int identity(1,1) not null constraint faph_id_pk primary key, -- primary key column
  faph_startdate datetime not null,
  faph_enddate datetime not null,
  faph_low_price money not null,
  faph_high_price money not null,
  faph_rate_price money not null,
  faph_discount smallmoney null,
  faph_tax_rate smallmoney null,
  faph_modified_date datetime,
  -- foreign key
  faph_faci_id int not null,
  faph_user_id int not null,
  -- add this later, on production
  constraint faph_faci_id_fk foreign key (faph_faci_id) references hotel.facilities(faci_id) on delete cascade on update cascade,
);


-- Create a new table called 'Facility_Photos' in schema 'Hotel'
-- Drop the table if it already exists
IF OBJECT_ID('Hotel.Facility_Photos', 'U') IS NOT NULL
DROP TABLE Hotel.Facility_Photos

-- Create the table in the specified schema
CREATE TABLE Hotel.Facility_Photos
(
  fapho_id INT IDENTITY(1,1) NOT NULL CONSTRAINT fapho_id_pk PRIMARY KEY, -- primary key column
  fapho_photo_filename nvarchar(150) NULL,
  fapho_thumbnail_filename nvarchar(150) NOT NULL,
  fapho_original_filename nvarchar(150) NOT NULL,
  fapho_file_size smallint NULL,
  fapho_file_type nvarchar(50) NULL,
  fapho_primary BIT NULL CHECK(fapho_primary IN(0,1)),
  fapho_url nvarchar(255) NULL,
  fapho_modified_date datetime,
  -- FOREIGN KEY
  fapho_faci_id INT NOT NULL,
  CONSTRAINT fapho_faci_id_fk FOREIGN KEY (fapho_faci_id) REFERENCES Hotel.Facilities(faci_id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO


--TRIGGERS
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
GO

/* 
    Menggunakan trigger ini beresiko!
    NOTE : anda harus mengubah pada tabel Hotel.Hotels field hotel_rating_star 
    dari tipe data int, menjadi float untuk menampung hasil dari trigger ini
*/


CREATE OR ALTER TRIGGER Hotel.tr_Hotel_Reviews_Rating_Star
ON Hotel.Hotel_Reviews
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @hotel_id INT

    IF EXISTS(SELECT 1 FROM inserted)
        SET @hotel_id = (SELECT TOP 1 hore_hotel_id FROM inserted)
    ELSE IF EXISTS(SELECT 1 FROM deleted)
        SET @hotel_id = (SELECT TOP 1 hore_hotel_id FROM deleted)
    ELSE
        RETURN;

    UPDATE Hotel.Hotels
    SET hotel_rating_star = (
        SELECT CAST(FORMAT(AVG(cast(hore_rating AS numeric(2,1))), 'N1') AS numeric (2,1))
        FROM Hotel.Hotel_Reviews
        WHERE hore_hotel_id = @hotel_id )
    WHERE hotel_id = @hotel_id;

END;
GO

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
END;
GO

CREATE OR ALTER TRIGGER Hotel.Facilities_insert_validation
ON Hotel.Facilities
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @faci_user_id INT
    DECLARE @faci_hotel_id INT
    DECLARE @faci_rate_price MONEY
    DECLARE @faci_low_price MONEY
    DECLARE @faci_high_price MONEY

    SELECT 
        @faci_user_id = faci_user_id,
        @faci_hotel_id = faci_hotel_id,
        @faci_low_price = faci_low_price,
        @faci_high_price = faci_high_price,
        @faci_rate_price = 
        (
    CASE
      WHEN faci_discount IS NULL AND faci_tax_rate IS NULL THEN (faci_high_price + faci_low_price) / 2
      WHEN faci_discount IS NULL THEN (((faci_high_price + faci_low_price) / 2) + (((faci_high_price + faci_low_price) / 2) * (faci_tax_rate/100)))
      WHEN faci_tax_rate IS NULL THEN (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * (faci_discount/100)))
      ELSE (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * faci_discount/100)) + (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * faci_discount/100)) * (faci_tax_rate/100)
    END
  )
    FROM inserted   

    IF NOT EXISTS (SELECT 1 FROM Hotel.Hotels WHERE hotel_id = @faci_hotel_id)
    BEGIN
        RAISERROR ('Hotel does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        RETURN;
    END
    
    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @faci_user_id AND usro_role_id IN (2, 4))
    BEGIN
        RAISERROR ('User does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        RETURN;
    END
    
    IF EXISTS (SELECT faci_low_price, faci_high_price 
               FROM inserted 
               WHERE faci_high_price < faci_low_price) 
    BEGIN 
        RAISERROR ('High price cannot be lower than low price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 

    IF (@faci_rate_price > @faci_high_price OR @faci_rate_price < @faci_low_price) 
    BEGIN 
        RAISERROR ('Rate price cannot be lower than low price OR cannot be higher than high price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 


    BEGIN TRY
        INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, 
                    faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_discount, faci_tax_rate, faci_modified_date, 
                    faci_cagro_id, faci_hotel_id, faci_user_id, faci_expose_price) 
        SELECT 
            i.faci_name, 
            i.faci_description, 
            i.faci_max_number,
            i.faci_measure_unit,
            i.faci_room_number,
            i.faci_startdate,
            i.faci_enddate,
            i.faci_low_price, 
            i.faci_high_price, 
            @faci_rate_price,
            i.faci_discount,
            i.faci_tax_rate,
            GETDATE(),
            i.faci_cagro_id,
            i.faci_hotel_id,
            i.faci_user_id,
            i.faci_expose_price
        FROM inserted i
    END TRY

    
BEGIN CATCH
        ROLLBACK TRANSACTION
    -- Handle the exception here, for example by logging the error
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    
    SELECT 
      @ErrorMessage = ERROR_MESSAGE(),
      @ErrorSeverity = ERROR_SEVERITY(),
      @ErrorState = ERROR_STATE();
      
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH
END;
GO

-- DROP TRIGGER Hotel.Facilities_Update_validation

CREATE OR ALTER TRIGGER Hotel.Facilities_update_validation
ON Hotel.Facilities
AFTER UPDATE 
AS
BEGIN
    DECLARE @faci_id INT;
    DECLARE @faci_user_id INT;
    DECLARE @faci_hotel_id INT;
  DECLARE @faci_startdate DATETIME;
  DECLARE @faci_enddate DATETIME;
  DECLARE @faci_discount SMALLMONEY;
  DECLARE @faci_tax_rate SMALLMONEY;
    DECLARE @faci_low_price MONEY;
    DECLARE @faci_high_price MONEY;
    DECLARE @faci_rate_price MONEY;

    SELECT 
        @faci_id = faci_id,
        @faci_user_id = faci_user_id,
        @faci_hotel_id = faci_hotel_id,
        @faci_startdate = faci_startdate,
        @faci_enddate = faci_enddate,
        @faci_discount = faci_discount,
        @faci_tax_rate = faci_tax_rate,
        @faci_low_price = faci_low_price,
        @faci_high_price = faci_high_price,
        @faci_rate_price = 
        (
    CASE
      WHEN faci_discount IS NULL AND faci_tax_rate IS NULL THEN (faci_high_price + faci_low_price) / 2
      WHEN faci_discount IS NULL THEN (((faci_high_price + faci_low_price) / 2) + (((faci_high_price + faci_low_price) / 2) * (faci_tax_rate/100)))
      WHEN faci_tax_rate IS NULL THEN (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * (faci_discount/100)))
      ELSE (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * faci_discount/100)) + (((faci_high_price + faci_low_price) / 2) - (((faci_high_price + faci_low_price) / 2) * faci_discount/100)) * (faci_tax_rate/100)
    END
  )
    FROM inserted   

    IF NOT EXISTS (SELECT 1 FROM Hotel.Hotels WHERE hotel_id = @faci_hotel_id)
    BEGIN
        RAISERROR ('Hotel does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        RETURN;
    END
    
    IF NOT EXISTS (SELECT 1 FROM Users.user_roles WHERE usro_user_id = @faci_user_id AND usro_role_id IN (2, 4))
    BEGIN
        RAISERROR ('User does not exist or you do not have permission', 16, 1)
        ROLLBACK TRANSACTION
        RETURN;
    END
    
    IF EXISTS (SELECT faci_low_price, faci_high_price 
               FROM inserted 
               WHERE faci_high_price < faci_low_price) 
    BEGIN 
        RAISERROR ('High price cannot be lower than low price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 

    IF (@faci_rate_price > @faci_high_price OR @faci_rate_price < @faci_low_price) 
    BEGIN 
        RAISERROR ('Rate price cannot be lower than low price OR cannot be higher than high price', 16, 1) 
        ROLLBACK TRANSACTION
        RETURN;
    END 
    

    IF UPDATE(faci_startdate)
    OR UPDATE(faci_enddate)
    OR UPDATE(faci_low_price)
    OR UPDATE(faci_high_price) 
    OR UPDATE(faci_rate_price) 
    OR UPDATE(faci_discount) 
    OR UPDATE(faci_tax_rate)
    BEGIN
        BEGIN TRANSACTION
      UPDATE Hotel.Facilities 
      SET 
        faci_rate_price = @faci_rate_price
      WHERE 
        faci_id = @faci_id

      INSERT INTO Hotel.Facility_Price_History (faph_startdate, faph_enddate, faph_low_price, faph_high_price, faph_rate_price, faph_discount, faph_tax_rate, faph_modified_date, faph_faci_id, faph_user_id)
      VALUES (@faci_startdate, @faci_enddate, @faci_low_price, @faci_high_price, @faci_rate_price, @faci_discount, @faci_tax_rate, GETDATE(), @faci_id, @faci_user_id);
        COMMIT TRANSACTION
    END
END;
GO

--DROP TRIGGER Hotel.tr_hotel_facilities_price_history

CREATE OR ALTER TRIGGER Hotel.tr_hotel_facilities_price_history
ON Hotel.Facilities
AFTER INSERT 
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @faph_startdate DATETIME
  DECLARE @faph_enddate DATETIME  
  DECLARE @faph_modified_date DATETIME
  DECLARE @faph_low_price MONEY;
  DECLARE @faph_high_price MONEY;
  DECLARE @faph_rate_price MONEY;
  DECLARE @faph_discount SMALLMONEY;
  DECLARE @faph_tax_rate SMALLMONEY;
  DECLARE @faph_faci_id INT;
  DECLARE @faph_user_id INT;

  SELECT @faph_startdate = faci_startdate, @faph_enddate = faci_enddate, @faph_low_price = faci_low_price, 
  @faph_high_price = faci_high_price, @faph_rate_price = faci_rate_price, @faph_discount = faci_discount, 
  @faph_modified_date = faci_modified_date, @faph_tax_rate = faci_tax_rate, @faph_faci_id = faci_id, @faph_user_id = faci_user_id 
  FROM inserted;

  INSERT INTO Hotel.Facility_Price_History (faph_startdate, faph_enddate, faph_low_price, faph_high_price, faph_rate_price, faph_discount, faph_tax_rate, faph_modified_date, faph_faci_id, faph_user_id)
  VALUES (@faph_startdate, @faph_enddate, @faph_low_price, @faph_high_price, @faph_rate_price, @faph_discount, @faph_tax_rate, @faph_modified_date, @faph_faci_id, @faph_user_id);
END;
GO

--DROP TRIGGER Hotel.tr_hotel_facilities_price_history

CREATE OR ALTER TRIGGER Hotel.tr_facility_photos_fapho_primary
ON Hotel.Facility_Photos
AFTER INSERT 
AS
BEGIN
  SET NOCOUNT ON;

  BEGIN TRY
    -- Start transaction
    BEGIN TRANSACTION
    DECLARE @fapho_faci_id INT
    DECLARE @fapho_id INT
    DECLARE @fapho_primary INT

    SELECT 
        @fapho_id = fapho_id,
        @fapho_faci_id = fapho_faci_id,
        @fapho_primary = fapho_primary
    FROM inserted

    -- If any row is updated, check if the value of fapho_primary is changed to 1
    IF NOT EXISTS (SELECT TOP 1 * FROM Hotel.Facility_Photos WHERE fapho_primary = 1 AND fapho_faci_id = @fapho_faci_id) AND (@fapho_primary = 0)
    BEGIN
      -- Only allow one record with fapho_primary = 1 for each faci_id
      UPDATE Hotel.Facility_Photos
      SET 
        fapho_primary = 1
      WHERE fapho_id = @fapho_id
    END

    -- Commit transaction
    COMMIT TRANSACTION

  END TRY

  BEGIN CATCH
    -- Rollback transaction in case of any errors
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
  END CATCH;
END;
GO

-- DROP TRIGGER Hotel.Hotel_Facility_Photos_insert_validation

CREATE OR ALTER TRIGGER Hotel.Hotel_Facility_Photos_insert_validation
ON Hotel.Facility_Photos
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @fapho_faci_id INT

    SELECT 
        @fapho_faci_id = fapho_faci_id
    FROM inserted

 

    IF NOT EXISTS (SELECT 1 FROM Hotel.Facilities WHERE faci_id = @fapho_faci_id)
    BEGIN
        RAISERROR ('Facility does not exist!', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        INSERT INTO Hotel.Facility_Photos (fapho_photo_filename, fapho_thumbnail_filename, fapho_original_filename, fapho_file_size, fapho_file_type, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id)
    SELECT fapho_photo_filename, fapho_thumbnail_filename, fapho_original_filename, fapho_file_size, fapho_file_type, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id
      FROM inserted;
    END
END;
GO

-- DROP TRIGGER Hotel.Hotel_Facility_Photos_update_validation

CREATE OR ALTER TRIGGER Hotel.Hotel_Facility_Photos_update_primary_validation
ON Hotel.Facility_Photos
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @fapho_faci_id INT
    DECLARE @fapho_primary INT

    SELECT 
        @fapho_faci_id = fapho_faci_id,
        @fapho_primary =  fapho_primary
    FROM inserted

    IF (@fapho_primary = 1)
        BEGIN
      -- update other records with the same fapho_faci_id to have fapho_primary = 0
      UPDATE p
      SET fapho_primary = 0
      FROM Hotel.Facility_Photos p
      JOIN inserted i ON p.fapho_faci_id = i.fapho_faci_id
      WHERE p.fapho_id <> i.fapho_id
      AND (i.fapho_primary = 1 OR (i.fapho_primary IS NULL AND p.fapho_primary = 1));

      -- set inserted records with fapho_primary = 1
      UPDATE p
      SET fapho_primary = 1
      FROM Hotel.Facility_Photos p
      JOIN inserted i ON p.fapho_id = i.fapho_id
      WHERE i.fapho_primary = 1
      AND (p.fapho_primary IS NULL OR p.fapho_primary = 0);
        END

    IF NOT EXISTS (SELECT * FROM inserted WHERE fapho_primary = 1 AND fapho_faci_id = @fapho_faci_id) 
        BEGIN
            UPDATE Hotel.Facility_Photos
            SET fapho_primary = 1
            WHERE fapho_id = (
            SELECT MIN(fapho_id) FROM hotel.Facility_Photos
            WHERE fapho_faci_id = @fapho_faci_id 
            )
        END
END;
GO


--SP
CREATE OR ALTER PROCEDURE [Hotel].[spSelectHotel]
AS
BEGIN 
  SET NOCOUNT ON;

  SELECT 
    hotel_id AS HotelId
    ,hotel_name AS HotelName
    ,hotel_description AS HotelDescription
    ,hotel_status AS HotelStatus
    ,hotel_reason_status AS HotelReasonStatus
    ,hotel_rating_star AS HotelRatingStar
    ,hotel_phonenumber AS HotelPhonenumber
    ,hotel_modified_date AS HotelModifiedDate
    ,hotel_addr_id AS HotelAddrId
    ,hotel_addr_description AS HotelAddrDescription
  FROM Hotel.Hotels;
  END;
GO

-- DROP PROCEDURE IF EXISTS [Hotel].[spSelectHotelById];

CREATE OR ALTER PROCEDURE Hotel.spSelectHotelByName
  @hotelName NVARCHAR(85)
AS
BEGIN 
  SET NOCOUNT ON;

  IF EXISTS (SELECT * FROM Hotel.Hotels WHERE hotel_name LIKE '%' + @hotelName + '%')
    BEGIN
        SELECT 
        hotel_id AS HotelId, 
        hotel_name AS HotelName, 
        hotel_description AS HotelDescription, 
        hotel_status AS HotelStatus, 
        hotel_reason_status AS HotelReasonStatus, 
        hotel_rating_star AS HotelRatingStar, 
        hotel_phonenumber AS HotelPhonenumber, 
        hotel_modified_date AS HotelModifiedDate, 
        hotel_addr_id AS HotelAddrId 
    FROM 
        [Hotel].[Hotels]
    WHERE 
        hotel_name LIKE '%' + @hotelName + '%'
    END
    -- ELSE
    -- BEGIN
    --     RAISERROR('Hotel with ID %d could not be found', 16, 1, @hotelId)
    -- END
END;
GO

-- DROP PROCEDURE IF EXISTS [Hotel].[spSelectHotelById];

CREATE OR ALTER PROCEDURE Hotel.spSelectHotelById
  @hotelId int
AS
BEGIN 
  SET NOCOUNT ON;

  IF EXISTS (SELECT * FROM Hotel.Hotels WHERE hotel_id = @hotelId)
    BEGIN
        SELECT 
      hotel_id AS HotelId
      ,hotel_name AS HotelName
      ,hotel_description AS HotelDescription
      ,hotel_status AS HotelStatus
      ,hotel_reason_status AS HotelReasonStatus
      ,hotel_rating_star AS HotelRatingStar
      ,hotel_phonenumber AS HotelPhonenumber
      ,hotel_modified_date AS HotelModifiedDate
      ,hotel_addr_id AS HotelAddrId
      ,hotel_addr_description AS HotelAddrDescription
    FROM Hotel.Hotels
    WHERE hotel_id = @hotelId;
    END
    -- ELSE
    -- BEGIN
    --     RAISERROR('Hotel with ID %d could not be found', 16, 1, @hotelId)
    -- END
END;
GO

CREATE OR ALTER PROCEDURE [Hotel].[spInsertHotel]
(
    @hotel_name nvarchar(85),
    @hotel_phonenumber nvarchar(25),
    @hotel_status bit,
    @add_id nvarchar(50),
    @hotel_description nvarchar(500) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
        
        DECLARE @addr_id int

        -- Check if add_id exists in Master.Address
        SELECT @addr_id = addr_id
        FROM Master.Address
        WHERE addr_line1 LIKE '%' + @add_id + '%'

        -- If add_id doesn't exist in Master.Address, insert new row
        IF @addr_id IS NULL
        BEGIN
            INSERT INTO Master.Address (addr_line1) 
            VALUES (@add_id)

            SET @addr_id = SCOPE_IDENTITY() -- Get the ID of the newly inserted row
        END

        -- Insert new row into Hotel.Hotels
        INSERT INTO Hotel.Hotels 
        (
            hotel_name, 
            hotel_phonenumber, 
            hotel_status, 
            hotel_addr_id,
            hotel_description,
            hotel_modified_date
        )
        VALUES 
        (
            @hotel_name,
            @hotel_phonenumber,
            @hotel_status,
            @addr_id,
            @hotel_description,
            GETDATE()
        )
        
        COMMIT TRANSACTION;
        
        SELECT CAST(scope_identity() as int);
    END TRY
    BEGIN CATCH
        -- Rollback transaction if any error occurs
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        -- Raise error message
        DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity int = ERROR_SEVERITY()
        DECLARE @ErrorState int = ERROR_STATE()
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
























-- Dummy Data Users
INSERT INTO Users.users (user_full_name, user_type, user_company_name, user_email, user_phone_number, user_modified_date)
VALUES ('John Smith', 'T', 'Acme Inc.', 'john.smith@acme.com', '123-456-7890', GETDATE()),
       ('Jane Doe', 'C', 'XYZ Corp.', 'jane.doe@xyz.com', '123-456-7891', GETDATE()),
       ('Bob Johnson', 'I', 'ABC Inc.', 'bob.johnson@abc.com', '123-456-7892', GETDATE()),
       ('Samantha Williams', 'T', 'Def Corp.', 'samantha.williams@def.com', '123-456-7893', GETDATE()),
       ('Michael Brown', 'C', 'Ghi Inc.', 'michael.brown@ghi.com', '123-456-7894', GETDATE()),
       ('Emily Davis', 'I', 'Jkl Ltd.', 'emily.davis@jkl.com', '123-456-7895', GETDATE()),
       ('William Thompson', 'T', 'Mno Inc.', 'william.thompson@mno.com', '123-456-7896', GETDATE()),
       ('Ashley Johnson', 'C', 'Pqr Corp.', 'ashley.johnson@pqr.com', '123-456-7897', GETDATE()),
       ('David Anderson', 'I', 'Stu Inc.', 'david.anderson@stu.com', '123-456-7898', GETDATE()),
       ('Jessica Smith', 'T', 'Vwx Corp.', 'jessica.smith@vwx.com', '123-456-7899', GETDATE()),
	   ('David Brown', 'T', 'Example Co', 'david.brown@example.com', '555-555-1222', GETDATE()),
	   ('Jessica Smith', 'C', 'Test Inc', 'jessica.smith@test.com', '555-555-1223', GETDATE()),
	   ('James Johnson', 'I', 'Acme Inc', 'james.johnson@acme.com', '555-555-1224', GETDATE()),
	   ('Samantha Williams', 'C', 'XYZ Corp', 'samantha.williams@xyz.com', '555-555-1225', GETDATE()),
	   ('Robert Davis', 'T', 'Example Co', 'robert.davis@example.com', '555-555-1226', GETDATE());

-- Insert 5 rows into the users.roles table
SET IDENTITY_INSERT users.roles ON;
INSERT INTO users.roles (role_id, role_name)
VALUES 
(1, 'Guest'),
(2, 'Manager'),
(3, 'OfficeBoy'),
(4, 'Admin'),
(5, 'User');
SET IDENTITY_INSERT users.roles OFF;

-- Insert 15 rows into the users.user_roles table
INSERT INTO users.user_roles (usro_user_id, usro_role_id)
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 1),
(7, 2),
(8, 3),
(9, 4),
(10, 5),
(11, 1),
(12, 2),
(13, 3),
(14, 4),
(15, 5);


--Data dummy Master

SET IDENTITY_INSERT Master.category_group ON;
INSERT INTO master.category_group (cagro_id, cagro_name, cagro_description, cagro_type, cagro_icon, cagro_icon_url)
VALUES
  (1, 'ROOM', 'Rooms for guests to stay in', 'category', 'room.png', 'https://example.com/room.png'),
  (2, 'RESTAURANT', 'On-site restaurant for guests to dine in', 'service', 'restaurant.png', 'https://example.com/restaurant.png'),
  (3, 'MEETING ROOM', 'Rooms for meetings and events', 'facility', 'meeting_room.png', 'https://example.com/meeting_room.png'),
  (4, 'GYM', 'Fitness center for guests to use', 'facility', 'gym.png', 'https://example.com/gym.png'),
  (5, 'AULA', 'Multipurpose room for events', 'facility', 'aula.png', 'https://example.com/aula.png'),
  (6, 'SWIMMING POOL', 'Outdoor swimming pool for guests to use', 'facility', 'swimming_pool.png', 'https://example.com/swimming_pool.png'),
  (7, 'BALROOM', 'Ballroom for events and parties', 'facility', 'balroom.png', 'https://example.com/balroom.png');
SET IDENTITY_INSERT Master.category_group OFF;

SET IDENTITY_INSERT Master.Address ON;
INSERT INTO Master.Address (addr_id, addr_line1, addr_line2, addr_postal_code, addr_spatial_location, addr_prov_id)
VALUES (1, '123 Main Street', '', 'A1AA1', geography::Point(43.65, -79.38, 4326), 1),
    (2, '456 Maple Avenue', '', 'B2BB2', geography::Point(43.65, -79.38, 4326), 1),
    (3, '789 Oak Boulevard', '', 'C3CC3', geography::Point(43.65, -79.38, 4326), 1),
    (4, '321 Pine Street', '', 'D4DD4', geography::Point(43.65, -79.38, 4326), 1),
    (5, '654 Cedar Road', '', 'E5EE5', geography::Point(43.65, -79.38, 4326), 1),
    (6, '987 Spruce Lane', '', 'F6FF6', geography::Point(43.65, -79.38, 4326), 1),
    (7, '246 Fir Avenue', '', 'G77G7', geography::Point(43.65, -79.38, 4326), 1),
    (8, '369 Hemlock Drive', '', 'H8HH8', geography::Point(43.65, -79.38, 4326), 1),
    (9, '159 Willow Way', '', 'I9II9', geography::Point(43.65, -79.38, 4326), 1),
    (10, '753 Maple Street', '', 'J0JJ0', geography::Point(43.65, -79.38, 4326), 1),
    (11, '1 Parliament Hill', '', 'K1KA6', geography::Point(45.42, -75.70, 4326), 2),
    (12, '2 Sussex Drive', '', 'K1NK1', geography::Point(45.42, -75.70, 4326), 2),
    (13, '3 Rideau Street', '', 'K1NJ9', geography::Point(45.42, -75.70, 4326), 2),
    (14, '4 Wellington Street', '', 'K1PJ9', geography::Point(45.42, -75.70, 4326), 2),
    (15, '5 Elgin Street', '', 'K1PK7', geography::Point(45.42, -75.70, 4326), 2),
    (16, 'Avenida de la Constitución, 3', '', '41001', geography::Point(37.38, -6.00, 4326), 16),
    (17, 'Plaza de Santo Domingo, 3', '', '41001', geography::Point(37.38, -6.00, 4326), 16),
    (18, 'Calle de la Ribera, 15', '', '41001', geography::Point(37.38, -6.00, 4326), 16),
    (19, 'Calle del Arenal, 12', '', '41001', geography::Point(37.38, -6.00, 4326), 16),
    (20, 'Calle de la Ribera, 25', '', '41001', geography::Point(37.38, -6.00, 4326), 16);
SET IDENTITY_INSERT Master.Address OFF;




-- Data Hotel

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Grand Hyatt Jakarta', 'Luxury hotel in the heart of Jakarta', 1, '089299212341', 4, 'Jl. M. H. Thamrin No.30, Jakarta Pusat', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Aston Priority Simatupang Hotel & Conference Center', 'Contemporary hotel in South Jakarta', 0, '085788387777', 5, 'Jl. Let. Jend. T.B. Simatupang Kav. 9 Kebagusan Pasar Minggu, Jakarta Selatan', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('The Trans Luxury Hotel Bandung', 'Luxury hotel in Bandung', 0, '085873488881', 6, 'Jl. Gatot Subroto No. 289, Bandung, Jawa Barat', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Padma Resort Ubud', 'Resort with rice field views in Ubud', 1, '086730111113', 7, 'Banjar Carik, Desa Puhu, Payangan, Gianyar, Bali', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Four Seasons Resort Bali at Sayan', 'Luxury resort in the heart of Bali', 0, '089997757712', 8, 'Sayan, Ubud, Bali', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('The Stones - Legian, Bali', 'Beachfront hotel in Legian, Bali', 1, '081263005888', 9, 'Jl. Raya Pantai Kuta, Banjar Legian Kelod, Legian, Bali', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Aryaduta Makassar', 'City hotel in Makassar', 1, '083822871111', 4, 'Jl. Somba Opu No. 297, Makassar, Sulawesi Selatan', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Hotel Tugu Malang', 'Hotel mewah dengan desain klasik Indonesia', 0, '0341363891', GETDATE(), 5, 'Jl. Tugu No. 3, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('The Shalimar Boutique Hotel', 'Hotel butik bintang 4 dengan taman tropis', 0, '0341550888', GETDATE(), 5, 'Jalan Salak No. 38-42, Oro Oro Dowo, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Hotel Santika Premiere Malang', 'Hotel bintang 4 dengan restoran dan kolam renang', 1, '0341405405', GETDATE(), 5, 'Jl. Letjen S. Parman No. 60, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Ijen Suites Resort & Convention', 'Hotel mewah dengan kolam renang dan spa', 1, '0341404888', GETDATE(), 5, 'Jalan Raya Kahuripan No. 16, Tlogomas, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Atria Hotel Malang', 'Hotel mewah dengan pemandangan pegunungan', 0, '0341402888', GETDATE(), 5, 'Jl. Letjen Sutoyo No.79, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Jambuluwuk Batu Resort', 'Resor bintang 4 dengan taman dan kolam renang', 0, '082172129020', GETDATE(), 5, 'Jl. Trunojoyo No.99, Oro Oro Ombo, Batu, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Aria Gajayana Hotel', 'Hotel mewah dengan pemandangan pegunungan', 0, '0341320188', GETDATE(), 5, 'Jl. Hayam Wuruk No. 5, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('The Batu Hotel & Villas', 'Hotel mewah dengan kolam renang dan restoran', 1, '0341512555', GETDATE(), 5, 'Jalan Raya Selecta No.1');


--* DATA REVIEWS *--
-- Data 1
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Hotel ini sangat menyenangkan!', 5, GETDATE(), 1, 1);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Pelayanan hotel sangat baik', 4, GETDATE(), 5, 1);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Kamar hotel sangat bersih dan nyaman', 5, GETDATE(), 6, 1);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Sangat puas dengan penginapan ini', 1, GETDATE(), 10, 1);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Sarapan paginya enak dan bervariasi', 1, GETDATE(), 11, 1);

-- Data 2
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Saya suka kamar mandinya yang luas', 4, GETDATE(), 1, 2);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Lokasi hotelnya sangat dekat dengan pusat kota', 4, GETDATE(), 5, 2);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Makanan di restoran hotelnya enak', 5, GETDATE(), 6, 2);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Pengalaman menginap yang menyenangkan', 5, GETDATE(), 10, 2);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Pelayanan receptionistnya ramah dan cepat', 5, GETDATE(), 11, 2);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Hotel yang direkomendasikan', 5, GETDATE(), 15, 2);

-- Data 3
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Kamar hotelnya luas dan bersih', 5, GETDATE(), 1, 3);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Sarapan paginya enak dan bergizi', 4, GETDATE(), 5, 3);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Fasilitas hotelnya lengkap', 5, GETDATE(), 6, 3);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Sangat puas dengan pelayanan hotelnya', 5, GETDATE(), 10, 3);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Harga yang sangat terjangkau', 5, GETDATE(), 11, 3);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Saya suka dengan suasana hotelnya yang tenang', 5, GETDATE(), 15, 3);

--DATA 4
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Hotel yang bagus untuk staycation', 5, GETDATE(), 1, 4);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Pemandangannya indah dari kamar', 4, GETDATE(), 5, 4);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Tempat yang pas untuk liburan keluarga', 5, GETDATE(), 6, 4);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Saya akan merekomendasikan hotel ini ke teman', 5, GETDATE(), 10, 4);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Sangat menyenangkan menginap di hotel ini', 5, GETDATE(), 11, 4);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Layanan hotelnya memuaskan', 5, GETDATE(), 15, 4);

-- DATA 5
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Kamar hotelnya bersih dan nyaman', 5, GETDATE(), 1, 5);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Lokasi hotelnya sangat dekat dengan pusat kota', 4, GETDATE(), 5, 5);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Pelayanan yang sangat baik', 5, GETDATE(), 6, 5);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Hotel yang cocok untuk perjalanan bisnis', 5, GETDATE(), 10, 5);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Saya suka kolam renangnya yang besar', 4, GETDATE(), 11, 5);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES
('Hotelnya direkomendasikan untuk liburan keluarga', 5, GETDATE(), 15, 5);



--* DATA FACILITIES *--
--DATA 1
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Single Room', 'Cozy and comfortable room with a single bed', 1, 'beds', '101', '2023-03-14 14:00:00', '2023-03-16 12:00:00', 500000, 700000, 1, NULL, 10, GETDATE(), 1, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Double Room', 'Spacious room with a double bed', 2, 'beds', '102', '2023-03-14 14:00:00', '2023-03-16 12:00:00', 700000, 900000, 1, NULL, 10, GETDATE(), 1, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Twin Room', 'Room with two single beds', 2, 'beds', '103', '2023-03-14 14:00:00', '2023-03-16 12:00:00', 600000, 800000, 1, NULL, 10, GETDATE(), 1, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Deluxe Room', 'Luxurious room with a king-size bed and a sofa', 3, 'beds', '104', '2023-03-14 14:00:00', '2023-03-16 12:00:00', 1000000, 1500000, 1, NULL, 10, GETDATE(), 1, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Restaurant', 'Fine dining restaurant with a wide selection of menu', 50, 'people', 'R101', '2023-03-14 08:00:00', '2023-03-15 22:00:00', 500000, 1000000, 2, NULL, 10, GETDATE(), 2, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Meeting Room', 'Meeting room with modern facilities and equipment', 30, 'people', 'M101', '2023-03-14 08:00:00', '2023-03-15 18:00:00', 750000, 1250000, 2, NULL, 10, GETDATE(), 3, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Gym', 'Fitness center with state-of-the-art equipment', NULL, 'people', 'G101', '2023-03-14 06:00:00', '2023-03-16 22:00:00', 100000, 150000, 3, NULL, 10, GETDATE(), 4, 1, 2);
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Swimming Pool', 'Outdoor swimming pool with a view of the city', NULL, 'people', 'SP101', '2023-03-14 08:00:00', '2023-03-16 18:00:00', 200000, 300000, 3, NULL, 10, GETDATE(), 6, 1, 2);


--DATA 2
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('Deluxe Room', 'A comfortable room with a king-size bed and a balcony', 2, 'beds', 'DR-101', '2023-04-01', '2023-04-07', 900000, 1000000, 1, NULL, NULL, 1, 2, 4, GETDATE());
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('Twin Room', 'A cozy room with two twin-size beds', 2, 'beds', 'TR-201', '2023-04-01', '2023-04-07', 700000, 800000, 1, NULL, NULL, 1, 2, 4, GETDATE());
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('Family Room', 'A spacious room with a king-size bed and two twin-size beds', 4, 'beds', 'FR-301', '2023-04-01', '2023-04-07', 1200000, 1400000, 1, NULL, NULL, 1, 2, 4, GETDATE());
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('Standard Room', 'A basic room with a queen-size bed', 2, 'beds', 'SR-401', '2023-04-01', '2023-04-07', 500000, 600000, 1, NULL, NULL, 1, 2, 4, GETDATE());
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('The Garden', 'A cozy restaurant with a beautiful garden view', 60, 'people', 'GDN-101', '2023-04-01', '2023-04-07', 5000000, 6000000, 1, NULL, NULL, 2, 2, 4, GETDATE());
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('The Spa', 'A relaxing spa with various treatments and facilities', 10, 'people', 'SPA-201', '2023-04-01', '2023-04-07', 15000000, 18000000, 1, NULL, NULL, 5, 2, 4, GETDATE());
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_expose_price, faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id, faci_modified_date)
VALUES
('The Meeting Room', 'A functional meeting room with modern amenities', 25, 'people', 'MTG-301', '2023-04-01', '2023-04-07', 3000000, 3500000, 1, NULL, NULL, 3, 2, 4, GETDATE());;


--DATA 3
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Deluxe Room', 'Spacious room with modern amenities', 2, 'beds', 'D01', '2023-03-14', '2023-03-16', 1000000, 1500000, NULL, 1, 10, 10, GETDATE(), 1, 3, 9);
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Superior Room', 'Cozy room with city view', 2, 'beds', 'D02', '2023-03-14', '2023-03-16', 900000, 1200000, NULL, 1, NULL, NULL, GETDATE(), 1, 3, 9);
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Pool Villa', 'Private villa with pool access', 2, 'people', 'PV01', '2023-03-14', '2023-03-16', 3000000, 3500000, NULL, 1, NULL, NULL, GETDATE(), 1, 3, 9);
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Meeting Room 1', 'Suitable for small meetings', 20, 'people', 'M01', '2023-03-14', '2023-03-16', 1500000, 2000000, NULL, 1, NULL, NULL, GETDATE(), 3, 3, 9);
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Gym', 'Fully equipped gym with personal trainer', 50, 'people', 'G01', '2023-03-14', '2023-03-16', 750000, 1000000, NULL, 1, 20, 10, GETDATE(), 4, 3, 9);
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Sauna', 'Relaxing sauna for your wellness', 10, 'people', 'S01', '2023-03-14', '2023-03-16', 300000, 400000, NULL, 1, NULL, NULL, GETDATE(), 6, 3, 9);
INSERT INTO Hotel.Facilities 
(faci_name, faci_description, faci_max_number, faci_measure_unit, faci_room_number, faci_startdate, faci_enddate, faci_low_price, faci_high_price, faci_rate_price, faci_expose_price, faci_discount, faci_tax_rate, faci_modified_date, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES 
('Ballroom', 'Elegant ballroom for your event', 100, 'people', 'B01', '2023-03-14', '2023-03-16', 10000000, 15000000, NULL, 1, 10, 10, GETDATE(), 7, 3, 9);


--* DATA Facility Photos *--
INSERT INTO Hotel.Facility_Photos (fapho_photo_filename, fapho_thumbnail_filename, fapho_original_filename, fapho_file_size, fapho_file_type, fapho_primary, fapho_url, fapho_modified_date, fapho_faci_id)
SELECT 'photo1.jpg', 'thumb1.jpg', 'orig1.jpg', 1024, 'jpg', 0, 'https://example.com/photo1.jpg', GETDATE(), faci_id FROM Hotel.Facilities
UNION ALL
SELECT 'photo2.jpg', 'thumb2.jpg', 'orig2.jpg', 2048, 'jpg', 0, 'https://example.com/photo2.jpg', GETDATE(), faci_id FROM Hotel.Facilities
UNION ALL
SELECT 'photo3.jpg', 'thumb3.jpg', 'orig3.jpg', 3072, 'jpg', 0, 'https://example.com/photo3.jpg', GETDATE(), faci_id FROM Hotel.Facilities;

DECLARE @fapho_id INT
DECLARE @fapho_faci_id INT

DECLARE curFacilityPhotos CURSOR FOR
SELECT fapho_id, fapho_faci_id
FROM Hotel.Facility_Photos

OPEN curFacilityPhotos

FETCH NEXT FROM curFacilityPhotos INTO @fapho_id, @fapho_faci_id

WHILE @@FETCH_STATUS = 0
BEGIN
    UPDATE Hotel.Facility_Photos
    SET fapho_primary = 0
    WHERE fapho_id = @fapho_id AND fapho_faci_id = @fapho_faci_id
    
    FETCH NEXT FROM curFacilityPhotos INTO @fapho_id, @fapho_faci_id
END

CLOSE curFacilityPhotos
DEALLOCATE curFacilityPhotos;


