using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using Krypto.Operations.Crypto_Operations;
using Krypto.Operations.File_Operations;
using Microsoft.VisualBasic.CompilerServices;


namespace Krypto.Krypto // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CryptoOperations cryptoOperations = new CryptoOperations();
            FileOperations fileOperations = new FileOperations();



            String times = "";

            
            BigInteger modulus = 100000000;

            for (int u = 0; u < 1000; u++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                BigInteger[] res = (cryptoOperations.Factorization(modulus,
                    FactorizationSelection.LenstraEllipticCurveFactorization));
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                    //watch.ElapsedTicks - TimeSpan.TicksPerMillisecond / 1000;
                times += elapsedMs+",";
                Console.Write(modulus + "\n");
                modulus++;
            }
            
            fileOperations.saveToFile("/Files/","times_optimized_50k_100k.txt",times);
            

            //BigInteger tmp = cryptoOperations.getGenerator(modulus, GeneratorSearch.PollardRho);

            /*
            var temp = new BigInteger();
            for (int i = 1; i < modulus; i++)
            {
                temp = BigInteger.ModPow(tmp, i, modulus);
                if (temp == 1 && i != modulus-1)
                {
                    Console.WriteLine("Invalid generator!");
                    break;
                }
                Console.Write(temp + ", ");
            }
            Console.WriteLine("\nGenerator: " + tmp);
            */

            /*
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
            */


        }
    }
}