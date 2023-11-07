DROP DATABASE IF EXISTS juego;
CREATE DATABASE juego; 

USE juego;

CREATE TABLE jugadores (

   id_jugador INT,
   username VARCHAR(20) NOT NULL,
   pass VARCHAR(20) NOT NULL,
   PRIMARY KEY(id_jugador)

)ENGINE=InnoDB;

CREATE TABLE partidas (

 id_partidas INT,
 fecha_hora_fin DATETIME,
 duracion FLOAT,
 ganador VARCHAR(20),
 PRIMARY KEY(id_partidas)

)ENGINE=InnoDB;

CREATE TABLE participaciones (

 id_jugador INT,
 id_partida INT,
 FOREIGN KEY (id_jugador) REFERENCES jugadores(id_jugador),
 FOREIGN KEY (id_partida) REFERENCES partidas(id_partidas)

)ENGINE=InnoDB;

/*será nuestra tabla puente para saber cuantas partidas ha hecho X jugador
*/

/*INSERT INTO jugadores VALUES (1,'crisbelbo', '1234');*/
INSERT INTO jugadores VALUES (2,'user1', '4321');
INSERT INTO jugadores VALUES (3,'user2', '1234');
INSERT INTO jugadores VALUES (4,'user3', '1234');

/*INSERT INTO partidas VALUES (1,'2023-10-04', 4, 'crisbelbo');*/ 
INSERT INTO partidas VALUES (2,'2023-10-04', 1.3, 'user1');
/*INSERT INTO partidas VALUES (3,'2023-10-05', 2, 'crisbelbo');*/
INSERT INTO partidas VALUES (4,'2023-10-07', 1.5, 'user3');
INSERT INTO partidas VALUES (5,'2023-10-09', 6, 'user3');
INSERT INTO partidas VALUES (6,'2023-10-05', 0.3, 'user1');
INSERT INTO partidas VALUES (7,'2023-10-04', 3, 'user2');

 
/*INSERT INTO participaciones VALUES (1,1);*/
INSERT INTO participaciones VALUES (2,2);
/*INSERT INTO participaciones VALUES (1,3);*/
INSERT INTO participaciones VALUES (4,4);
INSERT INTO participaciones VALUES (4,5);
INSERT INTO participaciones VALUES (2,6);
INSERT INTO participaciones VALUES (3,7);





