using System;
using System.Numerics;
using Krypto.Operations.Crypto_Operations;
using Krypto.Operations.File_Operations;


namespace Krypto.Krypto // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CryptoOperations cryptoOperations = new CryptoOperations();
            FileOperations fileOperations = new FileOperations();
            /*
            
            Console.WriteLine(cryptoOperations.BabyStepGiantStep(37, 2, 7));

            
            var elapsedMs = watch.ElapsedTicks/(TimeSpan.TicksPerMillisecond / 1000);
            Console.WriteLine(elapsedMs);
            */

            BigInteger[] primes = fileOperations.readFileNumbers("/Files/", "primes.txt");

            String text = "0";
            for (int i = 10; i < (primes.Length)/2; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                cryptoOperations.FindGenerator(primes[i]);
                watch.Stop();
                var elapsedMs = watch.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000);
                text += "," + elapsedMs;
            }

            fileOperations.saveToFile("/Files/", "time.txt", text);
        }
    }
}