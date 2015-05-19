CREATE TABLE [dbo].[SocialConnection]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[NetworkName] NVARCHAR(20) UNIQUE NOT NULL,
	[ProfileLink] NVARCHAR(100) NOT NULL,
	[MainImage] NVARCHAR(50),
	[HoverImage] NVARCHAR(50)
)
