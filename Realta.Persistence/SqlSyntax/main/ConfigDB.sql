-- CREATE DATABASE WEBApiDbDemo;

-- USE WEBApiDbDemo;

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
  fapho_file_size smallint NOT NULL,
  fapho_file_type nvarchar(50) NOT NULL,
  fapho_primary BIT NULL CHECK(fapho_primary IN(0,1)),
  fapho_url nvarchar(255) NULL,
  fapho_modified_date datetime,
  -- FOREIGN KEY
  fapho_faci_id INT NOT NULL,
  CONSTRAINT fapho_faci_id_fk FOREIGN KEY (fapho_faci_id) REFERENCES Hotel.Facilities(faci_id) ON DELETE CASCADE ON UPDATE CASCADE
);
