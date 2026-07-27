//#define Jiji
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {

        static void Main()
        {
            Parallel.Invoke(
                () => { Console.WriteLine($"Task 1 on thread {Task.CurrentId}"); Task.Delay(3000).Wait(); },
                () => { Console.WriteLine($"Task 2 on thread {Task.CurrentId}"); Task.Delay(3000).Wait(); },
                () => { Console.WriteLine($"Task 3 on thread {Task.CurrentId}"); Task.Delay(3000).Wait(); });
            Console.WriteLine("All tasks completed");
        }
        
    }
}
