using System;

namespace Calculator
{
    public partial class BigNumber
    {
        /// <summary>
        /// tìm sinx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber Sinx(BigNumber src)
        {
            //n = n - (6,28318530717958647692528676655900576) * (n / 2 / BN_PI).Floor();
            BigNumber src_Temp = src;
            if (src.Abs() > "6.28318530717958647692528676655900576")
            {
                src_Temp = src - src.signum * (2 * BN_PI) * (src / "6.28318530717958647692528676655900576").Floor();
            }
            BigNumber result = src_Temp, f_comma_x = 0;
            BigNumber lastFactorial = 6;
            int n = 1, sign = -1;
            do
            {
                //fx = ((n & 1) == 0 ? 1 : -1) / lastFactorial;  //fx=(-1)^n / (2n+1)!
                //fx = sign / lastFactorial;  //fx=(-1)^n / (2n+1)!
                f_comma_x = (sign / lastFactorial * src_Temp.Pow(2 * n + 1, 31)).Round(40);       //f'(x)
                result = (result + f_comma_x).Round(40);
                n++;
                lastFactorial = lastFactorial * (2 * n) * (2 * n + 1);
				sign = -sign;
            }
            while (f_comma_x.Abs() > 1E-40);
            return result;
        }
        /// <summary>
        /// Sin(x)
        /// </summary>
        public BigNumber Sin()
        {
            return Sinx(this);
        }
        /// <summary>
        /// tìm cosx
        /// </summary>
        /// <param name="x">x</param>
        private BigNumber Cosx(BigNumber x)
        {
            return Sinx(BN_PI / 2 - x);
        }
        /// <summary>
        /// Cos(x)
        /// </summary>
        public BigNumber Cos()
        {
            return Cosx(this);
        }
        /// <summary>
        /// tìm tanx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber Tanx(BigNumber src)
        {
            BigNumber src_Temp = src;
            if (src.Abs() > "6,28318530717958647692528676655900576")
            {
                src_Temp = src - src.signum * (2 * BN_PI) * (src / 2 / "6,28318530717958647692528676655900576").Floor();
            }
            // epsilon = | |src| - pi/2 |, neu epsilon qua nho thi tra ve exception
            BigNumber epsilon = (src_Temp.Abs() - BN_PI / 2).Abs();

            if (epsilon < "1E-35") throw new Exception("Invalid parameter for this function");
            return Sinx(src_Temp) / Cosx(src_Temp);
        }
        /// <summary>
        /// Tan(x)
        /// </summary>
        public BigNumber Tan()
        {
            return Tanx(this);
        }
        /// <summary>
        /// tìm arcsinx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber ArcSinx(BigNumber src)
        {
            //n = n - (6,28318530717958647692528676655900576) * (n / 2 / BN_PI).Floor();
            if (src.Abs() > One__)
            {
                throw new Exception("Invalid argument in arcsin/arccos function");
            }
            BigNumber result = src, fx = 0.5, f_comma_x = 0, xPow = src;    //f'(x)
            if (src.Abs() < 0.8)
            {
                int n = 1;
                do
                {
                    xPow = xPow * src * src;
                    f_comma_x = (fx * xPow / (2 * n + 1));
                    result += f_comma_x;
                    n++;
                    fx = fx * (2 * n - 1) / (2 * n);	//fx=(2n-1)!! / (2n)!!
                }
                while (f_comma_x.Abs() > 1e-35);
                return result.Round(31);
            }
            BigNumber source = (1 - src * src).Sqrt(numDefaultPlaces);
            result = ArcSinx(source);
            result = "1,57079632679489661923132169163975" - result;
            result.signum = src.signum;
            return result;
        }
        /// <summary>
        /// Arcsin(x)
        /// </summary>
        public BigNumber ArcSin()
        {
            return ArcSinx(this);
        }
        /// <summary>
        /// tìm arccosx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber ArcCosx(BigNumber src)
        {
            return "1,57079632679489661923132169163975" - ArcSinx(src);
        }
        /// <summary>
        /// Arccos(x)
        /// </summary>
        public BigNumber ArcCos()
        {
            return ArcCosx(this);
        }
        /// <summary>
        /// tìm arctanx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber ArcTanx(BigNumber src)
        {
            return ArcSinx(src / (src * src + 1).Sqrt());
        }
        /// <summary>
        /// Arctan(x)
        /// </summary>
        public BigNumber ArcTan()
        {
            return ArcTanx(this);
        }

        #region Binary Split Algorithm
        static BigNumber factorial(string str)
        {
            long n = long.Parse(str);
            BigNumber p = 1, r = 1;
            loop(n, ref p, ref r);
            //int kkk = BitCount(n);
            return r * Two__.Pow(BitCount(n));
        }

        static void loop(long n, ref BigNumber p, ref BigNumber r)
        {
            if (n <= 2) return;
            loop(n / 2, ref p, ref r);
            //BigNumber part = partProduct(n / 2 + 1 + ((n / 2) & 1), n - 1 + (n & 1));
            p = p * partProduct(n / 2 + 1 + ((n / 2) & 1), n - 1 + (n & 1));
            r = r * p;
        }

        static BigNumber partProduct(long n, long m)
        {
            if (m <= (n + 1)) return n;
            if (m == (n + 2)) return (BigNumber)n * m;
            long k = (n + m) / 2;
            if ((k & 1) != 1) k = k - 1;
            return partProduct(n, k) * partProduct(k + 2, m);
        }

        static long BitCount(long v)
        {
            long w = v;
            w -= (0xaaaaaaaa & w) >> 1;
            w = (w & 0x33333333) + ((w >> 2) & 0x33333333);
            w = w + (w >> 4) & 0x0f0f0f0f;
            w += w >> 8;
            w += w >> 16;
            return v - (w & 0xff);
        }
        #endregion

        /// <summary>
        /// tính giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác cao, nhưng mất nhiều thời gian
        /// </summary>
        public BigNumber Factorial()
        {
            return factorial(this.IntString);
        }
        /// <summary>
        /// tính nhanh giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác thấp
        /// </summary>
        static private string fastFactorial(string inp_num)
        {
            BigNumber x = inp_num;
            x = 2 * x + 1;
            if (x >= 10000)
            {
                x = (2 * BN_PI).LogE() + (x / 2).LogE() * x - x - (1 - 7 / (30 * x * x)) / (6 * x);
                x = x / 2 / Ten__.LogE();
                BigNumber ex = x.Floor();
                x = Ten__.Pow(x - ex);

                try { x.exponent = int.Parse(ex.IntString) + 1; }
                catch { throw new Exception("Result is too large"); }

                x = x.Round(23);
                return x.StrValue;
            }
            return factorial(inp_num).StrValue;
        }
        /// <summary>
        /// tính nhanh giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác thấp
        /// </summary>
        public string FastFactorial()
        {
            return fastFactorial(this.StrValue);
        }
    }
}