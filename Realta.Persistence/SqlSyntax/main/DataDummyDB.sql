
USE WEBApiDbDemo
--Dummy Data For SCHEMA Hotel

-- DBCC CHECKIDENT ('Hotel.Hotels', RESEED, 0);
-- DELETE FROM Hotel.Hotels
-- * DATA HOTEL * --
INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Grand Hyatt Jakarta', 'Luxury hotel in the heart of Jakarta', 1, '+62 21 29921234', 4, 'Jl. M. H. Thamrin No.30, Jakarta Pusat', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Aston Priority Simatupang Hotel & Conference Center', 'Contemporary hotel in South Jakarta', 0, '+62 21 78838777', 5, 'Jl. Let. Jend. T.B. Simatupang Kav. 9 Kebagusan Pasar Minggu, Jakarta Selatan', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('The Trans Luxury Hotel Bandung', 'Luxury hotel in Bandung', 0, '+62 22 87348888', 6, 'Jl. Gatot Subroto No. 289, Bandung, Jawa Barat', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Padma Resort Ubud', 'Resort with rice field views in Ubud', 1, '+62 361 3011111', 7, 'Banjar Carik, Desa Puhu, Payangan, Gianyar, Bali', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Four Seasons Resort Bali at Sayan', 'Luxury resort in the heart of Bali', 0, '+62 361 977577', 8, 'Sayan, Ubud, Bali', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('The Stones - Legian, Bali', 'Beachfront hotel in Legian, Bali', 1, '+62 361 3005888', 9, 'Jl. Raya Pantai Kuta, Banjar Legian Kelod, Legian, Bali', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_addr_id, hotel_addr_description, hotel_modified_date)
VALUES ('Aryaduta Makassar', 'City hotel in Makassar', 1, '+62 411 871111', 4, 'Jl. Somba Opu No. 297, Makassar, Sulawesi Selatan', GETDATE());

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Hotel Tugu Malang', 'Hotel mewah dengan desain klasik Indonesia', 0, '(0341) 363891', GETDATE(), 5, 'Jl. Tugu No. 3, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('The Shalimar Boutique Hotel', 'Hotel butik bintang 4 dengan taman tropis', 0, '(0341) 550888', GETDATE(), 5, 'Jalan Salak No. 38-42, Oro Oro Dowo, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Hotel Santika Premiere Malang', 'Hotel bintang 4 dengan restoran dan kolam renang', 1, '(0341) 405405', GETDATE(), 5, 'Jl. Letjen S. Parman No. 60, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Ijen Suites Resort & Convention', 'Hotel mewah dengan kolam renang dan spa', 1, '(0341) 404888', GETDATE(), 5, 'Jalan Raya Kahuripan No. 16, Tlogomas, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Atria Hotel Malang', 'Hotel mewah dengan pemandangan pegunungan', 0, '(0341) 402888', GETDATE(), 5, 'Jl. Letjen Sutoyo No.79, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Jambuluwuk Batu Resort', 'Resor bintang 4 dengan taman dan kolam renang', 0, '(0341) 596333', GETDATE(), 5, 'Jl. Trunojoyo No.99, Oro Oro Ombo, Batu, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('Aria Gajayana Hotel', 'Hotel mewah dengan pemandangan pegunungan', 0, '(0341) 320188', GETDATE(), 5, 'Jl. Hayam Wuruk No. 5, Klojen, Malang');

INSERT INTO Hotel.Hotels (hotel_name, hotel_description, hotel_status, hotel_phonenumber, hotel_modified_date, hotel_addr_id, hotel_addr_description)
VALUES ('The Batu Hotel & Villas', 'Hotel mewah dengan kolam renang dan restoran', 1, '(0341) 512555', GETDATE(), 5, 'Jalan Raya Selecta No.1');

SELECT * FROM Hotel.Hotels;


SELECT * FROM Users.user_roles WHERE usro_role_id IN (2,4)
-- THIS DUMMY FOR Facility_User 2,4,7,9,12,14,
-- DELETE FROM Hotel.Facilities
-- DBCC CHECKIDENT ('Hotel.Facilities', RESEED, 0);

-- THIS DUMMY FOR User_reviews 1,5,6,10,11,15
-- DELETE FROM Hotel.Hotel_reviews
-- DBCC CHECKIDENT ('Hotel.Hotel_Reviews', RESEED, 0);

SELECT * FROM Master.category_group

INSERT INTO Hotel.Hotel_Reviews (hore_user_review, hore_rating, hore_created_on, hore_user_id, hore_hotel_id)
VALUES 
('Pengalaman menginap di hotel ini sangat menyenangkan.', 5, '2022-01-01', 1, 1);

SELECT * FROM Hotel.Facilities

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


SELECT * FROM Hotel.Hotels
SELECT * FROM Hotel.Hotel_Reviews
SELECT * FROM Hotel.Facilities
SELECT * FROM Hotel.facility_price_history


-- DELETE FROM Hotel.Facility_Photos
-- DELETE FROM Hotel.Facilities
-- DELETE FROM Hotel.facility_price_history
-- DBCC CHECKIDENT ('Hotel.Facilities', RESEED, 0);
-- DBCC CHECKIDENT ('Hotel.facility_price_history', RESEED, 0);

ALTER TABLE Hotel.facility_price_history
ALTER COLUMN 
  --faph_discount smallmoney null;
  faph_tax_rate smallmoney null;

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





SELECT * FROM Hotel.Facility_Price_History

-- DELETE FROM Hotel.Facility_Price_History
-- DBCC CHECKIDENT ('Hotel.Facility_Price_History', RESEED, 0);


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




SELECT * FROM Hotel.Facility_Photos 


DELETE FROM Hotel.Facility_Photos
DBCC CHECKIDENT ('Hotel.Facility_Photos', RESEED, 0);






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
