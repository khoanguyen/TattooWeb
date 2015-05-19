CREATE TABLE [dbo].[Artist]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FullName] NVARCHAR(50) NOT NULL,
	[PhoneNo] NVARCHAR(20),
	[ArtistImage] NVARCHAR(100) NOT NULL,
	[NameKey] NVARCHAR(100) NOT NULL,
	[BriefIntro] NVARCHAR(200),
	[DefaultArtist] Bit Default 0,
)
