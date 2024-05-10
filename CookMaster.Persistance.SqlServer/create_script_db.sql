--create database
 IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CookMasterDB')
  BEGIN
    CREATE DATABASE CookMasterDB
  END
GO
-- use crreated database
USE CookMasterDB
GO


/****** Object:  Table [dbo].[Recipes]    Script Date: 10.05.2024 12:57:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recipes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PrepareTime] [nchar](10) NULL,
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


/****** Object:  Table [dbo].[Users]    Script Date: 10.05.2024 12:59:05 ******/


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


/****** Object:  Table [dbo].[UserMenu]    Script Date: 10.05.2024 12:59:43 ******/


CREATE TABLE [dbo].[UserMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[RecipeCategory] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Products]    Script Date: 10.05.2024 13:00:16 ******/


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

/****** Object:  Table [dbo].[Steps]    Script Date: 10.05.2024 13:01:14 ******/

CREATE TABLE [dbo].[Steps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Steps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Photos]    Script Date: 10.05.2024 13:01:52 ******/


CREATE TABLE [dbo].[Photos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[Data] [varbinary](max) NULL,
	[FilePath] [nvarchar](120) NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  Table [dbo].[RecipesDetails]    Script Date: 10.05.2024 13:03:10 ******/


CREATE TABLE [dbo].[RecipesDetails](
	[IdRecipe] [int] NULL,
	[IdStep] [int] NULL,
	[IdProduct] [int] NULL,
	[IdPhoto] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RecipesDetails]  WITH CHECK ADD  CONSTRAINT [FK_RecipesDetails_Photos] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photos] ([Id])
GO

ALTER TABLE [dbo].[RecipesDetails] CHECK CONSTRAINT [FK_RecipesDetails_Photos]
GO

ALTER TABLE [dbo].[RecipesDetails]  WITH CHECK ADD  CONSTRAINT [FK_RecipesDetails_Products] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Products] ([Id])
GO

ALTER TABLE [dbo].[RecipesDetails] CHECK CONSTRAINT [FK_RecipesDetails_Products]
GO

ALTER TABLE [dbo].[RecipesDetails]  WITH CHECK ADD  CONSTRAINT [FK_RecipesDetails_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[RecipesDetails] CHECK CONSTRAINT [FK_RecipesDetails_Recipes]
GO

ALTER TABLE [dbo].[RecipesDetails]  WITH CHECK ADD  CONSTRAINT [FK_RecipesDetails_Steps] FOREIGN KEY([IdStep])
REFERENCES [dbo].[Steps] ([Id])
GO

ALTER TABLE [dbo].[RecipesDetails] CHECK CONSTRAINT [FK_RecipesDetails_Steps]
GO

/****** Object:  Table [dbo].[UserMenuRecipes]    Script Date: 10.05.2024 13:03:50 ******/


CREATE TABLE [dbo].[UserMenuRecipes](
	[IdUser] [int] NULL,
	[IdUserMenu] [int] NULL,
	[IdRecipe] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserMenuRecipes]  WITH CHECK ADD  CONSTRAINT [FK_UserMenuRecipes_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[UserMenuRecipes] CHECK CONSTRAINT [FK_UserMenuRecipes_Recipes]
GO

ALTER TABLE [dbo].[UserMenuRecipes]  WITH CHECK ADD  CONSTRAINT [FK_UserMenuRecipes_UserMenu] FOREIGN KEY([IdUserMenu])
REFERENCES [dbo].[UserMenu] ([Id])
GO

ALTER TABLE [dbo].[UserMenuRecipes] CHECK CONSTRAINT [FK_UserMenuRecipes_UserMenu]
GO

ALTER TABLE [dbo].[UserMenuRecipes]  WITH CHECK ADD  CONSTRAINT [FK_UserMenuRecipes_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserMenuRecipes] CHECK CONSTRAINT [FK_UserMenuRecipes_Users]
GO






