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

            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs + "ms");
            BigInteger modulus = 630;
            BigInteger[] res = (cryptoOperations.Factorization(modulus,
                FactorizationSelection.LenstraEllipticCurveFactorization));

            for (int i = 0; i < res.Length; i++)
            {
                Console.Write(res[i]+", ");
            }


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