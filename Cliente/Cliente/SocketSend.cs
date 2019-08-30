using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;


namespace Cliente
{
    static class SocketSend
    {

        public static Socket socketSend;
        public static int portSend;
        public static IPEndPoint iPEndPointSend;
        public static int x;
    
        public static void Send( int Y_rojo)
        {
            portSend = 12345; //Puerto para recibir datos
            iPEndPointSend = new IPEndPoint(IPAddress.Parse("192.168.1.71"), portSend);
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);          
            socketSend.Connect( iPEndPointSend); //Establecer conexión con el cliente                               
            socketSend.Send( BitConverter.GetBytes(Y_rojo), SocketFlags.None);
            socketSend.Close();
        }
    }
}
