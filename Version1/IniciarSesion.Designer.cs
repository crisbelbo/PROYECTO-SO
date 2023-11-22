
namespace Version1
{
    partial class IniciarSesion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.usuarioIn = new System.Windows.Forms.TextBox();
            this.contraseñaIn = new System.Windows.Forms.TextBox();
            this.inicioSesion = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DataGridListaConectados = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Desconectar = new System.Windows.Forms.Button();
            this.AceptarConsulta = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ConsultaIn = new System.Windows.Forms.TextBox();
            this.Consulta1 = new System.Windows.Forms.RadioButton();
            this.AceptarRegistro = new System.Windows.Forms.Button();
            this.contraseñaReg = new System.Windows.Forms.TextBox();
            this.usuarioReg = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Jugar = new System.Windows.Forms.Button();
            this.ListaConectados = new System.Windows.Forms.Button();
            this.LblChat = new System.Windows.Forms.Label();
            this.EscribirEnChat = new System.Windows.Forms.TextBox();
            this.EnviarMensaje = new System.Windows.Forms.Button();
            this.CasillaChat = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridListaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Introduce los datos para iniciar sesión en el juego:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contraseña:";
            // 
            // usuarioIn
            // 
            this.usuarioIn.Location = new System.Drawing.Point(162, 100);
            this.usuarioIn.Name = "usuarioIn";
            this.usuarioIn.Size = new System.Drawing.Size(139, 26);
            this.usuarioIn.TabIndex = 3;
            this.usuarioIn.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contraseñaIn
            // 
            this.contraseñaIn.Location = new System.Drawing.Point(194, 139);
            this.contraseñaIn.Name = "contraseñaIn";
            this.contraseñaIn.PasswordChar = '*';
            this.contraseñaIn.Size = new System.Drawing.Size(145, 26);
            this.contraseñaIn.TabIndex = 4;
            // 
            // inicioSesion
            // 
            this.inicioSesion.Location = new System.Drawing.Point(136, 206);
            this.inicioSesion.Name = "inicioSesion";
            this.inicioSesion.Size = new System.Drawing.Size(132, 41);
            this.inicioSesion.TabIndex = 5;
            this.inicioSesion.Text = "Iniciar Sesión";
            this.inicioSesion.UseVisualStyleBackColor = true;
            this.inicioSesion.Click += new System.EventHandler(this.inicioSesion_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "No tienes una cuenta? Registrate ya!";
            // 
            // DataGridListaConectados
            // 
            this.DataGridListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridListaConectados.Location = new System.Drawing.Point(702, 72);
            this.DataGridListaConectados.Name = "DataGridListaConectados";
            this.DataGridListaConectados.RowHeadersWidth = 62;
            this.DataGridListaConectados.RowTemplate.Height = 28;
            this.DataGridListaConectados.Size = new System.Drawing.Size(320, 255);
            this.DataGridListaConectados.TabIndex = 27;
            this.DataGridListaConectados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(698, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "Consulta aquí la lista de conectados:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(1203, 157);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label6.MaximumSize = new System.Drawing.Size(30, 30);
            this.label6.MinimumSize = new System.Drawing.Size(30, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 30);
            this.label6.TabIndex = 29;
            // 
            // Desconectar
            // 
            this.Desconectar.Location = new System.Drawing.Point(461, 39);
            this.Desconectar.Name = "Desconectar";
            this.Desconectar.Size = new System.Drawing.Size(146, 41);
            this.Desconectar.TabIndex = 30;
            this.Desconectar.Text = "Desconectarme";
            this.Desconectar.UseVisualStyleBackColor = true;
            this.Desconectar.Click += new System.EventHandler(this.Desconectar_Click);
            // 
            // AceptarConsulta
            // 
            this.AceptarConsulta.Location = new System.Drawing.Point(388, 621);
            this.AceptarConsulta.Name = "AceptarConsulta";
            this.AceptarConsulta.Size = new System.Drawing.Size(101, 35);
            this.AceptarConsulta.TabIndex = 36;
            this.AceptarConsulta.Text = "Aceptar";
            this.AceptarConsulta.UseVisualStyleBackColor = true;
            this.AceptarConsulta.Click += new System.EventHandler(this.AceptarConsulta_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 628);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "Nombre del jugador:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 563);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(393, 20);
            this.label8.TabIndex = 34;
            this.label8.Text = "Introduce el nombre del jugador que quieras consultar:";
            // 
            // ConsultaIn
            // 
            this.ConsultaIn.Location = new System.Drawing.Point(217, 625);
            this.ConsultaIn.Name = "ConsultaIn";
            this.ConsultaIn.Size = new System.Drawing.Size(144, 26);
            this.ConsultaIn.TabIndex = 33;
            // 
            // Consulta1
            // 
            this.Consulta1.AutoSize = true;
            this.Consulta1.Location = new System.Drawing.Point(64, 692);
            this.Consulta1.Name = "Consulta1";
            this.Consulta1.Size = new System.Drawing.Size(449, 24);
            this.Consulta1.TabIndex = 32;
            this.Consulta1.TabStop = true;
            this.Consulta1.Text = "Consulta 1: Dime cuantas partidas ha ganado este jugador\r\n";
            this.Consulta1.UseVisualStyleBackColor = true;
            // 
            // AceptarRegistro
            // 
            this.AceptarRegistro.Location = new System.Drawing.Point(163, 475);
            this.AceptarRegistro.Name = "AceptarRegistro";
            this.AceptarRegistro.Size = new System.Drawing.Size(102, 41);
            this.AceptarRegistro.TabIndex = 43;
            this.AceptarRegistro.Text = "ACEPTAR";
            this.AceptarRegistro.UseVisualStyleBackColor = true;
            this.AceptarRegistro.Click += new System.EventHandler(this.AceptarRegistro_Click);
            // 
            // contraseñaReg
            // 
            this.contraseñaReg.Location = new System.Drawing.Point(180, 414);
            this.contraseñaReg.Name = "contraseñaReg";
            this.contraseñaReg.Size = new System.Drawing.Size(155, 26);
            this.contraseñaReg.TabIndex = 42;
            // 
            // usuarioReg
            // 
            this.usuarioReg.Location = new System.Drawing.Point(163, 369);
            this.usuarioReg.Name = "usuarioReg";
            this.usuarioReg.Size = new System.Drawing.Size(157, 26);
            this.usuarioReg.TabIndex = 41;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(81, 464);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 20);
            this.label9.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 414);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 20);
            this.label10.TabIndex = 39;
            this.label10.Text = "Contraseña:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(77, 372);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 38;
            this.label11.Text = "Usuario:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 313);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(262, 20);
            this.label12.TabIndex = 37;
            this.label12.Text = "Registrate para poder iniciar sesión:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1035, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 20);
            this.label13.TabIndex = 45;
            this.label13.Text = "Número de servicios:";
            // 
            // Jugar
            // 
            this.Jugar.Location = new System.Drawing.Point(461, 196);
            this.Jugar.Name = "Jugar";
            this.Jugar.Size = new System.Drawing.Size(146, 41);
            this.Jugar.TabIndex = 46;
            this.Jugar.Text = "Vamos a jugar!";
            this.Jugar.UseVisualStyleBackColor = true;
            this.Jugar.Click += new System.EventHandler(this.Jugar_Click);
            // 
            // ListaConectados
            // 
            this.ListaConectados.Location = new System.Drawing.Point(1074, 69);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.Size = new System.Drawing.Size(146, 41);
            this.ListaConectados.TabIndex = 47;
            this.ListaConectados.Text = "Lista Conectados";
            this.ListaConectados.UseVisualStyleBackColor = true;
            this.ListaConectados.Click += new System.EventHandler(this.button1_Click);
            // 
            // LblChat
            // 
            this.LblChat.AutoSize = true;
            this.LblChat.Location = new System.Drawing.Point(636, 403);
            this.LblChat.Name = "LblChat";
            this.LblChat.Size = new System.Drawing.Size(56, 20);
            this.LblChat.TabIndex = 49;
            this.LblChat.Text = "CHAT:";
            // 
            // EscribirEnChat
            // 
            this.EscribirEnChat.Location = new System.Drawing.Point(661, 872);
            this.EscribirEnChat.Name = "EscribirEnChat";
            this.EscribirEnChat.Size = new System.Drawing.Size(349, 26);
            this.EscribirEnChat.TabIndex = 50;
            // 
            // EnviarMensaje
            // 
            this.EnviarMensaje.Location = new System.Drawing.Point(1016, 863);
            this.EnviarMensaje.Name = "EnviarMensaje";
            this.EnviarMensaje.Size = new System.Drawing.Size(96, 44);
            this.EnviarMensaje.TabIndex = 51;
            this.EnviarMensaje.Text = "ENVIAR";
            this.EnviarMensaje.UseVisualStyleBackColor = true;
            this.EnviarMensaje.Click += new System.EventHandler(this.EnviarMensaje_Click);
            // 
            // CasillaChat
            // 
            this.CasillaChat.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CasillaChat.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CasillaChat.Location = new System.Drawing.Point(640, 426);
            this.CasillaChat.Name = "CasillaChat";
            this.CasillaChat.Size = new System.Drawing.Size(492, 409);
            this.CasillaChat.TabIndex = 52;
            this.CasillaChat.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(166, 9);
            this.label14.MaximumSize = new System.Drawing.Size(10, 10);
            this.label14.MinimumSize = new System.Drawing.Size(80, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 25);
            this.label14.TabIndex = 53;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(147, 20);
            this.label15.TabIndex = 54;
            this.label15.Text = "Usuario conectado:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(492, 356);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(688, 20);
            this.label16.TabIndex = 55;
            this.label16.Text = "Ya tienes un contrincante contra el cual jugar, cuando estés listo pulsa el botón" +
    " \"Vamos a jugar!\"";
            // 
            // IniciarSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 936);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.CasillaChat);
            this.Controls.Add(this.EnviarMensaje);
            this.Controls.Add(this.EscribirEnChat);
            this.Controls.Add(this.LblChat);
            this.Controls.Add(this.ListaConectados);
            this.Controls.Add(this.Jugar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.AceptarRegistro);
            this.Controls.Add(this.contraseñaReg);
            this.Controls.Add(this.usuarioReg);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.AceptarConsulta);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ConsultaIn);
            this.Controls.Add(this.Consulta1);
            this.Controls.Add(this.Desconectar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DataGridListaConectados);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inicioSesion);
            this.Controls.Add(this.contraseñaIn);
            this.Controls.Add(this.usuarioIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "IniciarSesion";
            this.Text = "INICIO DE SESIÓN";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridListaConectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox usuarioIn;
        private System.Windows.Forms.TextBox contraseñaIn;
        private System.Windows.Forms.Button inicioSesion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DataGridListaConectados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Desconectar;
        private System.Windows.Forms.Button AceptarConsulta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ConsultaIn;
        private System.Windows.Forms.RadioButton Consulta1;
        private System.Windows.Forms.Button AceptarRegistro;
        private System.Windows.Forms.TextBox contraseñaReg;
        private System.Windows.Forms.TextBox usuarioReg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button Jugar;
        private System.Windows.Forms.Button ListaConectados;
        private System.Windows.Forms.Label LblChat;
        private System.Windows.Forms.TextBox EscribirEnChat;
        private System.Windows.Forms.Button EnviarMensaje;
        private System.Windows.Forms.GroupBox CasillaChat;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}