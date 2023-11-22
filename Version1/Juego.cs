using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Version1
{
    public partial class Juego : Form
    {
        private List<string> simbolos; // Lista de símbolos del juego Dobble
        private List<PictureBox> CartasPictureBoxes; // Lista de PictureBox para mostrar las cartas
        private Random selectorCartaRandom = new Random();

        public Juego()
        {
            InitializeComponent();
            // Inicializar la lista de símbolos
            simbolos = new List<string> { "StormTropper", "El Mandaloriano", "Luke Skywalker", "Blurrg", "Gamorreano", "R2-D2", "Ahsoka Tano", "Fennec Shand", "Speeder", "Bantha", "Grogu", "IG-11", "Jawa", "Boba Fett" };

            // Inicializar la lista de PictureBox
            CartasPictureBoxes = new List<PictureBox>();

            // Crear cartas y mostrarlas en la interfaz gráfica
            CreateAndDisplayCards();
        }

        private void Juego_Load(object sender, EventArgs e)
        {

        }


        private void CreateAndDisplayCards()
        {
            const int TamañoCarta = 100;
            const int spacing = 20;
            const int SimbolosPorCarta = 8;

            // Crear una matriz que represente las cartas y sus símbolos
            List<List<string>> cards = DobbleGenerator.GenerateCards(simbolos);

            // Calcular las dimensiones del formulario según el número de cartas y el tamaño de las cartas
            int formWidth = SimbolosPorCarta * (TamañoCarta + spacing) + spacing;
            int formHeight = (cards.Count / SimbolosPorCarta) * (TamañoCarta + spacing) + spacing;

            // Configurar las dimensiones del formulario
            this.ClientSize = new Size(formWidth, formHeight);

            // Mostrar las cartas en la interfaz gráfica
            for (int i = 0; i < cards.Count; i++)
            {
                int row = i / SimbolosPorCarta;
                int col = i % SimbolosPorCarta;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(TamañoCarta, TamañoCarta);
                pictureBox.Location = new Point(col * (TamañoCarta + spacing) + spacing, row * (TamañoCarta + spacing) + spacing);
                pictureBox.BackColor = Color.White;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;

                // Mostrar los símbolos en la carta
                for (int j = 0; j < SimbolosPorCarta; j++)
                {
                    string symbol = cards[i][j];
                    string imagePath = $"C:/Users/crisb/Desktop/SO/PROYECTO_SO/Version1/imagenes_cartas/{symbol}.png"; // Dirección de la carpeta de imagenes

                    if (File.Exists(imagePath))
                    {
                        PictureBox symbolPictureBox = new PictureBox();
                        symbolPictureBox.Image = Image.FromFile(imagePath);
                        symbolPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        symbolPictureBox.Size = new Size(TamañoCarta / SimbolosPorCarta, TamañoCarta / SimbolosPorCarta);
                        symbolPictureBox.Location = new Point(j * (TamañoCarta / SimbolosPorCarta), 0);
                        pictureBox.Controls.Add(symbolPictureBox);
                    }
                }

                CartasPictureBoxes.Add(pictureBox);
                this.Controls.Add(pictureBox);
            }
        }

    }

    // Generamos las cartas del juego Dobble
    public static class DobbleGenerator
    {
        public static List<List<string>> GenerateCards(List<string> simbolos)
        {
            List<List<string>> cards = new List<List<string>>();

            // Añadir una carta con todos los símbolos
            List<string> allSymbolsCard = new List<string>(simbolos);
            cards.Add(allSymbolsCard);

            // Añadir las demás cartas
            for (int i = 0; i < simbolos.Count; i++)
            {
                List<string> newCard = new List<string>(simbolos);
                newCard.RemoveAt(0); // Remover el primer símbolo
                newCard.Insert(0, simbolos[i]); // Insertar el primer símbolo al final
                cards.Add(newCard);
            }

            return cards;
        }
    }
}
