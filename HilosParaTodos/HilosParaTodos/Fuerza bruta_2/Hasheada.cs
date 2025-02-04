namespace HilosParaTodos;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

class FuerzaBruta
{
    private static string hashContraseña;
    private static string contraseñaEncontrada = null;
    private static readonly object lockObj = new object(); 

    static void Main()
    {
        string diccionario = "C:\\Users\\ruben\\RiderProjects\\psp-Hilos\\HilosParaTodos\\HilosParaTodos\\password.txt";
        if (!File.Exists(diccionario))
        {
            Console.WriteLine("El archivo no existe.");
            return;
        }
        string[] contraseñas = File.ReadAllLines(diccionario);

        Random random = new Random();
        string contraseñaElegida = contraseñas[random.Next(contraseñas.Length)];
        hashContraseña = CalcularHash(contraseñaElegida);

        Console.WriteLine("Contraseña elegida: " + contraseñaElegida);

        int totalHilos = 8;
        int contraseñasPorHilo = contraseñas.Length / totalHilos;
        Thread[] hilos = new Thread[totalHilos];

        for (int i = 0; i < totalHilos; i++)
        {
            int inicio = i * contraseñasPorHilo;
            int fin = (i == totalHilos - 1) ? contraseñas.Length : (i + 1) * contraseñasPorHilo;

            hilos[i] = new Thread(() => FuerzaBrutaHilo(contraseñas, inicio, fin));
            hilos[i].Start();
        }

        foreach (Thread hilo in hilos)
        {
            hilo.Join();
        }

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

            // Usamos un bloqueo para asegurar que solo un hilo accede a la variable global al mismo tiempo
            lock (lockObj)
            {
                if (hash == hashContraseña && contraseñaEncontrada == null)
                {
                    contraseñaEncontrada = contraseñas[i];
                    return;
                }
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
