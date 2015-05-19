CREATE TABLE [dbo].[CompanyProfile]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Intro] NVARCHAR(800),
	[Title] NVARCHAR(50),
	[HomepageImage] NVARCHAR(50),
	[CompanyAddress] NVARCHAR(200) NOT NULL,
	[ContactNumber] NVARCHAR(20) NOT NULL
)
