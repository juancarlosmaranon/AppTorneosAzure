CREATE TABLE Equipo(
	IdEquipo int NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Jugador1 int NOT NULL,
	Jugador2 int NOT NULL,
	Jugador3 int NOT NULL,
	ConfirmJug2 int NOT NULL,
	ConfirmJug3 int NOT NULL,
	GANADOS int NULL,
	PERDIDOS int NULL,
	EMPATES int NULL,
 CONSTRAINT PK_Equipo PRIMARY KEY CLUSTERED 
(
	IdEquipo ASC
))
GO
/****** Object:  Table EquipoLiga    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE EquipoLiga(
	Id int NOT NULL,
	IdLiga int NOT NULL,
	IdEquipo int NOT NULL,
	Victorias int NOT NULL,
	Derrotas int NOT NULL,
	EMPATES int NULL,
	Inscrito int NULL
)
GO
/****** Object:  Table Liga    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Liga(
	IdLiga int NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	FechaInicio datetime NULL,
	FechaFin datetime NULL,
	Estado int NOT NULL,
 CONSTRAINT PK_Liga PRIMARY KEY CLUSTERED 
(
	IdLiga ASC
))
GO
/****** Object:  Table Partida    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Partida(
	IdPartida int NOT NULL,
	IdLiga int NOT NULL,
	IdEquipo1 int NOT NULL,
	IdEquipo2 int NOT NULL,
	Administrador int NOT NULL,
	CodigoPartida nvarchar(50) NOT NULL,
	Ganador int NOT NULL,
	InicioPartida datetime NOT NULL,
	Estado nvarchar(50) NOT NULL,
 CONSTRAINT PK_Partida PRIMARY KEY CLUSTERED 
(
	IdPartida ASC
))
GO
/****** Object:  Table Usuario    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Usuario(
	IdUsuario int NOT NULL,
	UsuarioTag nvarchar(50) NOT NULL,
	Contrasenia nvarchar(50) NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Email nvarchar(50) NOT NULL,
 CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED 
(
	IdUsuario ASC
))
GO
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (1, N'jc', N'jc', N'jc', N'jc@gmail.com')
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (2, N'jc1', N'jc1', N'jc1', N'jc1@gmail.com')
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (3, N'@jc2', N'jc2', N'jc2', N'jc2@gmail.com')
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (4, N'@jc3', N'jc3', N'jc3', N'jc3@gmail.com')
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (5, N'@jc4', N'jc4', N'jc4', N'jc4@gmail.com')
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (6, N'@jc5', N'jc5', N'jc5', N'jc5@gmail.com')
INSERT Usuario (IdUsuario, UsuarioTag, Contrasenia, Nombre, Email) VALUES (7, N'@jc6', N'jc6', N'jc6', N'jc6@gmail.com')
GO
ALTER TABLE Equipo  WITH CHECK ADD  CONSTRAINT FK_Equipo_Usuario1 FOREIGN KEY(Jugador1)
REFERENCES Usuario (IdUsuario)
GO
ALTER TABLE Equipo CHECK CONSTRAINT FK_Equipo_Usuario1
GO
ALTER TABLE Equipo  WITH CHECK ADD  CONSTRAINT FK_Equipo_Usuario2 FOREIGN KEY(Jugador2)
REFERENCES Usuario (IdUsuario)
GO
ALTER TABLE Equipo CHECK CONSTRAINT FK_Equipo_Usuario2
GO
ALTER TABLE Equipo  WITH CHECK ADD  CONSTRAINT FK_Equipo_Usuario3 FOREIGN KEY(Jugador3)
REFERENCES Usuario (IdUsuario)
GO
ALTER TABLE Equipo CHECK CONSTRAINT FK_Equipo_Usuario3
GO
ALTER TABLE EquipoLiga  WITH CHECK ADD  CONSTRAINT FK_EquipoLiga_Equipo FOREIGN KEY(IdEquipo)
REFERENCES Equipo (IdEquipo)
GO
ALTER TABLE EquipoLiga CHECK CONSTRAINT FK_EquipoLiga_Equipo
GO
ALTER TABLE EquipoLiga  WITH CHECK ADD  CONSTRAINT FK_EquipoLiga_Liga FOREIGN KEY(IdLiga)
REFERENCES Liga (IdLiga)
GO
ALTER TABLE EquipoLiga CHECK CONSTRAINT FK_EquipoLiga_Liga
GO
ALTER TABLE Partida  WITH CHECK ADD  CONSTRAINT FK_Partida_Equipo FOREIGN KEY(IdEquipo1)
REFERENCES Equipo (IdEquipo)
GO
ALTER TABLE Partida CHECK CONSTRAINT FK_Partida_Equipo
GO
ALTER TABLE Partida  WITH CHECK ADD  CONSTRAINT FK_Partida_Equipo2 FOREIGN KEY(IdEquipo2)
REFERENCES Equipo (IdEquipo)
GO
ALTER TABLE Partida CHECK CONSTRAINT FK_Partida_Equipo2
GO
ALTER TABLE Partida  WITH CHECK ADD  CONSTRAINT FK_Partida_Liga FOREIGN KEY(IdLiga)
REFERENCES Liga (IdLiga)
GO
ALTER TABLE Partida CHECK CONSTRAINT FK_Partida_Liga
GO
/****** Object:  StoredProcedure BUSCARUSUARIOTAG_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE BUSCARUSUARIOTAG_SP(@UsuarioTag NVARCHAR(50))
AS
    SELECT * FROM Usuario WHERE UsuarioTag = @UsuarioTag
GO
/****** Object:  StoredProcedure COMPROBAREMAIL_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE COMPROBAREMAIL_SP(@Email NVARCHAR(50))
AS
    SELECT * FROM Usuario WHERE Email = @Email
GO
/****** Object:  StoredProcedure DELETEEQUIPO_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE DELETEEQUIPO_SP(@IDEQUIPO INT)
AS
    DELETE FROM Equipo WHERE IdEquipo = @IDEQUIPO
GO
/****** Object:  StoredProcedure DETALLESUSUARIO_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE DETALLESUSUARIO_SP(@IdUsuario INT)
AS
    SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure EQUIPOSUSER_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE EQUIPOSUSER_SP(@IDJUGADOR INT)
AS
    SELECT * FROM Equipo WHERE Jugador1 = @IDJUGADOR OR Jugador2 = @IDJUGADOR OR Jugador3 = @IDJUGADOR
GO
/****** Object:  StoredProcedure INSERTAREQUIPO_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE INSERTAREQUIPO_SP(@Nombre nvarchar(50), @Jugador1 int, @Jugador2 int, @Jugador3 int)
AS
 INSERT INTO EQUIPO VALUES((SELECT ISNULL(MAX(IdEquipo),0) FROM Equipo)+1, @Nombre, @Jugador1, @Jugador2, @Jugador3, -1, -1, 0, 0, 0)
GO
/****** Object:  StoredProcedure INSERTARUSUARIO_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE INSERTARUSUARIO_SP(@UsuarioTag NVARCHAR(50), @Contrasenia NVARCHAR(50), @Nombre NVARCHAR(50), @Email NVARCHAR(50))
AS
    INSERT INTO USUARIO VALUES ((SELECT ISNULL(MAX(IdUsuario),0) FROM USUARIO)+1, @UsuarioTag, @Contrasenia, @Nombre, @Email)
GO
/****** Object:  StoredProcedure LOGIN_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE LOGIN_SP(@Email NVARCHAR(50), @Contrasenia NVARCHAR(50))
AS
    SELECT * FROM Usuario WHERE Email = @Email AND Contrasenia = @Contrasenia
GO
/****** Object:  StoredProcedure SELECTEQUIPOID_SP    Script Date: 23/03/2023 18:09:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE SELECTEQUIPOID_SP(@IDEQUIPO INT)
AS
    SELECT * FROM Equipo WHERE IdEquipo = @IDEQUIPO
GO
