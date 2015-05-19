/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [Credentials] ([UserName], [Password], [FullName]) Values
('billy411', 'whyUneedit?', 'Billy Truong')
GO

INSERT INTO [Artist] ([FullName], [PhoneNo], [ArtistImage],[NameKey],[DefaultArtist] ) Values
('Bill Truong','408-661-0236','billy.jpg','1-billy-truong', 1)
GO
INSERT INTO [Artist] ([FullName], [PhoneNo], [ArtistImage],[NameKey],[DefaultArtist] ) Values
('Khuong Nguyen', '123-456-7890', 'khuongnguyen.jpg', '2-khuong-nguyen', 0)
GO
INSERT INTO [Artist] ([FullName], [PhoneNo], [ArtistImage],[NameKey],[DefaultArtist] ) Values
('Khoa Nguyen', '321-654-0987', 'khoa.jpg', '3-khoa-nguyen', 0)

GO

INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('bluekoy.jpg', 1, 'Blue Koy', 1)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('cherryblossom.jpg', 1, 'Cherry Blossom', 1)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragon2.jpg', 1, 'Dragon 2', 1)
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragonsleeve005.jpg', 1, 'Dragon Sleeve', 1)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('bluekoy.jpg', 1, 'Blue Koy', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('cherryblossom.jpg', 1, 'Cherry Blossom',0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragon2.jpg', 1, 'Dragon 2', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragonsleeve005.jpg', 1, 'Dragon Sleeve', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('bluekoy.jpg', 1, 'Blue Koy', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('cherryblossom.jpg', 1, 'Cherry Blossom', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragon2.jpg', 1, 'Dragon 2', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragonsleeve005.jpg', 1, 'Dragon Sleeve', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('bluekoy.jpg', 1, 'Blue Koy', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('cherryblossom.jpg', 1, 'Cherry Blossom', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragon2.jpg', 1, 'Dragon 2', 1)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragonsleeve005.jpg', 1, 'Dragon Sleeve', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('bluekoy.jpg', 1, 'Blue Koy', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('cherryblossom.jpg', 1, 'Cherry Blossom', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragon2.jpg', 1, 'Dragon 2', 0)
GO
INSERT INTO ArtWork (ArtImage, ArtistId, ArtDesc,ShowOnIntroScreen) Values
('dragonsleeve005.jpg', 1, 'Dragon Sleeve', 0)


GO

INSERT INTO CompanyProfile (CompanyAddress, ContactNumber, HomepageImage, Intro, Title) Values
('San Jose California', '408-448-4350', 'newgeneration_tattoo.jpg', 
'Getting a tattoo is an excellent and bold form of personal expression. Whether or not it is your first tattoo, it is important to feel confident and comfortable with your artist in order to ensure the quality of work. Here at New Generation Tattoo, we use the newest technology and highest quality of ink to create your ideal artwork. With over ten years of diverse experience, each of our artists has his own distinctive personal style and specialties that is necessary to create the art you want. We also personally work with each of our clients to design their unique tattoo. We are proud of our clean and safe environment and are dedicated to making this an enjoyable experience for all of our valued clients. ',
'Welcome to New Generation Tattoo!')

GO

INSERT INTO SocialConnection (MainImage, HoverImage, NetworkName, ProfileLink) Values
('twitter.png','twitter_hover.png', 'twitter', 'http://www.twitter.com')
GO
INSERT INTO SocialConnection (MainImage, HoverImage, NetworkName, ProfileLink) Values
('facebook.png','facebook_hover.png', 'facebook', 'http://www.facebook.com')
GO
INSERT INTO SocialConnection (MainImage, HoverImage, NetworkName, ProfileLink) Values
('google.png','google_hover.png', 'google plus', 'http://plus.google.com')
GO