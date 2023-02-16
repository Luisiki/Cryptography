using System;
using Krypto.Operace;


namespace Krypto.Krypto // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Operations operace = new Operations(37);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(operace.BabyStepGiantStep(37, 2, 7));

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs);
        }
    }
}