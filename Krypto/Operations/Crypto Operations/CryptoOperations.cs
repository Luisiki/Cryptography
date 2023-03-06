using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Krypto.Operations.Crypto_Operations
{
    public enum GeneratorSearch : ushort
    {
        Brute = 1,
        PollardRho = 2,
        IndexCalculator = 3,
        IndexCandidates = 4
    }

    public enum EulerPhiSelection : ushort
    {
        Brute = 1,
        Mobius = 2,
        Classic = 3
    }

    public enum FactorizationSelection : ushort
    {
        Brute = 1,
        PollarRho = 2,
        BabyStepGiantStep = 3,
        LenstraEllipticCurveFactorization = 4
    }


    public class CryptoOperations
    {


        public CryptoOperations()
        {
            Initialization();
        }

        private void Initialization()
        {
        }
        public static BigInteger[] Factorization(BigInteger n, FactorizationSelection selection)
        {
            switch (selection)
            {
                case FactorizationSelection.Brute:
                    return factorizationBrute(n);
                case FactorizationSelection.PollarRho:
                    return pollardRho(n);
                case FactorizationSelection.BabyStepGiantStep:
                    return BSGS(n);
                case FactorizationSelection.LenstraEllipticCurveFactorization:
                    return LECF(n);
                default:
                    break;
            }
            return new BigInteger[] { };
        }


        public static BigInteger BabyStepGiantStep(BigInteger modulo, BigInteger generator, BigInteger b)
        {
            BigInteger order = modulo - 1;
            BigInteger h = Intermediaries.Sqrt(order);

            List<BigInteger> BabyStep = new List<BigInteger>();

            for (BigInteger j = 0; j < h; j++)
            {
                BabyStep.Add(Intermediaries.PowMod(generator, j, modulo));
            }

            BigInteger intermediary = Intermediaries.PowMod(generator, order - h, modulo);
            List<BigInteger> GiantStep = new List<BigInteger>();

            for (BigInteger i = 0; i < h; i++)
            {
                GiantStep.Add((b*Intermediaries.PowMod(intermediary,i,modulo))% modulo);
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

        private static BigInteger FindGenerator(BigInteger modulus)
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
                    temp = Intermediaries.PowMod(potentialGenerator, j, modulus);
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
                Console.WriteLine(Intermediaries.PowMod(potentialGenerator, i, modulus));
            }
            */

            return potentialGenerator;
        }

        public static  BigInteger powMod(BigInteger x, BigInteger y, BigInteger modulo)
        {
            return (Intermediaries.PowMod(x, y, modulo));
        }

        private static BigInteger[] generatePrimes(BigInteger limit)
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
        private static BigInteger[] pollardRho(BigInteger input)
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

        private static BigInteger[] pollard(BigInteger input, BigInteger modulo)
        {
            BigInteger x, y;
            BigInteger c, d = 1;

            if (input % 2 == 0)
            {
                return new BigInteger[] { 2, modulo / 2 };
            }

            x = Intermediaries.getRandomBigInteger(2, input - 1);
            y = x;

            c = Intermediaries.getRandomBigInteger(2, input - 1);

            BigInteger index = 0;
            while (d == 1)
            {
                x = CryptoOperations.fX(x, c, modulo);
                y = CryptoOperations.gx(y, c, modulo);

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
        private static bool MillerRabin(BigInteger candidate, int numberOfIterations)
        {
            if (candidate % 2 == 0)
            {
                return false;
            }

            BigInteger a = Intermediaries.getRandomBigInteger(2, candidate-1);

            if (Intermediaries.PowMod(a, candidate - 1, candidate) != 1)
            {
                return false;
            }

            BigInteger[] sd = miller(candidate);

            a = Intermediaries.getRandomBigInteger(2, candidate - 1);
            var temp = Intermediaries.PowMod(a, sd[1], candidate);

            if (!(temp != 1 && temp != candidate - 1))
            {
                return false;
            }


            for (int i = 2; i < sd[0] - 1; i+=2)
            {
                temp = Intermediaries.PowMod(a, i, candidate);
                if (!(temp != 1 && temp != candidate - 1))
                {
                    return false;
                }
            }

            return true;
        }

        private static BigInteger[] miller(BigInteger n)
        {
            BigInteger s = 0, d = 0;

            while (d % 2 != 1)
            {
                s = Intermediaries.getRandomBigInteger(2, n - 1);
                d = ((n - 1) * Intermediaries.PowMod(Intermediaries.PowMod(2, s, n), n - 2, n))%n;
            }

            return new BigInteger[]{s, d} ;
        }

        /// <summary>
        /// pollard[0] == factor
        /// pollard[1] == remainder to be factored, if 1 than it is finished
        /// </summary>
        /// <param name="input"></param>
        /// <param name="modulo"></param>
        /// <returns></returns>
        private static BigInteger abs(BigInteger input)
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
        private static BigInteger fX(BigInteger x, BigInteger c, BigInteger n)
        {
            return (Intermediaries.PowMod(x, 2, n) + c) % n;
        }

        /// <summary>
        /// 2x + d mod n
        /// </summary>
        /// <param name="x"></param>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static BigInteger gx(BigInteger x, BigInteger d, BigInteger n)
        {
            return ((x << 1) + d) % n;
        }

        /// <summary>
        /// finds one Cyclic group generator, searchType specifies which algorithm you want to use
        /// </summary>
        /// <param name="modulus"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static BigInteger getGenerator(BigInteger modulus, GeneratorSearch searchType)
        {
            switch (searchType)
            {
                case GeneratorSearch.Brute:
                    return FindGenerator(modulus);
                case GeneratorSearch.PollardRho:
                    return pollardGenerator(modulus);
                case GeneratorSearch.IndexCalculator:
                    break;
                case GeneratorSearch.IndexCandidates:
                    break;
                default:
                    throw new ArgumentException();
            }



            return -1;
        }

        private static BigInteger pollardGenerator(BigInteger modulus)
        {
            BigInteger generator = Intermediaries.getRandomBigInteger(2, modulus - 1);
            BigInteger[] primes = pollardRho(eulerPhi(modulus,EulerPhiSelection.Mobius));
            Array.Sort(primes);

            for (int i = 1; i < primes.Length; i++)
            {
                if (Intermediaries.PowMod(generator, i, modulus) != 1)
                {
                    generator = Intermediaries.getRandomBigInteger(2, modulus - 1);
                    while ((BigInteger.GreatestCommonDivisor(generator, modulus) != 1))
                    {
                        generator = Intermediaries.getRandomBigInteger(2, modulus - 1);
                    }
                }
            }


            return generator;
        }

        public static BigInteger eulerPhi(BigInteger modulus, EulerPhiSelection selection)
        {
            switch (selection)
            {
                case EulerPhiSelection.Brute:
                    return eulerPhiBrute(modulus);
                case EulerPhiSelection.Mobius:
                    return eulerPhiMobius(modulus);
                case EulerPhiSelection.Classic:
                    break;
                default:
                    return -1;
            }
            return -1;
        }

        private static BigInteger eulerPhiBrute(BigInteger modulus)
        {
            BigInteger res = modulus - 1;

            for (BigInteger i = 2; i < modulus; i++)
            {
                if (BigInteger.GreatestCommonDivisor(i, modulus) != 1)
                {
                    res--;
                }
            }

            return res;
        }


        private static BigInteger eulerPhiMobius(BigInteger modulus)
        {
            BigInteger[] primes = pollardRho(modulus);
            if (modulus == primes[0])
            {
                return modulus - 1;
            }

            BigInteger result = 0;

            for (int i = 1; i <= modulus; i++)
            {
                if (modulus % i == 0)
                {
                    result += mobiusFunction(i) * modulus / i;
                    Console.WriteLine(mobiusFunction(i) + " " + i );
                }
            }
            return result;
        }

        private static BigInteger mobiusFunction(BigInteger n)
        {
            if (n == 1)
            {
                return 1;
            }

            BigInteger[] factors = pollardRho(n);


            if (!isWithoutSquares(factors))
            {
                return 0;
            }

            if (factors.Length % 2 == 1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// Möbius function
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static bool isWithoutSquares(BigInteger[] primeFactors)
        {
            List<BigInteger> list = new List<BigInteger>(primeFactors);
            var hashSet = new HashSet<BigInteger>(list);
            if (list.Count == hashSet.Count)
            {
                return true;
            }

            return false;
        }


        private struct Point
        {
            public BigInteger x;
            public BigInteger y;

            public static Point pointAddition(Point point, EllpticCurve curve)
            {
                Point result = new Point();

                BigInteger s = (3 * point.x * point.x + curve.a) *
                               Intermediaries.PoMod(point.y<<1, curve.modulo - 2, curve.modulo);
                

                result.x = (s*s - point.x - point.x) % curve.modulo;
                result.y = (s*(point.x  - result.x) - point.y) % curve.modulo;

                return result;
            }
        }

        private struct EllpticCurve
        {
            public BigInteger a, b, modulo;

        }

        private static BigInteger[] LECF(BigInteger modulus)
        {
            List<BigInteger> list = new List<BigInteger>();
            int numberOfIterations = 0;
            while (modulus !=1 )
            {
                list.Add(LenstraEllipticCurveFactorization(modulus));
                numberOfIterations++;
                if (list[list.Count-1] == -1)
                {
                    list.RemoveAt(list.Count-1);
                }
                else
                {
                    modulus /= list[list.Count - 1];
                }

                if (numberOfIterations > 500)
                {
                    list.Add(modulus);
                    modulus /= modulus;
                }
                

                
            }

            BigInteger[] result = new BigInteger[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                result[i] = (BigInteger)list[i];
            }
            return result;
        }


        private static BigInteger LenstraEllipticCurveFactorization(BigInteger modulus)
        {
            if (modulus % 2 == 0)
            {
                return 2;
            }else if (modulus % 3 == 0)
            {
                return 3;
            }else if (modulus % 5 == 0)
            {
                return 5;
            }else if (modulus % 7 == 0)
            {
                return 7;
            }else if (modulus % 11 == 0)
            {
                return 11;
            }



            Point P0 = new Point();
            P0.x = Intermediaries.getRandomBigInteger(1, modulus - 1);
            P0.y = Intermediaries.getRandomBigInteger(1, modulus - 1);

            EllpticCurve E = new EllpticCurve();
            E.a = Intermediaries.getRandomBigInteger(1, modulus - 1);

            //b = y^2-x0^3-a*x0
            E.b = Intermediaries.getRandomBigInteger(1, modulus - 1);
            //(P0.y * P0.y - P0.x * P0.x * P0.x - E.a * P0.x) % modulus;
            E.modulo = modulus;

            List<Point> points = new List<Point>();

            points.Add(P0);

            BigInteger d = 0;
            if (modulus > 10000000)
            {
                for (int i = 1; i < Intermediaries.Sqrt(Intermediaries.Sqrt(modulus)); i++)
                {
                    points.Add(Point.pointAddition(points[i - 1], E));

                    d = BigInteger.GreatestCommonDivisor(modulus, points[i - 1].y);

                    if (d > 1 && d < modulus)
                    {
                        return d;
                    }
                }
            }
            else
            {
                for (int i = 1; i < Intermediaries.Sqrt(modulus); i++)
                {
                    points.Add(Point.pointAddition(points[i - 1], E));

                    d = BigInteger.GreatestCommonDivisor(modulus, points[i - 1].y);

                    if (d > 1 && d < modulus)
                    {
                        return d;
                    }
                }
            }
            
            
            return -1;
        }

        private static BigInteger[] factorizationBrute(BigInteger n)
        {
            BigInteger[] result;
            List<BigInteger> factors = new List<BigInteger>();

            for (BigInteger i = 2; i < Intermediaries.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    n /= i;
                    factors.Add(i);
                    i--;
                }
            }

            result = new BigInteger[factors.Count];

            for (int i = 0; i < factors.Count; i++)
            {
                result[i] = factors[i];
            }

            return result;
        }

        private static BigInteger[] BSGS(BigInteger n)
        {
            List<BigInteger> factors = new List<BigInteger>();

            while (n != 1)
            {
                
                if (n % 2 ==0)
                {
                    n /= 2;
                    factors.Add(2);
                }else if (n % 3 == 0)
                {
                    n /= 3;
                    factors.Add(3);
                }
                else if (n % 5 == 0)
                {
                    n /= 5;
                    factors.Add(5);
                }
                else if (n % 7 == 0)
                {
                    n /= 7;
                    factors.Add(7);
                }
                else if (MillerRabin(n, 10))
                {
                    n /= n;
                    factors.Add(n);
                }
                else
                {
                    var temp = BSGSfactorization(n);
                    n /= temp;

                    factors.Add(temp);
                }
                
            }

            BigInteger[] result = new BigInteger[factors.Count];
            for (int i = 0; i < factors.Count; i++)
            {
                result[i] = factors[i];
            }

            return result;
        }

        private static BigInteger BSGSfactorization(BigInteger n)//baby step giant step
        {
            BigInteger x = Intermediaries.Sqrt(n);
            BigInteger y = 0;

            List<BigInteger> list = new List<BigInteger>();

            while (true)
            {
                BigInteger z = x * x - n * y * y;
                if (z == 1)
                {
                    return x;
                }
                if (z < 1)
                {
                    x++;
                    y = Intermediaries.Sqrt(n - x * x);
                }
                else
                {
                    Dictionary<BigInteger, BigInteger> babyStep = new Dictionary<BigInteger, BigInteger>();
                    BigInteger m = Intermediaries.Sqrt(z);
                    BigInteger giantStep = m;

                    for (BigInteger i = 0; i < m; i++)
                    {
                        BigInteger xi = Intermediaries.PowMod(x + i, n - 2, n) * z % n;
                        babyStep[xi] = i;
                    }

                    for (BigInteger i = 0; i < m; i++)
                    {
                        BigInteger xi = BigInteger.ModPow(giantStep, n - 2, n) % n;
                        if (babyStep.ContainsKey(xi))
                        {
                            return BigInteger.GreatestCommonDivisor(x + i + babyStep[xi], n);
                        }
                        giantStep = (giantStep * x) % n;
                    }
                    x++;
                    y = Intermediaries.Sqrt(n - x * x);
                }
            }
        }


        private static BigInteger[] quadraticSieve(BigInteger n)
        {
            BigInteger B = Intermediaries.Sqrt(n);//smoothness bound

            BigInteger[] BsmoothNumbers = generateB_smoothNumbers(B);
            BigInteger[] Bi = getBi(BsmoothNumbers, n);

            throw new NotImplementedException();
        }

        private static BigInteger[] generateB_smoothNumbers(BigInteger smoothnessBound)
        {
            BigInteger[] factors;
            List<BigInteger> numbers = new List<BigInteger>();

            for (int i = 2; i < smoothnessBound; i++)
            {
                factors = LECF(i);
                if (getBiggest(factors) < smoothnessBound)
                {
                    numbers.Add(i);
                }
                
            }

            BigInteger[] result = new BigInteger[numbers.Count];
            for (int i = 0; i < numbers.Count; i++)
            {
                result[i] = numbers[i];
            }

            return result;
        }

        private static BigInteger getBiggest(BigInteger[] input)
        {
            BigInteger res = input[0];
            if (input.Length == 1)
            {
                return res;
            }

            for (int i = 1; i < input.Length; i++)
            {
                if (res < input[i])
                {
                    res = input[i];
                }
            }
            return res;
        }

        private static BigInteger[] getBi(BigInteger[] input, BigInteger modulus)
        {
            BigInteger[] bis = new BigInteger[input.Length];


            for (int i = 0; i < input.Length; i++)
            {
                bis[i] = (input[i] * input[i]) % modulus;
            }
            return bis;
        }
        
        private static List<BigInteger[]> getExponentVectors(BigInteger[] bi)
        {
            List<BigInteger[]> vectors = new List<BigInteger[]>();
            for (int i = 0; i < bi.Length; i++)
            {
                

            }
            throw new NotImplementedException();
        }

        private static BigInteger[] getExponents(BigInteger[] factors)
        {
            Array.Sort(factors);
            List<BigInteger> list = new List<BigInteger>(factors);

            HashSet <BigInteger> hashSet = new HashSet<BigInteger>(factors);

            for (int i = 0; i < hashSet.Count; i++)
            {
                var number = factors[i];















            }
            throw new NotImplementedException();

        }


        public static void sqrttest()
        {
            for (int i = 0; i < 10000000; i++)
            {
                if (Intermediaries.SqrtCeiling(i) != (BigInteger)Math.Ceiling(Math.Sqrt(i)))
                {
                    Console.WriteLine(i);
                }
            }
        }
        

    }

    internal class Intermediaries
    {
        private static Random random = new Random();

        /// <summary>
        /// Returns x^y mod p
        /// </summary>
        /// <param name="x">base</param>
        /// <param name="y">exponent</param>
        /// <param name="p">modulus</param>
        /// <returns></returns>
        public static BigInteger PowMod(BigInteger x, BigInteger y, BigInteger p)
        {
            if (y < (BigInteger)0)
            {
                y = p + y;
            }

            return (PMod((BigInteger)x, (BigInteger)y, (BigInteger)p));
        }

        public static BigInteger PoMod(BigInteger x, BigInteger y, BigInteger p)
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

        public static BigInteger Sqrt(BigInteger n)
        {
            if (n == 0)
            {
                return 0;
            }

            BigInteger x = n;
            BigInteger y = (x + 1) / 2;
            while (y < x)
            {
                x = y;
                y = (x + n / x) / 2;
            }

            return x;
        }

        public static BigInteger SqrtCeiling(BigInteger n)
        {
            if (n == 0)
                return BigInteger.Zero;
            if (n == 1)
                return BigInteger.One;

            BigInteger result = n;
            BigInteger prevResult;

            do
            {
                prevResult = result;
                result = (prevResult + n / prevResult) / 2;
            } while (result < prevResult);

            if (prevResult * prevResult == n)
                return prevResult;
            else if (prevResult * prevResult < n)
                return prevResult + 1;
            else
                return prevResult;
        }


        public static BigInteger getRandomBigInteger(BigInteger min, BigInteger max)
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
