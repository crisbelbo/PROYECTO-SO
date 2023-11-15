//programa en C para consultar los datos de la base de datos
//Incluir esta libreria para poder hacer las llamadas en shiva2.upc.es
//#include <my_global.h>
#include <mysql.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>
#include<stdbool.h>  
#include <pthread.h>
// Estructura especial para almacenar resultados de consultas
//Creamos una conexion al servidor MYSQL 
MYSQL_RES *resultado;
int err;
MYSQL_ROW row; 
MYSQL *conn;

int i;
int sockets[100];

int contador; //Para el numero de servicios
int idPartida;
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//Estructura para almacenar 100 usuarios conectados
typedef struct{ //Clase "Conectado"
	char nombre[20];
	int socket;
}Conectado;

typedef struct{ //Lista de Conectados
	Conectado conectados[100];
	int num;
}ListaConectados;

ListaConectados milista;

//Declaramos la lista de Partidas, de 6 jugadores cada una
typedef struct {
	int ID;
	int numJugadores;
	char tablero[30];
	//Sockets
	char nombre1[20];
	char nombre2[20];
	char nombre3[20];
	char nombre4[20];
	char nombre5[20];
	char nombre6[20];
	int sock[6];
}Partida;

typedef struct {
	Partida Partidas [1000];
	int num;
} ListaPartidas;

ListaPartidas miListaPartidas;

//FUNCIONES PARA LA LISTA DE CONECTADOS:
//HACER FUNCION PARA DAR NUMERO DE USUARIOS DE LA BASE DE DATOS, UNA CONSULTA, Y LUEGO EN EL ID +1
int DameID(char consulta[100], char respuesta[100])
{
	sprintf(consulta, "SELECT COUNT(jugadores.username)FROM jugadores");
	//recogemos el resultado de la consulta 
	err=mysql_query (conn, consulta);
	
	if(err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		//El resultado debe ser una matriz con una sola fila
		//y una columna que contiene el numero de partidas
		printf ("Numero de jugadores registrados: %s\n", row[0]);
		sprintf(respuesta, "%s", row[0]);
	}
}

int AnadeConectado (ListaConectados *lista, char nombre[20], int socket) //Añade a la lista un nuevo usuario conectado
{
	if (lista->num == 100)
	{
		return -1; //Está la lista llena
	}
	else
	{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0; //Se ha añadido correctamente
	}
	
	
}

/*int CompletarConectado (ListaConectados *milista, char usuario[20],int sock_conn) *///Añade el usuario a la lista realizando una búsqueda
/*{*/
/*	int i=0;*/
/*	int encontrado=0;*/
/*printf("Bienvenido, %d, %s,%d\n",sock_conn, usuario,milista->num);*/
/*	while(i<milista->num && encontrado==0)*/
/*	{*/
/*		printf("%d\n",milista->conectados[i].socket);*/
/*		if (milista->conectados[i].socket == sock_conn)*/
/*		{*/
/*			strcpy(milista->conectados[i].nombre,usuario);*/
/*			printf("name:%s\n",milista->conectados[i].nombre);*/
/*			encontrado=1;*/
/*		}*/
/*		else*/
/*		{*/
/*			i++;*/
/*		}*/
/*	}*/
/*}*/

void Conectados(ListaConectados *lista, char conectados[300]) //Pone en el vector conectados el núm de usuarios en la lista y el usuario de cada jugador separado por barras
{
	printf("Voy a empezar \n");
	
	sprintf(conectados, "%d", lista->num);
	
	int i;
	//printf("Conectados: %s\n", conectados);
	for(i=0; i<lista->num; i++)
	{
		sprintf(conectados,"%s/%s",conectados, lista->conectados[i].nombre); //%s, conectados
	
	}
	printf("Conectados: %s\n",conectados);
}



//Función para saber la posicion de ese usuario mediante el socket
int GetSocket (ListaConectados *lista, char nombre[20])
{
	int i = 0;
	int encontrado = 0;
	while((i < lista->num) && !encontrado)
	{
		if (strcmp(lista->conectados[i].nombre,nombre) == 0)
		{
			encontrado = 1;
		}
		if(!encontrado)
		{
			i = i+1;
		}
	}
	if (encontrado)
	{
		return lista->conectados[i].socket; //Devuelve el socket
	}
	else
	{
		return -1; //No está en la lista
	}
}

int DameIdPartida(char consulta[100], char respuesta[100])
{
	sprintf(consulta, "SELECT partidas.id_partidas FROM partidas");
	//recogemos el resultado de la consulta 
	err=mysql_query (conn, consulta);
	
	if(err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		//El resultado debe ser una matriz con una sola fila
		//y una columna que contiene el numero de partidas
		printf ("ID de partidas: %d\n", row[0]);
		sprintf(respuesta, "%s", row[0]);
	}
}

//FUNCIONES PARA LAS CONSULTAS:

//Funcion que devuelve cuantas partidas ha ganado X jugador
int DameNumPartidasGanadas(char jugador1[20], char consulta[100], char respuesta3[512])
{
	sprintf(consulta,"SELECT COUNT(partidas.ganador) FROM partidas WHERE partidas.ganador='%s' ", jugador1);
	printf ("consulta: %s\n", consulta);
	//recogemos el resultado de la consulta 
	err=mysql_query (conn, consulta);
	
	if(err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		//El resultado debe ser una matriz con una sola fila
		//y una columna que contiene el numero de partidas
		printf ("Numero de partidas: %s\n", row[0]);
		sprintf(respuesta3, "3/%s", row[0]);
	}
	
}

//FUNCIONES INICIALES, INICIO SESION, REGISTRO ETC

//Función para registrar a un nuevo usuario
int Registro (char usuario[20],char contrasena[20], char respuesta2[512], char consulta[80], int id) 
{
	
	printf ("Nombre: %s, Contraseña: %s\n", usuario, contrasena);
	sprintf(consulta, "SELECT jugadores.username FROM jugadores WHERE jugadores.username='%s' ",usuario);
	printf("Consulta: %s\n", consulta);
	// hacemos la consulta para saber si ese usuario ya está registrado
	int err =mysql_query (conn, consulta);
	printf("%s\n", err);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	id = DameID(consulta, respuesta2)+1;
	//Recogemos el resultado
	resultado = mysql_store_result (conn);
	row=mysql_fetch_row(resultado);
	printf("row: %s\n", row);
	
	if (row==NULL)
	{
		printf("Voy a añadir este usuario: %s\n", usuario);
		printf("%s\n", row[0]);
		/*int idJugador = atoi(row[0]);*/
		char insert[150];
		sprintf(insert,"INSERT INTO jugadores VALUES(%d,'%s','%s');",id,usuario,contrasena); //Para que el Id sea la siguiente posición libre
		err=mysql_query(conn, insert);
		
		if (err!=0)
		{
			printf ("Error al insertar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
			exit (1);
			sprintf(respuesta2,"2/No se ha podido insertar el usuario");          		 
		}
		else
		{
			sprintf(respuesta2,"2/Registrado correctamente");
			printf("Se ha registrado correctamente el usuario: %s\n", usuario);
		}
	}
	else
	{
		sprintf(respuesta2,"2/No se ha podido acceder a la base de datos");
	} 
	
}

//Funcion para iniciar sesión
void InicioSesion(char usuario[20], char contrasena[20], char consulta[80], char respuesta1[512], int codigo, char conectado[300], int sock_conn)
{
	
	printf ("Codigo: %d, Nombre: %s, Contraseña: %s\n", codigo, usuario, contrasena);
	
	sprintf(consulta, "SELECT jugadores.username, jugadores.pass FROM jugadores WHERE (jugadores.username='%s' AND jugadores.pass='%s')",usuario,contrasena);
	err=mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	//Recogemos el resultado de la consulta
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if (row == NULL)
	{
		printf("No se ha obtenido la consulta \n");
		sprintf(respuesta1,"1/INCORRECTO");
		
	}
	else
	{
		printf("Inicio de sesion completado \n");
		sprintf(respuesta1,"1/INICIO DE SESION COMPLETADO");
		pthread_mutex_lock( &mutex );
		AnadeConectado(&milista, usuario, sock_conn);
		pthread_mutex_unlock( &mutex);
	
		
	}
}

//Funcion para enviar al cliente una invitacion para jugar
int Invitacion (ListaConectados *lista,char respuesta[512],char jugador_origen[20],int socketOrigen, char jugador_destino[20],int socketDestino, int *sock_conn)
{
	int i = 0;
	printf("El socket destino es: %d \n", socketDestino);
	printf("El socket origen es: %d \n", socketOrigen);
	
	//Devolveremos el codigo, la respuesta y el nombre del destino de la invitacion
	
	sprintf(respuesta,"7/Quieres jugar con %s?/%s",jugador_origen, jugador_origen);
	*sock_conn = socketDestino;
}

//Enviamos al cliente la respuesta de la invitacion
int ConfirmacionInvitacion(ListaConectados *lista,ListaPartidas *lista_partidas,char confirmar[10], int socketOrigen,int socketDestino, char respuesta[512], char jugador_origen [20], char jugador_destino[20], int *sock_conn)
{
	lista_partidas->Partidas[idPartida].numJugadores++;
	if (strcmp(confirmar,"Si") == 0)
	{
		strcpy(lista_partidas->Partidas[idPartida].nombre1,jugador_destino);
		lista_partidas->Partidas[idPartida].sock[0] = GetSocket(lista,jugador_destino);
		printf("sock1: %d",lista_partidas->Partidas[idPartida].sock[0]);
		sprintf(respuesta,"8/Si/%d/%s", idPartida,jugador_destino);
		if (lista_partidas->Partidas[idPartida].numJugadores == 2)
		{
			
			strcpy(lista_partidas->Partidas[idPartida].nombre2,jugador_origen);
			lista_partidas->Partidas[idPartida].sock[1] = GetSocket(lista,jugador_origen);
			printf("sock2: %d",lista_partidas->Partidas[idPartida].sock[1]);
			sprintf(respuesta, "%s/%s", respuesta, lista_partidas->Partidas[idPartida].nombre2);
		}
		else if (lista_partidas->Partidas[idPartida].numJugadores == 3)
		{
			strcpy(lista_partidas->Partidas[idPartida].nombre3,jugador_origen);
			lista_partidas->Partidas[idPartida].sock[2] = GetSocket(lista,jugador_origen);
			sprintf(respuesta, "%s/%s/%s", respuesta, lista_partidas->Partidas[idPartida].nombre2,lista_partidas->Partidas[idPartida].nombre3);
		}
		else if (lista_partidas->Partidas[idPartida].numJugadores == 4)
		{strcpy(lista_partidas->Partidas[idPartida].nombre4,jugador_origen);
		lista_partidas->Partidas[idPartida].sock[3] = GetSocket(lista,jugador_origen);
		sprintf(respuesta, "%s/%s/%s/%s", respuesta, lista_partidas->Partidas[idPartida].nombre2,lista_partidas->Partidas[idPartida].nombre3,lista_partidas->Partidas[idPartida].nombre4);
		}
		
		else if (lista_partidas->Partidas[idPartida].numJugadores == 5)
		{strcpy(lista_partidas->Partidas[idPartida].nombre5,jugador_origen);
		lista_partidas->Partidas[idPartida].sock[4] = GetSocket(lista,jugador_origen);
		sprintf(respuesta, "%s/%s/%s/%s/%s", respuesta, lista_partidas->Partidas[idPartida].nombre2,lista_partidas->Partidas[idPartida].nombre3,lista_partidas->Partidas[idPartida].nombre4,lista_partidas->Partidas[idPartida].nombre5);
		}
		
		else if (lista_partidas->Partidas[idPartida].numJugadores == 6)
		{strcpy(lista_partidas->Partidas[idPartida].nombre6,jugador_origen);
		lista_partidas->Partidas[idPartida].sock[5] = GetSocket(lista,jugador_origen);
		sprintf(respuesta, "%s/%s/%s/%s/%s/%s", respuesta, lista_partidas->Partidas[idPartida].nombre2,lista_partidas->Partidas[idPartida].nombre3,lista_partidas->Partidas[idPartida].nombre4,4,lista_partidas->Partidas[idPartida].nombre5,lista_partidas->Partidas[idPartida].nombre6);
		}
		
		printf("Jugador que invita:%s, Jugador invitado:%s\n",lista_partidas->Partidas[idPartida].nombre1,lista_partidas->Partidas[idPartida].nombre2);
	}
	else
	{
		sprintf(respuesta,"8/NO/%s No quiere jugar contigo",jugador_origen);
	}
	
	*sock_conn = socketDestino;
}

//Funcion para desconectarse y eliminarte de la lista de conectados
int Desconectar(ListaConectados *lista, char nombre[20], char respuestaDesconectar[512])
{
	int posicion = GetSocket (lista,nombre);
	if (posicion == -1)
	{
		return -1; //No está conectado este usuario
	}
	
	
	else
	{
		int i;
		for (i = posicion; i < lista->num-1; i++)
		{
			strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
			lista->conectados[i].socket = lista->conectados[i+1].socket;
		}
		lista->num--;
		sprintf(respuestaDesconectar,"0/Te has desconectado, hasta la próxima");
		return 0;
	}
}

//Función para el bucle de atención al cliente
void *Atencion(void*socket)
{
	int sock_conn;
	int *s;
	s=(int *)socket;
	sock_conn= *s;
	
	char conectado[300];
	int desconectar=0;
	printf("El socket por el cual me comunico es: %d", sock_conn);
	
	//Conexion mediante sockets con el cliente
	int  sock_listen;
	int ret;
	char peticion[512];
	char respuesta[512];
	/*	char usuario[80];*/
	
	
	
	int terminar = 0;
	
	while(terminar == 0) //Bucle de atencion al cliente
	{
		char usuario[80];
		char contrasena[80];
		int id;
		
		ret = read(sock_conn, peticion, sizeof(peticion));
		printf("Recibido \n");
		
		peticion[ret]='\0';
		
		printf("Peticion %s \n", peticion);
		
		char *p = strtok(peticion, "/");
		int codigo = atoi(p);
		int cont;
		char jugador1 [10];
		char jugador2 [10];
		char consulta [80];	
		
		//PETICIONES
		if (codigo == 0) //Desconectarse y eliminar de la lista de conectados
		{
			terminar = 1;
			desconectar=1;
			pthread_mutex_lock( &mutex );
			Desconectar(&milista,usuario,respuesta);
			Conectados(&milista,conectado);
			pthread_mutex_unlock( &mutex);
			
			printf("Respuesta: %s \n", respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
		}
		
		else if (codigo == 1) //Inicio de sesión
		{
			p=strtok(NULL,"/");
			strcpy(usuario,p);
			p=strtok(NULL,"/");
			strcpy(contrasena,p);
			
			InicioSesion(usuario,contrasena, consulta, respuesta, codigo, conectado,sock_conn); //Lamamos a la funcion InicioSesion
			printf("Voy a sacar conectados \n");
			Conectados(&milista,conectado);
			printf("Usuarios Conectados: %s\n", &milista);
			printf("Ya tengo conectados: %s\n",conectado);
			
		/*	printf("Respuesta: %s \n", respuesta);*/
			
			desconectar=0;
			
		}
		else if(codigo==2) //Registro de nuevo usuario
		{
			p = strtok( NULL, "/");
			strcpy (usuario, p);
			p = strtok( NULL, "/");
			strcpy(contrasena,p);
			Registro(usuario,contrasena, respuesta, consulta, id);
			//Enviamos la respuesta
			printf("Respuesta: %s \n", respuesta);
			
			
		}
		else if(codigo ==3) //Consulta 1
		{
			p = strtok( NULL, "/");
			strcpy(usuario,p);
			DameNumPartidasGanadas(usuario,consulta,respuesta);
			//Enviamos la respuesta
			printf("El socket por el cual mando la respuesta es: %d", sock_conn);
			printf("Ha ganado: %s partidas\n", respuesta);
			
			
		}
		
		else if(codigo==5) //Lista de conectados
		{
			printf("Voy a arrancar los nombres de usuario\n");
			pthread_mutex_lock( &mutex );
			Conectados(&milista, respuesta);
			pthread_mutex_unlock( &mutex);
		
			printf("La respuesta es: %s\n", respuesta);
			sprintf(respuesta, "5/%d/%s",milista.num,&milista);
			
		
		}
		
		else if(codigo ==6) //Consulta 3
		{
			p = strtok( NULL, "/");
			strcpy(usuario,p);
			DameNumPartidasGanadas(usuario,consulta,respuesta);
			// Enviamos la respuesta
			printf("Ha ganado: %s partidas\n", respuesta);
			
			
		}
		if (codigo == 7) //Invitacion
		{
			int socketOrigen;
			int socketDestino;
			char jugador_origen[20];
			char jugador_destino[20];
			
			p = strtok( NULL, "/");
			strcpy(jugador_origen,p);
			socketOrigen = GetSocket(&milista,p);
			p = strtok (NULL,"/");
			strcpy(jugador_destino,p);
			socketDestino = GetSocket(&milista,p);
			pthread_mutex_lock( &mutex );
			Invitacion(&milista,respuesta,jugador_origen,socketOrigen,jugador_destino,socketDestino,&sock_conn);
			pthread_mutex_unlock( &mutex);
			printf("Respuesta: %s \n", respuesta);
			
		}
		
		if (codigo == 8) //Recepción de la invitación
		{
			int socketOrigen;
			int socketDestino;
			char confirmar[10];
			char jugador_origen[20];
			char jugador_destino[20];
			
			p = strtok( NULL, "/");
			strcpy(jugador_origen,p);
			socketOrigen = GetSocket(&milista,p);
			p= strtok (NULL,"/");
			strcpy(jugador_destino,p);
			socketDestino = GetSocket(&milista,p);
			printf("%d %d\n",socketDestino, socketOrigen);
			p=strtok(NULL,"/");
			strcpy(confirmar,p);
			
			pthread_mutex_lock( &mutex );
			ConfirmacionInvitacion(&milista,&miListaPartidas,confirmar,socketOrigen,socketDestino,respuesta,jugador_origen,jugador_destino,&sock_conn);
			Conectados(&milista,conectado);
			pthread_mutex_unlock( &mutex);
			printf("Respuesta: %s \n", respuesta);
			
		}
		
		if (codigo==9) //Jugar
		{
			/*			p= strtok(NULL,"/");*/
			/*			int id = atoi(p);*/
			
			miListaPartidas.num = idPartida;
			miListaPartidas.num++;
			printf("HOLA\n");
			
			// Enviamos la respuesta
			/*			char respuestaInvitados[20];*/
			
			/*			int socketsJugadores[4];*/
			
			//int numJugadores = DameSocketsJugadoresPartida(id,&miListaPartidas,&miLista,socketsJugadores);
			//printf("%d\n",socketsJugadores[1]);
			sprintf(respuesta,"9/Selecciona a la persona con la que quieres jugar :)");
			//strcpy(respuestaInvitados,"14/1");
			write (sock_conn,respuesta, strlen(respuesta));
			printf("Respuesta: %s \n", respuesta);
			
			
		}
			
			
		
		if (codigo != 0)
		{
			// Enviamos la respuesta
			write(sock_conn, respuesta, strlen(respuesta));
		}
		
		if ((codigo == 1) || (codigo == 2) || (codigo == 3) || (codigo == 5) || (codigo == 6)|| (codigo == 7)|| (codigo == 8)) //Contador de servicios
		{
			pthread_mutex_lock(&mutex); //No me interrumpas ahora
			contador = contador + 1;
			pthread_mutex_unlock(&mutex); //ya puedes interrumpirme
			//notificamos a los clientes conectados
			char notificacion[20];
			sprintf(notificacion, "4/%d", contador);
			int j;
			for(j=0; j<i;j++)
			{
				write(sockets[j], notificacion, strlen(notificacion));
			}
			printf("El numero de servicios es: %d\n", contador);
		}
		
	}
	// cerrar la conexion con el servidor 
	close(sock_conn);
}
int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	if((sock_listen=socket(AF_INET, SOCK_STREAM, 0))<0)
		printf("Error creando el socket\n");
	
	memset(&serv_adr,0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_port = htons(9050);
	if(bind(sock_listen, (struct sockaddr *)&serv_adr, sizeof(serv_adr))<0)
		printf("Error al bind \n");
	
	if(listen(sock_listen, 100)<0)
		printf("Error en el listen \n");
	//Creamos la conexión con SQL
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	milista.num=0;
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "juego",0, NULL, 0);
	if (conn==NULL) 
	{
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	contador=0;
	pthread_t thread;
	i=0;
	char usuario[20];
	
	for(;;) //bucle para atender a los clientes
	{
		printf("Escuchando \n");
		
		sock_conn=accept(sock_listen, NULL, NULL);
		printf("He recibido conexión\n");
		
		sockets[i]=sock_conn; //Socket que usaremos para este cliente
		
		pthread_create(&thread, NULL, Atencion,&sockets[i]); //Crear Thread
		i=i+1;	
	}	
}
//HAY QUE HACER FUNCION COMPLETAR CONECTADO PARA CUANDO TENGA EL USUARIO DEL SOCKET PODER COMPLETAR LA LISTA DE CARA A LOS USUARIOS MUERTOS






