using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Krypto.Operations.Crypto_Operations;

namespace Krypto.Operations.Mode_testing
{
    internal class Testing
    {
        private String[] timeOutputs;

        private Stopwatch watch;

        public Testing()
        {

        }


        /// <summary>
        /// Tests a specific algorithm, saves recorded times into "fileName" + algorithm name + .txt file.
        /// </summary>
        /// <param name="modulusStart"></param>
        /// <param name="modulusFinish"></param>
        /// <param name="selection"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FactorTesting(BigInteger modulusStart, BigInteger modulusFinish, FactorizationSelection selection, String fileName)
        {

            return true;
        }

        /// <summary>
        /// Tests all factorization algorithms implemented, saves all times recorded into "fileName" + algorithm name + .txt file.
        /// Returns true when finished.
        /// </summary>
        /// <param name="modulusStart"></param>
        /// <param name="modulusFinish"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FactorTesting(BigInteger modulusStart, BigInteger modulusFinish, String fileName)
        {

            return true;
        }

        /// <summary>
        /// Tests all specified factorization algorithms, saves all times recorded into "fileName" + algorithm name + .txt file.
        /// Returns true when finished.        /// </summary>
        /// <param name="modulusStart"></param>
        /// <param name="modulusFinish"></param>
        /// <param name="selections"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FactorTesting(BigInteger modulusStart, BigInteger modulusFinish, FactorizationSelection[] selections, String fileName)
        {
            for (int i = 0; i < selections.Length; i++)
            {
                switch (selections[i])
                {
                    case FactorizationSelection.Brute:
                        break;
                    case FactorizationSelection.BabyStepGiantStep:
                        break;
                    case FactorizationSelection.PollarRho:
                        break;
                    case FactorizationSelection.LenstraEllipticCurveFactorization:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return true;
        }















    }
}
