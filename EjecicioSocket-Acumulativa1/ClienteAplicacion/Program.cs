using System;
using ClienteLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteAplicacion
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la direccíón del servidor:");
            string servidor = Console.ReadLine().Trim();
            int puerto;

            do
            {
                Console.WriteLine("Ingrese el puerto a conectarse: ");
            } while (!Int32.TryParse(Console.ReadLine().Trim(), out puerto));

            Console.WriteLine("Conectado al servidor!");

            Conect clienteSocket = new Conect(servidor,puerto);

            if (clienteSocket.Conectar())
            {
                Comunicacion(clienteSocket);
            }
            else
            {
                Console.WriteLine("Se ha producido un error...");
            }
            Console.ReadKey();
        }
        static void Comunicacion(Conect clienteSocket)
        {
            Console.Clear();
            bool terminar = false;
            while (!terminar)
            {
                Console.WriteLine("Ingrese el mensaje:");
                string mensaje = Console.ReadLine().Trim();
                clienteSocket.Escribir(mensaje);

                if (mensaje.ToLower() == "chao")
                {
                    terminar = true;
                }
                else
                {
                    mensaje = clienteSocket.Leer();
                    if (mensaje != null)
                    {
                        Console.WriteLine("S: {0}", mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }
                    }
                    else
                    {
                        terminar = true;
                    }
                }
            }
            clienteSocket.Desconectar();

        }
    }
}
