--create database
 IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CookMasterDB')
  BEGIN
    CREATE DATABASE CookMasterDB
  END
GO
-- use crreated database
USE CookMasterDB
GO

/****** Object:  Table [dbo].[Users]    Script Date: 29.04.2024 14:21:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IdMenu] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[UserMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[RecipeCategory] [nvarchar](50) NULL,
	[IdMenuRecipe] [int] NULL,
 CONSTRAINT [PK_UserMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Recipes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PrepareTime] [nchar](10) NULL,
	[MealCount] [int] NULL,
	[Rate] [float] NULL,
	[Popularity] [float] NULL,
	[Description] [nvarchar](50) NULL,
	[IdFavourite] [int] NULL,
	[IdStepsRecipe] [int] NULL,
	[IdProductRecipe] [int] NULL,
	[IdPhoto] [int] NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Steps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Steps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Category] [nvarchar](50) NULL,
	[Amount] [nvarchar](50) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Photos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[Data] [varbinary](max) NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserMenu] FOREIGN KEY([IdMenu])
REFERENCES [dbo].[UserMenu] ([Id])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserMenu]
GO
-- table userMenu



ALTER TABLE [dbo].[UserMenu]  WITH CHECK ADD  CONSTRAINT [FK_UserMenu_Recipes] FOREIGN KEY([IdMenuRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[UserMenu] CHECK CONSTRAINT [FK_UserMenu_Recipes]
GO

ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Photos] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photos] ([Id])
GO

ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Photos]
GO

ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Products] FOREIGN KEY([IdProductRecipe])
REFERENCES [dbo].[Products] ([Id])
GO

ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Products]
GO

ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Steps] FOREIGN KEY([IdStepsRecipe])
REFERENCES [dbo].[Steps] ([Id])
GO

ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Steps]
GO

ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users] FOREIGN KEY([IdFavourite])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users]
GO




