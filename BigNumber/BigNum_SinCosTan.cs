using System;

namespace Calculator
{
    public partial class BigNumber
    {
        /// <summary>
        /// tìm sinx
        /// </summary>
        /// <param name="src">x</param>
        static void Sinx(BigNumber src, BigNumber dst)
        {
            if (CheckIfCommonMultiple(src, BN_PI)) { SetZero(dst); return; }
            BigNumber f_comma_x = new BigNumber();
            BigNumber lastFactorial = "6";
            BigNumber pow = new BigNumber();
            BigNumber div = new BigNumber();
            BigNumber abs = new BigNumber();
            BigNumber add = new BigNumber();
            BigNumber mul = new BigNumber();
            int n = 1, sign = -1;
            Copy(src, dst);
            do
            {
                //f_comma_x = (sign * src.Pow(2 * n + 1) / lastFactorial);
                Power(src, 2 * n + 1, pow, numDefaultPlaces);
                pow.signum = (sbyte)(sign * src.signum);
                Div(pow, lastFactorial, f_comma_x, numDefaultPlaces);   //f'(x)=(-1)^n / (2n+1)!
                //dst = dst + f_comma_x;
                Add(dst, f_comma_x, add);
                Round(add, add, numDefaultPlaces);
                Copy(add, dst);
                n++;
                //lastFactorial = lastFactorial * (2 * n) * (2 * n + 1);
                Mul(lastFactorial, (2 * n) * (2 * n + 1), mul);
                Copy(mul, lastFactorial);
				sign = -sign;
                Abs(abs, f_comma_x);
            }
            while (Compare(abs, 1E-50) > 0);
            //Approximate(dst); // rủi ro có thừa 
        }
        /// <summary>
        /// tìm cosx
        /// </summary>
        /// <param name="src">x</param>
        static void Cosx(BigNumber src, BigNumber dst)
        {
            if (Compare(src, 0) == 0) { dst = "1"; return; }
            if (CheckIfCommonMultiple(src, BN_HalfOfPi)) { SetZero(dst); return; }
            BigNumber param = new BigNumber();
            Sub(BN_HalfOfPi, src, param);   // param = pi/2 - src
            Sinx(param, dst); 
        }
        /// <summary>
        /// tìm tanx
        /// </summary>
        /// <param name="src">x</param>
        static void Tanx(BigNumber src, BigNumber dst)
        {
            // neu src la boi cua pi/2
            if (CheckIfCommonMultiple(src, BN_HalfOfPi))
                throw new Exception("Invalid parameter for this function");
            BigNumber sin = new BigNumber();
            Sinx(src, sin);
            // neu sin = 0 thi khoi can tinh cos, thoat luon
            if (Compare(sin, "0") == 0) { SetZero(dst); return; }
            BigNumber cos = new BigNumber();
            Cosx(src, cos);
            Div(sin, cos, dst);
        }
        /// <summary>
        /// tìm arcsinx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber ArcSinx(BigNumber src)
        {
            //n = n - (6,28318530717958647692528676655900576) * (n / 2 / BN_PI).Floor();
            if (src.Abs() > "1")
            {
                throw new Exception("Invalid argument in arcsin/arccos function");
            }
            BigNumber result = src, fx = 0.5, f_comma_x = 0, xPow = src;    //f'(x)
            // neu |src| cang gan voi 1 thi thoi gian tinh cang lau
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
                while (Compare(f_comma_x.Abs(), 1e-36) > 0);
                return result.Round(31);
            }
            var source = (1 - src * src).Sqrt(numDefaultPlaces);
            result = ArcSinx(source);
            result = BN_HalfOfPi - result;
            result.signum = src.signum;
            return result;
        }
        /// <summary>
        /// tìm arccosx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber ArcCosx(BigNumber src)
        {
            return BN_HalfOfPi - ArcSinx(src);
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
        /// tính nhanh giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác thấp
        /// </summary>
        private string fastFactorial(string inp_num)
        {
            BigNumber x = inp_num;
            x = 2 * x + 1;
            x = (2 * BN_PI).LogE() + (x / 2).LogE() * x - x - (1 - 7 / (30 * x * x)) / (6 * x);
            x = x / 2 / Ten.LogE();
            BigNumber ex = x.Floor();
            x = Ten.Pow(x - ex);

            try { x.exponent = long.Parse(ex.IntString) + 1; }
            catch { throw new Exception("Exponent is too large"); }

            x = x.Round(23);
            return x.StrValue;
        }
    }
}