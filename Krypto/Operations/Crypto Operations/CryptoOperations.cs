using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Krypto.Operations.Crypto_Operations
{
    public class CryptoOperations
    {

        private Intermediaries intern = null;

        public CryptoOperations()
        {
            Initialization();
        }

        private void Initialization()
        {
            this.intern = new Intermediaries();
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

        public BigInteger FindGenerator(BigInteger modulus)
        {
            var rand = new Random();
            bool flag = true;

            BigInteger temp;
            BigInteger potentialGenerator = 1;

            for (BigInteger i = 0; (i < modulus - 1) && flag; i++)
            {
                potentialGenerator = rand.NextInt64(2, (long)modulus);

                for (BigInteger j = 1; j < modulus ; j++)
                {
                    temp = intern.PowMod(potentialGenerator, j, modulus);
                    if (temp ==1 && (j != (modulus - 1)))
                    {
                        break;
                    }

                    if (temp == 1 && j == (modulus - 1))
                    {
                        flag = false;
                        break;
                    }
                
                }
            }

            /* Check
            for (BigInteger i = 1; (i < modulus); i++)
            {
                Console.WriteLine(intern.PowMod(potentialGenerator, i, modulus));
            }
            */

            return potentialGenerator;
        }

        public BigInteger powMod(BigInteger x, BigInteger y, BigInteger modulo)
        {
            return (intern.PowMod(x, y, modulo));
        }

        public BigInteger quadraticSieve(BigInteger n)
        {
            BigInteger limit = intern.SqrtFast(n);
            BigInteger[] primes = generatePrimes(limit);

            BigInteger[] factors = new BigInteger[primes.Length];
            BigInteger[] exponents = new BigInteger[primes.Length];
            int k = primes.Length;



            while (true)
            {
                BigInteger a = intern.getRandomBigInteger(2, n - 1);
                BigInteger x = intern.PowMod(a, 2, n);
                BigInteger y = x;

                for (int i = 0; i < k; i++)
                {
                    BigInteger p = primes[i];
                    int e = 0;
                    while (BigInteger.Remainder(y, p) == 0)
                    {
                        y = BigInteger.Divide(y, p);
                        e++;
                    }

                    factors[i] = intern.PowMod(primes[i], ((exponents[i] + e) % 2), n);
                    exponents[i] = (exponents[i] + e) % 2;
                }

                if (y == 1)
                {
                    BigInteger factor = 1;

                    for (int i = 0; i < k; i++)
                    {
                        factor = BigInteger.Multiply(factor, intern.PowMod(factors[i], exponents[i], n));
                    }

                    return factor;
                }
            }
        }

        private BigInteger[] generatePrimes(BigInteger limit)
        {
            bool[] sieve = new bool[(int)BigInteger.Remainder(limit + 1, int.MaxValue)];

            sieve[0] = sieve[1] = true;
            for (int i = 2; i * i <= limit; i++)
            {
                if (!sieve[i])
                {
                    for (int j = i * i; j < (int)BigInteger.Remainder(limit + 1, int.MaxValue); j += i)
                    {
                        sieve[j] = true;
                    }
                }
            }


            BigInteger[] primes = new BigInteger[(int)limit + 1];
            int k = 0;

            for (int i = 2; i <= limit; i++)
            {
                if (!sieve[i])
                {
                    primes[k++]= i;
                }
            }
            Array.Resize(ref primes, k);

            return primes;
        }

        /// <summary>
        /// Pollard Rho algorithm, returns a field of all factors, not all are primes, some may be composites -> need to fix
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public BigInteger[] pollardRho(BigInteger input)
        {
            List<BigInteger> factors = new List<BigInteger>();
            BigInteger[] temp = new BigInteger[2];
            temp[0] = input;
            temp[1] = input;

            temp = pollard(temp[0], temp[1]);
            factors.Add(temp[0]);
            while (temp[1] != (BigInteger)1)
            {
                temp = pollard(temp[1], temp[1]);

                factors.Add(temp[0]);
            }  


            BigInteger[] res = new BigInteger[factors.Count];
            for (int i = 0; i < factors.Count; i++)
            {
                res[i] = factors[i];
            }

            return res;
        }

        private BigInteger[] pollard(BigInteger input, BigInteger modulo)
        {
            BigInteger x, y, z;
            BigInteger c, d = 1;



            x = intern.getRandomBigInteger(2, input - 1);
            y = x;

            c = intern.getRandomBigInteger(2, input - 1);

            BigInteger index = 0;
            while (d == 1)
            {
                x = fX(x, c, modulo);
                y = gx(y, c, modulo);

                d = BigInteger.GreatestCommonDivisor(abs(x - 1), modulo);

                index++;
                if (index > 10000)
                {
                    c++;
                    if (d != 1)
                    {
                        if (c > input || MillerRabin(d, 100))
                        {
                            return new BigInteger[] { d, 1 };
                        }
                    }

                    index = 0;
                }
            }




            return new BigInteger[] { d, modulo / d };
        }

        /// <summary>
        /// Miller rabin primality test
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public bool MillerRabin(BigInteger candidate, int numberOfIterations)
        {
            if (candidate % 2 == 0)
            {
                return false;
            }

            for (int j = 0; j < numberOfIterations; j++)
            {
                BigInteger a = intern.getRandomBigInteger(2, candidate - 2);
                BigInteger[] ds = getMillerRabinIntern(candidate);//d,s

                var variable = intern.PowMod(a, ds[0], candidate);
                if (variable != 1 && variable != candidate - 1)
                {
                    return false;
                }


                for (int i = 0; i < ds[1]; i++)
                {
                    variable = intern.PowMod(variable, 2, candidate);
                    if (variable != 1 && variable != candidate - 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// returns d and s, where n - 1 = 2^s * d mod n
        /// </summary>
        /// <returns></returns>
        private BigInteger[] getMillerRabinIntern(BigInteger n)
        {
            BigInteger s, d;

            s = intern.getRandomBigInteger(2, n - 2);

            d = (n - 1) / intern.PowMod(2, s, n);

            return new BigInteger[]{d, s};
        }

        /// <summary>
        /// pollard[0] == factor
        /// pollard[1] == remainder to be factored, if 1 than it is finished
        /// </summary>
        /// <param name="input"></param>
        /// <param name="modulo"></param>
        /// <returns></returns>
        

        private BigInteger abs(BigInteger input)
        {
            if (input < 0)
            {
                return -input;
            }

            return input;
        }

        /// <summary>
        /// x^2 + c mod n
        /// </summary>
        /// <param name="x"></param>
        /// <param name="c"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private BigInteger fX(BigInteger x, BigInteger c, BigInteger n)
        {
            return (intern.PowMod(x, 2, n) + c) % n;
        }
        /// <summary>
        /// 2x + d mod n
        /// </summary>
        /// <param name="x"></param>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private BigInteger gx(BigInteger x, BigInteger d, BigInteger n)
        {
            return ((x << 1) + d) % n;
        }





    }

    internal class Intermediaries
    {
        private static Random random = new Random();

        public long EulerPhi(long modulus)
        {
            return modulus - 1;
        }

        public long EulerPhi(long modulus1, long modulus2)
        {
            return ((modulus1 - 1) * (modulus2 - 1));
        }

        public static BigInteger Sqrt(BigInteger n)
        {
            if (n == 0) return 0;
            BigInteger x = n / 2 + 1;
            BigInteger y = (x + n / x) / 2;
            while (y < x)
            {
                x = y;
                y = (x + n / x) / 2;
            }
            return x;
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


        public BigInteger getRandomBigInteger(BigInteger min, BigInteger max)
        {
            byte[] bytes = max.ToByteArray();
            BigInteger result;

            do
            {
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= 0x7F; //remove sign
                result = new BigInteger(bytes);
            } while (result < min || result > max);

            return result;
        }


    }
}
