--CREAR UNA BASE DE DATOS LLAMADA Licorera 
CREATE DATABASE [Licorera_DB]; 
GO   

--USA Licorera_DB 
USE Licorera_DB; 
GO 

--CREAR LAS TABLAS DESCRITAS ANTERIORMENTE EN LA BASE DE DATOS


--TABLA “Notificaciones” 
CREATE TABLE [Notificaciones] ( 
	Id INT PRIMARY KEY IDENTITY(1,1), 
	Nombre NVARCHAR(50) NOT NULL, 
	Descripcion NVARCHAR(250) NOT NULL 
); 
GO

--TABLA “Roles” 
CREATE TABLE [Roles]( 
	Id INT PRIMARY KEY IDENTITY(1,1), 
	Nombre NVARCHAR(50) NOT NULL 
); 
GO

--TABLA”Catalogos” 
CREATE TABLE [Catalogos]( 
  Id INT PRIMARY KEY IDENTITY(1,1), 
  Nombre VARCHAR(50) NOT NULL, 
  Descripcion VARCHAR(250) NOT NULL, 
);
GO

--TABLA  “Carritos” 
CREATE TABLE [Carritos]( 
  Id INT  PRIMARY KEY IDENTITY (1,1), 
  CantidadProductos INT NOT NULL DEFAULT 0, 
  Subtotal DECIMAL(10,2) NOT NULL
);
GO

--TABLA “descuentos” 
CREATE TABLE [Descuentos] ( 
	Id INT PRIMARY KEY IDENTITY(1,1), 
	Estado BIT NOT NULL, 
	Tipo VARCHAR(30) NOT NULL, 
	Porcentaje  DECIMAL(10,2) NOT NULL, 
	Fecha_inicio DATETIME NOT NULL,
	Fecha_final DATETIME NOT NULL,
);
GO

--TABLA "TipoBebidas"
CREATE TABLE [TipoBebidas](
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre NVARCHAR(30)
);
GO

--TABLA "Bebidas": Registra las bebidas. 
CREATE TABLE [Bebidas]( 
	Id INT PRIMARY KEY IDENTITY(1,1), 
	Nombre NVARCHAR(50) NOT NULL, 
	Precio DECIMAL(10,2) NOT NULL,  
	Cantidad_Existente INT NOT NULL, 
	Id_Descuentos INT NOT NULL,
	Id_TipoBebidas INT NOT NULL,
	FOREIGN KEY (Id_Descuentos) REFERENCES [Descuentos](Id),
	FOREIGN KEY (Id_TipoBebidas) REFERENCES [TipoBebidas](Id)
);
GO



--TABLA “Personas” 
CREATE TABLE [Personas] ( 
      Id INT PRIMARY KEY IDENTITY(1,1), 
      Nombre VARCHAR(50) NOT NULL, 
      Cedula VARCHAR(10) NOT NULL, 
	  Telefono VARCHAR(10) NOT NULL, 
      Correo_electronico VARCHAR(100) NOT NULL, 
      Rol_Id INT NOT NULL, 
	  FOREIGN KEY (Rol_Id) REFERENCES Roles(Id) 
); 
GO

 ---TABLA "Administrador” 
CREATE TABLE [Administradores] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Sueldo DECIMAL(10,2)NOT NULL, 
    Estado BIT NOT NULL,
    FOREIGN KEY (Id) REFERENCES Personas(Id),
); 
GO

--TABLA INTERMEDIA A/B
CREATE TABLE [Administradores_Bebidas] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Administradores INT NOT NULL,
	Id_Bebidas INT NOT NULL,
    FOREIGN KEY (Id_Administradores) REFERENCES [Administradores](Id),
    FOREIGN KEY (Id_Bebidas) REFERENCES [Bebidas](Id) 	
);
GO

 ---Tabla "Usuarios” 
CREATE TABLE [Usuarios] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Fecha_nacimiento DATE NOT NULL,
	Id_Catalogos INT NOT NULL,
	Id_Carritos INT NOT NULL,
    FOREIGN KEY (Id) REFERENCES Personas(Id),
    FOREIGN KEY (Id_Catalogos) REFERENCES [Catalogos](Id),
    FOREIGN KEY (Id_Carritos) REFERENCES [Carritos](Id)
); 
GO

--TABLA INTERMEDIA A/N
CREATE TABLE [Administradores_Notificaciones] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Administradores INT NOT NULL,
	Id_Notificaciones INT NOT NULL,
    FOREIGN KEY (Id_Administradores) REFERENCES [Administradores](Id),
    FOREIGN KEY (Id_Notificaciones) REFERENCES [Notificaciones](Id) 	
);
GO

--TABLA INTERMEDIA U/N
CREATE TABLE [Usuarios_Notificaciones] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Usuarios INT NOT NULL,
	Id_Notificaciones INT NOT NULL,
    FOREIGN KEY (Id_Usuarios) REFERENCES [Usuarios](Id),
    FOREIGN KEY (Id_Notificaciones) REFERENCES [Notificaciones](Id) 	
);
GO


--TABLA INTERMEDIA A/C
CREATE TABLE [Administradores_Catalogos] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Administradores INT NOT NULL,
	Id_Catalogos INT NOT NULL,
    FOREIGN KEY (Id_Administradores) REFERENCES [Administradores](Id),
    FOREIGN KEY (Id_Catalogos) REFERENCES [Catalogos](Id) 	
);
GO

--TABLA “reservas” 
CREATE TABLE [Reservas]( 
	Id INT PRIMARY KEY IDENTITY(1,1), 
	Estado BIT NOT NULL ,
	Id_Administradores INT NOT NULL,
	Id_Usuarios INT NOT NULL,
	FOREIGN KEY (Id_Administradores) REFERENCES [Administradores](Id),
	FOREIGN KEY (Id_Usuarios) REFERENCES [Usuarios](Id)
);
GO

--TABLA “Pagos” 
CREATE TABLE [Pagos]( 
  Id INT  PRIMARY KEY IDENTITY (1,1), 
  Tipo  VARCHAR (30) NOT NULL, 
  Monto DECIMAL(10,2) NOT NULL
); 
GO

--TABLA INTERMEDIA D/U
CREATE TABLE [Descuentos_Usuarios] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Descuentos INT NOT NULL,
	Id_Usuarios INT NOT NULL,
    FOREIGN KEY (Id_Descuentos) REFERENCES [Descuentos](Id),
    FOREIGN KEY (Id_Usuarios) REFERENCES [Usuarios](Id),	
);
GO

--TABLA "Facturas": Registra las facturas. Cada factura tiene asociado 
CREATE TABLE [Facturas]( 
	Id INT PRIMARY KEY IDENTITY(1,1), 
	Fecha DateTime, 
	Total DECIMAL(10,2), 
	Id_Usuarios INT NOT NULL, 
	TotalConDescuento DECIMAL(10,2), 
	Id_Descuentos INT NOT NULL,
	Id_Reservas INT NOT NULL,
	Id_Pagos INT NOT NULL,
	FOREIGN KEY (Id_Descuentos) REFERENCES [Descuentos](Id),
	FOREIGN KEY (Id_Usuarios) REFERENCES [Usuarios](Id),
	FOREIGN KEY (Id_Reservas) REFERENCES [Reservas](Id),
	FOREIGN KEY (Id_Pagos) REFERENCES [Pagos](Id)
); 
GO

--TABLA INTERMEDIA F/B
CREATE TABLE [Facturas_Bebidas] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Bebidas INT NOT NULL,
	Id_Facturas INT NOT NULL,
    FOREIGN KEY (Id_Bebidas) REFERENCES [Bebidas](Id),
    FOREIGN KEY (Id_Facturas) REFERENCES [Facturas](Id),	
);
GO


--TABLA INTERMEDIA B/U
CREATE TABLE [Bebidas_Usuarios] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Bebidas INT NOT NULL,
	Id_Usuarios INT NOT NULL,
    FOREIGN KEY (Id_Bebidas) REFERENCES [Bebidas](Id),
    FOREIGN KEY (Id_Usuarios) REFERENCES [Usuarios](Id),	
);
GO

--TABLA INTERMEDIA B/CR
CREATE TABLE [Bebidas_Carritos] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Bebidas INT NOT NULL,
	Id_Carritos INT NOT NULL,
    FOREIGN KEY (Id_Bebidas) REFERENCES [Bebidas](Id),
    FOREIGN KEY (Id_Carritos) REFERENCES [Carritos](Id),	
);
GO

--TABLA INTERMEDIA B/R
CREATE TABLE [Bebidas_Reservas] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Bebidas INT NOT NULL,
	Id_Reservas INT NOT NULL,
    FOREIGN KEY (Id_Bebidas) REFERENCES [Bebidas](Id),
    FOREIGN KEY (Id_Reservas) REFERENCES [Reservas](Id),	
);
GO

--TABLA INTERMEDIA B/C
CREATE TABLE [Bebidas_Catalogos] ( 
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Id_Bebidas INT NOT NULL,
	Id_Catalogos INT NOT NULL,
    FOREIGN KEY (Id_Bebidas) REFERENCES [Bebidas](Id),
    FOREIGN KEY (Id_Catalogos) REFERENCES [Catalogos](Id),	
);
GO

EXEC sp_ListarBebidas

SELECT * FROM Descuentos
SELECT * FROM TipoBebidas
SELECT * FROM Bebidas

INSERT INTO Descuentos (Fecha_inicio,Porcentaje,Fecha_final,Estado,Tipo) VALUES (GETDATE(),0.20,'2025-05-01',1,'Por producto')
INSERT INTO TipoBebidas (Nombre) VALUES ('Cerveza')
INSERT INTO Bebidas (Nombre,Precio,Cantidad_Existente,Id_TipoBebidas,Id_Descuentos) VALUES ('Costeñita',7000.00,5,1,1)


INSERT INTO TipoBebidas (Nombre)
VALUES ('RON'),
	   ('VODKA'),
	   ('WHISKY'),
	   ('TEQUILA'),
	   ('GINEBRA'),
	   ('BRANDY'),
	   ('LICOR DE FRUTAS');

UPDATE TipoBebidas 
SET Nombre = 'CERVEZA' WHERE Id = 1;
GO;

-- Procedimiento almacenado: sp_GuardarBebida
CREATE PROCEDURE sp_GuardarBebida 
	@nombre NVARCHAR(50),
	@precio DECIMAL(10,2),
	@cant_exis INT,
	@id_desc INT,
	@id_tbebida INT
AS 
BEGIN
	INSERT INTO Bebidas (Nombre, Precio, Cantidad_Existente, Id_Descuentos, Id_TipoBebidas)
	VALUES (@nombre,@precio,@cant_exis,@id_desc,@id_tbebida);
END;
GO;

-- Procedimiento almacenado: sp_ListarTipoBebida
CREATE PROCEDURE sp_ListarTipoBebida
AS
BEGIN
	SELECT * FROM TipoBebidas;
END;
GO;
EXEC sp_ListarBebidas;

EXEC sp_GuardarBebida 'Tequila mañanero', 85500.00,40, 1, 5;
EXEC sp_ListarTipoBebida;
GO

-- Procedimiento almacenado: sp_ListarBebidasTabla
CREATE PROCEDURE sp_ListarBebidasTabla
AS
BEGIN
    SELECT B.Id, B.Nombre, B.Precio, B.Cantidad_Existente, Tb.Nombre AS Tipo, D.Porcentaje AS Descuento 
    FROM Bebidas B INNER JOIN TipoBebidas Tb ON B.Id_TipoBebidas = Tb.Id INNER JOIN Descuentos D ON B.Id_Descuentos = D.Id;
END;
GO

EXEC sp_ListarBebidasTabla;
GO

-- Procedimiento almacenado: sp_ListarBebidas
CREATE PROCEDURE sp_ListarBebidas
AS
BEGIN
    SELECT * FROM Bebidas
END;
GO

--Procedimiento almacenado: sp_ListarDescuento
CREATE PROCEDURE sp_ListarDescuento
AS
BEGIN
	SELECT * FROM Descuentos;
END;
GO


--Procedimiento almacenado: sp_ListarDescuentoPorTipo
CREATE PROCEDURE sp_ListarDescuentoPorTipo
AS
BEGIN
	SELECT DISTINCT * FROM Descuentos;
END;
GO

EXEC sp_ListarDescuentoPorTipo;
GO

--Procedimiento almacenado: sp_BorrarBebida
CREATE PROCEDURE sp_BorrarBebida
    @Id INT
AS
BEGIN
    DELETE FROM Bebidas WHERE Id = @Id;
END