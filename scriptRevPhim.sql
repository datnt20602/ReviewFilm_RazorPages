USE [master]
GO
/****** Object:  Database [review_phim_prn221]    Script Date: 3/12/2024 2:23:04 PM ******/
CREATE DATABASE [review_phim_prn221]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'review_phim_prn221', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.TIENDAT\MSSQL\DATA\review_phim_prn221.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'review_phim_prn221_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.TIENDAT\MSSQL\DATA\review_phim_prn221_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [review_phim_prn221] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [review_phim_prn221].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [review_phim_prn221] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [review_phim_prn221] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [review_phim_prn221] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [review_phim_prn221] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [review_phim_prn221] SET ARITHABORT OFF 
GO
ALTER DATABASE [review_phim_prn221] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [review_phim_prn221] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [review_phim_prn221] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [review_phim_prn221] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [review_phim_prn221] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [review_phim_prn221] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [review_phim_prn221] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [review_phim_prn221] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [review_phim_prn221] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [review_phim_prn221] SET  ENABLE_BROKER 
GO
ALTER DATABASE [review_phim_prn221] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [review_phim_prn221] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [review_phim_prn221] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [review_phim_prn221] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [review_phim_prn221] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [review_phim_prn221] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [review_phim_prn221] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [review_phim_prn221] SET RECOVERY FULL 
GO
ALTER DATABASE [review_phim_prn221] SET  MULTI_USER 
GO
ALTER DATABASE [review_phim_prn221] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [review_phim_prn221] SET DB_CHAINING OFF 
GO
ALTER DATABASE [review_phim_prn221] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [review_phim_prn221] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [review_phim_prn221] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [review_phim_prn221] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'review_phim_prn221', N'ON'
GO
ALTER DATABASE [review_phim_prn221] SET QUERY_STORE = OFF
GO
USE [review_phim_prn221]
GO
/****** Object:  Table [dbo].[feedback]    Script Date: 3/12/2024 2:23:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedback](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[comment] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
	[grade] [float] NULL,
	[movie_id] [int] NULL,
	[updated_at] [datetime] NULL,
	[user_id] [int] NULL,
 CONSTRAINT [PK__feedback__3213E83F6C7B82EA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movies]    Script Date: 3/12/2024 2:23:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movies](
	[movie_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [ntext] NULL,
	[director] [nvarchar](255) NULL,
	[genre] [nvarchar](255) NULL,
	[image] [nvarchar](255) NULL,
	[movie_name] [nvarchar](255) NULL,
	[release_date] [datetime] NULL,
	[average_grade] [float] NULL,
	[user_id] [int] NULL,
 CONSTRAINT [PK__movies__83CDF7497948CC7D] PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reply]    Script Date: 3/12/2024 2:23:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reply](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[content] [nvarchar](255) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[feedback_id] [int] NULL,
	[replied_by_user_id] [int] NULL,
 CONSTRAINT [PK__reply__3213E83F2415D26A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[url]    Script Date: 3/12/2024 2:23:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[url](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[movie_id] [int] NULL,
	[url_value] [varchar](255) NULL,
	[user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 3/12/2024 2:23:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[password] [varchar](255) NULL,
	[active] [bit] NOT NULL,
	[codeActive] [varchar](255) NULL,
 CONSTRAINT [PK__user__B9BE370F922705D8] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[feedback] ADD  CONSTRAINT [DF__feedback__commen__32E0915F]  DEFAULT (NULL) FOR [comment]
GO
ALTER TABLE [dbo].[feedback] ADD  CONSTRAINT [DF__feedback__create__33D4B598]  DEFAULT (NULL) FOR [created_at]
GO
ALTER TABLE [dbo].[feedback] ADD  CONSTRAINT [DF__feedback__grade__34C8D9D1]  DEFAULT (NULL) FOR [grade]
GO
ALTER TABLE [dbo].[feedback] ADD  CONSTRAINT [DF__feedback__movie___35BCFE0A]  DEFAULT (NULL) FOR [movie_id]
GO
ALTER TABLE [dbo].[feedback] ADD  CONSTRAINT [DF__feedback__update__36B12243]  DEFAULT (NULL) FOR [updated_at]
GO
ALTER TABLE [dbo].[feedback] ADD  CONSTRAINT [DF__feedback__user_i__37A5467C]  DEFAULT (NULL) FOR [user_id]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__director__29572725]  DEFAULT (NULL) FOR [director]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__genre__2A4B4B5E]  DEFAULT (NULL) FOR [genre]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__image__2B3F6F97]  DEFAULT (NULL) FOR [image]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__movie_na__2C3393D0]  DEFAULT (NULL) FOR [movie_name]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__release___2D27B809]  DEFAULT (NULL) FOR [release_date]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__average___2E1BDC42]  DEFAULT (NULL) FOR [average_grade]
GO
ALTER TABLE [dbo].[movies] ADD  CONSTRAINT [DF__movies__user_id__2F10007B]  DEFAULT (NULL) FOR [user_id]
GO
ALTER TABLE [dbo].[reply] ADD  CONSTRAINT [DF__reply__content__3C69FB99]  DEFAULT (NULL) FOR [content]
GO
ALTER TABLE [dbo].[reply] ADD  CONSTRAINT [DF__reply__created_a__3D5E1FD2]  DEFAULT (NULL) FOR [created_at]
GO
ALTER TABLE [dbo].[reply] ADD  CONSTRAINT [DF__reply__updated_a__3E52440B]  DEFAULT (NULL) FOR [updated_at]
GO
ALTER TABLE [dbo].[reply] ADD  CONSTRAINT [DF__reply__feedback___3F466844]  DEFAULT (NULL) FOR [feedback_id]
GO
ALTER TABLE [dbo].[reply] ADD  CONSTRAINT [DF__reply__replied_b__403A8C7D]  DEFAULT (NULL) FOR [replied_by_user_id]
GO
ALTER TABLE [dbo].[url] ADD  DEFAULT (NULL) FOR [movie_id]
GO
ALTER TABLE [dbo].[url] ADD  DEFAULT (NULL) FOR [url_value]
GO
ALTER TABLE [dbo].[url] ADD  DEFAULT (NULL) FOR [user_id]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF__user__email__24927208]  DEFAULT (NULL) FOR [email]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF__user__name__25869641]  DEFAULT (NULL) FOR [name]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF__user__password__267ABA7A]  DEFAULT (NULL) FOR [password]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [FK__feedback__movie___398D8EEE] FOREIGN KEY([movie_id])
REFERENCES [dbo].[movies] ([movie_id])
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [FK__feedback__movie___398D8EEE]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [FK__feedback__user_i__38996AB5] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [FK__feedback__user_i__38996AB5]
GO
ALTER TABLE [dbo].[movies]  WITH CHECK ADD  CONSTRAINT [FK__movies__user_id__300424B4] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[movies] CHECK CONSTRAINT [FK__movies__user_id__300424B4]
GO
ALTER TABLE [dbo].[reply]  WITH CHECK ADD  CONSTRAINT [FK__reply__feedback___412EB0B6] FOREIGN KEY([feedback_id])
REFERENCES [dbo].[feedback] ([id])
GO
ALTER TABLE [dbo].[reply] CHECK CONSTRAINT [FK__reply__feedback___412EB0B6]
GO
ALTER TABLE [dbo].[reply]  WITH CHECK ADD  CONSTRAINT [FK__reply__replied_b__4222D4EF] FOREIGN KEY([replied_by_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[reply] CHECK CONSTRAINT [FK__reply__replied_b__4222D4EF]
GO
ALTER TABLE [dbo].[url]  WITH CHECK ADD  CONSTRAINT [FK__url__movie_id__47DBAE45] FOREIGN KEY([movie_id])
REFERENCES [dbo].[movies] ([movie_id])
GO
ALTER TABLE [dbo].[url] CHECK CONSTRAINT [FK__url__movie_id__47DBAE45]
GO
ALTER TABLE [dbo].[url]  WITH CHECK ADD  CONSTRAINT [FK__url__user_id__48CFD27E] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[url] CHECK CONSTRAINT [FK__url__user_id__48CFD27E]
GO
USE [master]
GO
ALTER DATABASE [review_phim_prn221] SET  READ_WRITE 
GO
