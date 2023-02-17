using System;
using System.Numerics;
using System.Reflection;
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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            var elapsedMs = watch.ElapsedTicks/(TimeSpan.TicksPerMillisecond / 1000);
            Console.WriteLine(elapsedMs);
            */

            BigInteger[] primes = fileOperations.readFileNumbers("/Files/", "primes.txt");
            BigInteger[] generators = fileOperations.readFileNumbers("/Files/", "generators.txt");
            var rand = new Random();
            BigInteger temp;
            for (int i = 10; i < generators.Length; i++)
            {
                temp = cryptoOperations.powMod(generators[i], rand.Next(10, (int)primes[i]-1), primes[i]);
                Console.WriteLine(generators[i] + "^" +
                                  cryptoOperations.BabyStepGiantStep(primes[i], generators[i], temp) + " = " + temp);
                Console.WriteLine("a = " + generators[i] + ", b = " + temp + ", modulo = " + primes[i]);
            }



        }
    }
}