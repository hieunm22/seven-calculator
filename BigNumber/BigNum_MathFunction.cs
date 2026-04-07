using System;
using System.Globalization;

namespace Calculator
{
    public partial class BigNumber
    {
        static void Abs(BigNumber d, BigNumber s)
        {
            BigNumber.Copy(s, d);
            if (d.signum != 0) d.signum = 1;
        }

        static void Neg(BigNumber s, BigNumber d)
        {
            BigNumber.Copy(s, d);
            if (d.signum != 0) d.signum = (sbyte)-(d.signum);
        }

        static private void IntPow(int places, BigNumber src, int mexp, BigNumber dst)
        {
            BigNumber A, B, C;
            int nexp, ii, signflag, local_precision;

            if (mexp == 0)
            {
                BigNumber.Copy(BigNumber.One, dst);
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
                BigNumber.SetZero(dst);
                return;
            }

            A = new BigNumber();
            B = new BigNumber();
            C = new BigNumber();

            local_precision = places + 8;

            BigNumber.Copy(BigNumber.One, B);
            BigNumber.Copy(src, C);

            while (true)
            {
                ii = nexp & 1;
                nexp = nexp >> 1;

                if (ii != 0)                       /* exponent -was- odd */
                {
                    BigNumber.Mul(B, C, A);
                    BigNumber.Round(A, B, local_precision);

                    if (nexp == 0)
                        break;
                }

                BigNumber.Mul(C, C, A);
                BigNumber.Round(A, C, local_precision);
            }

            if (signflag > 0)
            {
                BigNumber.Reciprocal(B, dst, places);
            }
            else
            {
                BigNumber.Round(B, dst, places);
            }

        }

        static public void Power(BigNumber xx, BigNumber yy, BigNumber rr, int places)
        {
            int iflag;
            BigNumber tmp8 = new BigNumber();
            BigNumber tmp9 = new BigNumber();
            int M_size_flag = BigNumber.GetSizeofInt();

            if (yy.signum == 0)
            {
                BigNumber.Copy(BigNumber.One, rr);
                return;
            }

            if (xx.signum == 0)
            {
                BigNumber.SetZero(rr);
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
                    String sbuf = BigNumber.ToIntString(yy);
                    int Exp = Convert.ToInt32(sbuf);
                    BigNumber.IntPow(places, xx, Exp, rr);
                    return;
                }
            }

            tmp8 = new BigNumber();
            tmp9 = new BigNumber();

            BigNumber.LogE(xx, tmp9, (places + 8));
            BigNumber.Mul(tmp9, yy, tmp8);
            BigNumber.Exp(tmp8, rr, places);
        }
        
        static public void DEG(BigNumber xx, BigNumber rr)
        {
            //12.3456789 = 12°34'56.789"
            BigNumber dd = "0", mm = 0, ss = 0.0, mTemp = BigNumber.Zero, sTemp = "0";
            BigNumber.Copy(rr, dd);
            if (!BigNumber.IsInteger(rr))
            {
                BigNumber.Floor(dd, rr);
                BigNumber.Sub(rr, dd, mTemp);
                BigNumber.Copy(mTemp.mantissa[0], mm);

                BigNumber.Copy(mTemp.mantissa[1], ss);
                if (mTemp.mantissa[2] != 0)
                {
                    ss.dataLength = mTemp.dataLength - 2;
                    Array.Copy(mTemp.mantissa, 2, ss.mantissa, 1, ss.dataLength - 2);
                }

                BigNumber.Mul(mm, 60, mTemp);
                BigNumber.Add(mTemp, ss, ss);
                BigNumber.Div(ss, 3600, mTemp);
                BigNumber.Add(dd, mTemp, xx);
            }
        }
        /// <summary>
        /// DMS
        /// </summary>
        static public void DMS(BigNumber xx, BigNumber rr)
        {
            BigNumber dd = "0", mm = "0", ss = "0", mTemp = "0", sTemp = "0", mfloor = "0";
            BigNumber.Copy(rr, dd);
            if (!BigNumber.IsInteger(rr))
            {
                BigNumber.Floor(dd, rr);        // dd = rr.floor();
                BigNumber.Sub(rr, dd, mTemp);   // mtemp=rr-dd
                BigNumber.Mul(mTemp, 0.6, mm);  // mm=mtemp*60
                BigNumber.Mul(mm, 100, mTemp);  // mm=mtemp*60
                if (!BigNumber.IsInteger(mTemp))
                {
                    BigNumber.Floor(mfloor, mTemp);         //mTemp=mm.floor()
                    BigNumber.Sub(mTemp, mfloor, sTemp);    //stemp=mm-mfloor
                    BigNumber.Mul(sTemp, 60.0, ss);         //ss=stemp*60
                }
                else
                {
                    BigNumber.Add(dd, mm, xx);
                    return;
                }
                BigNumber.Div(mfloor, 100.0, mfloor);
                BigNumber.Add(dd, mfloor, xx);

                BigNumber.Div(ss, 10000.0, ss);
                BigNumber.Add(xx, ss, xx);
            }
            else
            {
                BigNumber.Copy(rr, xx);
            }
        }

        static public void Reciprocal(BigNumber src, BigNumber dst, int places)
        {
            BigNumber.Div(BigNumber.One, src, dst, places);
        }

        static private void Exp(BigNumber src, BigNumber dst, int places)
        {
            BigNumber A = 0, B = 0, C = 0;
            int dplaces, nn = 0, ii = 0;

            if (src.signum == 0)
            {
                BigNumber.Copy(BigNumber.One, dst);
                return;
            }

            if (src.exponent <= -3)
            {
                M_raw_exp(src, C, (places + 6));
                BigNumber.Round(C, dst, places);
                return;
            }

            if (M_exp_compute_nn(ref nn, A, src) != 0)
            {
                throw new Exception("Input is too large");
            }

            dplaces = places + 8;

            BigNumber.CheckLogPlaces(dplaces);
            BigNumber.Mul(A, BN_lc_log2, B);
            BigNumber.Sub(src, B, A);

            while (true)
            {
                if (A.signum != 0)
                {
                    if (A.exponent == 0)
                        break;
                }

                if (A.signum >= 0)
                {
                    nn++;
                    BigNumber.Sub(A, BN_lc_log2, B);
                    BigNumber.Copy(B, A);
                }
                else
                {
                    nn--;
                    BigNumber.Add(A, BN_lc_log2, B);
                    BigNumber.Copy(B, A);
                }
            }

            BigNumber.Mul(A, 1.0 / 512, C);

            M_raw_exp(C, B, dplaces);

            ii = 9;

            while (true)
            {
                BigNumber.Mul(B, B, C);
                BigNumber.Round(C, B, dplaces);

                if (--ii == 0) break;
            }

            BigNumber.IntPow(dplaces, BigNumber.Two, nn, A);
            BigNumber.Mul(A, B, C);
            BigNumber.Round(C, dst, places);
        }

        static int M_exp_compute_nn(ref int n, BigNumber b, BigNumber a)
        {
            BigNumber tmp0, tmp1;

            String cp = "";
            int kk;

            n = 0;

            tmp0 = new BigNumber();
            tmp1 = new BigNumber();

            BigNumber.Mul(BN_exp_log2R, a, tmp1);

            if (tmp1.signum >= 0)
            {
                BigNumber.Add(tmp1, 0.5, tmp0);
                BigNumber.Floor(tmp1, tmp0);
            }
            else
            {
                BigNumber.Sub(tmp1, 0.5, tmp0);
                BigNumber.Ceil(tmp1, tmp0);
            }

            kk = tmp1.exponent;


            cp = BigNumber.ToIntString(tmp1);
            n = Convert.ToInt32(cp);

            BigNumber.SetFromLong(b, (long)(n));
            BigNumber.Normalize(b);

            kk = BigNumber.Compare(b, tmp1);

            return (kk);
        }

        static private void M_raw_exp(BigNumber xx, BigNumber rr, int places)
        {
            BigNumber tmp0, digit, term;
            int tolerance, local_precision, prev_exp;
            long m1;

            tmp0 = new BigNumber();
            term = new BigNumber();
            digit = new BigNumber();

            local_precision = places + 8;
            tolerance = -(places + 4);
            prev_exp = 0;

            BigNumber.Add(BigNumber.One, xx, rr);
            BigNumber.Copy(xx, term);

            m1 = 2L;

            while (true)
            {
                BigNumber.SetFromLong(digit, m1);
                BigNumber.Mul(term, xx, tmp0);
                BigNumber.Div(tmp0, digit, term, local_precision);
                BigNumber.Add(rr, term, tmp0);
                BigNumber.Copy(tmp0, rr);

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
            BigNumber.Copy(src, dst);

            if (IsInteger(dst)) return;
            if (dst.exponent <= 0)       /* if |bb| < 1, result is -1 or 0 */
            {
                if (dst.signum < 0)
                {
                    BigNumber.Neg(BigNumber.One, dst);
                }
                else
                {
                    BigNumber.SetZero(dst);
                }

                return;
            }

            if (dst.signum < 0)
            {
                BigNumber mtmp = new BigNumber();
                BigNumber.Neg(dst, mtmp);

                mtmp.dataLength = mtmp.exponent;

                BigNumber.Normalize(mtmp);

                BigNumber.Add(mtmp, BigNumber.One, dst);
                dst.signum = -1;
            }
            else
            {
                dst.dataLength = dst.exponent;
                BigNumber.Normalize(dst);
            }
        }

        static void Ceil(BigNumber dst, BigNumber src)
        {
            BigNumber mtmp;

            BigNumber.Copy(src, dst);

            if (IsInteger(dst))          /* if integer, we're done */
            {
                return;
            }
            if (dst.exponent <= 0)       /* if |bb| < 1, result is 0 or 1 */
            {
                if (dst.signum < 0)
                    BigNumber.SetZero(dst);
                else
                    BigNumber.Copy(BigNumber.One, dst);

                return;
            }

            if (dst.signum < 0)
            {
                dst.dataLength = dst.exponent;
                BigNumber.Normalize(dst);
            }
            else
            {
                mtmp = new BigNumber();
                BigNumber.Copy(dst, mtmp);

                mtmp.dataLength = mtmp.exponent;
                BigNumber.Normalize(mtmp);

                BigNumber.Add(mtmp, BigNumber.One, dst);
            }
        }

        static void SQrtGuess(BigNumber a, BigNumber r)
        {
            /* sqrt algorithm actually finds 1/sqrt */
            double dd;
            String buf = BigNumber.ToExpString(a, 15);

            scci.NumberFormat.NumberDecimalSeparator = Misc.DecimalSym;

            dd = Convert.ToDouble(buf, scci);
            BigNumber.SetFromDouble(r, (1.0 / Math.Sqrt(dd)));
        }

        static void Sqrt(BigNumber src, BigNumber dst, int places)
        {
            int ii, nexp, tolerance, dplaces;
            bool bflag;

            if (src.signum <= 0)
            {
                if (src.signum == -1)
                {
                    throw new Exception("Cannot square root a negative number");
                }
            }

            BigNumber last_x = new BigNumber();
            BigNumber guess = new BigNumber();
            BigNumber tmpN = new BigNumber();
            BigNumber tmp7 = new BigNumber();
            BigNumber tmp8 = new BigNumber();
            BigNumber tmp9 = new BigNumber();

            BigNumber.Copy(src, tmpN);

            nexp = src.exponent / 2;
            tmpN.exponent -= 2 * nexp;

            BigNumber.SQrtGuess(tmpN, guess);

            tolerance = places + 4;
            dplaces = places + 16;
            bflag = false;

            BigNumber.Neg(BigNumber.Ten, last_x);

            ii = 0;

            while (true)
            {
                BigNumber.Mul(tmpN, guess, tmp9);
                BigNumber.Mul(tmp9, guess, tmp8);
                BigNumber.Round(tmp8, tmp7, dplaces);
                BigNumber.Sub(BigNumber.Three, tmp7, tmp9);
                BigNumber.Mul(tmp9, guess, tmp8);
                BigNumber.Mul(tmp8, 0.5, tmp9);

                if (bflag)
                {
                    break;
                }

                BigNumber.Round(tmp9, guess, dplaces);

                if (ii != 0)
                {
                    BigNumber.Sub(guess, last_x, tmp7);

                    if (tmp7.signum == 0)
                        break;

                    /*
                     *   if we are within a factor of 4 on the error term,
                     *   we will be accurate enough after the *next* iteration
                     *   is complete. (note that the sign of the exponent on
                     *   the error term will be a negative number).
                     */

                    if ((-4 * tmp7.exponent) > tolerance)
                        bflag = true;
                }

                BigNumber.Copy(guess, last_x);
                ii++;
            }

            BigNumber.Mul(tmp9, tmpN, tmp8);
            BigNumber.Round(tmp8, dst, places);
            dst.exponent += nexp;
        }
        /// <summary>
        /// copy src -> dst (dst = src)
        /// </summary>
        static private void Copy(BigNumber src, BigNumber dst)
        {
            int j = (src.dataLength + 1) >> 1;

            if (j > dst.mantissa.Length)
            {
                BigNumber.Expand(dst, j + 31);
            }

            dst.dataLength = src.dataLength;
            dst.exponent = src.exponent;
            dst.signum = src.signum;

            Array.Copy(src.mantissa, dst.mantissa, j);
        }
        /// <summary>
        /// compare 2 numbers
        /// </summary>
        static public int Compare(BigNumber ltmp, BigNumber rtmp)
        {
            int llen, rlen, lsign, rsign, i, j, lexp, rexp;

            llen = ltmp.dataLength;
            rlen = rtmp.dataLength;

            lsign = ltmp.signum;
            rsign = rtmp.signum;

            lexp = ltmp.exponent;
            rexp = rtmp.exponent;

            if (rsign == 0) return (lsign);

            if (lsign == 0) return (-rsign);

            if (lsign == -rsign) return (lsign);

            if (lexp > rexp) goto E1;

            if (lexp < rexp) goto E2;

            if (llen < rlen)
                j = (llen + 1) >> 1;
            else
                j = (rlen + 1) >> 1;

            for (i = 0; i < j; i++)
            {
                if (ltmp.mantissa[i] > rtmp.mantissa[i]) goto E1;
                if (ltmp.mantissa[i] < rtmp.mantissa[i]) goto E2;
            }

            if (llen == rlen)
                return (0);
            else
            {
                if (llen > rlen)
                    goto E1;
                else
                    goto E2;
            }

        E1: if (lsign == 1)
                return (1);
            else
                return (-1);

        E2: if (lsign == 1)
                return (-1);
            else
                return (1);

        }

        static private int MM_lc_log_digits = 128;

        static void M_get_log_guess(BigNumber a, BigNumber r)
        {
            double dd;

            String buf = BigNumber.ToExpString(a, 15);
            dd = BigNumber.ExpStringToDouble(buf);
            BigNumber.SetFromDouble(r, (1.00001 * Math.Log(dd)));        /* induce error of 10 ^ -5 */
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

            BigNumber.M_get_log_guess(nn, guess);

            tolerance = -(places + 4);
            maxp = places + 16;
            local_precision = 18;

            ii = 0;

            while (true)
            {
                BigNumber.Exp(guess, tmp1, local_precision);

                BigNumber.Sub(tmp1, nn, tmp3);
                BigNumber.Add(tmp1, nn, tmp2);

                BigNumber.Div(tmp3, tmp2, tmp1, local_precision);
                BigNumber.Mul(BigNumber.Two, tmp1, tmp0);
                BigNumber.Sub(guess, tmp0, tmp3);

                if (ii != 0)
                {
                    if (((3 * tmp0.exponent) < tolerance) || (tmp0.signum == 0))
                        break;
                }

                BigNumber.Round(tmp3, guess, local_precision);

                local_precision *= 3;

                if (local_precision > maxp) local_precision = maxp;

                ii = 1;
            }

            BigNumber.Round(tmp3, rr, places);

        }

        static void M_log_basic_iteration(BigNumber nn, BigNumber rr, int places)
        {
            BigNumber tmp0, tmp1, tmp2, tmpX;

            if (places < 360)
            {
                BigNumber.M_log_solve_cubic(nn, rr, places);
            }
            else
            {
                tmp0 = new BigNumber();
                tmp1 = new BigNumber();
                tmp2 = new BigNumber();
                tmpX = new BigNumber();

                BigNumber.M_log_solve_cubic(nn, tmpX, 110);
                BigNumber.Neg(tmpX, tmp0);
                BigNumber.Exp(tmp0, tmp1, (places + 8));
                BigNumber.Mul(tmp1, nn, tmp2);
                BigNumber.Sub(tmp2, BigNumber.One, tmp1);

                BigNumber.M_log_near_1(tmp1, tmp0, (places - 104));

                BigNumber.Add(tmpX, tmp0, tmp1);
                BigNumber.Round(tmp1, rr, places);
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
         *                      1  -   |   2    *  (a  - b )
         *                            /              n    n
         *                           /
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
            BigNumber tmp1, tmp2, tmp3, tmp4, tmpC2, sum, pow_2, tmpA0, tmpB0;
            int tolerance, dplaces;

            tmpA0 = new BigNumber();
            tmpB0 = new BigNumber();
            tmpC2 = new BigNumber();
            tmp1 = new BigNumber();
            tmp2 = new BigNumber();
            tmp3 = new BigNumber();
            tmp4 = new BigNumber();
            sum = new BigNumber();
            pow_2 = new BigNumber();

            tolerance = places + 8;
            dplaces = places + 16;

            BigNumber.Copy(aa, tmpA0);
            BigNumber.Copy(bb, tmpB0);
            BigNumber.Copy("0.5", pow_2);

            BigNumber.Mul(aa, aa, tmp1);		    /* 0.5 * [ a ^ 2 - b ^ 2 ] */
            BigNumber.Mul(bb, bb, tmp2);
            BigNumber.Sub(tmp1, tmp2, tmp3);
            BigNumber.Mul("0.5", tmp3, sum);

            while (true)
            {
                BigNumber.Sub(tmpA0, tmpB0, tmp1);      /* C n+1 = 0.5 * [ An - Bn ] */
                BigNumber.Mul("0.5", tmp1, tmp4);      /* C n+1 */
                BigNumber.Mul(tmp4, tmp4, tmpC2);       /* C n+1 ^ 2 */

                /* do the AGM */

                BigNumber.Add(tmpA0, tmpB0, tmp1);
                BigNumber.Mul("0.5", tmp1, tmp3);

                BigNumber.Mul(tmpA0, tmpB0, tmp2);
                BigNumber.Sqrt(tmp2, tmpB0, dplaces);

                BigNumber.Round(tmp3, tmpA0, dplaces);

                /* end AGM */

                BigNumber.Mul(BigNumber.Two, pow_2, tmp2);
                BigNumber.Copy(tmp2, pow_2);

                BigNumber.Mul(tmpC2, pow_2, tmp1);
                BigNumber.Add(sum, tmp1, tmp3);

                if ((tmp1.signum == 0) || ((-2 * tmp1.exponent) > tolerance))
                    break;

                BigNumber.Round(tmp3, sum, dplaces);
            }

            BigNumber.Sub(BigNumber.One, tmp3, tmp4);
            BigNumber.Reciprocal(tmp4, rr, places);
        }

        /****************************************************************************/

        /*
	        calculate log (1 + x) with the following series:

                  x
	        y = -----      ( |y| < 1 )
	            x + 2


                    [ 1 + y ]                 y^3     y^5     y^7
	        log [-------]  =  2 * [ y  +  ---  +  ---  +  ---  ... ]
                    [ 1 - y ]                  3       5       7

        */

        static void M_log_near_1(BigNumber xx, BigNumber rr, int places)
        {
            BigNumber tmp0, tmp1, tmp2, tmpS, term;
            int tolerance, dplaces, local_precision;
            long m1;

            tmp0 = new BigNumber();
            tmp1 = new BigNumber();
            tmp2 = new BigNumber();
            tmpS = new BigNumber();
            term = new BigNumber();

            tolerance = xx.exponent - (places + 6);
            dplaces = (places + 12) - xx.exponent;

            BigNumber.Add(xx, BigNumber.Two, tmp0);
            BigNumber.Div(xx, tmp0, tmpS, (dplaces + 6));
            BigNumber.Copy(tmpS, term);
            BigNumber.Mul(tmpS, tmpS, tmp0);
            BigNumber.Round(tmp0, tmp2, (dplaces + 6));

            m1 = 3L;

            while (true)
            {
                BigNumber.Mul(term, tmp2, tmp0);

                if ((tmp0.exponent < tolerance) || (tmp0.signum == 0))
                    break;

                local_precision = dplaces + tmp0.exponent;

                if (local_precision < 20)
                    local_precision = 20;

                BigNumber.SetFromLong(tmp1, m1);
                BigNumber.Round(tmp0, term, local_precision);
                BigNumber.Div(term, tmp1, tmp0, local_precision);
                BigNumber.Add(tmpS, tmp0, tmp1);
                BigNumber.Copy(tmp1, tmpS);
                m1 += 2;
            }

            BigNumber.Mul(BigNumber.Two, tmpS, tmp0);
            BigNumber.Round(tmp0, rr, places);

        }

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

                dplaces += 6 + (int)Math.Log10((double)places);

                BigNumber.Copy(BigNumber.One, tmp7);
                tmp7.exponent = -places;

                BigNumber.LogAGMRFunc(BigNumber.One, tmp7, tmp8, dplaces);

                BigNumber.Mul(tmp7, "0.5", tmp6);

                BigNumber.LogAGMRFunc(BigNumber.One, tmp6, tmp9, dplaces);

                BigNumber.Sub(tmp9, tmp8, BN_lc_log2);

                tmp7.exponent -= 1;

                BigNumber.LogAGMRFunc(BigNumber.One, tmp7, tmp9, dplaces);

                BigNumber.Sub(tmp9, tmp8, BN_lc_log10);
                BigNumber.Reciprocal(BN_lc_log10R, BN_lc_log10, dplaces);

            }
        }

        static void Log10(BigNumber src, BigNumber dst, int places)
        {
            BigNumber tmp8 = new BigNumber();
            BigNumber tmp9 = new BigNumber();
            int dplaces = places + 4;

            BigNumber.CheckLogPlaces(dplaces + 45);
            BigNumber.LogE(src, tmp9, dplaces);
            BigNumber.Mul(tmp9, BN_lc_log10R, tmp8);
            BigNumber.Round(tmp8, dst, places);
        }

        static void LogE(BigNumber src, BigNumber dst, int places)
        {
            BigNumber tmp0, tmp1, tmp2;
            int mexp, dplaces;

            if (src.signum <= 0)
            {
                throw new Exception("Negative argument in logarit function");
            }

            tmp0 = new BigNumber();
            tmp1 = new BigNumber();
            tmp2 = new BigNumber();

            dplaces = places + 8;

            mexp = src.exponent;

            if (mexp == 0 || mexp == 1)
            {
                BigNumber.Sub(src, BigNumber.One, tmp0);

                if (tmp0.signum == 0)    /* is input exactly 1 ?? */
                {                           /* if so, result is 0    */
                    BigNumber.SetZero(dst);
                    return;
                }

                if (tmp0.exponent <= -4)
                {
                    M_log_near_1(tmp0, dst, places);
                    return;
                }
            }

            /* make sure our log(10) is accurate enough for this calculation */
            /* (and log(2) which is called from M_log_basic_iteration) */

            BigNumber.CheckLogPlaces(dplaces + 25);

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

                BigNumber.Copy(src, tmp2);

                mexp = tmp2.exponent - 2;
                tmp2.exponent = 2;

                M_log_basic_iteration(tmp2, tmp0, dplaces);

                BigNumber.SetFromLong(tmp1, (long)mexp);
                BigNumber.Mul(tmp1, BN_lc_log10, tmp2);
                BigNumber.Add(tmp2, tmp0, tmp1);

                BigNumber.Round(tmp1, dst, places);
            }

        }

        static public void Round(BigNumber src, BigNumber dst, int places)
        {
            BigNumber t0_5 = BigNumber.Zero;
            BigNumber.Copy(BigNumber.Five, t0_5);
            int ii = places + 1;

            if (src.dataLength <= ii)
            {
                Copy(src, dst);
                return;
            }

            t0_5.exponent = src.exponent - ii;

            if (src.signum > 0)
            {
                BigNumber.Add(src, t0_5, dst);
            }
            else
            {
                BigNumber.Sub(src, t0_5, dst);
            }

            dst.dataLength = ii;
            BigNumber.Normalize(dst);
        }

        static readonly int sMinPrecision = 32;
        /// <summary>
        /// checkif atmp is an integer
        /// </summary>
        public static bool IsInteger(BigNumber atmp)
        {
            if (atmp.signum == 0) return true;
            return (atmp.exponent >= atmp.dataLength);
        }

        static int GetSizeofInt()
        {
            return (sizeof(int));
        }

        static int NextPowerOfTwo(int n)
        {
            int ct, k;
            int size_flag = GetSizeofInt();
            int bit_limit = 8 * size_flag + 1;

            if (n <= 2)
                return (n);

            k = 2;
            ct = 0;

            while (true)
            {
                if (k >= n)
                    break;

                k = k << 1;

                if (++ct == bit_limit)
                {
                    throw (new Exception("'NextPowerOfTwo', ERROR: sizeof(int) too small"));
                }
            }

            return (k);

        }

        static private int Digits(BigNumber atm)
        {
            return atm.dataLength < sMinPrecision ? sMinPrecision : atm.dataLength;
        }

        static private int MaxDigits(BigNumber a, BigNumber b)
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

        static private void Scale(BigNumber ctmp, int count)
        {
            int ii, numb, ct;
            ct = count;
            byte numdiv = 0, numdiv2 = 0, numrem = 0;

            ii = (ctmp.dataLength + ct + 1) >> 1;

            if (ii > ctmp.mantissa.Length)
            {
                BigNumber.Expand(ctmp, (ii + 32));
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

                    while (true)
                    {
                        Unpack(ctmp.mantissa[ii], ref  numdiv2, ref numrem);

                        ctmp.mantissa[ii + 1] = (byte)(10 * numrem + numdiv);
                        numdiv = numdiv2;

                        if (ii == 0)
                        {
                            break;
                        }

                        ii--;
                    }

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
                        while (true)
                        {
                            Unpack(ctmp.mantissa[ii - 1], ref  numdiv, ref numrem);

                            ctmp.mantissa[ii] = (byte)(10 * numrem + numdiv2);
                            numdiv2 = numdiv;

                            if (--ii == 0)
                            {
                                break;
                            }
                        }

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
        static public void Expand(BigNumber atm, int newLength)
        {
            if (newLength > atm.mantissa.Length)
            {
                byte[] newdata = new byte[newLength];
                Array.Copy(atm.mantissa, newdata, atm.mantissa.Length);
                atm.mantissa = newdata;
            }
            else
            {
                throw new Exception("'Expand', newLength smaller than current length");
            }
        }
        /// <summary>
        /// unpacks the msb and lsb nibbles of a packed byte
        /// </summary>
        static private void Unpack(byte packed, ref byte msb, ref byte lsb)
        {
            msb = s_MsbLookup[packed % 100];
            lsb = s_LsbLookup[packed % 10];
        }

        static private void UnpackMult(int tbl_lookup, ref byte msb, ref byte lsb)
        {
            msb = s_MsbLookupMult[tbl_lookup];
            lsb = s_LsbLookupMult[tbl_lookup];
        }

        static private void Pad(BigNumber atm, int length)
        {
            int ct = length;
            byte numdiv = 0, numref = 0;

            if (atm.dataLength >= ct)
            {
                return;
            }

            int numb = (ct + 1) >> 1;

            if (numb > atm.mantissa.Length)
            {
                BigNumber.Expand(atm, numb + 32);
            }

            int num1 = (atm.dataLength + 1) >> 1;

            if ((atm.dataLength % 2) != 0)
            {
                Unpack(atm.mantissa[num1 - 1], ref numdiv, ref numref);
                atm.mantissa[num1 - 1] = (byte)(10 * numdiv);
            }

            for (int i = num1; i < numb; i++)
            {
                atm.mantissa[i] = 0;
            }

            atm.dataLength = ct;
        }
        /// <summary>
        /// normalize number
        /// </summary>
        /// <param name="atm"></param>
        static private void Normalize(BigNumber atm)
        {
            if (atm.signum == 0)
            {
                return;
            }
            int ucp = 0;
            int i;
            int index;
            int datalength = atm.dataLength;
            int exponent = atm.exponent;
            byte numdiv = 0, numrem = 0, numrem2 = 0;

            BigNumber.Pad(atm, datalength + 3);

            // remove leading zeroes
            while (true)
            {
                // extract first 2 nibbles
                Unpack(atm.mantissa[0], ref numdiv, ref numrem);

                // if first digit is greater 1 we are done
                if (numdiv >= 1)
                {
                    break;
                }

                // otherwise we have leading zeroes
                index = (datalength + 1) >> 1;

                if (numrem == 0)    // both nibbles are 0
                {
                    i = 0;
                    ucp = 0;

                    while (true)
                    {
                        if (atm.mantissa[ucp] != 0)
                        {
                            break;
                        }
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
            }

            // remove trailing zeroes
            while (true)
            {
                index = ((datalength + 1) >> 1) - 1;

                if ((datalength & 1) == 0)   /* back-up full bytes at a time if the */
                {				            /* current length is an even number    */
                    ucp = index;
                    if (atm.mantissa[ucp] == 0)
                    {
                        while (true)
                        {
                            datalength -= 2;
                            index--;
                            ucp--;

                            if (atm.mantissa[ucp] != 0)
                            {
                                break;
                            }
                        }
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

        static private String ToFullString(BigNumber atm)
        {
            String res = "";
            byte numdiv = 0, numrem = 0;

            int max_i = (atm.dataLength + 1) >> 1;
            int exp = atm.exponent;

            for (int i = 0; i < max_i; i++)
            {
                BigNumber.Unpack(atm.mantissa[i], ref numdiv, ref numrem);
                res += (char)('0' + numdiv);
                res += (char)('0' + numrem);
            }

            res = res.Substring(0, atm.dataLength);

            if (exp > 0)
            {
                for (int i = res.Length; i < exp; i++)
                {
                    res += "0";
                }

                if (exp < res.Length)
                {
                    res = res.Insert(exp, Misc.DecimalSym);
                }
            }
            else if (exp <= 0)
            {
                while (exp++ <= 0)
                {
                    res = "0" + res;
                }

                if (exp <= res.Length)
                {
                    res = res.Insert(1, Misc.DecimalSym);
                }
            }
            if (atm < "0") res = "-" + res;
            return res;
        }

        static private String ToExpString(BigNumber atm, int digits)
        {
            int i, index, first, max_i, num_digits, dec_places;
            byte numdiv = 0, numrem = 0;

            BigNumber ctmp = new BigNumber();
            String res = "";

            dec_places = digits;

            if (dec_places < 0)
            {
                BigNumber.Copy(atm, ctmp);
            }
            else
            {
                BigNumber.Round(atm, ctmp, dec_places);
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
                        res += Misc.DecimalSym;
                    }

                    for (i = 0; i < dec_places; i++)
                    {
                        res += "0";
                    }

                    res += "e+0";

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

            while (true)
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
                    res += Misc.DecimalSym[0];
                }

                res += (char)('0' + numrem);

                if (++i == num_digits)
                    break;
            }

            i = ctmp.exponent - 1;

            if (i >= 0)
                res += "e+" + i;
            else if (i < 0)
                res += "e" + i;


            return res;
        }

        static double ExpStringToDouble(String src)
        {
            scci.NumberFormat.NumberDecimalSeparator = Misc.DecimalSym;
            return Convert.ToDouble(src, scci);
        }

        static private String ToIntString(BigNumber atm)
        {
            int ct, dl, numb, ii;
            ct = atm.exponent;
            dl = atm.dataLength;
            String result = String.Empty;

            byte numdiv = 0, numrem = 0;

            if (ct <= 0 || atm.signum == 0)
            {
                return "0";
            }

            if (ct > 112)
            {
                BigNumber.Expand(atm, ct + 31);
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

            while (true)
            {
                Unpack(atm.mantissa[ucp++], ref numdiv, ref numrem);

                result += (char)('0' + numdiv);
                result += (char)('0' + numrem);

                if (--numb == 0)
                {
                    break;
                }
            }

            if (ct > dl)
            {
                for (int i = 0; i < (ct + 1 - dl); i++)
                {
                    result += '0';
                }
            }

            result = result.Substring(0, ct + ii);

            return result;
        }

    }
}