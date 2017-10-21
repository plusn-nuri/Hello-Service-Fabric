using System;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using Reggie.Interfaces;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var reggie = ActorProxy.Create<IReggie>(new ActorId("bob@bob.bob"), applicationName: "HelloFab1");

            reggie.Register(new Person { Email = "bob@bob.bob" }).GetAwaiter().GetResult();

            var person = reggie.Get().Result;

            Console.WriteLine($"Got {person.Email}");

            Console.ReadKey();
        }
    }
}
