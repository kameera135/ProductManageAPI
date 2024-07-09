/*
Wrote Date: 09 Jul 2024
Wrote by: Kameera Hemachandra
Modified Date: 09 Jul 2024
Modified By: Kameera Hemachandra

DataBase Name: PMS (Product Management System)
*/

BEGIN TRAN

USE PMS

IF OBJECT_ID('Products') IS NOT NULL BEGIN DROP TABLE Products END
IF OBJECT_ID('Users') IS NOT NULL BEGIN DROP TABLE Users END

CREATE TABLE Users
(
	UserID bigint IDENTITY(1,1) NOT NULL,
	UserName nvarchar(100) NOT NULL,
	PasswordHash nvarchar(255) NOT NULL,
	PasswordSalt nvarchar(25) NOT NULL,
	FirstName nvarchar(100) NULL,
	LastName nvarchar(100) NULL,
	Email nvarchar(150) NULL,
	Phone nvarchar(100) NULL,
	CreatedAt datetime NULL,
	UpdatedAt datetime NULL,
	DeletedAt datetime NULL,
	CreatedBy bigint NULL,
	UpdatedBy bigint NULL,
	DeletedBy bigint NULL,
)

CREATE TABLE Products
(
	ProductID bigint IDENTITY(1,1) NOT NULL,
	ProductName nvarchar(200) NOT NULL,
	Price decimal NOT NULL,
	IsActive bit DEFAULT 1 NOT NULL,
	CreatedAt datetime NULL,
	UpdatedAt datetime NULL,
	DeletedAt datetime NULL,
	CreatedBy bigint NULL,
	UpdatedBy bigint NULL,
	DeletedBy bigint NULL,
)

--Primary Keys
ALTER TABLE Users ADD CONSTRAINT PK_Users PRIMARY KEY (UserID);
ALTER TABLE Products ADD CONSTRAINT PK_Products PRIMARY KEY (ProductID);

--Foreign Keys

--Insert Valus
INSERT [dbo].[Users] ([UserName],[PasswordHash], [PasswordSalt], [FirstName], [LastName], [Email], [Phone],[CreatedAt], [UpdatedAt], [DeletedAt], [CreatedBy], [UpdatedBy], [DeletedBy]) VALUES (N'admin','39d0b09aa11dd4c072bcd449ad5757e05d16b2b206e7665532be9f114fcb7fc4592b9d1b3e61888f50b404ba8e885d42c7006e846c2de2f165e157c7fb6aa1d8', '25625732412409945593', N'Admin', N'User', N'adminuser@mail.com', N'982653368', GETDATE(), NULL, NULL,NULL,NULL, NULL)


COMMIT TRAN