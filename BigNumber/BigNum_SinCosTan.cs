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
            if (src.Abs() > BN_DoublePI)
            {
                src_Temp = src - src.signum * BN_DoublePI * (src / BN_DoublePI).Round(10).Floor();
            }
            BigNumber result = src_Temp, f_comma_x = 0;
            BigNumber lastFactorial = 6;
            int n = 1, sign = -1;
            do
            {
                //fx = ((n & 1) == 0 ? 1 : -1) / lastFactorial;  //fx=(-1)^n / (2n+1)!
                //fx = sign / lastFactorial;  //fx=(-1)^n / (2n+1)!
                f_comma_x = (sign / lastFactorial * src_Temp.Pow(2 * n + 1, 31)).Round(36);       //f'(x)
                result = (result + f_comma_x).Round(36);
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
            if (src.Abs() > BN_DoublePI)
            {
                src_Temp = src - src.signum * BN_DoublePI * (src / BN_DoublePI).Floor();
            }
            // epsilon = | |src| / (pi/2) |, neu epsilon lam tron den 10 chu so ma ra so nguyen thi tra ve exception
            // neu src_Temp la boi cua pi/2
            var epsilon = src_Temp.Abs() / BN_PI * 2;   //3.2637657012293963088473017370737
            if (IsInteger(epsilon.Round(10))) throw new Exception("Invalid parameter for this function");

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
            x = x / 2 / Ten__.LogE();
            BigNumber ex = x.Floor();
            x = Ten__.Pow(x - ex);

            try { x.exponent = int.Parse(ex.IntString) + 1; }
            catch { throw new Exception("Exponent is too overflow"); }

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