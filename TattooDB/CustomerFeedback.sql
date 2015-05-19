CREATE TABLE [dbo].[CustomerFeedback]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CustomerName] NVARCHAR(50) NOT NULL DEFAULT 'Anonymous',
	[CustomerEmail] NVARCHAR(50),
	[CustomerPhone] NVARCHAR(20),
	[Message] NVARCHAR(200) NOT NULL,
	[SentDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[IsRead] BIT NOT NULL Default 0
)

GO 

CREATE NONCLUSTERED INDEX IX_FeedBack_CustomerName ON CustomerFeedback(CustomerName)

GO
