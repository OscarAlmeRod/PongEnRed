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

namespace Cliente
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        int jugador1Score = 0; //Puntos del jugador 1
        int jugador2Score = 0; //Puntos del jugador 2
        Thread threadReceive; //Hilo que simpre estara escuchando los mensajes enviados


        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            initVriablesSocket();
            timer = new System.Timers.Timer();
            timer.Interval = 25;
            timer.Elapsed += datosServidor; // Add event handler          
            timer.Enabled = false;
            SocketRecived.init();//Inicializar el socket servidor para estar escuchando
            threadReceive = new Thread(new ThreadStart(SocketRecived.Recibir)); //Hilo para escuchar
            threadReceive.Start(); //Inica a escuchar
        }
        public void initVriablesSocket() {
            SocketRecived.x = pictureBox3.Location.X;
            SocketRecived.y = pictureBox3.Location.Y;
            SocketRecived.y_azul = pictureBox1.Location.Y;          
        }

        //Actualiza a los elementos del juego con los datos recibidos
        public void datosServidor(Object source, System.Timers.ElapsedEventArgs e) {                             
            pictureBox3.Location = new Point( SocketRecived.x, SocketRecived.y); //Actualizando pelota
            pictureBox1.Location = new Point( pictureBox1.Location.X, SocketRecived.y_azul); //Raqueta azul           
            label1.Text = SocketRecived.p_azul.ToString();
            label2.Text = SocketRecived.p_rojo.ToString();
        }

        //Mover la raqueta ROJA
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            //Cuando se presiono la tecla hacia abajo
            switch (e.KeyData) {
                case Keys.Down: //Mover hacia arriba
                    if (pictureBox2.Location.Y <= 396)
                        pictureBox2.Location = new Point(pictureBox2.Location.X, pictureBox2.Location.Y + 10);
                    break;
                case Keys.Up: //Mover hacia abajo
                    if (pictureBox2.Location.Y >= 0)
                        pictureBox2.Location = new Point(pictureBox2.Location.X, pictureBox2.Location.Y - 10);
                    break;
            }
            SocketSend.Send(pictureBox2.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e) {           
                button1.Visible = button1.Enabled = false; //Se deshabilita el boton                                                 
                timer.Start(); // Start the timer para que empiece a estar actualizando las coordenadas de l pelota       
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e){
            SocketRecived.socketReceive.Close(); //Cerramos el socket
            threadReceive.Interrupt();//Cerramos el hilo
            timer.Stop();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
