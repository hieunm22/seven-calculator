/// -------- ToujoursEnBeta
/// Author & Copyright : Peter Luschny
/// License: LGPL version 3.0 or (at your option)
/// Creative Commons Attribution-ShareAlike 3.0
/// Comments mail to: peter(at)luschny.de
/// Created: 2010-03-01

namespace Sharith.Math.MathUtils
{
    using System;
    using System.Threading.Tasks;
    using Calculator;

    public static class XMath
    {
        /// <summary>
        /// Bit count
        /// </summary>
        /// <param name="w"></param>
        /// <returns>Number of bits.</returns>
        public static long BitCount(long w)
        {
            w -= (0xaaaaaaaa & w) >> 1;
            w = (w & 0x33333333) + ((w >> 2) & 0x33333333);
            w = w + (w >> 4) & 0x0f0f0f0f;
            w += w >> 8;
            w += w >> 16;

            return w & 0xff;
        }

        /// <summary>
        /// Calculates the bit length of an integer.
        /// </summary>
        /// <param name="w"></param>
        /// <returns>bit length</returns>
        private static int BitLength(long w)
        {
            return w < 1 << 15
            ? (w < 1 << 7 ? (w < 1 << 3 ? (w < 1 << 1 ? (w < 1 << 0 ? (w < 0 ? 32 : 0) : 1)
            : (w < 1 << 2 ? 2 : 3)) : (w < 1 << 5 ? (w < 1 << 4 ? 4 : 5) : (w < 1 << 6 ? 6 : 7)))
            : (w < 1 << 11 ? (w < 1 << 9 ? (w < 1 << 8 ? 8 : 9) : (w < 1 << 10 ? 10 : 11))
            : (w < 1 << 13 ? (w < 1 << 12 ? 12 : 13) : (w < 1 << 14 ? 14 : 15))))
            : (w < 1 << 23
            ? (w < 1 << 19 ? (w < 1 << 17 ? (w < 1 << 16 ? 16 : 17) : (w < 1 << 18 ? 18 : 19))
            : (w < 1 << 21 ? (w < 1 << 20 ? 20 : 21) : (w < 1 << 22 ? 22 : 23)))
            : (w < 1 << 27
            ? (w < 1 << 25 ? (w < 1 << 24 ? 24 : 25) : (w < 1 << 26 ? 26 : 27))
            : (w < 1 << 29 ? (w < 1 << 28 ? 28 : 29)
            : (w < 1 << 30 ? 30 : 31))));
        }

        /// <summary>
        /// Floor of the binary logarithm.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FloorLog2(long n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException("n > 0 required");
            }
            return BitLength(n) - 1;
        }

        /// <summary>
        /// Floor of the square root.
        /// </summary>
        /// <param name="n">value to calculate</param>
        /// <returns>floor of the square root of value n</returns>
        public static long FloorSqrt(long n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n >= 0 required");
            }
            return (long)Math.Floor(Math.Sqrt(n));
        }

        /// <summary>
        /// Ceiling of the binary logarithm.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CeilLog2(int n)
        {
            int ret = FloorLog2(n);
            if (n != (1 << ret)) ret++;
            return ret;
        }

        private static BigNumber[] top21Factorial = {
        1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 
        479001600, 6227020800, 87178291200, 1307674368000, 20922789888000, 
        355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000 };

        public static BigNumber Factorial(long n)
        {
            return top21Factorial[n];
        }

        const int PARALLEL_THRESHOLD = 1024;

        // <returns>a[start]*a[start+1]*...*a[start+length-1]</returns>
        public static BigNumber Product(long[] a, int start, long length)
        {
            if (length == 0) return 1;

            long len = (length + 1) / 2;
            long[] b = new long[len];

            long i, j, k;

            for (k = 0, i = start, j = start + length - 1; i < j; i++, k++, j--)
            {
                b[k] = a[i] * a[j];
            }

            if (i == j) b[k++] = a[j];

            if (k > PARALLEL_THRESHOLD)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    return RecProduct(b, (k - 1) / 2 + 1, k - 1);
                });

                var left = RecProduct(b, 0, (k - 1) / 2);
                var right = task.Result;
                return left * right;
            }

            return RecProduct(b, 0, k - 1);
        }

        public static BigNumber Product(long[] a, int start, long length, int increment)
        {
            if (length == 0) return 1;

            long len = (1 + (length + 1) / 2) / increment;
            long[] b = new long[len];

            int i, k = 0;
            bool toggel = false;

            for (i = start; i < start+length; i += increment)
            {
                if ((toggel = !toggel)) b[k] = a[i];
                else b[k++] *= a[i];
            }
       
            if (len > PARALLEL_THRESHOLD)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    return RecProduct(b, (len - 1) / 2 + 1, len - 1);
                });

                var left = RecProduct(b, 0, (len - 1) / 2);
                var right = task.Result;
                return left * right;
            }

            return RecProduct(b, 0, len - 1);
        }

        public static BigNumber RecProduct(long[] s, long n, long m)
        {
            if (n > m)
            {
                return 1;
            }
            if (n == m)
            {
                return s[n].ToString();
            }

            long k = (n + m) >> 1;
            return RecProduct(s, n, k) * RecProduct(s, k + 1, m);
        }
    } 
}
