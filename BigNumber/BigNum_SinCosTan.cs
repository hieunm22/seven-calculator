using System;
namespace Calculator
{
    public partial class BigNumber
    {
        /// <summary>
        /// tìm sinx
        /// </summary>
        /// <param name="n">x</param>
        private BigNumber Sinx(BigNumber src)
        {
            //n = n - (2 * BigNumber.BN_PI) * (n / 2 / BigNumber.BN_PI).Floor();
            BigNumber src_Temp = src;
            if (src > 2 * BigNumber.BN_PI)
            {
                src_Temp = src - (2 * BigNumber.BN_PI) * (src / 6.3).Floor();
            }
            if (src < -2 * BigNumber.BN_PI)
            {
                src_Temp = src + (2 * BigNumber.BN_PI) * (src / 6.3).Floor();
            }
            BigNumber result = src_Temp, fx = 0, f_comma_x = 0;
            int n = 1;
            do
            {
                fx = new BigNumber("-1").Pow(n) / BigNumber.fastFactorial((2 * n + 1).ToString());    //fx=(-1)^n / (2n+1)!
                //fx = fx / BigNumber.fastFactorial((2 * n + 1).ToString());   //fx=(-1)^n / (2n+1)!
                f_comma_x = (fx * src_Temp.Pow(2 * n + 1, 31)).Round(40);   //f'(x)
                result = (result + f_comma_x).Round(40);
                n++;
            }
            while (f_comma_x.Abs() > 1E-45);
            return result;
        }
        /// <summary>
        /// Sin(x)
        /// </summary>
        /// <param name="x">x</param>
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
            return Sinx(BigNumber.BN_PI / 2 - x);
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
        /// <param name="x">x</param>
        private BigNumber Tanx(BigNumber x)
        {
            BigNumber src_Temp = x;
            if (x > 2 * BigNumber.BN_PI)
            {
                src_Temp = x - (2 * BigNumber.BN_PI) * (x / 6.3).Floor();
            }
            if (x < -2 * BigNumber.BN_PI)
            {
                src_Temp = x + (2 * BigNumber.BN_PI) * (x / 6.3).Floor();
            }

            //Parser par = new Parser();
            //string expression = string.Format("{0} % {1}", x.StringValue, (BigNumber.BN_PI * 2).StringValue);
            //par.EvaluateSci(expression);
            //x = par.strResult;

            // epsilon = | |x| - pi/2 |, neu epsilon qua nho thi tra ve exception
            BigNumber epsilon = (src_Temp.Abs() - BigNumber.BN_PI / 2).Abs();

            if (epsilon < "1E-45") throw new Exception("Invalid parameter for this function");
            else return Sinx(src_Temp) / Cosx(src_Temp);
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
        /// <param name="n">x</param>
        private BigNumber ArcSinx(BigNumber src)
        {
            //n = n - (2 * BigNumber.BN_PI) * (n / 2 / BigNumber.BN_PI).Floor();
            if (src.Abs() > 1)
            {
                throw new Exception("Invalid argument in arcsin function");
            }
            BigNumber result = src, fx = 0, f_comma_x = 0;
            int n = 1;
            do
            {
                fx = (superFactorial(2 * n - 1) / superFactorial(2 * n)); //fx=(2n-1)!! / (2n)!!
                f_comma_x = (fx * src.Pow(2 * n + 1) / (2 * n + 1));  //f'(x)
                result = (result + f_comma_x);
                n++;
            }
            while (f_comma_x.Abs() > 1E-45);
            return result.Round(31);
        }
        /// <summary>
        /// Arcsin(x)
        /// </summary>
        /// <param name="x">x</param>
        public BigNumber ArcSin()
        {
            return ArcSinx(this);
        }
        /// <summary>
        /// tìm arccosx
        /// </summary>
        /// <param name="n">x</param>
        private BigNumber ArcCosx(BigNumber src)
        {
            return BigNumber.BN_PI / 2 - ArcSinx(src); ;
        }
        /// <summary>
        /// Arccos(x)
        /// </summary>
        /// <param name="x">x</param>
        public BigNumber ArcCos()
        {
            return ArcCosx(this);
        }
        /// <summary>
        /// tìm arctanx
        /// </summary>
        /// <param name="n">x</param>
        private BigNumber ArcTanx(BigNumber src)
        {
            return ArcSinx(src / (src * src + 1).Sqrt());
        }
        /// <summary>
        /// Arctan(x)
        /// </summary>
        /// <param name="x">x</param>
        public BigNumber ArcTan()
        {
            return ArcTanx(this);
        }

        static private BigNumber factorial(BigNumber src)
        {
            BigNumber resultLong = BigNumber.One, max = src.Floor();
            if (IsInteger(src))
            {
                for (BigNumber i = 1; i <= max; i = i + 1)
                {
                    resultLong = resultLong * i;
                }
            }
            return resultLong;
        }
        /// <summary>
        /// tính giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác cao, nhưng mất nhiều thời gian
        /// </summary>
        /// <param name="x"></param>
        static public BigNumber Factorial(BigNumber xx)
        {
            return factorial(xx);
        }

        static private string fastFactorial(string inp_num)
        {
            double dou = double.Parse(inp_num);
            decimal resultlong = 1;
            long somu = 0, dauvao = long.Parse(inp_num);
            if (dou - (long)dou == 0)
            {
                for (long i = 1; i <= dauvao; i++)
                {
                    resultlong *= i;
                    if (dou > 27)
                    {
                        while (resultlong > (decimal)15)
                        {
                            resultlong /= (decimal)10;
                            somu++;
                        }
                    }
                }
            }
            else return "1";

            return string.Format("{0}E{1}", Math.Round(resultlong, 25), somu);
        }
        /// <summary>
        /// tính nhanh giai thừa của 1 số lớn, kết quả của phép tính với độ chính xác thấp
        /// </summary>
        /// <param name="x">số cần tính giai thừa</param>
        static public string FastFactorial(BigNumber xx)
        {
            return fastFactorial(xx.StringValue);
        }

        public BigNumber FactorialValue
        {
            get
            {
                if (calc.F3)
                {
                    return FastFactorial(this);
                }
                else
                {
                    return Factorial(this);
                }
            }
        }
        /// <summary>
        /// superFactorial(int n) = n!! = 1*3*5*7*... or 2*4*6*8*...
        /// </summary>
        private BigNumber superFactorial(int n)
        {
            BigNumber result = 1;
            if (n % 2 == 0)
            {
                result = result * factorial(n / 2) * BigNumber.Two.Pow(n / 2);
            }
            else
            {
                result = factorial(n) / superFactorial(n - 1);
            }
            return result;
        }
    }
}