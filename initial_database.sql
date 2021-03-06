USE [SocketApplication]
GO
/****** Object:  Table [dbo].[user]    Script Date: 11/27/2016 10:49:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[username] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[user] ([username], [password]) VALUES (N'test', N'1234')
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 11/27/2016 10:49:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetUser]
	@username varchar(255)
as

	set nocount on;
	select username, [password]
	from [user]
	where lower(username) = lower(@username)


GO
