USE [master]
GO
/****** Object:  Database [photo_manager1]    Script Date: 01/13/2015 19:34:33 ******/
CREATE DATABASE [photo_manager1] ON  PRIMARY 
( NAME = N'photo_manager1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLSERVER2008\MSSQL\DATA\photo_manager1.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'photo_manager1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLSERVER2008\MSSQL\DATA\photo_manager1_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [photo_manager1] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [photo_manager1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [photo_manager1] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [photo_manager1] SET ANSI_NULLS OFF
GO
ALTER DATABASE [photo_manager1] SET ANSI_PADDING OFF
GO
ALTER DATABASE [photo_manager1] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [photo_manager1] SET ARITHABORT OFF
GO
ALTER DATABASE [photo_manager1] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [photo_manager1] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [photo_manager1] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [photo_manager1] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [photo_manager1] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [photo_manager1] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [photo_manager1] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [photo_manager1] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [photo_manager1] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [photo_manager1] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [photo_manager1] SET  DISABLE_BROKER
GO
ALTER DATABASE [photo_manager1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [photo_manager1] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [photo_manager1] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [photo_manager1] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [photo_manager1] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [photo_manager1] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [photo_manager1] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [photo_manager1] SET  READ_WRITE
GO
ALTER DATABASE [photo_manager1] SET RECOVERY FULL
GO
ALTER DATABASE [photo_manager1] SET  MULTI_USER
GO
ALTER DATABASE [photo_manager1] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [photo_manager1] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'photo_manager1', N'ON'
GO
USE [photo_manager1]
GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 01/13/2015 19:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL,
 CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error] 
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01/13/2015 19:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 01/13/2015 19:34:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorsXml]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO
GO
/****** Object:  Table [dbo].[Album]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[DateCreated] [datetimeoffset](7) NULL,
	[DateModified] [datetimeoffset](7) NULL,
	[Url] [nvarchar](512) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NOT NULL,
	[ImageSize] [nvarchar](50) NOT NULL,
	[ImageType] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[DateCreated] [datetimeoffset](7) NULL,
	[PlaceCreated] [nvarchar](50) NULL,
	[CameraModel] [nvarchar](50) NULL,
	[FocalLengthOfTheLens] [nvarchar](50) NULL,
	[Diaphragm] [nvarchar](50) NULL,
	[ShutterSpeed] [nvarchar](50) NULL,
	[ISO] [nvarchar](50) NULL,
	[FlashInUse] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SearchPhoto]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchPhoto]

@Name [nvarchar](20)

AS
BEGIN

SELECT p.Id FROM Photo p
WHERE p.Name LIKE '%' + @Name + '%'

END
GO
/****** Object:  Table [dbo].[AlbumPhoto]    Script Date: 01/13/2015 19:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlbumPhoto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[PhotoId] [int] NOT NULL,
	[IsTitlePhoto] [bit] NULL,
 CONSTRAINT [PK_AlbumPhoto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_ELMAH_Error_ErrorId]    Script Date: 01/13/2015 19:34:34 ******/
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
/****** Object:  ForeignKey [FK_User_Role]    Script Date: 01/13/2015 19:34:34 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  ForeignKey [FK_Album_User]    Script Date: 01/13/2015 19:34:37 ******/
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_User]
GO
/****** Object:  ForeignKey [FK_Photo_User]    Script Date: 01/13/2015 19:34:37 ******/
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_User]
GO
/****** Object:  ForeignKey [FK_AlbumPhoto_Album]    Script Date: 01/13/2015 19:34:37 ******/
ALTER TABLE [dbo].[AlbumPhoto]  WITH CHECK ADD  CONSTRAINT [FK_AlbumPhoto_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Album] ([Id])
GO
ALTER TABLE [dbo].[AlbumPhoto] CHECK CONSTRAINT [FK_AlbumPhoto_Album]
GO
/****** Object:  ForeignKey [FK_AlbumPhoto_Photo]    Script Date: 01/13/2015 19:34:37 ******/
ALTER TABLE [dbo].[AlbumPhoto]  WITH CHECK ADD  CONSTRAINT [FK_AlbumPhoto_Photo] FOREIGN KEY([PhotoId])
REFERENCES [dbo].[Photo] ([Id])
GO
ALTER TABLE [dbo].[AlbumPhoto] CHECK CONSTRAINT [FK_AlbumPhoto_Photo]
GO
