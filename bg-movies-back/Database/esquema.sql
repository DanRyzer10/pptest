CREATE DATABASE bg_movies;

USE bg_movies;

CREATE TABLE resenias (
    id INT,
    movie_id INT,
    autor VARCHAR(100),
    comentario TEXT,
    calificacion INT,
    fecha_creacion DATETIME
);