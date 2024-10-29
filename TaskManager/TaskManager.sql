-- Modelo
--   Usuario:
--   	id
--   	nombre
--   correo
--  	password
--   Tarea:
--   	id
--  idUsuario
--  	descripción
--  	DataInicio
--  	DataFin

CREATE TABLE Usuario(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT,
    correo TEXT,
    password TEXT
);

CREATE TABLE Tarea(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    idUsuario INTEGER,
    descripcion TEXT,
    dataInicio TEXT,
    dataFin TEXT,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(id)
);

-- Insertar datos de prueba
INSERT INTO Usuario(nombre, correo, password) VALUES ('Juan', 'juan@gmail.com', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=');
INSERT INTO Usuario(nombre, correo, password) VALUES ('Pedro', 'pedro@gmail.com', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=');
INSERT INTO Usuario(nombre, correo, password) VALUES ('Maria', 'maria@gmail.com', 'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=');

INSERT INTO Tarea(idUsuario, descripcion, dataInicio, dataFin) VALUES (1, 'Tarea 1', '2024-10-01', '2024-10-10');
INSERT INTO Tarea(idUsuario, descripcion, dataInicio, dataFin) VALUES (1, 'Tarea 2', '2024-10-11', '2024-10-20');
INSERT INTO Tarea(idUsuario, descripcion, dataInicio, dataFin) VALUES (2, 'Tarea 3', '2024-10-21', '2024-10-30');
