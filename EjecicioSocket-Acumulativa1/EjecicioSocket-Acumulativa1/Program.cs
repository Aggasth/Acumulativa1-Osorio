using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjecicioSocket_Acumulativa1
{
    public class Program
    {
        private int puerto;
        private Socket server;
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en puerto {0}", puerto);
            ServerSocket serverSocket = new ServerSocket(puerto);
            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("Esperando por un cliente...");
                    Socket socket = serverSocket.ObtenerCliente();
                    Console.WriteLine("Se ha conectado un cliente.");
                    ClienteCom clienteCom = new ClienteCom(socket);
                    Comunicacion(clienteCom);
                }
            }
            else
            {
                Console.WriteLine("Error con el puerto {0}, contactese con el admin", puerto);
            }
        }

        static void Comunicacion(ClienteCom clienteCom)
        {
            bool terminar = false;
            while (!terminar)
            {
                string mensaje = clienteCom.Leer();
                if (mensaje != null)
                {
                    Console.WriteLine("Cliente: {0}", mensaje);
                    if (mensaje.ToLower() == "chao")
                    {
                        terminar = true;
                    }
                    else
                    {
                        Console.Write("Ingrese Respuesta del Servidor: ");
                        mensaje = Console.ReadLine().Trim();
                        clienteCom.Escribir(mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }

                    }
                }
                else
                {
                    terminar = false;
                }
                if (terminar)
                {
                    clienteCom.Desconectar();
                }
            }
            clienteCom.Desconectar();
        }
    }
}
