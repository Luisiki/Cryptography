using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
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
            BigInteger modulus = 10;


            List<BigInteger> brutes = new List<BigInteger>();
            while (modulus < 1001)
            {
                brutes.Add(cryptoOperations.eulerPhi(modulus, EulerPhiSelection.Brute));
                modulus++;
            }
            /*
            modulus = 10;
            List<BigInteger> mobiuses = new List<BigInteger>();
            while (modulus < 1001)
            {
                mobiuses.Add(cryptoOperations.eulerPhi(modulus, EulerPhiSelection.Mobius));
                modulus++;
            }
            */

            modulus = 10;
            var hashSet = new HashSet<BigInteger>();
            String errors = "";
            String modulusErrors="";
            /*
            foreach (var value in mobiuses)
            {
                if (!hashSet.Add(value))
                {
                    errors += value + ",";
                    modulusErrors += modulus + ",";
                }

                modulus++;
            }*/
            String brutess = "";
            foreach (var VARIABLE in brutes)
            {
                brutess += VARIABLE + ",";
            }

            //fileOperations.saveToFile("/Files/", "error_outputs.txt", errors);
            //fileOperations.saveToFile("/Files/", "modulus_Errors.txt", modulusErrors);
            fileOperations.saveToFile("/Files/", "correct_values.txt", brutess);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileOperations.get_dir(),
            };


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