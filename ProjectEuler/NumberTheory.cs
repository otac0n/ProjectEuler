namespace ProjectEuler
{
    using System;
    using System.Linq;
    using System.Numerics;

    public static class NumberTheory
    {
        public static long Factorial(long num)
        {
            long product = 1;
            while (num > 0)
            {
                product *= num;
                num--;
            }

            return product;
        }

        public static long GCD(this long a, long b)
        {
            long temp;

            while (a > 0)
            {
                temp = b % a;
                b = a;
                a = temp;
            }

            return b;
        }

        public static BigInteger GCD(this BigInteger a, BigInteger b)
        {
            BigInteger temp;

            while (a > 0)
            {
                temp = b % a;
                b = a;
                a = temp;
            }

            return b;
        }

        public static BigInteger nCr(long n, long r, PrimesList state)
        {
            var factorsNumerator = PrimeMath.FactorFactorial(n, state);
            var factorsDenom1 = PrimeMath.FactorFactorial(r, state);
            var factorsDenom2 = PrimeMath.FactorFactorial(n - r, state);

            BigInteger result = 1;
            foreach (var factor in factorsNumerator.Keys)
            {
                var power = factorsNumerator[factor];

                if (factorsDenom1.ContainsKey(factor))
                {
                    power -= factorsDenom1[factor];
                }

                if (factorsDenom2.ContainsKey(factor))
                {
                    power -= factorsDenom2[factor];
                }

                result *= BigInteger.Pow(factor, (int)power);
            }

            return result;
        }

        public static BigInteger nPr(long n, long r, PrimesList state)
        {
            var factorsNumerator = PrimeMath.FactorFactorial(n, state);
            var factorsDenom = PrimeMath.FactorFactorial(n - r, state);

            BigInteger result = 1;
            foreach (var factor in factorsNumerator.Keys)
            {
                var power = factorsNumerator[factor];

                if (factorsDenom.ContainsKey(factor))
                {
                    power -= factorsDenom[factor];
                }

                result *= BigInteger.Pow(factor, (int)power);
            }

            return result;
        }

        public static bool IsRelativelyPrime(this long a, long b)
        {
            long temp;

            if (a < b)
            {
                temp = a;
                a = b;
                b = temp;
            }

            while (b > 1)
            {
                temp = b;
                b = a % b;
                a = temp;
            }

            return b == 1;
        }

        public static int DigitalSum(this long a)
        {
            int sum = 0;
            foreach (var c in a.ToString())
            {
                sum += (int)(c - '0');
            }

            return sum;
        }

        public static int DigitalSum(this int a)
        {
            return DigitalSum((long)a);
        }

        public static int DigitalSum(this BigInteger a)
        {
            int sum = 0;
            foreach (var c in a.ToString())
            {
                sum += (int)(c - '0');
            }

            return sum;
        }

        public static bool IsAnagram(long a, long b)
        {
            var aStr = a.ToString();
            var bStr = b.ToString();

            if (aStr.Length != bStr.Length)
            {
                return false;
            }

            var aChars = new int[10];
            var bChars = new int[10];

            foreach (var c in aStr)
            {
                aChars[c - '0']++;
            }

            foreach (var c in bStr)
            {
                bChars[c - '0']++;
            }

            for (int i = 0; i < 10; i++)
            {
                if (aChars[i] != bChars[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsPalindrome(long num, int @base)
        {
            return num == ReverseNumber(num, @base);
        }

        public static bool IsPalindrome(BigInteger num, int @base)
        {
            return num == ReverseNumber(num, @base);
        }

        public static string PermutationKey(long num)
        {
           return string.Join(",", from c in num.ToString()
                                   group c by c into d
                                   orderby d.Key
                                   select d.Key + "=" + d.Count());
        }

        public static string PermutationKey(string str)
        {
            return string.Join(",", from c in str
                                    group c by c into d
                                    orderby d.Key
                                    select d.Key + "=" + d.Count());
        }

        public static long ReverseNumber(long num, int @base)
        {
            long reversed = 0;
            long k = num;
            while (k > 0)
            {
                reversed = @base * reversed + k % @base;
                k = k / @base;
            }

            return reversed;
        }

        public static BigInteger ReverseNumber(BigInteger num, int @base)
        {
            BigInteger reversed = 0;
            BigInteger k = num;
            while (k > 0)
            {
                reversed = @base * reversed + k % @base;
                k = k / @base;
            }

            return reversed;
        }

        public static long Square(long index)
        {
            return index * index;
        }
        
        public static long Triangle(long index)
        {
            return index % 2 == 0 ? (index / 2) * (index + 1) : ((index + 1) / 2) * index;
        }

        public static long Pentagon(long index)
        {
            return index % 2 == 0 ? (index / 2) * (3 * index - 1) : ((3 * index - 1) / 2) * index;
        }

        public static long Hexagon(long index)
        {
            return index * (2 * index - 1);
        }

        public static bool IsSquare(long num)
        {
            var sqrt = (long)Math.Sqrt(num);
            return sqrt * sqrt == num;
        }

        public static bool IsSquare(long num, out long sqrt)
        {
            sqrt = (long)Math.Sqrt(num);
            return sqrt * sqrt == num;
        }

        public static bool IsSquare(BigInteger num, out BigInteger sqrt)
        {
            var str = num.ToString();
            var d = str.Length;
            var n = (d - (d % 2 == 0 ? 2 : 1)) / 2;
            var estimate = BigInteger.Pow(10, n) * (d % 2 == 0 ? 5 : 2);

            return IsSquare(num, out sqrt, estimate);
        }

        public static bool IsSquare(BigInteger num, out BigInteger sqrt, BigInteger estimate)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException("num");
            }

            if (num <= long.MaxValue)
            {
                long lsqrt = 0;
                var ret = IsSquare((long)num, out lsqrt);
                sqrt = lsqrt;
                return ret;
            }

            sqrt = num.Sqrt(estimate);

            return num == BigInteger.Pow(sqrt, 2);
        }

        public static bool IsCubic(long num)
        {
            var rt = (long)Math.Round(Math.Pow(num, 1.0d / 3), 0);

            bool foundBelow = false;
            bool foundAbove = false;
            while(!(foundBelow && foundAbove))
            {
                var pow = rt * rt * rt;

                if (pow < num)
                {
                    foundBelow = true;
                    rt++;
                }
                else if (pow > num)
                {
                    foundAbove = true;
                    rt--;
                }
                else
                {
                    break;
                }
            }

            return !(foundBelow && foundAbove);
        }

        public static BigInteger Sqrt(this BigInteger num)
        {
            var str = num.ToString();
            var d = str.Length;
            var n = (d - (d % 2 == 0 ? 2 : 1)) / 2;
            var estimate = BigInteger.Pow(10, n) * (d % 2 == 0 ? 5 : 2);

            var sqrt = estimate;

            BigInteger newSqrt = sqrt;
            do
            {
                sqrt = newSqrt;

                newSqrt = (sqrt + num / sqrt) / 2;
            }
            while (newSqrt != sqrt);

            return sqrt;
        }

        public static BigInteger Sqrt(this BigInteger num, BigInteger estimate)
        {
            var sqrt = estimate;

            BigInteger newSqrt = sqrt;
            do
            {
                sqrt = newSqrt;

                newSqrt = (sqrt + num / sqrt) / 2;
            }
            while (newSqrt != sqrt);

            return sqrt;
        }

        public static bool IsTriangular(long num)
        {
            var discriminant = 8 * num + 1;
            return IsSquare(discriminant);
        }

        public static bool IsPentagonal(long num)
        {
            var discriminant = 24 * num + 1;

            var sqrt = (long)Math.Sqrt(discriminant);
            if (sqrt * sqrt != discriminant)
            {
                return false;
            }

            return (sqrt + 1) % 6 == 0;
        }

        public static bool IsHexagonal(long num)
        {
            var discriminant = 8 * num + 1;

            var sqrt = (long)Math.Sqrt(discriminant);
            if (sqrt * sqrt != discriminant)
            {
                return false;
            }

            return (sqrt + 1) % 4 == 0;
        }
    }
}
