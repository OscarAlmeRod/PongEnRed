using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
namespace Cliente
{
    static class SocketRecived
    {
        //Socket para recibir datos, toma el papel de servidor
        public static Socket socketReceive;
        public static int portReceive;
        public static IPEndPoint iPEndPointReceive;
        public static int x, y, y_azul, p_azul, p_rojo; //Datos recibidos                

        public static Socket temp;
        public static byte[] mensajeRecibido;

        public static void init() {
            //Para recibir
            socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            portReceive = 12340;//Puerto por el que se recibiran datos
            iPEndPointReceive = new IPEndPoint(IPAddress.Parse("192.168.1.71"), portReceive);
            socketReceive.Bind(iPEndPointReceive); //Establecer conexión
            socketReceive.Listen(100);

            //Incializar socket que recibe datos                    
            temp = null;
            mensajeRecibido = new byte[20];
        }
        //Metodo para recibir datos del servidor. 
        public static void Recibir()
        {           
            while (true) {                       
                temp = socketReceive.Accept();                                                
                temp.Receive(mensajeRecibido, 0, mensajeRecibido.Length, SocketFlags.None);               
                x = BitConverter.ToInt32( mensajeRecibido, 0);
                y = BitConverter.ToInt32( mensajeRecibido, 4);
                y_azul = BitConverter.ToInt32(mensajeRecibido, 8);
                p_azul = BitConverter.ToInt32(mensajeRecibido, 12);
                p_rojo  = BitConverter.ToInt32(mensajeRecibido, 16);
                temp.Close();                
            }
            
        }
    }
}
