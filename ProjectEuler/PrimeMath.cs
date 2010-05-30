using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public static class PrimeMath
    {
        public static long ModPow(long @base, long exponent, long modulus)
        {
            long result = 1;

            while (exponent > 0)
            {
                if ((exponent & 1) != 0)
                {
                    result = (result * @base) % modulus;
                }

                exponent >>= 1;
                @base = (@base * @base) % modulus;
            }

            return result;
        }

        public static bool IsPossiblyPrime(long number, int rounds)
        {
            if (rounds <= 1)
            {
                return ModPow(2, number - 1, number) == 1;
            }
            else
            {
                Random r = new Random();
                for (int i = 0; i < rounds; i++)
                {
                    var b = r.Next(2, (int)Math.Min(number, int.MaxValue));
                    if (ModPow(b, number - 1, number) != 1)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
