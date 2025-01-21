namespace HilosParaTodos;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

class FuerzaBrutaSimple
{
    private static string hashContraseñaElegida;
    private static string contraseñaEncontrada = null;

    static void Main()
    {
        
        string diccionarioPath = "C:\\Users\\ruben\\RiderProjects\\psp-Hilos\\HilosParaTodos\\HilosParaTodos\\password.txt";
        if (!File.Exists(diccionarioPath))
        {
            Console.WriteLine("El archivo no existe.");
            return;
        }
        string[] contraseñas = File.ReadAllLines(diccionarioPath);

        
        Random random = new Random();
        string contraseñaElegida = contraseñas[random.Next(contraseñas.Length)];
        hashContraseñaElegida = CalcularHash(contraseñaElegida);

        Console.WriteLine("Contraseña elegida: " + contraseñaElegida);

        
        int mitad = contraseñas.Length / 2;

        Thread hilo1 = new Thread(() => FuerzaBrutaHilo(contraseñas, 0, mitad));
        Thread hilo2 = new Thread(() => FuerzaBrutaHilo(contraseñas, mitad, contraseñas.Length));

        hilo1.Start();
        hilo2.Start();

        hilo1.Join();
        hilo2.Join();

        if (contraseñaEncontrada != null)
        {
            Console.WriteLine("Contraseña encontrada: " + contraseñaEncontrada);
        }
        else
        {
            Console.WriteLine("No se encontró la contraseña.");
        }
    }

    static void FuerzaBrutaHilo(string[] contraseñas, int inicio, int fin)
    {
        for (int i = inicio; i < fin; i++)
        {
            if (contraseñaEncontrada != null) return;

            string hash = CalcularHash(contraseñas[i]);

            if (hash == hashContraseñaElegida)
            {
                contraseñaEncontrada = contraseñas[i];
                return;
            }
        }
    }

    static string CalcularHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
