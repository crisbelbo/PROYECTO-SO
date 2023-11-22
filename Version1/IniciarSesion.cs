using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Version1
{
    public partial class IniciarSesion : Form
    {
        Socket server;
        Thread atender;

        //delegate void DelegadoParaEscribir(string mensaje);

        //Variables de cada partida

        string[] nameJugadores = new string[6]; //Vector con los nombres de los 6 jugadores 
        int invitar = 0;
        int jugadores = 0;
        int idPartida;
        int numJugadoresP = 0;
        int jugador;

        //Variables del Chat

        string autor;
        string mensaje;

        delegate void DelegadorParaEscribir(string mensaje);
        delegate void DelegadoGB(GroupBox mensaje);

        public IniciarSesion()
        {
            InitializeComponent();
            /*CheckForIllegalCrossThreadCalls = false;*/ //necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que se crearon
        }

        public void PonContador(string contador)
        {
            label6.Text = contador;
        }
         private void Form2_Load(object sender, EventArgs e) //Iniciamos la conexión
        {
            //Creamos un IDEndPoint con el IP y puerto del servidor
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9090);

            //Acceso shiva:
            //IPAddress direc = IPAddress.Parse("147.83.117.22");
            //IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Variables escondidas Chat

            CasillaChat.Visible = false;
            LblChat.Visible = false;

            //Botones escondidos desconectar y jugar

            Desconectar.Visible = false;
            Jugar.Visible = false;

            //Variables escondidas Lista Conectados

            DataGridListaConectados.Visible = false;
            label5.Visible = false; //Consulta aquí la lista de conectados
            ListaConectados.Visible = false;

            //Variables escondidas numero de consultas

            label13.Visible = false; //Número de servicios
            label6.Visible = false; //Casilla donde aparecen el numero de servicios

            //Variables escondidas de la consulta

            label8.Visible = false; //Introduce el nombre del usuario que quieras consultar
            label7.Visible = false; //Nombre del usuario
            ConsultaIn.Visible = false; //Textbox donde va el usuario a consultar
            AceptarConsulta.Visible = false;
            Consulta1.Visible = false; //Radio Button

            label14.Visible = false; //Usuario
            label15.Visible = false; //Usuarios conectados:
            label16.Visible = false; //Cuando estés listo pulsa el botón Jugar

            Jugar.Visible = false;

            //Creamos el socket
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); //Intentamos conectar el socket
                //pongo en marcha el thread
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
                MessageBox.Show("Se ha establecido conexión con el servidor");
            }
            catch (SocketException exc)
            {
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
            
        }

        private void AtenderServidor() 
        {

            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                this.server.Receive(msg2);
                string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] trozos = mensaje.Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                

                switch (codigo)
                {
                    case 0: //Desconexión
                        Invoke(new Action(() =>
                        {
                            //Recibimos la respuesta del servidor
                            DataGridListaConectados.Rows.Clear();
                            int num = Convert.ToInt32(trozos[1]);
                            DataGridListaConectados.RowCount = num;
                            int n = 0;
                            for (int i = 0; i < num; i++)
                            {
                                DataGridListaConectados.Rows[n].Cells[0].Value = trozos[n + 2];
                                n++;
                            }
                            this.DataGridListaConectados.Rows[0].Cells[0].Selected = false;
                            this.BackColor = Color.LightSlateGray;
                            DataGridListaConectados.Columns.Clear();
                            usuarioReg.Clear();
                            contraseñaReg.Clear();
                            MessageBox.Show(trozos[2]);
                            //Variables escondidas de inicio de sesión y registro

                            label1.Visible = true;
                            usuarioIn.Visible = true;
                            contraseñaIn.Visible = true;
                            label2.Visible = true; //Usuario:
                            label3.Visible = true; //Contraseña:
                            inicioSesion.Visible = true;
                            label4.Visible = true; //No tienes cuenta? Regístrate ya!
                            label12.Visible = true; //Regístrate para poder iniciar sesión
                            usuarioReg.Visible = true;
                            contraseñaReg.Visible = true;
                            label11.Visible = true; //Nuevo usuario
                            label10.Visible = true; //Nueva contraseña
                            AceptarRegistro.Visible = true;
                            //Cerramos la conexión
                            server.Shutdown(SocketShutdown.Both);
                            server.Close();
                            atender.Abort();
                            this.Close();

                        }));
                        
                        break;

                    case 1: //Inicio de sesión
                        Invoke(new Action(() =>
                        {
                            
                            if (trozos[1] == "INCORRECTO")
                            {
                                MessageBox.Show("Nombre de usuario o contraseña incorrecta.");
                            }
                            else
                            {
                                MessageBox.Show("Bienvenido " + usuarioIn.Text + "!");
                                this.BackColor = Color.LightGreen;

                                //Variables escondidas de inicio de sesión y registro

                                label1.Visible = false;
                                usuarioIn.Visible = false;
                                contraseñaIn.Visible = false;
                                label2.Visible = false; //Usuario:
                                label3.Visible = false; //Contraseña:
                                inicioSesion.Visible = false;
                                label4.Visible = false; //No tienes cuenta? Regístrate ya!
                                label12.Visible = false; //Regístrate para poder iniciar sesión
                                usuarioReg.Visible = false;
                                contraseñaReg.Visible = false;
                                label11.Visible = false; //Nuevo usuario
                                label10.Visible = false; //Nueva contraseña
                                AceptarRegistro.Visible = false;
                                label15.Visible = true;
                                label14.Text = usuarioIn.Text;
                                label14.Visible = true;

                                //Botones escondidos desconectar y jugar

                                Desconectar.Visible = true;
                                Jugar.Visible = true;

                                //Variables escondidas Lista Conectados

                                DataGridListaConectados.Visible = true;
                                label5.Visible = true; //Consulta aquí la lista de conectados
                                ListaConectados.Visible = true;

                                //Variables escondidas numero de consultas

                                label13.Visible = true; //Número de servicios
                                label6.Visible = true; //Casilla donde aparecen el numero de servicios

                                //Variables escondidas de la consulta

                                label8.Visible = true; //Introduce el nombre del usuario que quieras consultar
                                label7.Visible = true; //Nombre del usuario
                                ConsultaIn.Visible = true; //Textbox donde va el usuario a consultar
                                AceptarConsulta.Visible = true;
                                Consulta1.Visible = true; //Radio Button

                            }
                        }));
                        
                        break;

                     case 2: //Registro

                        if (mensaje == "2/Registrado correctamente")
                        {
                            MessageBox.Show("Usuario registrado correctamente");
                        }
                        else if (mensaje == "2/No se ha podido registrar al usuario")
                        {
                            MessageBox.Show("No se ha podido insertar el usuario");
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido acceder a la base de datos");
                            
                        }
                        break;

                    case 3: //Consulta

                        //Recibimos la respuesta del servidor
                        int partidasganadas = Convert.ToInt32(trozos[1]);
                        MessageBox.Show("Ha ganado: " + partidasganadas + " partida/s");

                        break;

                    case 4: //Cuantos servicios llevo

                        //Recibimos la respuesta del servidor
                       Invoke(new Action(() =>
                       {
                           label6.Text = trozos[1];
                       }));

                        break;

                    case 5: //Lista de conectados

                        Invoke(new Action(() =>
                        {
                            //Recibimos la respuesta del servidor
                            DataGridListaConectados.Rows.Clear();
                            int num = Convert.ToInt32(trozos[1]);
                            DataGridListaConectados.RowCount = num;
                            int n = 0;
                            for (int i = 0; i < num; i++)
                            {
                                DataGridListaConectados.Rows[n].Cells[0].Value = trozos[n + 2];
                                n++;
                            }
                            this.DataGridListaConectados.Rows[0].Cells[0].Selected = false;

                        }));
                       
                        break;

                    case 7: //Invitación

                        var result = MessageBox.Show("Te ha invitado: "+trozos[1]+", quieres jugar contra él?", "Invitación", MessageBoxButtons.YesNo);

                        Invoke(new Action(() =>
                        {
                            if (result == DialogResult.Yes)
                            {
                                // Enviamos al servidor el nombre del usuario (devuelve 8/nombre_Invitado/nombre_Invitador/Si)       
                                string respuestaInvitacion = "8/" + usuarioIn.Text + "/" + trozos[1] + "/Yes";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuestaInvitacion);
                                server.Send(msg);
                                
                                
                            }
                            if (result == DialogResult.No)
                            {
                                string respuestaInvitacion = "8/" + usuarioIn.Text + "/" + trozos[1] + "/No";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuestaInvitacion);
                                server.Send(msg);
                            }
                        }));
                        break;

                    case 8: //Respuesta de la invitación
                        Invoke(new Action(() =>
                        {
                            if (trozos[1] == "Yes")
                            {
                                idPartida = Convert.ToInt32(trozos[2]);

                                if (trozos.Length == 6)//Número de items en el "churro" que viene del servidor que hay que trocear
                                {
                                    MessageBox.Show("Ha aceptado la invitación " + trozos[4], "invitacion", MessageBoxButtons.OK);
                                    nameJugadores[0] = trozos[3];//El jugador que invita 
                                    nameJugadores[1] = trozos[4]; //Segundo Jugador
                                    CasillaChat.Visible = true;
                                    label16.Visible = true;
                                    Jugar.Visible = true;
                                }
                                //if (trozos.Length == 6)
                                //{
                                //    MessageBox.Show("Ha aceptado la invitación " + trozos[5], "invitacion", MessageBoxButtons.OK);
                                //    nameJugadores[2] = trozos[5]; //Tercer Jugador
                                //}
                                //if (trozos.Length == 7)
                                //{
                                //    MessageBox.Show("Ha aceptado la invitación " + trozos[6], "invitacion", MessageBoxButtons.OK);
                                //    nameJugadores[3] = trozos[6]; //Cuarto Jugador
                                //}
                                //if (trozos.Length == 8)
                                //{
                                //    MessageBox.Show("Ha aceptado la invitación " + trozos[7], "invitacion", MessageBoxButtons.OK);
                                //    nameJugadores[4] = trozos[7]; //Quinto Jugador
                                //}
                                //if (trozos.Length == 9)
                                //{
                                //    MessageBox.Show("Ha aceptado la invitación " + trozos[8], "invitacion", MessageBoxButtons.OK);
                                //    nameJugadores[5] = trozos[8]; //Sexto Jugador
                                //}
                            }
                            else
                            {
                                MessageBox.Show("No quiere jugar contra ti");
                            }

                            this.DataGridListaConectados.Rows[0].Cells[0].Selected = false;
                        }));
                        
                        break;

                    //case 9://Jugar
                        
                    //       MessageBox.Show(trozos[1]);

                    //    break;

                    case 10: //Chat

                        Invoke(new Action(() =>
                        {
                            autor = trozos[2];
                            mensaje = trozos[3];
                            CasillaChat.Text = autor + ":" + mensaje;

                        }));
                        
                        break;

                }
            }

        }

        private void inicioSesion_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + usuarioIn.Text + "/" + contraseñaIn.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            string[] trozos = mensaje.Split('/');
            //int codigo = Convert.ToInt32(trozos[0]);
               
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

     
        private void AceptarConsulta_Click(object sender, EventArgs e)
        {
            if (Consulta1.Checked)
            {
                string mensaje = "3/" + ConsultaIn.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
        }

        private void AceptarRegistro_Click(object sender, EventArgs e)
        {
            if (((usuarioReg.Text.Length > 1) && (contraseñaReg.Text.Length > 1)) && ((usuarioReg.Text != "") && (contraseñaReg.Text != ""))) //Que tenga un mínimo de longitud y no esté en blanco
            {
                string mensaje = "2/" + usuarioReg.Text + "/" + contraseñaReg.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("El nombre debe tener más de un caracter");
            }
        }

        private void Jugar_Click(object sender, EventArgs e)
        {
            string mensaje = "9/" + idPartida;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            Juego f = new Juego();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //Botón lista de conectados
        {
            string mensaje = "5/" + usuarioIn.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nameJugadores[jugadores] = DataGridListaConectados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (usuarioIn.Text == nameJugadores[jugadores])
            {
                MessageBox.Show("No puedes autoinvitarte");
            }
            else
            {
                string mensaje = "7/" + usuarioIn.Text + "/" + nameJugadores[jugadores];
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                jugadores++;
            }
        }

        private void EnviarMensaje_Click(object sender, EventArgs e)
        {
            string mensaje = "10/" + idPartida + "/" + usuarioIn.Text + "/" + EscribirEnChat.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            CasillaChat.Text =(usuarioIn.Text + ":" + EscribirEnChat.Text);
            EscribirEnChat.Clear();
        }
    }
}

