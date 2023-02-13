
USE WEBApiDbDemo
--Dummy Data For SCHEMA Hotel

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_rating_star, hotel_phonenumber, hotel_modified_date, hotel_addr_id)
VALUES
('Hotel Amaris Palembang', 'Hotel bintang 3 dengan fasilitas yang lengkap dan modern di Palembang', 3, '+62 823 3456 7891', '2022-01-01', 1),
('Grand Clarion Hotel Palembang', 'Hotel bintang 4 dengan kamar yang luas dan nyaman di Palembang', 4, '+62 823 1234 5678', '2022-02-01', 2),
('Aston Hotel Palembang', 'Hotel bintang 5 dengan fasilitas spa dan kolam renang di Palembang', 5, '+62 823 9012 3456', '2022-03-01', 3),
('Hotel Santika Palembang', 'Hotel bintang 3 dengan fasilitas kelas atas di Palembang', 3, '+62 823 7890 1234', '2022-04-01', 3),
('Ibis Hotel Palembang', NULL, 3, '+62 823 4567 8901', '2022-05-01', 2),
('Grand Mercure Hotel Palembang', 'Hotel bintang 4 dengan fasilitas mewah di Palembang', 4, '+62 823 1234 5679', '2022-06-01', 5),
('Marriott Hotel Palembang', 'Hotel bintang 5 dengan fasilitas spa dan fitness center di Palembang', 5, '+62 823 9012 3457', '2022-07-01', 1),
('Zest Hotel Palembang', 'Hotel bintang 3 dengan desain modern dan nyaman di Palembang', NULL, '+62 823 7890 1235', '2022-08-01', 4),
('The Westin Hotel Palembang', 'Hotel bintang 4 dengan fasilitas kelas atas di Palembang', 4, '+62 823 4567 8902', '2022-09-01', 5),
('Swiss-Belhotel Palembang', 'Hotel bintang 5 dengan fasilitas mewah di Palembang', 5, '+62 823 1234 5680', NULL, 4)
SELECT*FROM Hotel.Hotels

SELECT * FROM Users.user_roles WHERE usro_role_id IN (2,4)
-- THIS DUMMY FOR Facility_User 2,4,7,9,12,14,
-- DELETE FROM Hotel.Facilities
-- DBCC CHECKIDENT ('Hotel.Facilities', RESEED, 0);

SELECT * FROM Hotel.Facilities

--DATA 1
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, 
faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_rate_price, 
faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Pool', 'Outdoor pool with sun loungers and parasols', 100, 'people', 'POOL01', '2022-01-01', '2022-12-31', 50000, 100000, 75000, 25000, 10000, 6, 1, 2); -- POOL

--Checker Trigger
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, 
faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_rate_price, 
faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Restaurant', NULL, 100, 'people', 'REST01', '2022-01-01', '2022-12-31', 40000, 80000, 60000, 25000, 10000, 2, 1, 2) -- RESTAURANT
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, 
faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_rate_price, 
faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Gym', 'Fully equipped gym with treadmills stationary bikes, and weights', 50, 'people', 'GYM01', '2022-01-01', '2022-12-31', 30000, 50000, 40000, 25000, 10000, 4, 1, 2) -- GYM
,('Metting Room', 'Meeting room Luxury, and body treatments', NULL, 'people', 'MTG01', '2022-01-01', '2022-12-31', 80000, 120000, 100000, 25000, 10000, 3, 1, 2) -- MEETING
,('Deluxe Room', 'Kamar luas dengan fasilitas mewah, termasuk kamar mandi pribadi dengan shower dan bathtub', 2, NULL, 'DLR01', '2022-01-01', '2022-01-30', 200000, 250000, 230000, 25000, 10000, 1, 1, 2)
,('Superior Room', 'Kamar standar dengan fasilitas lengkap, termasuk kamar mandi pribadi dengan shower', 2,'beds', 'SPR01', '2022-01-01', '2022-01-30', 150000, 180000, 160000, NULL, 10000, 1, 1, 2)
,('Family Room', 'Kamar untuk keluarga, dengan 2 tempat tidur single dan 1 tempat tidur double, serta fasilitas lengkap', 4,'beds', 'FMR01', '2022-01-01', '2022-01-30', 250000, 300000, 270000, 25000, NULL, 1, 1, 2)
,('Standard Room', 'Kamar standar dengan fasilitas sederhana, termasuk kamar mandi pribadi dengan shower', 2,'beds', 'STR01','2022-01-01', '2022-01-30', 100000, 125000, 115000, 25000, 10000, 1, 1, 2)
,('Double Room', 'Kamar dengan 2 tempat tidur single, serta fasilitas lengkap', 2,'beds', 'DBR01', '2022-01-01', '2022-01-30', 150000, 175000, 160000, 25000, 10000, 1, 1, 2)


--DATA 2
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, 
faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_rate_price, 
faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Restaurant', 'Fine dining restaurant serving international cuisine', 100, 'people', 'REST02', '2022-01-01', '2022-12-31', 40000, 80000, 60000, 25000, 10000, 2, 2, 4) -- RESTAURANT
,('Pool', 'Outdoor pool with sun loungers and parasols', 100, 'people', 'POOL02', '2022-01-01', '2022-12-31', 50000, 100000, 75000, 25000, 10000, 6, 2, 4) -- POOL
,('Gym', 'Fully equipped gym with treadmills, stationary bikes, and weights', 50, 'people', 'GYM02', '2022-01-01', '2022-12-31',20000, 30000, 25000, 25000, 10000, 4, 2, 4) -- GYM
,('Deluxe Room', 'Kamar luas dengan fasilitas mewah, termasuk kamar mandi pribadi dengan shower dan bathtub', 2, 'beds', 'DLR02', '2022-01-01', '2022-01-30', 200000, 250000, 230000, 25000, 10000, 1, 2, 4)
,('Superior Room', 'Kamar standar dengan fasilitas lengkap, termasuk kamar mandi pribadi dengan shower', 2, 'beds', 'SPR02', '2022-01-01', '2022-01-30', 150000, 180000, 160000, 25000, 10000, 1, 2, 4)
,('Family Room', 'Kamar untuk keluarga, dengan 2 tempat tidur single dan 1 tempat tidur double, serta fasilitas lengkap', 4, 'beds', 'FMR02', '2022-01-01', '2022-01-30', 250000, 300000, 270000, 25000, 10000, 1, 2, 4)
,('Standard Room', 'Kamar standar dengan fasilitas sederhana, termasuk kamar mandi pribadi dengan shower', 2, 'beds', 'STR02', '2022-01-01', '2022-01-30', 100000, 125000, 115000, 25000, 10000, 1, 2, 4)
,('Double Room', 'Kamar dengan 2 tempat tidur single, serta fasilitas lengkap', 2, 'beds', 'DBR02', '2022-01-01', '2022-01-30', 150000, 175000, 160000, 25000, 10000, 1, 2, 4)


--DATA 3
INSERT INTO Hotel.Facilities (faci_name, faci_description, faci_max_number, faci_measure_unit, 
faci_room_number, faci_startdate, faci_endate, faci_low_price, faci_high_price, faci_rate_price, 
faci_discount, faci_tax_rate, faci_cagro_id, faci_hotel_id, faci_user_id)
VALUES
('Restaurant', 'Fine dining restaurant serving international cuisine', 100, 'people', 'REST03', '2022-01-01', '2022-12-31', 40000, 80000, 60000, 25000, 10000, 2, 3, 7) -- RESTAURANT
,('Swimming Pool', 'Our hotel has a large outdoor swimming pool with a depth of 1.5 meters', 50, 'people', 'POOL3', '2022-01-01', '2022-12-31', 50000, 100000, 75000, 25000, 10000, 6, 3, 7) -- SWIMING POOL
,('Fitness Center', 'Our hotel has a fully equipped fitness center open 24 hours a day', 30, 'people', 'FIT3', '2022-01-01', '2022-12-31', 30000, 50000, 40000, 25000, 10000, 4, 3, 7) -- GYM
,('Aula', 'Our hotel has a traditional Aula that can accommodate up to 10 people', 10, 'people', 'AULA3', '2022-01-01', '2022-12-31', 25000, 35000, 30000, 25000, 10000, 1, 3, 7) -- AULA
,('Deluxe Room', 'Kamar luas dengan fasilitas mewah, termasuk kamar mandi pribadi dengan shower dan bathtub', 2, 'beds', 'DLR03', '2022-01-01', '2022-01-30', 200000, 250000, 230000, 25000, 10000, 1, 3, 7)
,('Superior Room', 'Kamar standar dengan fasilitas lengkap, termasuk kamar mandi pribadi dengan shower', 2, 'beds', 'SPR03', '2022-01-01', '2022-01-30', 150000, 180000, 160000, 25000, 10000, 1, 3, 7)
,('Family Room', 'Kamar untuk keluarga, dengan 2 tempat tidur single dan 1 tempat tidur double, serta fasilitas lengkap', 4, 'beds', 'FMR03', '2022-01-01', '2022-01-30', 250000, 300000, 270000, 25000, 10000, 1, 3, 7)
,('Standard Room', 'Kamar standar dengan fasilitas sederhana, termasuk kamar mandi pribadi dengan shower', 2, 'beds', 'STR03', '2022-01-01', '2022-01-30', 100000, 125000, 115000, 25000, 10000, 1, 3, 7)
,('Double Room', 'Kamar dengan 2 tempat tidur single, serta fasilitas lengkap', 2, 'beds', 'DBR03', '2022-01-01', '2022-01-30', 150000, 175000, 160000, 25000, 10000, 1, 3, 7)



SELECT * FROM Hotel.Facility_Price_History

-- DELETE FROM Hotel.Facility_Price_History
-- DBCC CHECKIDENT ('Hotel.Facility_Price_History', RESEED, 0);


-- THIS DUMMY FOR User_reviews 1,5,6,10,11,15
-- DELETE FROM Hotel.Hotel_reviews
-- DBCC CHECKIDENT ('Hotel.Hotel_Reviews', RESEED, 0);

INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES 
('Pengalaman menginap di hotel ini sangat menyenangkan.', 5, '2022-01-01', 1, 1);

-- THIS CHECKER FOR TRIGGER
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES 
('Saya sangat puas dengan pelayanan di hotel ini.', 4, '2022-01-02', 6, 1);
INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES 
('Kamar yang kami tempati cukup luas dan bersih, tapi ada beberapa masalah dengan AC yang berisik.', 3, '2022-01-03', 6, 1), 
('Deskripsi ini sengaja di buat untuk pengecekan nilai NULL natinya', 1, NULL, 10, 1);
SELECT * FROM Hotel.Hotel_Reviews

SELECT * FROM Users.user_roles WHERE usro_role_id IN (2,4)

SELECT * FROM Hotel.Facility_Photos










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
SELECT*FROM Master.category_group
ORDER BY cagro_id ASC

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
SELECT*FROM Master.Address
ORDER BY addr_id ASC
