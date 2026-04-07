using System;
using System.Globalization;
using System.Linq;

namespace Calculator
{
    public partial class BigNumber
    {
        static void Abs(BigNumber d, BigNumber s)
        {
            Copy(s, d);
            if (d.signum < 0) d.signum = 1;
        }

        static void Neg(BigNumber s, BigNumber d)
        {
            Copy(s, d);
            if (d.signum != 0) d.signum = (sbyte)(-d.signum);
        }

        static private void IntPow(int places, BigNumber src, long mexp, BigNumber dst)
        {
            BigNumber A, B, C;
            int signflag, local_precision;
            long nexp, ii;

            if (mexp == 0)
            {
                Copy(One, dst);
                return;
            }
            else
            {
                if (mexp > 0)
                {
                    signflag = 0;
                    nexp = mexp;
                }
                else
                {
                    signflag = 1;
                    nexp = -mexp;
                }
            }

            if (src.signum == 0)
            {
                SetZero(dst);
                return;
            }

            A = new BigNumber();
            B = new BigNumber();
            C = new BigNumber();

            local_precision = places + 8;

            // eo hieu sao doan nay de one no lai doc duoc la 0
            Copy(One, B);
            Copy(src, C);

            ii = nexp & 1;
            nexp = nexp >> 1;

            if (ii != 0)                       /* exponent -was- odd */
            {
                Copy(C, A);     // <==> Mul(B, C, A);
                Round(A, B, local_precision);
            }

            while (ii == 0 || nexp != 0)
            {
                //if (ii != 0 && nexp == 0) break;

                Mul(C, C, A);
                Round(A, C, local_precision);

                ii = nexp & 1;
                nexp = nexp >> 1;

                if (ii != 0)                       /* exponent -was- odd */
                {
                    Mul(B, C, A);
                    Round(A, B, local_precision);
                }
            }

            if (signflag > 0)
            {
                Reciprocal(B, dst, places);
            }
            else
            {
                Round(B, dst, places);
            }
        }

        static void Power(BigNumber xx, BigNumber yy, BigNumber rr, int places)
        {
            int iflag;
            BigNumber tmp8 = new BigNumber();
            BigNumber tmp9 = new BigNumber();
            int M_size_flag = 4;

            if (yy.signum == 0)
            {
                Copy(One, rr);
                return;
            }

            if (xx.signum == 0)
            {
                SetZero(rr);
                return;
            }

            if (IsInteger(yy))
            {
                iflag = 0;

                if (M_size_flag == 2)            /* 16 bit compilers */
                {
                    if (yy.exponent <= 4)
                        iflag = 1;
                }
                else                             /* >= 32 bit compilers */
                {
                    if (yy.exponent <= 7)
                        iflag = 1;
                }

                if (iflag > 0)
                {
                    string sbuf = ToIntString(yy);
                    long exp = Convert.ToInt64(sbuf);
                    IntPow(places, xx, exp, rr);
                    return;
                }
            }

            tmp8 = new BigNumber();
            tmp9 = new BigNumber();

            LogE(xx, tmp9, (places + 8));
            Mul(tmp9, yy, tmp8);
            Exp(tmp8, rr, places);
        }
        /// <summary>
        /// DEG
        /// </summary>
        static void DEG(BigNumber dst, BigNumber src)
        {
            //12.3456789 = 12°34'56.789"
            BigNumber dd = 0, mm = 0, ss = 0, mTemp = 0;
            //BigNumber dd = "0", mm = 0, ss = 0.0, mTemp = "0";
            if (IsInteger(src))
            {
                Copy(src, dst);
                return;
            }

            Copy(src, dd);
            // tách phần nguyên và phần lẻ của src
            Floor(dd, src);
            Sub(src, dd, mTemp);

            // xử lý phần lẻ
            string mT = mTemp.ToFullString().Substring(2);
            if ((mT.Length & 1) == 1) mT += "0";

            // 2 ký tự đầu sẽ là số phút
            if (mT.Length <= 2) Copy(mT.PadRight(2, '0'), mm);
            else
            {
                Copy(mT.Substring(0, 2), mm);
                Copy(mT.Substring(2, mT.Length - 2), ss);
                if (ss >= 100) ss = ss.mantissa[0];
            }

            if (mTemp.mantissa.Length > 2 && mTemp.mantissa[2] != 0)
            //if (mTemp.mantissa[2] != 0)
            {
                byte[] man = new byte[mT.Length / 2];
                ss.dataLength = mTemp.dataLength - 2;
                //if (mTemp.StrValue.Length % 2 != 0) mT += "0";
                for (int i = 2; i < mT.Length / 2; i++)
                {
                    man[i - 2] = byte.Parse(mT.Substring(2 * i, 2));
                }

                // tìm hạng của ma trận ss.mantissa
                #region cách cũ
                //int rank = ss.mantissa.Length - 1;
                //for (; rank >= 0; rank--)
                //{
                //    if (ss.mantissa[rank] != 0) break;
                //} 
                #endregion
                int rank = ss.mantissa.GetArrayRank();

                byte[] resultAsrc = new byte[++rank + man.Length];
                Array.Copy(ss.mantissa, resultAsrc, rank);
                Array.Copy(man, 0, resultAsrc, rank, resultAsrc.Length - rank);
                ss.mantissa = resultAsrc;
            }

            // ss = mm * 60 + ss
            Mul(mm, 60, mTemp);
            Add(mTemp, ss, ss);
            // dst = dd + ss / 3600
            Div(ss, 3600, mTemp);
            Add(dd, mTemp, dst);
        }
        /// <summary>
        /// DMS
        /// </summary>
        static void DMS(BigNumber dst, BigNumber src)
        {
            BigNumber dd = "0", mm = "0", ss = "0", mTemp = "0", sTemp = "0", mfloor = "0";
            Copy(src, dd);
            if (!IsInteger(src))
            {
                Floor(dd, src);        // dd    = src.floor();
                Sub(src, dd, mTemp);   // mtemp = src - dd
                Mul(mTemp, 0.6, mm);   // mm    = mtemp * 0.6
                Mul(mm, 100, mTemp);   // mTemp = mm * 100
                if (!IsInteger(mTemp))
                {
                    Floor(mfloor, mTemp);         //mfloor  = mTemp.floor()
                    Sub(mTemp, mfloor, sTemp);    //sTemp   = mTemp - mfloor
                    Mul(sTemp, 60d, ss);          //ss      = sTemp * 60
                }
                else
                {
                    Add(dd, mm, dst);    // xx = dd + mm
                    return;
                }
                Div(mfloor, 100d, mfloor);
                Add(dd, mfloor, dst);

                Div(ss, 10000d, ss);
                Add(dst, ss, dst);
            }
            else
            {
                Copy(src, dst);
            }
        }

        static void Reciprocal(BigNumber src, BigNumber dst, int places)
        {
            Div(One, src, dst, places);
        }

        static private void Exp(BigNumber src, BigNumber dst, int places)
        {
            BigNumber A = 0, B = 0, C = 0;
            int dplaces, ii = 0;
            long nn = 0;

            if (src.signum == 0)
            {
                Copy(One, dst);
                return;
            }

            if (src.exponent <= -3)
            {
                M_raw_exp(src, C, (places + 6));
                Round(C, dst, places);
                return;
            }

            if (M_exp_compute_nn(ref nn, A, src) != 0)
            {
                throw new Exception("Input is too large");
            }

            dplaces = places + 8;

            CheckLogPlaces(dplaces);
            Mul(A, BN_lc_log2, B);
            Sub(src, B, A);

            while (A.signum == 0 || A.exponent != 0)
            {
                //if (A.signum != 0 && A.exponent == 0) break;

                nn += A.signum;
                BN_lc_log2.signum = (sbyte)(-A.signum);
                Add(A, BN_lc_log2, B);    // A.signum >= 0 => A - BN_lc_log2 else A + BN_lc_log2
                BN_lc_log2.signum = 1;
                Copy(B, A);
            }

            Mul(A, 1.0 / 512, C);

            M_raw_exp(C, B, dplaces);

            ii = 9;

            do
            {
                Mul(B, B, C);
                Round(C, B, dplaces);

                //if (--ii == 0) break;
            } while (--ii != 0);

            IntPow(dplaces, Two, nn, A);
            Mul(A, B, C);
            Round(C, dst, places);
        }
        /// <summary>
        /// kiểm tra số nhập vào của hàm exp có quá lớn hay không
        /// </summary>
        static long M_exp_compute_nn(ref long n, BigNumber b, BigNumber a)
        {
            BigNumber tmp0, tmp1;

            tmp0 = new BigNumber();
            tmp1 = new BigNumber();

            Mul(BN_exp_log2R, a, tmp1);

            if (tmp1.signum >= 0)
            {
                Add(tmp1, 0.5, tmp0);
                Floor(tmp1, tmp0);
            }
            else
            {
                Sub(tmp1, 0.5, tmp0);
                Ceil(tmp1, tmp0);
            }

            string cp = ToIntString(tmp1);
            n = Convert.ToInt64(cp);

            SetFromLong(b, (long)(n));
            Normalize(b);

            return Compare(b, tmp1);
        }

        static private void M_raw_exp(BigNumber xx, BigNumber rr, int places)
        {
            BigNumber tmp0, digit, term;
            int tolerance;
            long m1, local_precision, prev_exp;

            tmp0 = new BigNumber();
            term = new BigNumber();
            digit = new BigNumber();

            local_precision = places + 8;
            tolerance = -(places + 4);
            prev_exp = 0;

            Add(One, xx, rr);
            Copy(xx, term);

            m1 = 2L;

            while (true)
            {
                SetFromLong(digit, m1);
                Mul(term, xx, tmp0);
                Div(tmp0, digit, term, (int)local_precision);
                Add(rr, term, tmp0);
                Copy(tmp0, rr);

                if ((term.exponent < tolerance) || (term.signum == 0))
                    break;

                if (m1 != 2L)
                {
                    local_precision = local_precision + term.exponent - prev_exp;

                    if (local_precision < 20)
                        local_precision = 20;
                }

                prev_exp = term.exponent;
                m1++;
            }
        }

        static void Floor(BigNumber dst, BigNumber src)
        {
            Copy(src, dst);

            if (IsInteger(dst)) return;
            if (dst.exponent <= 0)       /* if |bb| < 1, result is -1 or 0 */
            {
                if (dst.signum < 0)
                {
                    Neg(One, dst);
                }
                else
                {
                    SetZero(dst);
                }

                return;
            }

            if (dst.signum < 0)
            {
                BigNumber mtmp = new BigNumber();
                Neg(dst, mtmp);

                mtmp.dataLength = mtmp.exponent;

                Normalize(mtmp);

                Add(mtmp, One, dst);
                dst.signum = -1;
            }
            else
            {
                dst.dataLength = dst.exponent;
                byte[] man = new byte[(dst.exponent + 3) / 2];
                Array.Copy(dst.mantissa, 0, man, 0, (dst.exponent + 1) / 2);
                dst.mantissa = man;
                Normalize(dst);
            }
        }

        static void Ceil(BigNumber dst, BigNumber src)
        {
            BigNumber mtmp;

            Copy(src, dst);

            if (IsInteger(dst)) return;     /* if integer, we're done */
            if (dst.exponent <= 0)          /* if |bb| < 1, result is 0 or 1 */
            {
                if (dst.signum < 0)
                    SetZero(dst);
                else
                    Copy(One, dst);

                return;
            }

            if (dst.signum < 0)
            {
                dst.dataLength = dst.exponent;
                Normalize(dst);
            }
            else
            {
                mtmp = new BigNumber();
                Copy(dst, mtmp);

                mtmp.dataLength = mtmp.exponent;
                Normalize(mtmp);

                Add(mtmp, One, dst);
            }
        }

        static void SQrtGuess(BigNumber a, BigNumber r)
        {
            /* sqrt algorithm actually finds 1/sqrt */
            double dd;
            string buf = ToExpString(a, 15);

            scci.NumberFormat.NumberDecimalSeparator = Common.DecimalSeparator;

            dd = Convert.ToDouble(buf, scci);
            if (dd != 0) SetFromDouble(r, (1.0 / Math.Sqrt(dd)));
        }

        static void Sqrt(BigNumber src, BigNumber dst, int places)
        {
            int ii, tolerance, dplaces;
            bool bflag;

            if (src.signum <= 0 && src.signum == -1)
            {
                throw new Exception("Invalid input");
            }

            BigNumber last_x = new BigNumber();
            BigNumber guess = new BigNumber();
            BigNumber tmpN = new BigNumber();
            BigNumber tmp7 = new BigNumber();
            BigNumber tmp8 = new BigNumber();
            BigNumber tmp9 = new BigNumber();

            Copy(src, tmpN);

            long nexp = src.exponent / 2;
            tmpN.exponent -= 2 * nexp;

            SQrtGuess(tmpN, guess);

            tolerance = places + 4;
            dplaces = places + 16;
            bflag = false;

            Neg(Ten, last_x);

            ii = 0;

            while (true)
            {
                Mul(tmpN, guess, tmp9);
                Mul(tmp9, guess, tmp8);
                Round(tmp8, tmp7, dplaces);
                Sub(3, tmp7, tmp9);
                Mul(tmp9, guess, tmp8);
                Mul(tmp8, 0.5, tmp9);

                if (bflag) break;

                Round(tmp9, guess, dplaces);

                if (ii != 0)
                {
                    Sub(guess, last_x, tmp7);

                    if (tmp7.signum == 0) break;

                    /*
                     *   if we are within a factor of 4 on the error term,
                     *   we will be accurate enough after the *next* iteration
                     *   is complete. (note that the _IsSign of the exponent on
                     *   the error term will be a negative number).
                     */

                    if ((-4 * tmp7.exponent) > tolerance) bflag = true;
                }

                Copy(guess, last_x);
                ii++;
            }

            Mul(tmp9, tmpN, tmp8);
            Round(tmp8, dst, places);
            dst.exponent += nexp;
        }
        /// <summary>
        /// copy src -> dst (dst = src)
        /// </summary>
        static private void Copy(BigNumber src, BigNumber dst)
        {
            long j = (src.dataLength + 1) >> 1;

            if (j > dst.mantissa.Length)
            {
                Expand(dst, j + 31);
            }

            dst.dataLength = src.dataLength;
            dst.exponent = src.exponent;
            dst.signum = src.signum;

            Array.Copy(src.mantissa, dst.mantissa, j);
        }
        /// <summary>
        /// compare 2 numbers
        /// </summary>
        public static long Compare(BigNumber ltmp, BigNumber rtmp)
        {
            long lsign, rsign;

            lsign = ltmp.signum;
            rsign = rtmp.signum;

            int res = (lsign == 1) ? 1 : -1;

            if (rsign == 0 || lsign == -rsign) return lsign;

            if (lsign == 0) return -rsign;

            //if (lsign == -rsign) return lsign;

            long lexp, rexp;
            lexp = ltmp.exponent;
            rexp = rtmp.exponent;

            if (lexp > rexp) return res;

            if (lexp < rexp) return -res;

            long llen, rlen, j;
            llen = ltmp.dataLength;
            rlen = rtmp.dataLength;

            if (llen < rlen)
                j = (llen + 1) >> 1;
            else
                j = (rlen + 1) >> 1;

            for (int i = 0; i < j; i++)
            {
                if (ltmp.mantissa[i] > rtmp.mantissa[i]) return res;
                if (ltmp.mantissa[i] < rtmp.mantissa[i]) return -res;
            }

            if (llen == rlen) return 0;
            if (llen > rlen)
                return res;
            return -res;
        }

        static void M_get_log_guess(BigNumber a, BigNumber r)
        {
            double dd;

            string buf = ToExpString(a, 15);
            dd = ExpStringToDouble(buf);
            SetFromDouble(r, (1.00001 * Math.Log(dd)));        /* induce error of 10 ^ -5 */
        }

        static void M_log_solve_cubic(BigNumber nn, BigNumber rr, int places)
        {
            BigNumber tmp0, tmp1, tmp2, tmp3, guess;
            int ii, maxp, tolerance, local_precision;

            guess = new BigNumber();
            tmp0 = new BigNumber();
            tmp1 = new BigNumber();
            tmp2 = new BigNumber();
            tmp3 = new BigNumber();

            M_get_log_guess(nn, guess);

            tolerance = -(places + 4);
            maxp = places + 16;
            local_precision = 18;

            ii = 0;

            while (true)
            {
                Exp(guess, tmp1, local_precision);

                Sub(tmp1, nn, tmp3);
                Add(tmp1, nn, tmp2);

                Div(tmp3, tmp2, tmp1, local_precision);
                Mul(Two, tmp1, tmp0);
                Sub(guess, tmp0, tmp3);

                if (ii != 0)
                {
                    if (((3 * tmp0.exponent) < tolerance) || (tmp0.signum == 0)) break;
                }

                Round(tmp3, guess, local_precision);

                local_precision *= 3;

                if (local_precision > maxp) local_precision = maxp;

                ii = 1;
            }

            Round(tmp3, rr, places);

        }

        static void M_log_basic_iteration(BigNumber nn, BigNumber rr, int places)
        {
            BigNumber tmp0, tmp1, tmp2, tmpX;

            if (places < 360)
            {
                M_log_solve_cubic(nn, rr, places);
            }
            else
            {
                tmp0 = new BigNumber();
                tmp1 = new BigNumber();
                tmp2 = new BigNumber();
                tmpX = new BigNumber();

                M_log_solve_cubic(nn, tmpX, 110);
                Neg(tmpX, tmp0);
                Exp(tmp0, tmp1, (places + 8));
                Mul(tmp1, nn, tmp2);
                Sub(tmp2, One, tmp1);

                M_log_near_1(tmp1, tmp0, (places - 104));

                Add(tmpX, tmp0, tmp1);
                Round(tmp1, rr, places);
            }
        }

        /****************************************************************************/

        /*
         *	define a notation for a function 'R' :
         *
         *
         *
         *                                    1
         *      R (a0, b0)  =  ------------------------------
         *
         *                          ----
         *                           \
         *                            \     n-1      2    2
         *                     1  -   /    2    *  (a  - b )
         *                           /               n    n
         *                          ----
         *                         n >= 0
         *
         *
         *      where a, b are the classic AGM iteration :
         *
         *
         *      a    =  0.5 * (a  + b )
         *       n+1            n    n
         *
         *
         *      b    =  sqrt(a  * b )
         *       n+1          n    n
         *
         *
         *
         *      define a variable 'c' for more efficient computation :
         *
         *                                      2     2     2
         *      c    =  0.5 * (a  - b )    ,   c  =  a  -  b
         *       n+1            n    n          n     n     n
         *
         */

        /****************************************************************************/

        static void LogAGMRFunc(BigNumber aa, BigNumber bb, BigNumber rr, int places)
        {
            BigNumber tmpA0 = new BigNumber();
            BigNumber tmpB0 = new BigNumber();
            BigNumber tmpC2 = new BigNumber();
            BigNumber tmp1 = new BigNumber();
            BigNumber tmp2 = new BigNumber();
            BigNumber tmp3 = new BigNumber();
            BigNumber tmp4 = new BigNumber();
            BigNumber sum = new BigNumber();
            BigNumber pow_2 = new BigNumber();

            int tolerance = places + 8;
            int dplaces = places + 16;

            Copy(aa, tmpA0);
            Copy(bb, tmpB0);
            Copy(0.5, pow_2);

            Mul(aa, aa, tmp1);		    /* 0.5 * [ a ^ 2 - b ^ 2 ] */
            Mul(bb, bb, tmp2);
            Sub(tmp1, tmp2, tmp3);
            Mul(0.5, tmp3, sum);

            while (true)
            {
                Sub(tmpA0, tmpB0, tmp1);      /* C n+1 = 0.5 * [ An - Bn ] */
                Mul(0.5, tmp1, tmp4);         /* C n+1 */
                Mul(tmp4, tmp4, tmpC2);       /* C n+1 ^ 2 */

                /* do the AGM */

                Add(tmpA0, tmpB0, tmp1);
                Mul(0.5, tmp1, tmp3);

                Mul(tmpA0, tmpB0, tmp2);
                Sqrt(tmp2, tmpB0, dplaces);

                Round(tmp3, tmpA0, dplaces);

                /* end AGM */

                Mul(Two, pow_2, tmp2);
                Copy(tmp2, pow_2);

                Mul(tmpC2, pow_2, tmp1);
                Add(sum, tmp1, tmp3);

                if ((tmp1.signum == 0) || ((-2 * tmp1.exponent) > tolerance)) break;

                Round(tmp3, sum, dplaces);
            }

            Sub(One, tmp3, tmp4);
            Reciprocal(tmp4, rr, places);
        }

        /****************************************************************************/

        /*
         * calculate log (1 + x) with the following series:
         *
         *        x
	     *  y = -----      ( |y| < 1 )
	     *      x + 2
         *
         *          [ 1 + y ]                     y^3     y^5     y^7
	     *  log [---------------]  =  2 * [ y  +  ---  +  ---  +  ---  ... ]
         *          [ 1 - y ]                      3       5       7
         * */

        static void M_log_near_1(BigNumber xx, BigNumber rr, int places)
        {
            BigNumber tmp0, tmp1, tmp2, tmpS, term;
            long local_precision, m1;

            tmp0 = new BigNumber();
            tmp1 = new BigNumber();
            tmp2 = new BigNumber();
            tmpS = new BigNumber();
            term = new BigNumber();

            long tolerance = xx.exponent - (places + 6);
            long dplaces = (places + 12) - xx.exponent;

            Add(xx, Two, tmp0);
            Div(xx, tmp0, tmpS, (dplaces + 6));
            Copy(tmpS, term);
            Mul(tmpS, tmpS, tmp0);
            Round(tmp0, tmp2, (dplaces + 6));

            m1 = 3L;

            Mul(term, tmp2, tmp0);
            while (tmp0.exponent >= tolerance && tmp0.signum != 0)
            {
                local_precision = dplaces + tmp0.exponent;

                if (local_precision < 20)
                    local_precision = 20;

                SetFromLong(tmp1, m1);
                Round(tmp0, term, local_precision);
                Div(term, tmp1, tmp0, local_precision);
                Add(tmpS, tmp0, tmp1);
                Copy(tmp1, tmpS);
                m1 += 2;
            }

            Mul(Two, tmpS, tmp0);
            Round(tmp0, rr, places);
        }

        static private int MM_lc_log_digits = 128;
        static void CheckLogPlaces(int places)
        {
            BigNumber tmp6, tmp7, tmp8, tmp9;
            int dplaces;

            dplaces = places + 4;

            if (dplaces > MM_lc_log_digits)
            {
                MM_lc_log_digits = dplaces + 4;

                tmp6 = new BigNumber();
                tmp7 = new BigNumber();
                tmp8 = new BigNumber();
                tmp9 = new BigNumber();

                dplaces += 6 + (int)Math.Log10(places);

                Copy(One, tmp7);
                tmp7.exponent = -places;

                LogAGMRFunc(One, tmp7, tmp8, dplaces);

                Mul(tmp7, "0.5", tmp6);

                LogAGMRFunc(One, tmp6, tmp9, dplaces);

                Sub(tmp9, tmp8, BN_lc_log2);

                tmp7.exponent -= 1;

                LogAGMRFunc(One, tmp7, tmp9, dplaces);

                Sub(tmp9, tmp8, BN_lc_log10);
                Reciprocal(BN_lc_log10R, BN_lc_log10, dplaces);
            }
        }

        static void Log10(BigNumber src, BigNumber dst, int places)
        {
            BigNumber tmp8 = new BigNumber();
            BigNumber tmp9 = new BigNumber();
            int dplaces = places + 4;

            CheckLogPlaces(dplaces + 45);
            LogE(src.StrValue, tmp9, dplaces);
            Mul(tmp9, BN_lc_log10R, tmp8);
            Round(tmp8, dst, places);
        }

        static void LogE(BigNumber src, BigNumber dst, int places)
        {
            BigNumber tmp0, tmp1, tmp2;
            int dplaces;

            if (src.signum <= 0)
            {
                throw new Exception("Invalid input");
            }

            tmp0 = new BigNumber();
            tmp1 = new BigNumber();
            tmp2 = new BigNumber();

            dplaces = places + 8;

            long mexp = src.exponent;

            if (mexp == 0 || mexp == 1)
            {
                Sub(src, One, tmp0);

                if (tmp0.signum == 0)    /* is input exactly 1 ?? */
                {                           /* if so, result is 0    */
                    SetZero(dst);
                    return;
                }

                if (tmp0.exponent <= -4)
                {
                    M_log_near_1(tmp0, dst, places);
                    return;
                }
            }

            /* make sure our log(10) is accurate enough for this input */
            /* (and log(2) which is called from M_log_basic_iteration) */

            CheckLogPlaces(dplaces + 25);

            if (Math.Abs(mexp) <= 3)
            {
                M_log_basic_iteration(src, dst, places);
            }
            else
            {
                /*
                 *  use log (x * y) = log(x) + log(y)
                 *
                 *  here we use y = exponent of our base 10 number.
                 *
                 *  let 'C' = log(10) = 2.3025850929940....
                 *
                 *  then log(x * y) = log(x) + ( C * base_10_exponent )
                 */

                Copy(src, tmp2);

                mexp = tmp2.exponent - 2;
                tmp2.exponent = 2;

                M_log_basic_iteration(tmp2, tmp0, dplaces);

                SetFromLong(tmp1, mexp);
                Mul(tmp1, BN_lc_log10, tmp2);
                Add(tmp2, tmp0, tmp1);

                Round(tmp1, dst, places);
            }
        }
        /// <summary>
        /// so sánh 1 số có phải bội số số kia hay không?
        /// </summary>
        static bool CheckIfCommonMultiple(BigNumber bn1, BigNumber bn2)
        {
            BigNumber div = new BigNumber();
            Div(bn1, bn2, div);
            Round(div, div, 10);
            // la so nguyen, khac khong, va la so le
            return IsInteger(div) && Compare(div, "0") != 0 && int.Parse(div.StrValue) % 2 != 0;
        }

        static void Round(BigNumber src, BigNumber dst, long places)
        {
            BigNumber t0_5 = 5;
            long ii = places + 1;

            if (src.dataLength <= ii)
            {
                Copy(src, dst);
                return;
            }

            t0_5.exponent = src.exponent - ii;

            Add(src, src.signum * t0_5, dst);

            dst.dataLength = ii;
            // cắt những phần tử thừa của mảng mantissa 
            //Array.Resize<byte>(ref dst.mantissa, (int)ii + 1);
            #region nếu mảng chứa những phần tử = 0 đằng sau thì cũng cắt nốt đi
            //long index = 0;
            //for (long i = dst.mantissa.Length - 1; i >= 0; i--)
            //{
            //    if (dst.mantissa[i] != 0) { index = i + 1; break; }
            //}
            //Array.Resize(ref dst.mantissa, (int)index);
            #endregion
            Normalize(dst);
        }
        /// <summary>
        /// checkif atmp is an integer
        /// </summary>
        static bool IsInteger(string atmpBN)
        {
            BigNumber atmp = atmpBN;
            return IsInteger(atmp);
        }

        static long NextPowerOfTwo(long n)
        {
            int ct, k;
            int size_flag = sizeof(int);
            int bit_limit = 8 * size_flag + 1;

            if (n <= 2)
                return (n);

            k = 2;
            ct = 0;

            while (k < n)
            {
                k = k << 1;

                if (++ct == bit_limit)
                {
                    throw (new Exception("'NextPowerOfTwo', ERROR: sizeof(int) too small"));
                }
            }

            return k;
        }

        static private long Digits(BigNumber atm)
        {
            return atm.dataLength < 32 ? 32 : atm.dataLength;
        }

        static private long MaxDigits(BigNumber a, BigNumber b)
        {
            if (Digits(a) < Digits(b))
            {
                return Digits(b);
            }
            else
            {
                return Digits(a);
            }
        }

        static private void Scale(BigNumber ctmp, long count)
        {
            long ii, numb;
            long ct = count;
            byte numdiv = 0, numdiv2 = 0, numrem = 0;

            ii = (ctmp.dataLength + ct + 1) >> 1;

            if (ii > ctmp.mantissa.Length)
            {
                Expand(ctmp, (ii + 32));
            }

            if ((ct & 1) != 0)          /* move odd number first */
            {
                ct--;

                ii = ((ctmp.dataLength + 1) >> 1) - 1;

                if ((ctmp.dataLength & 1) == 0)
                {
                    /*
                     *   original datalength is even:
                     *
                     *   uv  wx  yz   becomes  -->   0u  vw  xy  z0
                     */

                    numdiv = 0;
                    ii++;
                    do
                    {
                        ii--;
                        Unpack(ctmp.mantissa[ii], ref  numdiv2, ref numrem);

                        ctmp.mantissa[ii + 1] = (byte)(10 * numrem + numdiv);
                        numdiv = numdiv2;
                    } while (ii != 0);

                    ctmp.mantissa[0] = numdiv2;
                }
                else
                {
                    /*
                     *   original datalength is odd:
                     *
                     *   uv  wx  y0   becomes  -->   0u  vw  xy
                     */

                    Unpack(ctmp.mantissa[ii], ref  numdiv2, ref numrem);

                    if (ii == 0)
                    {
                        ctmp.mantissa[0] = numdiv2;
                    }
                    else
                    {
                        do
                        {
                            Unpack(ctmp.mantissa[ii - 1], ref  numdiv, ref numrem);

                            ctmp.mantissa[ii] = (byte)(10 * numrem + numdiv2);
                            numdiv2 = numdiv;
                        } while (--ii != 0);

                        ctmp.mantissa[0] = numdiv;
                    }
                }

                ctmp.exponent++;
                ctmp.dataLength++;
            }

            /* ct is even here */

            if (ct > 0)
            {
                numb = (ctmp.dataLength + 1) >> 1;
                ii = ct >> 1;

                byte[] newData = new byte[ctmp.mantissa.Length];
                Array.Copy(ctmp.mantissa, 0, newData, ii, numb);

                for (int i = 0; i < ii; i++)
                {
                    newData[i] = 0;
                }

                ctmp.mantissa = newData;

                ctmp.dataLength += ct;
                ctmp.exponent += ct;
            }

        }
        /// <summary>
        /// expand mantissa array with given length and copy old content
        /// throws: ArgumentOutOfRangeException
        /// </summary>
        static void Expand(BigNumber atm, long newLength)
        {
            if (newLength > atm.mantissa.Length)
            {
                var newdata = new byte[newLength];
                Array.Copy(atm.mantissa, newdata, atm.mantissa.Length);
                atm.mantissa = newdata;
            }
            else
            {
                throw new Exception("'Expand', new length is smaller than current length");
                //return;
            }
        }
        /// <summary>
        /// unpacks the msb and lsb nibbles of a packed byte
        /// </summary>
        static private void Unpack(byte packed, ref byte msb, ref byte lsb)
        {
            //msb = s_MsbLookup[packed % 100];
            msb = (byte)(packed / 10);
            //lsb = s_LsbLookup[packed % 10];
            lsb = (byte)(packed % 10);
        }

        static private void UnpackMult(int tbl_lookup, ref byte msb, ref byte lsb)
        {
            msb = (byte)(tbl_lookup / 100);
            lsb = (byte)(tbl_lookup % 100);

            //msb = s_MsbLookupMult[tbl_lookup];
            //lsb = s_LsbLookupMult[tbl_lookup];
        }

        static private void Pad(BigNumber atm, long length)
        {
            long ct = length;
            byte numdiv = 0, numref = 0;

            if (atm.dataLength >= ct)
            {
                return;
            }

            long numb = (ct + 1) >> 1;

            if (numb > atm.mantissa.Length)
            {
                Expand(atm, numb + 32);
            }

            long num1 = (atm.dataLength + 1) >> 1;

            if ((atm.dataLength % 2) != 0)
            {
                Unpack(atm.mantissa[num1 - 1], ref numdiv, ref numref);
                atm.mantissa[num1 - 1] = (byte)(10 * numdiv);
            }

            for (long i = num1; i < numb; i++)
            {
                atm.mantissa[i] = 0;
            }

            atm.dataLength = ct;
        }
        /// <summary>
        /// normalize number
        /// </summary>
        static private void Normalize(BigNumber atm)
        {
            if (atm.signum == 0) return;
            int i;
            long datalength = atm.dataLength;
            long exponent = atm.exponent;
            long index, ucp = 0;
            byte numdiv = 0, numrem = 0, numrem2 = 0;

            Pad(atm, datalength + 3);

            // extract first 2 nibbles
            Unpack(atm.mantissa[0], ref numdiv, ref numrem);
            // remove leading zeroes
            while (numdiv < 1)
            {
                // if first digit is greater 1 we are done
                //if (numdiv >= 1) break;

                // otherwise we have leading zeroes
                index = (datalength + 1) >> 1;

                if (numrem == 0)    // both nibbles are 0
                {
                    i = 0;
                    ucp = 0;

                    while (atm.mantissa[ucp] == 0 && ucp < atm.mantissa.Length - 1)
                    {
                        ucp++;
                        i++;
                    }

                    byte[] copy = new byte[atm.mantissa.Length];
                    Array.Copy(atm.mantissa, ucp, copy, 0, (index + 1 - i));
                    atm.mantissa = copy;

                    datalength -= 2 * i;
                    exponent -= 2 * i;
                }
                else
                {
                    for (i = 0; i < index; i++)
                    {
                        Unpack(atm.mantissa[i + 1], ref numdiv, ref numrem2);
                        atm.mantissa[i] = (byte)(10 * numrem + numdiv);
                        numrem = numrem2;
                    }

                    datalength--;
                    exponent--;
                }

                // extract first 2 nibbles
                Unpack(atm.mantissa[0], ref numdiv, ref numrem);
            }

            // remove trailing zeroes
            while (true)
            {
                index = ((datalength + 1) >> 1) - 1;

                if ((datalength & 1) == 0)  /* back-up full bytes at a time if the */
                {
                    /* current length is an even number    */
                    ucp = index;
                    while (atm.mantissa[ucp] == 0)
                    {
                        datalength -= 2;
                        index--;
                        ucp--;
                    }
                }

                Unpack(atm.mantissa[index], ref numdiv, ref numrem);

                if (numrem != 0)		/* last digit non-zero, all done */
                    break;

                if ((datalength & 1) != 0)   /* if odd, then first char must be non-zero */
                {
                    if (numdiv != 0)
                        break;
                }

                if (datalength == 1)
                {
                    atm.signum = 0;
                    exponent = 0;
                    break;
                }

                datalength--;
            }

            atm.dataLength = datalength;
            atm.exponent = exponent;
        }

        static CultureInfo scci = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);

        static private string ToFullString(BigNumber atm)
        {
            string res = "";
            byte numdiv = 0, numrem = 0;

            long max_i = (atm.dataLength + 1) >> 1;
            long exp = atm.exponent;

            for (int i = 0; i < max_i; i++)
            {
                Unpack(atm.mantissa[i], ref numdiv, ref numrem);
                res += (char)('0' + numdiv);
                res += (char)('0' + numrem);
            }

            res = res.Substring(0, (int)atm.dataLength);

            if (exp > 0)
            {
                res = res.PadRight((int)exp, '0');
                if (exp < res.Length)
                {
                    res = res.Insert((int)exp, Common.DecimalSeparator);
                }
            }
            else
            {
                res = res.PadLeft(1 - (int)exp + res.Length, '0');
                if (res.Length >= 2)
                {
                    res = res.Insert(1, Common.DecimalSeparator);
                }
            }
            if (atm.signum < 0) res = "-" + res;
            return res;
        }

        static private string ToExpString(BigNumber atm, int digits)
        {
            int index, first, dec_places;
            byte numdiv = 0, numrem = 0;
            long num_digits, max_i, i;

            var ctmp = new BigNumber();
            string res = "";

            dec_places = digits;

            if (dec_places < 0)
            {
                Copy(atm, ctmp);
            }
            else
            {
                Round(atm, ctmp, dec_places);
            }

            if (ctmp.signum == 0)
            {
                if (dec_places < 0)
                {
                    res = "0e+0";
                }
                else
                {
                    res = "0";

                    if (dec_places > 0)
                    {
                        res += Common.DecimalSeparator;
                    }

                    res = res.PadRight(dec_places + res.Length, '0') + "e+0";
                    //res += "e+0";
                }
                return res;
            }

            max_i = (ctmp.dataLength + 1) >> 1;

            if (dec_places < 0)
            {
                num_digits = ctmp.dataLength;
            }
            else
            {
                num_digits = dec_places + 1;
            }
            if (ctmp.signum == -1)
            {
                res += '-';
            }

            first = 1;

            i = 0;
            index = 0;

            do
            {
                if (index >= max_i)
                {
                    numdiv = 0;
                    numrem = 0;
                }
                else
                {
                    Unpack(ctmp.mantissa[index], ref numdiv, ref numrem);
                }

                index++;

                res += (char)('0' + numdiv);

                if (++i == num_digits)
                    break;

                if (first != 0)
                {
                    first = 0;
                    res += Common.DecimalSeparator;
                }

                res += (char)('0' + numrem);

                //if (++index == num_digits)
                //    break;
            } while (++i != num_digits);

            i = ctmp.exponent - 1;

            if (i >= 0)
                res += "e+" + i;
            else
                res += "e" + i;

            return res;
        }

        static double ExpStringToDouble(string src)
        {
            scci.NumberFormat.NumberDecimalSeparator = Common.DecimalSeparator;
            return Convert.ToDouble(src, scci);
        }

        static private string ToIntString(BigNumber atm)
        {
            int ii;
            long ct = atm.exponent;
            long dl = atm.dataLength;
            long numb;
            string result = string.Empty;

            byte numdiv = 0, numrem = 0;

            if (ct <= 0 || atm.signum == 0)
            {
                return "0";
            }

            if (ct > 112)
            {
                Expand(atm, ct + 31);
            }

            ii = 0;

            if (atm.signum == -1)
            {
                ii = 1;
                result += "-";
            }

            if (ct > dl)
            {
                numb = (dl + 1) >> 1;
            }
            else
            {
                numb = (ct + 1) >> 1;
            }

            int ucp = 0;

            do
            {
                Unpack(atm.mantissa[ucp++], ref numdiv, ref numrem);

                result += (char)('0' + numdiv);
                result += (char)('0' + numrem);
            } while (--numb != 0);

            if (ct > dl)
            {
                result = result.PadRight((int)(ct + 1 - dl + result.Length), '0');
            }

            result = result.Substring(0, (int)(ct + ii));

            return result;
        }
    }
}