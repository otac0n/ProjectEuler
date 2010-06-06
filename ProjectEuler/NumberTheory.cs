namespace ProjectEuler
{
    using System;
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
