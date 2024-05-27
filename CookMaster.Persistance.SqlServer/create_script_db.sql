--create database
 IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CookMasterDB')
  BEGIN
    CREATE DATABASE CookMasterDB
  END
GO
-- use crreated database
USE CookMasterDB
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Photos]    Script Date: 22.05.2024 20:15:08 ******/

CREATE TABLE [dbo].[Photos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRecipe] [int] NULL,
	[FileName] [nvarchar](50) NULL,
	[Data] [varbinary](max) NULL,
	[FilePath] [nvarchar](120) NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 22.05.2024 20:15:55 ******/

CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRecipe] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Category] [nvarchar](50) NULL,
	[Amount] [nvarchar](50) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Recipes]    Script Date: 22.05.2024 20:16:40 ******/

CREATE TABLE [dbo].[Recipes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMenu] [int] NULL,
	[IdUser] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[PrepareTime] [nchar](20) NULL,
	[MealCount] [int] NULL,
	[Rate] [float] NULL,
	[Popularity] [float] NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Steps]    Script Date: 22.05.2024 20:17:20 ******/

CREATE TABLE [dbo].[Steps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRecipe] [int] NULL,
	[Step_Num] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Steps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/****** Object:  Table [dbo].[UserMenu]    Script Date: 22.05.2024 20:18:12 ******/

CREATE TABLE [dbo].[UserMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[RecipeCategory] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 22.05.2024 20:19:46 ******/

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--- constraints
ALTER TABLE [dbo].[UserMenu]  WITH CHECK ADD  CONSTRAINT [FK_UserMenu_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserMenu] CHECK CONSTRAINT [FK_UserMenu_Users]
GO


ALTER TABLE [dbo].[Steps]  WITH CHECK ADD  CONSTRAINT [FK_Steps_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[Steps] CHECK CONSTRAINT [FK_Steps_Recipes]
GO

ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_UserMenu] FOREIGN KEY([IdMenu])
REFERENCES [dbo].[UserMenu] ([Id])
GO

ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_UserMenu]
GO

ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users]
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Recipes]
GO

ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Recipes]
GO





