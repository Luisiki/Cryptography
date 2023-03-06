using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Krypto.Operations.Crypto_Operations;
using Krypto.Operations.File_Operations;

namespace Krypto.Operations.Mode_testing
{
    internal class Testing
    {
        public Testing()
        {

        }


        /// <summary>
        /// Tests a specific algorithm, saves recorded times into "fileName" + algorithm name + modulusStart + modulusFinish + .txt file.
        /// </summary>
        /// <param name="modulusStart"></param>
        /// <param name="modulusFinish"></param>
        /// <param name="selection"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool FactorTesting(BigInteger modulusStart, BigInteger modulusFinish, FactorizationSelection selection, String fileName)
        {
            Stopwatch watch = new Stopwatch();
            String time = "";
            switch (selection)
            {
                case FactorizationSelection.Brute:
                    for (BigInteger n = modulusStart; n < modulusFinish; n++)
                    {
                        watch.Reset();
                        watch.Start();
                        CryptoOperations.Factorization(n, FactorizationSelection.Brute);
                        watch.Stop();
                        time += watch.ElapsedMilliseconds + ",";
                    }

                    FileOperations.saveToFile("/Files/Testing/",
                        fileName + "_brute_factorization" + modulusStart + "_" + modulusFinish + ".txt", time);
                    time = "";
                    break;
                case FactorizationSelection.BabyStepGiantStep:
                    for (BigInteger n = modulusStart; n < modulusFinish; n++)
                    {
                        watch.Reset();
                        watch.Start();
                        Console.WriteLine(n);
                        CryptoOperations.Factorization(n, FactorizationSelection.BabyStepGiantStep);
                        watch.Stop();
                        time += watch.ElapsedMilliseconds + ",";
                    }

                    FileOperations.saveToFile("/Files/Testing/",
                        fileName + "_BabyStepGiantStep_factorization" + modulusStart + "_" + modulusFinish + ".txt", time);
                    time = "";
                    break;
                case FactorizationSelection.PollarRho:
                    for (BigInteger n = modulusStart; n < modulusFinish; n++)
                    {
                        watch.Reset();
                        watch.Start();
                        CryptoOperations.Factorization(n, FactorizationSelection.PollarRho);
                        watch.Stop();
                        time += watch.ElapsedMilliseconds + ",";
                    }

                    FileOperations.saveToFile("/Files/Testing/",
                        fileName + "_PollarRho_factorization" + modulusStart + "_" + modulusFinish + ".txt", time);
                    time = "";
                    break;
                case FactorizationSelection.LenstraEllipticCurveFactorization:
                    for (BigInteger n = modulusStart; n < modulusFinish; n++)
                    {
                        watch.Reset();
                        watch.Start();
                        CryptoOperations.Factorization(n, FactorizationSelection.LenstraEllipticCurveFactorization);
                        watch.Stop();
                        time += watch.ElapsedMilliseconds + ",";
                    }

                    FileOperations.saveToFile("/Files/Testing/",
                        fileName + "_LenstraEllipticCurveFactorization_factorization" + modulusStart + "_" + modulusFinish + ".txt",
                        time);
                    time = "";
                    break;
                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Tests all factorization algorithms implemented, saves all times recorded into "fileName" + algorithm name + modulusStart + modulusFinish + .txt file.
        /// Returns true when finished.
        /// </summary>
        /// <param name="modulusStart"></param>
        /// <param name="modulusFinish"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool FactorTesting(BigInteger modulusStart, BigInteger modulusFinish, String fileName)
        {
            Stopwatch watch = new Stopwatch();
            String time = "";
            for (BigInteger n = modulusStart; n < modulusFinish; n++)
            {
                watch.Reset();
                watch.Start();
                CryptoOperations.Factorization(n, FactorizationSelection.Brute);
                watch.Stop();
                time += watch.ElapsedMilliseconds + ",";
            }
            FileOperations.saveToFile("/Files/Testing/", fileName + "_brute_factorization_" + "_" + modulusStart + "_" + modulusFinish + ".txt", time);
            time = "";
            for (BigInteger n = modulusStart; n < modulusFinish; n++)
            {
                watch.Reset(); 
                watch.Start(); 
                CryptoOperations.Factorization(n, FactorizationSelection.BabyStepGiantStep);
                watch.Stop();
                time += watch.ElapsedMilliseconds + ",";
            }
            FileOperations.saveToFile("/Files/Testing/", fileName + "_BabyStepGiantStep_factorization" + "_" + modulusStart + "_" + modulusFinish + ".txt", time);
            time = "";
            for (BigInteger n = modulusStart; n < modulusFinish; n++)
            {
                watch.Reset(); 
                watch.Start(); 
                CryptoOperations.Factorization(n, FactorizationSelection.PollarRho); 
                watch.Stop(); 
                time += watch.ElapsedMilliseconds + ",";
            }
            FileOperations.saveToFile("/Files/Testing/", fileName + "_PollarRho_factorization" + "_" + modulusStart + "_" + modulusFinish + ".txt", time);
            time = "";
            for (BigInteger n = modulusStart; n < modulusFinish; n++) 
            { 
                watch.Reset(); 
                watch.Start(); 
                CryptoOperations.Factorization(n, FactorizationSelection.LenstraEllipticCurveFactorization); 
                watch.Stop(); 
                time += watch.ElapsedMilliseconds + ",";
            }
            FileOperations.saveToFile("/Files/Testing/", fileName + "_LenstraEllipticCurveFactorization_factorization" + "_" + modulusStart + "_" + modulusFinish + ".txt", time);

            return true;
        }




        /// <summary>
        /// Tests all specified factorization algorithms, saves all times recorded into "fileName" + algorithm name + modulusStart + modulusFinish + .txt file.
        /// Returns true when finished.        /// </summary>
        /// <param name="modulusStart"></param>
        /// <param name="modulusFinish"></param>
        /// <param name="selections"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool FactorTesting(BigInteger modulusStart, BigInteger modulusFinish, FactorizationSelection[] selections, String fileName)
        {
            Stopwatch watch = new Stopwatch();
            String time = "";
            watch = new Stopwatch();
            for (int i = 0; i < selections.Length; i++)
            {
                switch (selections[i])
                {
                    case FactorizationSelection.Brute:
                        for (BigInteger n = modulusStart; n < modulusFinish; n++)
                        {
                            watch.Reset();
                            watch.Start();
                            CryptoOperations.Factorization(n, FactorizationSelection.Brute);
                            watch.Stop();
                            time += watch.ElapsedMilliseconds + ",";
                        }
                        FileOperations.saveToFile("/Files/Testing/", fileName+"_brute_factorization" +modulusStart+"_"+modulusFinish+".txt",time);
                        time = "";
                        break;
                    case FactorizationSelection.BabyStepGiantStep:
                        for (BigInteger n = modulusStart; n < modulusFinish; n++)
                        {
                            watch.Reset();
                            watch.Start();
                            CryptoOperations.Factorization(n, FactorizationSelection.BabyStepGiantStep);
                            watch.Stop();
                            time += watch.ElapsedMilliseconds + ",";
                        }
                        FileOperations.saveToFile("/Files/Testing/", fileName+"_BabyStepGiantStep_factorization" + modulusStart + "_" + modulusFinish + ".txt", time);
                        time = "";
                        break;
                    case FactorizationSelection.PollarRho:
                        for (BigInteger n = modulusStart; n < modulusFinish; n++)
                        {
                            watch.Reset();
                            watch.Start();
                            CryptoOperations.Factorization(n, FactorizationSelection.PollarRho);
                            watch.Stop();
                            time += watch.ElapsedMilliseconds + ",";
                        }
                        FileOperations.saveToFile("/Files/Testing/", fileName+"_PollarRho_factorization" + modulusStart + "_" + modulusFinish + ".txt", time);
                        time = "";
                        break;
                    case FactorizationSelection.LenstraEllipticCurveFactorization:
                        for (BigInteger n = modulusStart; n < modulusFinish; n++)
                        {
                            watch.Reset();
                            watch.Start();
                            CryptoOperations.Factorization(n, FactorizationSelection.LenstraEllipticCurveFactorization);
                            watch.Stop();
                            time += watch.ElapsedMilliseconds + ",";
                        }
                        FileOperations.saveToFile("/Files/Testing/", fileName + "_LenstraEllipticCurveFactorization_factorization" + modulusStart + "_" + modulusFinish + ".txt", time);
                        time = "";
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }















    }
}
