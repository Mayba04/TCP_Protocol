using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using SharedData;

namespace tcp_client
{
   
  
    class Program
    {
        static int port = 8080; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            TcpClient client = new TcpClient();

            client.Connect(ipPoint);

           
            try
            {
                Requset requset = new Requset();
                do
                {

                    Console.Write("Enter a :");
                    requset.A = double.Parse(Console.ReadLine());
                    Console.Write("Enter b :");
                    requset.B = double.Parse(Console.ReadLine());
                    Console.Write("Enter [1-4] :");
                    requset.Operation = (OperationType)Enum.Parse(typeof(OperationType), Console.ReadLine());
                    NetworkStream ns = client.GetStream();


                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ns, requset);

                    StreamReader sr = new StreamReader(ns);
                    string response = sr.ReadLine();

                    Console.WriteLine("server response: " + response);

                    // закриваємо потокі
                    //sw.Close();
                    //sr.Close();
                    //ns.Close();
                } while (requset.A == 0);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем сокет
                //socket.Shutdown(SocketShutdown.Both);
                //socket.Disconnect(true);
                //socket.Close();
                client.Close();
            }
        }
    }
}
