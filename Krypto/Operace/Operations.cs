using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Krypto.Operace
{
    public class Operations
    {

        private long modulo;
        private long Phi_m;
        private Intermediaries intern;


        public Operations(int modulo)
        {
            Initialization(modulo);
        }

        private void Initialization(int modulo)
        {
            this.modulo = modulo;
            this.intern = new Intermediaries();
            this.Phi_m = intern.EulerPhi(modulo);
        }

        public BigInteger BabyStepGiantStep(BigInteger modulo, BigInteger generator, BigInteger b)
        {
            BigInteger order = modulo - 1;
            BigInteger h = intern.SqrtFast(order);

            List<BigInteger> BabyStep = new List<BigInteger>();

            for (BigInteger j = 0; j < h; j++)
            {
                BabyStep.Add(intern.PowMod(generator, j, modulo));
            }

            BigInteger intermediary = intern.PowMod(generator, order - h, modulo);
            List<BigInteger> GiantStep = new List<BigInteger>();

            for (BigInteger i = 0; i < h; i++)
            {
                GiantStep.Add((b*intern.PowMod(intermediary,i,modulo))% modulo);
            }

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (BabyStep[j] == GiantStep[i])
                    {
                        return (i * h + j);
                    }
                }
            }
            return 0;
        }










    }

    internal class Intermediaries
    {
        public long EulerPhi(long modulus)
        {
            return modulus - 1;
        }

        public long EulerPhi(long modulus1, long modulus2)
        {
            return ((modulus1 - 1) * (modulus2 - 1));
        }



        /// <summary>
        /// Returns x^y mod p
        /// </summary>
        /// <param name="x">base</param>
        /// <param name="y">exponent</param>
        /// <param name="p">modulus</param>
        /// <returns></returns>
        public BigInteger PowMod(BigInteger x, BigInteger y, BigInteger p)
        {
            if (y < (BigInteger)0)
            {
                y = p + y;
            }

            return (PMod((BigInteger)x, (BigInteger)y, (BigInteger)p));
        }

        private static BigInteger PMod(BigInteger x, BigInteger y, BigInteger p)
        {

            BigInteger res = 1;
            x = x % p;
            while (y > 0)
            {
                if ((y & 1) == 1)
                    res = (res * x) % p;
                y = y >> 1;
                x = (x * x) % p;
            }
            return res;
        }

        private static readonly BigInteger FastSqrtSmallNumber = 4503599761588223UL;

        public BigInteger SqrtFast(BigInteger value)
        {
            if (value <= FastSqrtSmallNumber) // small enough for Math.Sqrt() or negative?
            {
                if (value.Sign < 0) throw new ArgumentException("Negative argument.");
                return (ulong)Math.Sqrt((ulong)value);
            }

            BigInteger root; // now filled with an approximate value
            int byteLen = value.ToByteArray().Length;
            if (byteLen < 128) // small enough for direct double conversion?
            {
                root = (BigInteger)Math.Sqrt((double)value);
            }
            else // large: reduce with bitshifting, then convert to double (and back)
            {
                root = (BigInteger)Math.Sqrt((double)(value >> (byteLen - 127) * 8)) << (byteLen - 127) * 4;
            }

            for (; ; )
            {
                var root2 = value / root + root >> 1;
                if ((root2 == root || root2 == root + 1) && IsSqrt(value, root)) return root;
                root = value / root2 + root2 >> 1;
                if ((root == root2 || root == root2 + 1) && IsSqrt(value, root2)) return root2;
            }
        }

        static bool IsSqrt(BigInteger value, BigInteger root)
        {
            var lowerBound = root * root;

            return value >= lowerBound && value <= lowerBound + (root << 1);
        }



    }
}
