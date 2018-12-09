using System;
using System.Threading;
using System.Threading.Tasks;

namespace UseCancelWaitHandle
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var task1 = new Task(() => {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    bool cancelled = token.WaitHandle.WaitOne(10000);
                    System.Console.WriteLine($"{i} - {cancelled}");
                    if (cancelled) {
                        throw new OperationCanceledException(token);
                    }
                }
            }, token);
            task1.Start();
            System.Console.WriteLine("Enter to cancel");
            Console.ReadLine();
            tokenSource.Cancel();
            System.Console.WriteLine("Main");
            Console.ReadKey();
        }
    }
}
