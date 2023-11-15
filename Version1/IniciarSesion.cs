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

        //Variables de cada partida

        string[] nameJugadores = new string[6]; //Vector con los nombres de los 6 jugadores 
        int invitar = 0;
        int jugadores = 0;
        int idPartida;
        int numJugadoresP = 0;
        int jugador;

        public IniciarSesion()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que se crearon
        }

         private void Form2_Load(object sender, EventArgs e) //Iniciamos la conexión
        {
            //Creamos un IDEndPoint con el IP y puerto del servidor
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            //Acceso shiva:
            //IPAddress direc = IPAddress.Parse("147.83.117.22");
            //IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Creamos el socket
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); //Intentamos conectar el socket
                MessageBox.Show("Se ha establecido conexión con el servidor");
            }
            catch (SocketException exc)
            {
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
            //pongo en marcha el thread
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
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

                        this.BackColor = Color.LightSlateGray;
                        dataGridView1.Columns.Clear();
                        MessageBox.Show("Te has desconectado, hasta la próxima!");
                        //Cerramos la conexión
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                        atender.Abort();
                        this.Close();
                        
                        break;

                    case 1: //Inicio de sesión

                        if (trozos[1] == "INCORRECTO")
                        {
                            MessageBox.Show("Nombre de usuario o contraseña incorrecta.");
                        }
                        else
                        {
                            MessageBox.Show("Bienvenido " + textBox1.Text + "!");
                            this.BackColor = Color.LightGreen;
                        }
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
                        MessageBox.Show("Ha ganado: " + partidasganadas + " partidas");

                        break;

                    case 4: //Cuantos servicios llevo

                        //Recibimos la respuesta del servidor
                        label6.Text = trozos[1];

                        break;

                    case 5: //Lista de conectados

                        //Recibimos la respuesta del servidor
                        dataGridView1.Rows.Clear();
                        int num = Convert.ToInt32(trozos[1]);
                        dataGridView1.RowCount = num;
                        int n = 0;
                        for (int i = 0; i < num; i++)
                        {
                            dataGridView1.Rows[n].Cells[0].Value = trozos[n+2];
                            n++;
                        }
                        this.dataGridView1.Rows[0].Cells[0].Selected = false;
                        break;

                    case 7: //Invitación

                        var result = MessageBox.Show(trozos[1], "Invitacion", MessageBoxButtons.YesNo);

                        Invoke(new Action(() =>
                        {
                            if (result == DialogResult.Yes)
                            {
                                // Enviamos al servidor el nombre del usuario (devuelve 10/nombre_Invitado/nombre_Invitador/Yes)       
                                string respuestaInvitacion = "7/" + textBox1.Text + "/" + trozos[2] + "/Si";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuestaInvitacion);
                                server.Send(msg);
                                //Ocultamos para esperar a que empiece la partida. 
                                
                            }
                            if (result == DialogResult.No)
                            {
                                string respuestaInvitacion = "7/" + textBox1.Text + "/" + trozos[2] + "/No";
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuestaInvitacion);
                                server.Send(msg);
                            }
                        }));
                        break;

                    case 8: //Respuesta de la invitación
                             
                        if (trozos[1] == "Si")
                        {
                            idPartida = Convert.ToInt32(trozos[2]);

                            if (trozos.Length == 5)
                            {
                                MessageBox.Show("Ha aceptado la invitación " + trozos[4], "invitacion", MessageBoxButtons.OK);
                                nameJugadores[0] = trozos[3];//El jugador que invita 
                                nameJugadores[1] = trozos[4]; //Segundo Jugador
                            }
                            if (trozos.Length == 6)
                            {
                                MessageBox.Show("Ha aceptado la invitación " + trozos[5], "invitacion", MessageBoxButtons.OK);
                                nameJugadores[2] = trozos[5]; //Tercer Jugador
                            }
                            if (trozos.Length == 7)
                            {
                                MessageBox.Show("Ha aceptado la invitación " + trozos[6], "invitacion", MessageBoxButtons.OK);
                                nameJugadores[3] = trozos[6]; //Cuarto Jugador
                            }
                            if (trozos.Length == 8)
                            {
                                MessageBox.Show("Ha aceptado la invitación " + trozos[7], "invitacion", MessageBoxButtons.OK);
                                nameJugadores[4] = trozos[7]; //Quinto Jugador
                            }
                            if (trozos.Length == 9)
                            {
                                MessageBox.Show("Ha aceptado la invitación " + trozos[8], "invitacion", MessageBoxButtons.OK);
                                nameJugadores[5] = trozos[8]; //Sexto Jugador
                            }
                        }
                        else
                        {
                            MessageBox.Show(trozos[2]);
                        }

                        this.dataGridView1.Rows[0].Cells[0].Selected = false;
                        break;

                    case 9://Jugar
                        
                           MessageBox.Show(trozos[1]);

                        break;

                }
            }

        }

        private void inicioSesion_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + textBox1.Text + "/" + textBox2.Text;
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
            if (radioButton2.Checked)
            {
                string mensaje = "3/" + textBox3.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
        }

        private void AceptarRegistro_Click(object sender, EventArgs e)
        {
            if (((usuarioIn.Text.Length > 1) && (contraseñaIn.Text.Length > 1)) && ((usuarioIn.Text != "") && (contraseñaIn.Text != ""))) //Que tenga un mínimo de longitud y no esté en blanco
            {
                string mensaje = "2/" + usuarioIn.Text + "/" + contraseñaIn.Text;
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
            string mensaje = "8/" + idPartida;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nameJugadores[jugadores] = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

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
            //if (invitar == 1)
            //{
            //    if (jugadores < 5)
            //    {

            //    }

            //}
            //else
            //{
            //    MessageBox.Show("Pulsa el botón 'Invitar'");
            //}
        }

        private void button1_Click(object sender, EventArgs e) //Botón lista de conectados
        {
            string mensaje = "5/" + usuarioIn.Text + "/" + contraseñaIn.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
