using System;
using System.Threading;
using System.Threading.Tasks;

namespace DetermineIfTaskIsCancel
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource1 = new CancellationTokenSource();
            var token1 = tokenSource1.Token;
            var task1 = new Task(() => {
                for (int i = 0; i < 10; i++)
                {
                    token1.ThrowIfCancellationRequested();
                    System.Console.WriteLine(i);
                } 
            }, token1);
            var tokenSource2 = new CancellationTokenSource();
            var token2 = tokenSource2.Token;
            var task2 = new Task(() => {
                for (int i = 0; i < 10; i++)
                {
                    token2.ThrowIfCancellationRequested();
                    System.Console.WriteLine(i);
                }
            }, token2);
            task1.Start();
            task2.Start();
            tokenSource2.Cancel();
            System.Console.WriteLine(task1.IsCanceled);
            System.Console.WriteLine(task2.IsCanceled);
            System.Console.WriteLine("Main");
            Console.ReadKey();
        }
    }
}
