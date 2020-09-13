USE [master]
GO
/****** Object:  Database [FaseIpc2_201700841]    Script Date: 12/09/2020 22:02:03 ******/
CREATE DATABASE [FaseIpc2_201700841]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FaseIpc2_201700841', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FaseIpc2_201700841.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FaseIpc2_201700841_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FaseIpc2_201700841_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FaseIpc2_201700841] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FaseIpc2_201700841].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FaseIpc2_201700841] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET ARITHABORT OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FaseIpc2_201700841] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FaseIpc2_201700841] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FaseIpc2_201700841] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FaseIpc2_201700841] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FaseIpc2_201700841] SET  MULTI_USER 
GO
ALTER DATABASE [FaseIpc2_201700841] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FaseIpc2_201700841] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FaseIpc2_201700841] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FaseIpc2_201700841] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FaseIpc2_201700841] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FaseIpc2_201700841] SET QUERY_STORE = OFF
GO
USE [FaseIpc2_201700841]
GO
/****** Object:  Table [dbo].[Archivo]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[color] [varchar](255) NULL,
	[columna] [varchar](255) NULL,
	[fila] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cargar/Guardar]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargar/Guardar](
	[Id_partida] [nchar](10) NOT NULL,
	[NombreUsuario] [nchar](10) NOT NULL,
	[Id_versus] [nchar](10) NULL,
	[Id_solitario] [nchar](10) NULL,
 CONSTRAINT [PK_Cargar/Guardar] PRIMARY KEY CLUSTERED 
(
	[Id_partida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleModo]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleModo](
	[DetalleModo] [nchar](10) NOT NULL,
	[Id_versus] [nchar](10) NULL,
	[Id_solitario] [nchar](10) NULL,
	[NombreTorneo] [nchar](10) NULL,
 CONSTRAINT [PK_DetalleModo] PRIMARY KEY CLUSTERED 
(
	[DetalleModo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallePartida]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePartida](
	[Detalle] [nchar](10) NOT NULL,
	[Ganador] [nchar](10) NULL,
	[Perdedor] [nchar](10) NULL,
	[Partida] [nchar](10) NULL,
	[Movimiento] [nchar](10) NULL,
	[Empate] [nchar](10) NULL,
 CONSTRAINT [PK_DetallePartida] PRIMARY KEY CLUSTERED 
(
	[Detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invitacion]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitacion](
	[Id_invitacion] [nchar](10) NOT NULL,
	[NombreUsuario] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Invitacion] PRIMARY KEY CLUSTERED 
(
	[Id_invitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modo]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modo](
	[NombreUsuario] [nchar](10) NULL,
	[DetalleModo] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participante]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participante](
	[NombreTorneo] [nchar](10) NULL,
	[NombreUsuario] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solitario]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solitario](
	[Id_solitario] [nchar](10) NOT NULL,
	[Detalle] [nchar](10) NULL,
 CONSTRAINT [PK_Solitario] PRIMARY KEY CLUSTERED 
(
	[Id_solitario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Torneo]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Torneo](
	[NombreTorneo] [nchar](10) NOT NULL,
	[Detalle] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Torneo] PRIMARY KEY CLUSTERED 
(
	[NombreTorneo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[NombreUsuario] [nchar](10) NOT NULL,
	[Nombre] [nchar](30) NOT NULL,
	[Apellido] [nchar](30) NOT NULL,
	[Contra] [nchar](10) NOT NULL,
	[FechaNac] [smalldatetime] NOT NULL,
	[Pais] [nchar](20) NOT NULL,
	[Correo] [nchar](35) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Versus]    Script Date: 12/09/2020 22:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Versus](
	[Id_versus] [nchar](10) NOT NULL,
	[Id_invitacion] [nchar](10) NOT NULL,
	[Detalle] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Versus] PRIMARY KEY CLUSTERED 
(
	[Id_versus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Archivo] ON 

INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (61, N'blanco', N'D', 4)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (62, N'negro', N'E', 4)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (63, N'blanco', N'D', 5)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (64, N'blanco', N'E', 5)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (65, N'negro', N'F', 5)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (66, N'blanco', N'D', 6)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (67, N'blanco', N'E', 6)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (68, N'blanco', N'F', 6)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (69, N'blanco', N'G', 7)
INSERT [dbo].[Archivo] ([id], [color], [columna], [fila]) VALUES (70, N'blanco', N'H', 8)
SET IDENTITY_INSERT [dbo].[Archivo] OFF
INSERT [dbo].[Usuario] ([NombreUsuario], [Nombre], [Apellido], [Contra], [FechaNac], [Pais], [Correo]) VALUES (N'lu        ', N'LUIS                          ', N'CUTZAL                        ', N'123       ', CAST(N'1997-10-03T00:00:00' AS SmallDateTime), N'Guatemala           ', N'cutzalluis@gmail.com               ')
ALTER TABLE [dbo].[Cargar/Guardar]  WITH CHECK ADD  CONSTRAINT [FK_Cargar/Guardar_Solitario] FOREIGN KEY([Id_solitario])
REFERENCES [dbo].[Solitario] ([Id_solitario])
GO
ALTER TABLE [dbo].[Cargar/Guardar] CHECK CONSTRAINT [FK_Cargar/Guardar_Solitario]
GO
ALTER TABLE [dbo].[Cargar/Guardar]  WITH CHECK ADD  CONSTRAINT [FK_Cargar/Guardar_Usuario] FOREIGN KEY([NombreUsuario])
REFERENCES [dbo].[Usuario] ([NombreUsuario])
GO
ALTER TABLE [dbo].[Cargar/Guardar] CHECK CONSTRAINT [FK_Cargar/Guardar_Usuario]
GO
ALTER TABLE [dbo].[Cargar/Guardar]  WITH CHECK ADD  CONSTRAINT [FK_Cargar/Guardar_Versus] FOREIGN KEY([Id_versus])
REFERENCES [dbo].[Versus] ([Id_versus])
GO
ALTER TABLE [dbo].[Cargar/Guardar] CHECK CONSTRAINT [FK_Cargar/Guardar_Versus]
GO
ALTER TABLE [dbo].[DetalleModo]  WITH CHECK ADD  CONSTRAINT [FK_DetalleModo_Solitario] FOREIGN KEY([Id_solitario])
REFERENCES [dbo].[Solitario] ([Id_solitario])
GO
ALTER TABLE [dbo].[DetalleModo] CHECK CONSTRAINT [FK_DetalleModo_Solitario]
GO
ALTER TABLE [dbo].[DetalleModo]  WITH CHECK ADD  CONSTRAINT [FK_DetalleModo_Torneo] FOREIGN KEY([NombreTorneo])
REFERENCES [dbo].[Torneo] ([NombreTorneo])
GO
ALTER TABLE [dbo].[DetalleModo] CHECK CONSTRAINT [FK_DetalleModo_Torneo]
GO
ALTER TABLE [dbo].[DetalleModo]  WITH CHECK ADD  CONSTRAINT [FK_DetalleModo_Versus] FOREIGN KEY([Id_versus])
REFERENCES [dbo].[Versus] ([Id_versus])
GO
ALTER TABLE [dbo].[DetalleModo] CHECK CONSTRAINT [FK_DetalleModo_Versus]
GO
ALTER TABLE [dbo].[Invitacion]  WITH CHECK ADD  CONSTRAINT [FK_Invitacion_Usuario] FOREIGN KEY([NombreUsuario])
REFERENCES [dbo].[Usuario] ([NombreUsuario])
GO
ALTER TABLE [dbo].[Invitacion] CHECK CONSTRAINT [FK_Invitacion_Usuario]
GO
ALTER TABLE [dbo].[Modo]  WITH CHECK ADD  CONSTRAINT [FK_Modo_DetalleModo] FOREIGN KEY([DetalleModo])
REFERENCES [dbo].[DetalleModo] ([DetalleModo])
GO
ALTER TABLE [dbo].[Modo] CHECK CONSTRAINT [FK_Modo_DetalleModo]
GO
ALTER TABLE [dbo].[Modo]  WITH CHECK ADD  CONSTRAINT [FK_Modo_Usuario] FOREIGN KEY([NombreUsuario])
REFERENCES [dbo].[Usuario] ([NombreUsuario])
GO
ALTER TABLE [dbo].[Modo] CHECK CONSTRAINT [FK_Modo_Usuario]
GO
ALTER TABLE [dbo].[Participante]  WITH CHECK ADD  CONSTRAINT [FK_Participante_Torneo] FOREIGN KEY([NombreTorneo])
REFERENCES [dbo].[Torneo] ([NombreTorneo])
GO
ALTER TABLE [dbo].[Participante] CHECK CONSTRAINT [FK_Participante_Torneo]
GO
ALTER TABLE [dbo].[Participante]  WITH CHECK ADD  CONSTRAINT [FK_Participante_Usuario] FOREIGN KEY([NombreUsuario])
REFERENCES [dbo].[Usuario] ([NombreUsuario])
GO
ALTER TABLE [dbo].[Participante] CHECK CONSTRAINT [FK_Participante_Usuario]
GO
ALTER TABLE [dbo].[Solitario]  WITH CHECK ADD  CONSTRAINT [FK_Solitario_DetallePartida] FOREIGN KEY([Detalle])
REFERENCES [dbo].[DetallePartida] ([Detalle])
GO
ALTER TABLE [dbo].[Solitario] CHECK CONSTRAINT [FK_Solitario_DetallePartida]
GO
ALTER TABLE [dbo].[Torneo]  WITH CHECK ADD  CONSTRAINT [FK_Torneo_DetallePartida] FOREIGN KEY([Detalle])
REFERENCES [dbo].[DetallePartida] ([Detalle])
GO
ALTER TABLE [dbo].[Torneo] CHECK CONSTRAINT [FK_Torneo_DetallePartida]
GO
ALTER TABLE [dbo].[Versus]  WITH CHECK ADD  CONSTRAINT [FK_Versus_DetallePartida] FOREIGN KEY([Detalle])
REFERENCES [dbo].[DetallePartida] ([Detalle])
GO
ALTER TABLE [dbo].[Versus] CHECK CONSTRAINT [FK_Versus_DetallePartida]
GO
USE [master]
GO
ALTER DATABASE [FaseIpc2_201700841] SET  READ_WRITE 
GO
