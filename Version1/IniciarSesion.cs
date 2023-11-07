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
        public IniciarSesion()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que se crearon
        }

        private void AtenderServidor() 
        {
            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            this.server.Receive(msg2);
            //string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
            //int codigo = Convert.ToInt32(trozos[0]);
            //string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
            int codigo = Convert.ToInt32(trozos[0]);
            string mensaje = trozos[1].Split('\0')[0];

            while (true)
            {
                switch (codigo)
                {
                    case 1: //Inicio de sesión

                        if (trozos[1] == "INCORRECTO")
                        {
                            MessageBox.Show("Nombre de usuario o contraseña incorrecta.");
                        }
                        else
                        {
                            MessageBox.Show("Bienvenido " + textBox1.Text);
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
                        label6.Text = mensaje;

                        break;

                    case 5: //Lista de conectados

                        //Recibimos la respuesta del servidor
                        dataGridView1.Rows.Clear();
                        int num = Convert.ToInt32(trozos[1]);
                        dataGridView1.RowCount = num;
                        int n = 0;
                        for (int i = 0; i < num; i++)
                        {
                            dataGridView1.Rows[n].Cells[0].Value = trozos[n + 2];
                            n++;
                        }
                        this.dataGridView1.Rows[0].Cells[0].Selected = false;
                        break;

                }
            }

        }

        private void Form2_Load(object sender, EventArgs e) //Iniciamos la conexión
        {
            //Creamos un IDEndPoint con el IP y puerto del servidor
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9005);

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
            MessageBox.Show("Te has desconectado");
            //Cerramos la conexión
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.Close();
            atender.Abort();
        }

     
        private void AceptarConsulta_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                // Quiere saber la longitud
                string mensaje = "3/" + textBox1.Text;
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
    }
}
