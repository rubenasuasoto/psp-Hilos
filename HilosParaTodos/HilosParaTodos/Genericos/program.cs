using HilosParaTodos;
using HilosParaTodos.Genericos;

FinishEvent finalEvent = new FinishEvent();
Wrapper<Action> finalEventv2 = new Wrapper<Action>(() => { });

Genericos t1 = new Genericos("x", finalEventv2);
Genericos t2 = new Genericos("y", finalEventv2);

finalEvent.FinishAction += () => { Console.WriteLine("Suscriptor C"); };

t1.Start();
t2.Start();



//MyEvents.finalizar = () => { Console.WriteLine("Suscriptor A"); };
//MyEvents.finalizar += () => { Console.WriteLine("Suscriptor B"); };