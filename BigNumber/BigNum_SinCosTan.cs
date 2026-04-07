using System;

namespace Calculator
{
    public partial class BigNumber
    {
        /// <summary>
        /// tìm sinx
        /// </summary>
        /// <param name="src">x</param>
        static void Sinx(BigNumber src, ref BigNumber dst)
        {
            if (Compare(src, 0) == 0) { SetZero(dst); return; }
            BigNumber f_comma_x = 0;
            BigNumber lastFactorial = 6;
            BigNumber pow = 0;
            BigNumber div = 0;
            int n = 1, sign = -1;
            dst = src;
            do
            {
                Power(src, 2 * n + 1, pow, numDefaultPlaces);
                pow.signum = (sbyte)(sign * src.signum);
                Div(pow, lastFactorial, f_comma_x, numDefaultPlaces);   //f'(x)=(-1)^n / (2n+1)!
                //f_comma_x = (sign * src.Pow(2 * n + 1) / lastFactorial);
                dst = dst + f_comma_x;
                n++;
                lastFactorial = lastFactorial * (2 * n) * (2 * n + 1);
				sign = -sign;
            }
            while (Compare(f_comma_x.Abs(), 1E-50) > 0);
            Approximate(dst);
        }
        /// <summary>
        /// tìm cosx
        /// </summary>
        /// <param name="src">x</param>
        static void Cosx(BigNumber src, ref BigNumber res)
        {
            if (Compare(src, 0) == 0) return;
            if (Compare(src, BN_PI / 2) == 0) return;
            BigNumber div = new BigNumber();
            BigNumber param = new BigNumber();
            Div(BN_PI, Two, div);
            Sub(div, src, param);
            // neu param qua nho so voi src thi coi nhu bang div==src
            Div(param, src, div);
            if (Compare(div.Abs(), 1e-16) < 0) SetZero(param);
            Sinx(param, ref res);
        }
        /// <summary>
        /// tìm tanx
        /// </summary>
        /// <param name="src">x</param>
        static void Tanx(BigNumber src, BigNumber dst)
        {
            BigNumber src_Temp = src;
            // neu src_Temp la boi cua pi/2
            var epsilon = src_Temp.Abs() / BN_PI * 2;
            // epsilon = | |src| / (pi/2) |, neu epsilon lam tron den 10 chu so ma ra so nguyen thi tra ve exception
            if (IsInteger(epsilon.Round(10))) throw new Exception("Invalid parameter for this function");
            BigNumber sin = new BigNumber();
            BigNumber cos = new BigNumber();
            Sinx(src_Temp, ref sin);
            Cosx(src_Temp, ref cos);
            Div(sin, cos, dst);
        }
        /// <summary>
        /// tìm arcsinx
        /// </summary>
        /// <param name="src">x</param>
        private BigNumber ArcSinx(BigNumber src)
        {
            //n = n - (6,28318530717958647692528676655900576) * (n / 2 / BN_PI).Floor();
            if (src.Abs() > One)
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
                while (Compare(f_comma_x.Abs(), 1e-35) > 0);
                return result.Round(31);
            }
            var source = (1 - src * src).Sqrt(numDefaultPlaces);
            result = ArcSinx(source);
            result = "1.570796326794896619231321691639751442098584" - result;
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
            return "1.570796326794896619231321691639751442098584" - ArcSinx(src);
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
        /// <summary>
        /// tính nhanh giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác thấp
        /// </summary>
        public string FastFactorial()
        {
            return fastFactorial(this.StrValue);
        }
    }
}