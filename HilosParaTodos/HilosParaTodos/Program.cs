using System.Diagnostics;
using HilosParaTodos;
//MyEvents.finalizar = () => { Console.WriteLine("Suscriptor A"); };
//MyEvents.finalizar += () => { Console.WriteLine("Suscriptor B"); };

/*
FinishEvent finalEvent = new FinishEvent();
Wrapper<Action> finalEventv2 = new Wrapper<Action>(() => { });

MiHilo t1 = new MiHilo("x", finalEventv2);
MiHilo t2 = new MiHilo("y", finalEventv2);

finalEvent.FinishAction += () => { Console.WriteLine("Suscriptor C"); };

t1.Start();
t2.Start();


int numberOfTasks = 100;  // Número de tareas a ejecutar
int maxThreads = Environment.ProcessorCount;  // Número de núcleos de la CPU
int runningTasks = 0;  // Contador para las tareas concurrentes

// Usamos un contador para medir la cantidad de tareas simultáneas
var tasksRunningEvent = new ManualResetEvent(false);
var threadCountEvent = new ManualResetEvent(false);

// Comienza la medición
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

for (int i = 0; i < numberOfTasks; i++)
{
    ThreadPool.QueueUserWorkItem((state) =>
    {
        Interlocked.Increment(ref runningTasks);  // Incrementamos el contador de tareas

        if (runningTasks == maxThreads)
        {
            tasksRunningEvent.Set();  // Marcamos cuando hemos alcanzado el número máximo de hilos
        }

        // Simula el trabajo
        Thread.Sleep(100);

        Interlocked.Decrement(ref runningTasks);  // Decrementamos el contador cuando la tarea termina

        if (runningTasks == 0)
        {
            threadCountEvent.Set();  // Esperamos cuando todas las tareas han terminado
        }
    });
}

tasksRunningEvent.WaitOne();  // Esperamos a que el número de tareas simultáneas alcance el máximo
stopwatch.Stop();

Console.WriteLine($"Tareas concurrentes alcanzadas: {runningTasks} (alcanza {maxThreads} simultáneos)");
Console.WriteLine("Tiempo total de ejecución: " + stopwatch.ElapsedMilliseconds + " ms");

// Esperamos que todas las tareas se completen
threadCountEvent.WaitOne();
*/