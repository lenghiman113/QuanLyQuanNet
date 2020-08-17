CREATE DATABASE QuanLyQuanNet
GO

USE QuanLyQuanNet
GO

-- Account
-- Customer
-- Playing hour
-- Service
-- Revenue

-- Functions : Add hour for a person
-- A customer have more than one hour
-- A customer have more than one service

CREATE TABLE account
(
	id INT IDENTITY PRIMARY KEY,
	username NVARCHAR(100) NOT NULL,
	password NVARCHAR(100) NOT NULL,
)
GO

CREATE TABLE customer
(
	id INT IDENTITY PRIMARY KEY,
	username NVARCHAR(100) NOT NULL,
	password NVARCHAR(100) NOT NULL DEFAULT '1',
	display_name NVARCHAR(100) NOT NULL DEFAULT 'ANONYMOUS',
	phoneNumber NVARCHAR(100) NOT NULL DEFAULT '0',
	email NVARCHAR(100) NOT NULL DEFAULT 'NONE',
)
GO

CREATE TABLE playingHour
(
	id INT IDENTITY PRIMARY KEY,
	hour_db TIME DEFAULT '01:00:00',
	id_customer INT NOT NULL,
	FOREIGN KEY(id_customer) REFERENCES customer(id)
)
GO


CREATE TABLE revenue
(
	id INT IDENTITY PRIMARY KEY,
	revenueNumber FLOAT NOT NULL DEFAULT '0'
)
GO

CREATE PROCEDURE SelectHourFromCustomer
@id_customer INT -- id from customer table
AS
BEGIN
	SELECT hour_db FROM customer , playingHour 
	WHERE customer.id = playingHour.id_customer AND customer.id = @id_customer
END
GO

CREATE PROCEDURE UpdateHourFromCustomer
@input_hour INT,
@id_hour INT
AS
BEGIN
	UPDATE playingHour
	SET hour_db = DATEADD(HOUR, @input_hour, hour_db)
	WHERE id = @id_hour
END
GO




CREATE PROCEDURE UpdateRevenue
@revenue INT
AS
BEGIN
	UPDATE revenue
	SET revenueNumber += @revenue 
END
GO

CREATE PROCEDURE PROC_insertAccount
@username NVARCHAR(100)
AS
BEGIN
	INSERT INTO customer(username)
	VALUES(@username)
END
GO



CREATE PROCEDURE PROC_insertHour_ForCustomerAccount
@id_customer INT
AS
BEGIN
	INSERT playingHour(id_customer)
	VALUES(@id_customer)
END
GO











-- Create an instance for account table 
INSERT INTO account(username, password)
VALUES('admin', 'admin')
GO

EXEC UpdateHourFromCustomer @input_hour = 2 , @id_hour = 2


/*
UPDATE playingHour 
SET hour_db = DATEADD(HOUR, 3, hour_db) 

SELECT hour_db FROM customer , playingHour WHERE customer.id = playingHour.id_customer AND customer.id = 1
GO
*/



-- EXEC SelectHourFromCustomer @id_customer = 1

-- SELECT * FROM customer , playingHour WHERE customer.id = playingHour.id
