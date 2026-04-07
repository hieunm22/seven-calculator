using System;

namespace Calculator
{
    public partial class BigNumber
    {
        private const int RDFT_LOOP_DIV = 64;
        static readonly double WR5000 = 0.707106781186547524400844362104849039284835937688;

        static private void M_bitrv2(long n, double[] a)
        {
            long j0, k0, j1, k1, m, i, j, k;
            double xr, xi, yr, yi;

            long l = n >> 2;
            m = 2;
            while (m < l)
            {
                l >>= 1;
                m <<= 1;
            }
            if (m == l)
            {
                j0 = 0;
                for (k0 = 0; k0 < m; k0 += 2)
                {
                    k = k0;
                    for (j = j0; j < j0 + k0; j += 2)
                    {
                        xr = a[j];
                        xi = a[j + 1];
                        yr = a[k];
                        yi = a[k + 1];
                        a[j] = yr;
                        a[j + 1] = yi;
                        a[k] = xr;
                        a[k + 1] = xi;
                        j1 = j + m;
                        k1 = k + 2 * m;
                        xr = a[j1];
                        xi = a[j1 + 1];
                        yr = a[k1];
                        yi = a[k1 + 1];
                        a[j1] = yr;
                        a[j1 + 1] = yi;
                        a[k1] = xr;
                        a[k1 + 1] = xi;
                        j1 += m;
                        k1 -= m;
                        xr = a[j1];
                        xi = a[j1 + 1];
                        yr = a[k1];
                        yi = a[k1 + 1];
                        a[j1] = yr;
                        a[j1 + 1] = yi;
                        a[k1] = xr;
                        a[k1 + 1] = xi;
                        j1 += m;
                        k1 += 2 * m;
                        xr = a[j1];
                        xi = a[j1 + 1];
                        yr = a[k1];
                        yi = a[k1 + 1];
                        a[j1] = yr;
                        a[j1 + 1] = yi;
                        a[k1] = xr;
                        a[k1 + 1] = xi;
                        for (i = n >> 1; i > (k ^= i); i >>= 1) { }
                    }
                    j1 = j0 + k0 + m;
                    k1 = j1 + m;
                    xr = a[j1];
                    xi = a[j1 + 1];
                    yr = a[k1];
                    yi = a[k1 + 1];
                    a[j1] = yr;
                    a[j1 + 1] = yi;
                    a[k1] = xr;
                    a[k1 + 1] = xi;
                    for (i = n >> 1; i > (j0 ^= i); i >>= 1) { }
                }
            }
            else
            {
                j0 = 0;
                for (k0 = 2; k0 < m; k0 += 2)
                {
                    for (i = n >> 1; i > (j0 ^= i); i >>= 1) { }
                    k = k0;
                    for (j = j0; j < j0 + k0; j += 2)
                    {
                        xr = a[j];
                        xi = a[j + 1];
                        yr = a[k];
                        yi = a[k + 1];
                        a[j] = yr;
                        a[j + 1] = yi;
                        a[k] = xr;
                        a[k + 1] = xi;
                        j1 = j + m;
                        k1 = k + m;
                        xr = a[j1];
                        xi = a[j1 + 1];
                        yr = a[k1];
                        yi = a[k1 + 1];
                        a[j1] = yr;
                        a[j1 + 1] = yi;
                        a[k1] = xr;
                        a[k1 + 1] = xi;
                        for (i = n >> 1; i > (k ^= i); i >>= 1) { }
                    }
                }
            }
        }

        static private void M_cftfsub(long n, double[] a)
        {
            long j, j1, j2, j3, l;
            double x0r, x0i, x1r, x1i, x2r, x2i, x3r, x3i;

            l = 2;
            if (n > 8)
            {
                M_cft1st(n, a);
                l = 8;
                while ((l << 2) < n)
                {
                    M_cftmdl(n, l, a);
                    l <<= 2;
                }
            }
            if ((l << 2) == n)
            {
                for (j = 0; j < l; j += 2)
                {
                    j1 = j + l;
                    j2 = j1 + l;
                    j3 = j2 + l;
                    x0r = a[j] + a[j1];
                    x0i = a[j + 1] + a[j1 + 1];
                    x1r = a[j] - a[j1];
                    x1i = a[j + 1] - a[j1 + 1];
                    x2r = a[j2] + a[j3];
                    x2i = a[j2 + 1] + a[j3 + 1];
                    x3r = a[j2] - a[j3];
                    x3i = a[j2 + 1] - a[j3 + 1];
                    a[j] = x0r + x2r;
                    a[j + 1] = x0i + x2i;
                    a[j2] = x0r - x2r;
                    a[j2 + 1] = x0i - x2i;
                    a[j1] = x1r - x3i;
                    a[j1 + 1] = x1i + x3r;
                    a[j3] = x1r + x3i;
                    a[j3 + 1] = x1i - x3r;
                }
            }
            else
            {
                for (j = 0; j < l; j += 2)
                {
                    j1 = j + l;
                    x0r = a[j] - a[j1];
                    x0i = a[j + 1] - a[j1 + 1];
                    a[j] += a[j1];
                    a[j + 1] += a[j1 + 1];
                    a[j1] = x0r;
                    a[j1 + 1] = x0i;
                }
            }
        }

        static private void M_cftbsub(long n, double[] a)
        {
            long j, j1, j2, j3, l;
            double x0r, x0i, x1r, x1i, x2r, x2i, x3r, x3i;

            l = 2;
            if (n > 8)
            {
                M_cft1st(n, a);
                l = 8;
                while ((l << 2) < n)
                {
                    M_cftmdl(n, l, a);
                    l <<= 2;
                }
            }
            if ((l << 2) == n)
            {
                for (j = 0; j < l; j += 2)
                {
                    j1 = j + l;
                    j2 = j1 + l;
                    j3 = j2 + l;
                    x0r = a[j] + a[j1];
                    x0i = -a[j + 1] - a[j1 + 1];
                    x1r = a[j] - a[j1];
                    x1i = -a[j + 1] + a[j1 + 1];
                    x2r = a[j2] + a[j3];
                    x2i = a[j2 + 1] + a[j3 + 1];
                    x3r = a[j2] - a[j3];
                    x3i = a[j2 + 1] - a[j3 + 1];
                    a[j] = x0r + x2r;
                    a[j + 1] = x0i - x2i;
                    a[j2] = x0r - x2r;
                    a[j2 + 1] = x0i + x2i;
                    a[j1] = x1r - x3i;
                    a[j1 + 1] = x1i - x3r;
                    a[j3] = x1r + x3i;
                    a[j3 + 1] = x1i + x3r;
                }
            }
            else
            {
                for (j = 0; j < l; j += 2)
                {
                    j1 = j + l;
                    x0r = a[j] - a[j1];
                    x0i = -a[j + 1] + a[j1 + 1];
                    a[j] += a[j1];
                    a[j + 1] = -a[j + 1] - a[j1 + 1];
                    a[j1] = x0r;
                    a[j1 + 1] = x0i;
                }
            }
        }

        static private void M_cft1st(long n, double[] a)
        {
            int j;
            long kj, kr;
            double ew, wn4r, wk1r, wk1i, wk2r, wk2i, wk3r, wk3i;
            double x0r, x0i, x1r, x1i, x2r, x2i, x3r, x3i;

            x0r = a[0] + a[2];
            x0i = a[1] + a[3];
            x1r = a[0] - a[2];
            x1i = a[1] - a[3];
            x2r = a[4] + a[6];
            x2i = a[5] + a[7];
            x3r = a[4] - a[6];
            x3i = a[5] - a[7];
            a[0] = x0r + x2r;
            a[1] = x0i + x2i;
            a[4] = x0r - x2r;
            a[5] = x0i - x2i;
            a[2] = x1r - x3i;
            a[3] = x1i + x3r;
            a[6] = x1r + x3i;
            a[7] = x1i - x3r;
            wn4r = WR5000;
            x0r = a[8] + a[10];
            x0i = a[9] + a[11];
            x1r = a[8] - a[10];
            x1i = a[9] - a[11];
            x2r = a[12] + a[14];
            x2i = a[13] + a[15];
            x3r = a[12] - a[14];
            x3i = a[13] - a[15];
            a[8] = x0r + x2r;
            a[9] = x0i + x2i;
            a[12] = x2i - x0i;
            a[13] = x0r - x2r;
            x0r = x1r - x3i;
            x0i = x1i + x3r;
            a[10] = wn4r * (x0r - x0i);
            a[11] = wn4r * (x0r + x0i);
            x0r = x3i + x1r;
            x0i = x3r - x1i;
            a[14] = wn4r * (x0i - x0r);
            a[15] = wn4r * (x0i + x0r);
            ew = PiHalf / n;
            kr = 0;
            for (j = 16; j < n; j += 16)
            {
                for (kj = n >> 2; kj > (kr ^= kj); kj >>= 1) { }
                wk1r = Math.Cos(ew * kr);
                wk1i = Math.Sin(ew * kr);
                wk2r = 1 - 2 * wk1i * wk1i;
                wk2i = 2 * wk1i * wk1r;
                wk3r = wk1r - 2 * wk2i * wk1i;
                wk3i = 2 * wk2i * wk1r - wk1i;
                x0r = a[j] + a[j + 2];
                x0i = a[j + 1] + a[j + 3];
                x1r = a[j] - a[j + 2];
                x1i = a[j + 1] - a[j + 3];
                x2r = a[j + 4] + a[j + 6];
                x2i = a[j + 5] + a[j + 7];
                x3r = a[j + 4] - a[j + 6];
                x3i = a[j + 5] - a[j + 7];
                a[j] = x0r + x2r;
                a[j + 1] = x0i + x2i;
                x0r -= x2r;
                x0i -= x2i;
                a[j + 4] = wk2r * x0r - wk2i * x0i;
                a[j + 5] = wk2r * x0i + wk2i * x0r;
                x0r = x1r - x3i;
                x0i = x1i + x3r;
                a[j + 2] = wk1r * x0r - wk1i * x0i;
                a[j + 3] = wk1r * x0i + wk1i * x0r;
                x0r = x1r + x3i;
                x0i = x1i - x3r;
                a[j + 6] = wk3r * x0r - wk3i * x0i;
                a[j + 7] = wk3r * x0i + wk3i * x0r;
                x0r = wn4r * (wk1r - wk1i);
                wk1i = wn4r * (wk1r + wk1i);
                wk1r = x0r;
                wk3r = wk1r - 2 * wk2r * wk1i;
                wk3i = 2 * wk2r * wk1r - wk1i;
                x0r = a[j + 8] + a[j + 10];
                x0i = a[j + 9] + a[j + 11];
                x1r = a[j + 8] - a[j + 10];
                x1i = a[j + 9] - a[j + 11];
                x2r = a[j + 12] + a[j + 14];
                x2i = a[j + 13] + a[j + 15];
                x3r = a[j + 12] - a[j + 14];
                x3i = a[j + 13] - a[j + 15];
                a[j + 8] = x0r + x2r;
                a[j + 9] = x0i + x2i;
                x0r -= x2r;
                x0i -= x2i;
                a[j + 12] = -wk2i * x0r - wk2r * x0i;
                a[j + 13] = -wk2i * x0i + wk2r * x0r;
                x0r = x1r - x3i;
                x0i = x1i + x3r;
                a[j + 10] = wk1r * x0r - wk1i * x0i;
                a[j + 11] = wk1r * x0i + wk1i * x0r;
                x0r = x1r + x3i;
                x0i = x1i - x3r;
                a[j + 14] = wk3r * x0r - wk3i * x0i;
                a[j + 15] = wk3r * x0i + wk3i * x0r;
            }
        }

        static private void M_cftmdl(long n, long l, double[] a)
        {
            long j, j1, j2, j3, k, kj, kr, m, m2;
            double ew, wn4r, wk1r, wk1i, wk2r, wk2i, wk3r, wk3i;
            double x0r, x0i, x1r, x1i, x2r, x2i, x3r, x3i;

            m = l << 2;
            for (j = 0; j < l; j += 2)
            {
                j1 = j + l;
                j2 = j1 + l;
                j3 = j2 + l;
                x0r = a[j] + a[j1];
                x0i = a[j + 1] + a[j1 + 1];
                x1r = a[j] - a[j1];
                x1i = a[j + 1] - a[j1 + 1];
                x2r = a[j2] + a[j3];
                x2i = a[j2 + 1] + a[j3 + 1];
                x3r = a[j2] - a[j3];
                x3i = a[j2 + 1] - a[j3 + 1];
                a[j] = x0r + x2r;
                a[j + 1] = x0i + x2i;
                a[j2] = x0r - x2r;
                a[j2 + 1] = x0i - x2i;
                a[j1] = x1r - x3i;
                a[j1 + 1] = x1i + x3r;
                a[j3] = x1r + x3i;
                a[j3 + 1] = x1i - x3r;
            }
            wn4r = WR5000;
            for (j = m; j < l + m; j += 2)
            {
                j1 = j + l;
                j2 = j1 + l;
                j3 = j2 + l;
                x0r = a[j] + a[j1];
                x0i = a[j + 1] + a[j1 + 1];
                x1r = a[j] - a[j1];
                x1i = a[j + 1] - a[j1 + 1];
                x2r = a[j2] + a[j3];
                x2i = a[j2 + 1] + a[j3 + 1];
                x3r = a[j2] - a[j3];
                x3i = a[j2 + 1] - a[j3 + 1];
                a[j] = x0r + x2r;
                a[j + 1] = x0i + x2i;
                a[j2] = x2i - x0i;
                a[j2 + 1] = x0r - x2r;
                x0r = x1r - x3i;
                x0i = x1i + x3r;
                a[j1] = wn4r * (x0r - x0i);
                a[j1 + 1] = wn4r * (x0r + x0i);
                x0r = x3i + x1r;
                x0i = x3r - x1i;
                a[j3] = wn4r * (x0i - x0r);
                a[j3 + 1] = wn4r * (x0i + x0r);
            }
            ew = PiHalf / n;
            kr = 0;
            m2 = 2 * m;
            for (k = m2; k < n; k += m2)
            {
                for (kj = n >> 2; kj > (kr ^= kj); kj >>= 1) { }
                wk1r = Math.Cos(ew * kr);
                wk1i = Math.Sin(ew * kr);
                wk2r = 1 - 2 * wk1i * wk1i;
                wk2i = 2 * wk1i * wk1r;
                wk3r = wk1r - 2 * wk2i * wk1i;
                wk3i = 2 * wk2i * wk1r - wk1i;
                for (j = k; j < l + k; j += 2)
                {
                    j1 = j + l;
                    j2 = j1 + l;
                    j3 = j2 + l;
                    x0r = a[j] + a[j1];
                    x0i = a[j + 1] + a[j1 + 1];
                    x1r = a[j] - a[j1];
                    x1i = a[j + 1] - a[j1 + 1];
                    x2r = a[j2] + a[j3];
                    x2i = a[j2 + 1] + a[j3 + 1];
                    x3r = a[j2] - a[j3];
                    x3i = a[j2 + 1] - a[j3 + 1];
                    a[j] = x0r + x2r;
                    a[j + 1] = x0i + x2i;
                    x0r -= x2r;
                    x0i -= x2i;
                    a[j2] = wk2r * x0r - wk2i * x0i;
                    a[j2 + 1] = wk2r * x0i + wk2i * x0r;
                    x0r = x1r - x3i;
                    x0i = x1i + x3r;
                    a[j1] = wk1r * x0r - wk1i * x0i;
                    a[j1 + 1] = wk1r * x0i + wk1i * x0r;
                    x0r = x1r + x3i;
                    x0i = x1i - x3r;
                    a[j3] = wk3r * x0r - wk3i * x0i;
                    a[j3 + 1] = wk3r * x0i + wk3i * x0r;
                }
                x0r = wn4r * (wk1r - wk1i);
                wk1i = wn4r * (wk1r + wk1i);
                wk1r = x0r;
                wk3r = wk1r - 2 * wk2r * wk1i;
                wk3i = 2 * wk2r * wk1r - wk1i;
                for (j = k + m; j < l + (k + m); j += 2)
                {
                    j1 = j + l;
                    j2 = j1 + l;
                    j3 = j2 + l;
                    x0r = a[j] + a[j1];
                    x0i = a[j + 1] + a[j1 + 1];
                    x1r = a[j] - a[j1];
                    x1i = a[j + 1] - a[j1 + 1];
                    x2r = a[j2] + a[j3];
                    x2i = a[j2 + 1] + a[j3 + 1];
                    x3r = a[j2] - a[j3];
                    x3i = a[j2 + 1] - a[j3 + 1];
                    a[j] = x0r + x2r;
                    a[j + 1] = x0i + x2i;
                    x0r -= x2r;
                    x0i -= x2i;
                    a[j2] = -wk2i * x0r - wk2r * x0i;
                    a[j2 + 1] = -wk2i * x0i + wk2r * x0r;
                    x0r = x1r - x3i;
                    x0i = x1i + x3r;
                    a[j1] = wk1r * x0r - wk1i * x0i;
                    a[j1 + 1] = wk1r * x0i + wk1i * x0r;
                    x0r = x1r + x3i;
                    x0i = x1i - x3r;
                    a[j3] = wk3r * x0r - wk3i * x0i;
                    a[j3 + 1] = wk3r * x0i + wk3i * x0r;
                }
            }
        }

        static private void M_rftfsub(long n, double[] a)
        {
            long i, i0, j, k;
            double ec, w1r, w1i, wkr, wki, wdr, wdi, ss, xr, xi, yr, yi;

            ec = 2 * PiHalf / n;
            wkr = 0;
            wki = 0;
            wdi = Math.Cos(ec);
            wdr = Math.Sin(ec);
            wdi *= wdr;
            wdr *= wdr;
            w1r = 1 - 2 * wdr;
            w1i = 2 * wdi;
            ss = 2 * w1i;
            i = n >> 1;
            while (true)
            {
                i0 = i - 4 * RDFT_LOOP_DIV;
                if (i0 < 4) i0 = 4;
                for (j = i - 4; j >= i0; j -= 4)
                {
                    k = n - j;
                    xr = a[j + 2] - a[k - 2];
                    xi = a[j + 3] + a[k - 1];
                    yr = wdr * xr - wdi * xi;
                    yi = wdr * xi + wdi * xr;
                    a[j + 2] -= yr;
                    a[j + 3] -= yi;
                    a[k - 2] += yr;
                    a[k - 1] -= yi;
                    wkr += ss * wdi;
                    wki += ss * (0.5 - wdr);
                    xr = a[j] - a[k];
                    xi = a[j + 1] + a[k + 1];
                    yr = wkr * xr - wki * xi;
                    yi = wkr * xi + wki * xr;
                    a[j] -= yr;
                    a[j + 1] -= yi;
                    a[k] += yr;
                    a[k + 1] -= yi;
                    wdr += ss * wki;
                    wdi += ss * (0.5 - wkr);
                }

                if (i0 == 4) break;

                wkr = 0.5 * Math.Sin(ec * i0);
                wki = 0.5 * Math.Cos(ec * i0);
                wdr = 0.5 - (wkr * w1r - wki * w1i);
                wdi = wkr * w1i + wki * w1r;
                wkr = 0.5 - wkr;
                i = i0;
            }
            xr = a[2] - a[n - 2];
            xi = a[3] + a[n - 1];
            yr = wdr * xr - wdi * xi;
            yi = wdr * xi + wdi * xr;
            a[2] -= yr;
            a[3] -= yi;
            a[n - 2] += yr;
            a[n - 1] -= yi;
        }

        static private void M_rftbsub(long n, double[] a)
        {
            long i, i0, j, k;
            double ec, w1r, w1i, wkr, wki, wdr, wdi, ss, xr, xi, yr, yi;

            ec = 2 * PiHalf / n;
            wkr = 0;
            wki = 0;
            wdi = Math.Cos(ec);
            wdr = Math.Sin(ec);
            wdi *= wdr;
            wdr *= wdr;
            w1r = 1 - 2 * wdr;
            w1i = 2 * wdi;
            ss = 2 * w1i;
            i = n >> 1;
            a[i + 1] = -a[i + 1];
            while (true)
            {
                i0 = i - 4 * RDFT_LOOP_DIV;
                
                if (i0 < 4) i0 = 4;
                
                for (j = i - 4; j >= i0; j -= 4)
                {
                    k = n - j;
                    xr = a[j + 2] - a[k - 2];
                    xi = a[j + 3] + a[k - 1];
                    yr = wdr * xr + wdi * xi;
                    yi = wdr * xi - wdi * xr;
                    a[j + 2] -= yr;
                    a[j + 3] = yi - a[j + 3];
                    a[k - 2] += yr;
                    a[k - 1] = yi - a[k - 1];
                    wkr += ss * wdi;
                    wki += ss * (0.5 - wdr);
                    xr = a[j] - a[k];
                    xi = a[j + 1] + a[k + 1];
                    yr = wkr * xr + wki * xi;
                    yi = wkr * xi - wki * xr;
                    a[j] -= yr;
                    a[j + 1] = yi - a[j + 1];
                    a[k] += yr;
                    a[k + 1] = yi - a[k + 1];
                    wdr += ss * wki;
                    wdi += ss * (0.5 - wkr);
                }

                if (i0 == 4) break;

                wkr = 0.5 * Math.Sin(ec * i0);
                wki = 0.5 * Math.Cos(ec * i0);
                wdr = 0.5 - (wkr * w1r - wki * w1i);
                wdi = wkr * w1i + wki * w1r;
                wkr = 0.5 - wkr;
                i = i0;
            }
            xr = a[2] - a[n - 2];
            xi = a[3] + a[n - 1];
            yr = wdr * xr + wdi * xi;
            yi = wdr * xi - wdi * xr;
            a[2] -= yr;
            a[3] = yi - a[3];
            a[n - 2] += yr;
            a[n - 1] = yi - a[n - 1];
            a[1] = -a[1];
        }

        static private void M_rdft(long n, int isgn, double[] a)
        {
            double xi;

            if (isgn >= 0)
            {
                if (n > 4)
                {
                    M_bitrv2(n, a);
                    M_cftfsub(n, a);
                    M_rftfsub(n, a);
                }
                else if (n == 4)
                {
                    M_cftfsub(n, a);
                }
                xi = a[0] - a[1];
                a[0] += a[1];
                a[1] = xi;
            }
            else
            {
                a[1] = 0.5 * (a[0] - a[1]);
                a[0] -= a[1];
                if (n > 4)
                {
                    M_rftbsub(n, a);
                    M_bitrv2(n, a);
                    M_cftbsub(n, a);
                }
                else if (n == 4)
                {
                    M_cftfsub(n, a);
                }
            }
        }

        static private void FastMulFFT(byte[] ww, byte[] uu, byte[] vv, long nbytes)
        {
            long j;
            ulong ul;
            double dtemp;
            double[] a;
            double[] b;

            long nn = nbytes;
            long nn2 = nbytes >> 1;

            if (nn > 8200)
            {
                a = new double[nn + 8];
                b = new double[nn + 8];
            }
            else
            {
                a = new double[8200];
                b = new double[8200];
            }

            int i = 0;
            for (j = 0; j < nn2; j++)
            {
                a[j] = (uu[i] * 100 + uu[i + 1]);
                b[j] = vv[i] * 100 + vv[i + 1];
                i += 2;
            }

            /* zero fill the second half of the arrays */
            for (j = nn2; j < nn; j++)
            {
                a[j] = 0.0;
                b[j] = 0.0;
            }

            /* perform the forward Fourier transforms for both numbers */
            M_rdft(nn, 1, a);
            M_rdft(nn, 1, b);

            /* perform the convolution ... */
            b[0] *= a[0];
            b[1] *= a[1];

            for (j = 3; j <= nn; j += 2)
            {
                dtemp = b[j - 1];
                b[j - 1] = dtemp * a[j - 1] - b[j] * a[j];
                b[j] = dtemp * a[j] + b[j] * a[j - 1];
            }

            /* perform the inverse transform on the result */
            M_rdft(nn, -1, b);

            /* perform a final pass to release all the carries */
            /* we are still in base 10000 at this point        */

            double carry = 0.0;
            j = nn;
            double nnr = 2.0 / nn;

            do
            {
                dtemp = b[--j]*nnr + carry + 0.5;
                ul = (ulong) (dtemp*1.0E-4);
                carry = ul;
                b[j] = dtemp - carry * 10000.0;
            } while (j != 0);

            /* copy result to our destination after converting back to base 100 */

            int w0 = 0;
            byte div = 0, rem = 0;

            UnpackMult((int)ul, ref div, ref rem);

            ww[w0 + 0] = div;
            ww[w0 + 1] = rem;

            for (j = 0; j <= (nn - 2); j++)
            {
                w0 += 2;

                UnpackMult((int)b[j], ref div, ref rem);

                ww[w0 + 0] = div;
                ww[w0 + 1] = rem;
            }
        }

        static private void FastMul(BigNumber aa, BigNumber bb, BigNumber rr)
        {
            long ii, k, nexp, sign;
            BigNumber M_ain = new BigNumber();
            BigNumber M_bin = new BigNumber();

            Copy(aa, M_ain);
            Copy(bb, M_bin);

            //GetSizeofInt();


            sign = M_ain.signum * M_bin.signum;
            nexp = M_ain.exponent + M_bin.exponent;

            if (M_ain.dataLength >= M_bin.dataLength)
                ii = M_ain.dataLength;
            else
                ii = M_bin.dataLength;

            ii = (ii + 1) >> 1;
            ii = NextPowerOfTwo(ii);

            k = 2 * ii;             /* required size of expression, in bytes  */

            Pad(M_ain, k);          /* fill out the data so the number of */
            Pad(M_bin, k);          /* bytes is an exact power of 2       */

            if (k > rr.mantissa.Length)
            {
                Expand(rr, (k + 31));
            }

            FastMulFFT(rr.mantissa, M_ain.mantissa, M_bin.mantissa, ii);

            rr.signum = (sbyte)sign;
            rr.exponent = nexp;
            rr.dataLength = 4 * ii;

            Normalize(rr);
        }
        /// <summary>
        /// rr = aa * bb
        /// </summary>
        static private void Mul(BigNumber aa, BigNumber bb, BigNumber rr)
        {
            int ai, itmp;
            sbyte sign;

            sign = (sbyte)(aa.signum * bb.signum);
            long nexp = aa.exponent + bb.exponent;

            if (sign == 0)
            {
                SetZero(rr);
                return;
            }

            long numdigits = aa.dataLength + bb.dataLength;
            long indexa = (aa.dataLength + 1) >> 1;
            long indexb = (bb.dataLength + 1) >> 1;

            if (indexa >= 48 && indexb >= 48)
            {
                FastMul(aa, bb, rr);
                return;
            }

            long ii = (numdigits + 1) >> 1;

            if (ii >= rr.mantissa.Length)
            {
                Expand(rr, ii + 31);
            }

            long index0 = indexa + indexb;
            rr.mantissa = new byte[index0 + 1];

            ii = indexa;

            do
            {
                index0--;
                long crp = index0;
                long jj = indexb;
                ai = aa.mantissa[--ii];

                do
                {
                    itmp = ai * bb.mantissa[--jj];

                    //rr.mantissa[crp - 1] += s_MsbLookupMult[itmp];
                    rr.mantissa[crp - 1] += (byte)(itmp / 100);
                    //rr.mantissa[crp] += s_LsbLookupMult[itmp];
                    rr.mantissa[crp] += (byte)(itmp % 100);

                    if (rr.mantissa[crp] >= 100)
                    {
                        rr.mantissa[crp] -= 100;
                        rr.mantissa[crp - 1]++;
                    }

                    crp--;

                    if (rr.mantissa[crp] >= 100)
                    {
                        rr.mantissa[crp] -= 100;
                        rr.mantissa[crp - 1]++;
                    }
                    //if (jj == 0) break;
                } while (jj != 0);
                //if (ii == 0) break;
            } while (ii != 0);

            rr.signum = sign;
            rr.exponent = nexp;
            rr.dataLength = numdigits > numDefaultPlaces ? numDefaultPlaces + 1 : numdigits;

            Normalize(rr);
        }

        static private void Div(BigNumber aa, BigNumber bb, BigNumber rr, long places)
        {
            int j, m, b0, indexr;
            sbyte sign;
            long trial_numer, nexp, iterations, k, icompare;

            BigNumber M_div_worka = new BigNumber();
            BigNumber M_div_workb = new BigNumber();
            BigNumber M_div_tmp7 = new BigNumber();
            BigNumber M_div_tmp8 = new BigNumber();
            BigNumber M_div_tmp9 = new BigNumber();

            sign = (sbyte)(aa.signum * bb.signum);

            if (sign == 0)      /* one number is zero, result is zero */
            {
                if (bb.signum == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero");
                }
                SetZero(rr);
                return;
            }

            if (aa.exponent > numDefaultPlaces || bb.exponent > numDefaultPlaces)
            {
                long aexp = aa.exponent;
                long bexp = bb.exponent;
                aa.exponent = 0;
                bb.exponent = 0;
                Div(aa, bb, rr, places);
                rr.exponent = aexp - bexp + rr.exponent;
                aa.exponent = aexp;
                bb.exponent = bexp;
                return;
            }

            if (bb.mantissa[0] >= 50)
            {
                Abs(M_div_worka, aa);
                Abs(M_div_workb, bb);
            }
            else       /* 'normal' step D1 */
            {
                k = 100 / (bb.mantissa[0] + 1);
                SetFromLong(M_div_tmp9, k);

                Mul(M_div_tmp9, aa, M_div_worka);
                Mul(M_div_tmp9, bb, M_div_workb);

                M_div_worka.signum = 1;
                M_div_workb.signum = 1;
            }

            b0 = 100 * M_div_workb.mantissa[0];

            if (M_div_workb.dataLength >= 3)
            {
                b0 += M_div_workb.mantissa[1];
            }

            nexp = M_div_worka.exponent - M_div_workb.exponent;

            if (nexp > 0)
            {
                iterations = nexp + places + 1;
            }
            else
            {
                iterations = places + 1;
            }

            k = (iterations + 1) >> 1;     /* required size of expression, in bytes */

            if (k > rr.mantissa.Length)
            {
                Expand(rr, k + 31);
            }

            /* clear the exponent in the working copies */

            M_div_worka.exponent = 0;
            M_div_workb.exponent = 0;

            /* if numbers are equalBT, ratio == 1.00000... */

            if ((icompare = Compare(M_div_worka, M_div_workb)) == 0)
            {
                iterations = 1;
                rr.mantissa[0] = 10;
                nexp++;
            }
            else			           /* ratio not 1, do the real division */
            {
                nexp += (icompare == 1) ? 1 : 0;                          /* to adjust the final exponent */
                M_div_worka.exponent += (1 + (icompare != 1 ? 1 : 0));	/* multiply numerator by 10 */

                indexr = 0;
                m = 0;

                while (true)
                {
                    /*
                     *  Knuth step D3. Only use the 3rd -> 6th digits if the number
                     *  actually has that many digits.
                     */

                    trial_numer = 10000L * M_div_worka.mantissa[0];

                    if (M_div_worka.dataLength >= 5)
                    {
                        trial_numer += 100 * M_div_worka.mantissa[1] + M_div_worka.mantissa[2];
                    }
                    else
                    {
                        if (M_div_worka.dataLength >= 3)
                        {
                            trial_numer += 100 * M_div_worka.mantissa[1];
                        }
                    }

                    j = (int)(trial_numer / b0);

                    /*
                     *    Since the library 'normalizes' all the results, we need
                     *    to look at the exponent of the number to decide if we
                     *    have a lead in 0n or 00.
                     */

                    if ((k = 2 - M_div_worka.exponent) > 0)
                    {
                        k++;
                        while (--k != 0) j /= 10;
                    }

                    if (j == 100) j = 99;   /* if qhat == base then decrease by 1      */

                    SetFromLong(M_div_tmp8, j);
                    Mul(M_div_tmp8, M_div_workb, M_div_tmp7);

                    /*
                     *    Compare our q-hat (index) against the desired number.
                     *    Value is is either correct, 1 too large, or 2 too large
                     *    per Theorem B on pg 272 of Art of Compter Programming,
                     *    Volume 2, 3rd Edition.
                     *
                     *    The above statement is only true if using the 2 leading
                     *    digits of the numerator and the leading digit of the
                     *    denominator. Since we are using the (3) leading digits
                     *    of the numerator and the (2) leading digits of the
                     *    denominator, we eliminate the case where our q-hat is
                     *    2 too large, (and q-hat being 1 too large is quite remote).
                     */

                    if (Compare(M_div_tmp7, M_div_worka) == 1)
                    {
                        j--;
                        Sub(M_div_tmp7, M_div_workb, M_div_tmp8);
                        Copy(M_div_tmp8, M_div_tmp7);
                    }

                    /*
                     *  Since we know q-hat is correct, step D6 is unnecessary.
                     *
                     *  Store q-hat, step D5. Since D6 is unnecessary, we can
                     *  do D5 before D4 and decide if we are done.
                     */

                    rr.mantissa[indexr++] = (byte)j;    /* index == 'qhat' */
                    m += 2;

                    if (m >= iterations) break;

                    /* step D4 */

                    Sub(M_div_worka, M_div_tmp7, M_div_tmp9);

                    /*
                     *  if the subtraction yields zero, the division is exact
                     *  and we are done early.
                     */

                    if (M_div_tmp9.signum == 0)
                    {
                        iterations = m;
                        break;
                    }

                    /* multiply by 100 and re-save */
                    M_div_tmp9.exponent += 2;
                    Copy(M_div_tmp9, M_div_worka);
                }
            }

            rr.signum = sign;
            rr.exponent = nexp;
            rr.dataLength = iterations;

            Normalize(rr);
        }
        /// <summary>
        /// divide two number and get the remainder of the division
        /// </summary>
        /// <param name="c1">divider</param>
        /// <param name="c2">divisor</param>
        /// <param name="res">remainder</param>
        static void DivGetRemainder(BigNumber c1, BigNumber c2, BigNumber res)
        {
            if (Compare(c1.Abs(), c2.Abs()) < 0) { Copy(c1, res); return; }
            if (c1.exponent - c2.exponent > numDefaultPlaces
                || c1.exponent > numDefaultPlaces || c2.exponent > numDefaultPlaces)
            {
                // tạm thời thế
                throw new Exception("Value takes a very long time to calculate");
            }
            //if (c1 >= 0) res = c1 - (|c1| / c2).Floor() * c2;
            //else res = c1 + (|c1| / c2).Floor() * c2;
            BigNumber sign = new BigNumber();
            BigNumber div = new BigNumber();
            BigNumber divRound = new BigNumber();
            BigNumber mul = new BigNumber();

            #region code cu ok
            Copy(c2, sign);             // c2 = sign
            sign.signum = c1.signum;    // c1, NOT c2
            Div(c1, c2, div);           // div = c1 / c2
            //Round(div, div, 32);      // sau phep nhan chia phai lam tron
            Floor(divRound, div.Abs()); // divRound = |div|.floor() = |c1 / c2|.floor()
            Mul(sign, divRound, mul);   // mul = sign * divRound = sign * |c1 / c2|.floor()
            //Approximate(mul);
            //Round(mul, mul, 32);      // sau phep nhan chia phai lam tron
            Sub(c1, mul, res);          // res = c1 - mul = c1 - sign * |c1 / c2|.floor()
            if (Compare(res.Abs(), c2.Abs()) > 0)
            {
                // recursion
                DivGetRemainder(res, c2, res);
            }
            #endregion

            #region code moi ok not
            //jump:if (Compare(c1, "0") >= 0)
            //{
            //    Div(c1, c2, div);
            //    Round(div, div, 32);
            //    Floor(divRound, div);
            //    Mul(divRound, c2, mul);
            //    Sub(c1, mul, res);
            //}
            //else
            //{
            //    Div(c1.Neg(), c2, div);
            //    Round(div, div, 32);
            //    Floor(divRound, div);
            //    Mul(divRound, c2, mul);
            //    Add(c1, mul, res);
            //}
            //if (Compare(res, c2.Abs()) > 0)
            //{
            //    Copy(res, c1);
            //    goto jump;
            //}
            #endregion
        }
		/// <summary>
        /// rr = aa / bb
        /// </summary>
        static void Div(BigNumber aa, BigNumber bb, BigNumber rr)
        {
            long places = MaxDigits(aa, bb);
            Div(aa, bb, rr, places);
        }
        /// <summary>
        /// result = src + dst
        /// </summary>
        static void Add(BigNumber src, BigNumber dst, BigNumber result)
        {
            int carry;
            sbyte sign;
            long aexp, bexp, adigits, bdigits, j;
            BigNumber A = 0, B = 0;

            if (src.signum == 0)
            {
                Copy(dst, result);
                return;
            }

            if (dst.signum == 0)
            {
                Copy(src, result);
                return;
            }

            if (src.signum == 1 && dst.signum == -1)
            {
                dst.signum = 1;
                Sub(src, dst, result);
                dst.signum = -1;
                return;
            }

            if (src.signum == -1 && dst.signum == 1)
            {
                src.signum = 1;
                Sub(dst, src, result);
                src.signum = -1;
                return;
            }

            sign = src.signum;
            aexp = src.exponent;
            bexp = dst.exponent;

            Copy(src, A);
            Copy(dst, B);

            if (aexp == bexp)
            {
                // scale (shift) 2 digits
                Scale(A, 2);
                Scale(B, 2);
            }
            else
            {
                // scale to larger exponent
                if (aexp > bexp)
                {
                    Scale(A, 2);
                    Scale(B, (aexp - bexp + 2));
                }
                else
                {
                    Scale(B, 2);
                    Scale(A, (bexp - aexp + 2));
                }
            }

            adigits = A.dataLength;
            bdigits = B.dataLength;

			BigNumber bn1 = new BigNumber(), bn2 = new BigNumber();
            if (adigits >= bdigits)
			{
				Copy(A, bn1);
				Copy(B, bn2);
				j = (bdigits + 1) >> 1;
			}
			else
			{
				Copy(B, bn1);
				Copy(A, bn2);
				j = (adigits + 1) >> 1;
			}
			Copy(bn1, result);

			carry = 0;
			while (j > 0)
			{
				j--;
				result.mantissa[j] += (byte)(carry + bn2.mantissa[j]);
				carry = (result.mantissa[j] >= 100) ? 1 : 0;
				if (result.mantissa[j] >= 100) result.mantissa[j] -= 100;
			}

            result.signum = sign;
            Normalize(result);
        }

        /// <summary>
        /// result = src - dst
        /// </summary>
        static void Sub(BigNumber src, BigNumber dst, BigNumber result)
        {
            int itmp, ChangeOrderFlag, borrow;
            sbyte sign;
            long adigits, bdigits, icompare, aexp, bexp, j;
            BigNumber A = 0, B = 0;
            if (src == 0) { Neg(dst, result); return; }
            if (dst.signum == 0)
            {
                Copy(src, result);
                return;
            }

            if (src.signum == 0)
            {
                Copy(dst, result);
                return;
            }

            if (src.signum == 1 && dst.signum == -1)
            {
                dst.signum = 1;
                Add(src, dst, result);
                dst.signum = -1;
                return;
            }

            if (src.signum == -1 && dst.signum == 1)
            {
                dst.signum = -1;
                Add(src, dst, result);
                dst.signum = 1;
                return;
            }

            Abs(A, src);
            Abs(B, dst);

            if ((icompare = Compare(A, B)) == 0)
            {
                SetZero(result);
                return;
            }

            if (icompare == 1)  /*  |a| > |b|  (do A-B)  */
            {
                ChangeOrderFlag = 1;
                sign = src.signum;
            }
            else                /*  |b| > |a|  (do B-A)  */
            {
                ChangeOrderFlag = 0;
                sign = (sbyte)-(src.signum);
            }

            aexp = A.exponent;
            bexp = B.exponent;

            if (aexp > bexp)
            {
                Scale(B, (aexp - bexp));
            }
            if (aexp < bexp)
            {
                Scale(A, (bexp - aexp));
            }

            adigits = A.dataLength;
            bdigits = B.dataLength;

            if (adigits > bdigits)
            {
                Pad(B, adigits);
            }

            if (adigits < bdigits)
            {
                Pad(A, bdigits);
            }

            var big = new BigNumber();
            var small = new BigNumber();

            if (ChangeOrderFlag == 1)		 // |a| > |b|  (do A-B)
            {
                Copy(A, big);
                Copy(B, small);
            }
            else   		// |b| > |a|  (do B-A) instead
            {
                Copy(B, big);
                Copy(A, small);
            }
            Copy(big, result);
            borrow = 0;
            j = (result.dataLength + 1) >> 1;
            while (j > 0)
            {
                j--;
                itmp = (result.mantissa[j] - (small.mantissa[j] + borrow));

                if (itmp >= 0)
                {
                    result.mantissa[j] = (byte)itmp;
                }
                else
                {
                    result.mantissa[j] = (byte)(100 + itmp);
                }
                borrow = (itmp < 0) ? 1 : 0;
            }

            result.signum = sign;
            Normalize(result);
        }

        static void SetZero(BigNumber mm)
        {
            mm.signum = 0;
            mm.mantissa = new byte[1];
            mm.mantissa[0] = 0;
            mm.exponent = 0;
            mm.dataLength = 1;
        }

        static private void SetFromDouble(BigNumber atm, double doubleValue)
        {
            SetFromString(atm, doubleValue.ToString());
        }

        static private void SetFromLong(BigNumber atm, long mm)
        {
            if (mm == 0)
            {
                SetZero(atm);
                return;
            }

            string ascii_number = mm.ToString();

            atm.signum = 1;

            if (mm < 0)
            {
                atm.signum = -1;
                ascii_number = ascii_number.Replace("-", "");
            }

            atm.exponent = ascii_number.Length;

            if ((atm.exponent % 2) != 0)
            {
                // append a 0 to least significant nibble in case of odd length
                ascii_number += "0";
            }

            long nbytes = (atm.exponent + 1) >> 1;    // display 2 digits per byte

            // allocate data array
            atm.mantissa = new byte[atm.exponent + 1];

            atm.dataLength = atm.exponent;

            for (int ii = 0, p = 0; ii < nbytes; ii++)
            {
                atm.mantissa[ii] = (byte)(10 * ascii_number[p] + ascii_number[p + 1] - 528);
                p += 2;
            }
            Normalize(atm);
        }

        static private void SetFromString(BigNumber atm, string value)
        {
            //SetZero(atm);

            int p;
            sbyte sign = 1;
            long exponent = 0;

            value = value.Trim().Replace(" ", "");  // trim whitespace
            value = value.ToLower();                // convert to lower

            if (value.Contains("e+"))
            {
                value = value.Replace("e+", "e"); // remove optional '+' character
            }
            if (value.StartsWith("+"))
            {
                sign = 1;
                value = value.Substring(1);
            }
            if (value.StartsWith("-"))
            {
                sign = -1;
                value = value.Substring(1);
            }

            if (value.IndexOf("e") > 0) // e cannot be leading
            {
                string[] com = value.Split('e');

                try
                {
                    if (com[1][0] == '+') throw new Exception("Input string is invalid");
                    exponent = Convert.ToInt64(com[1]);
                }
                catch { throw new Exception("Number is too large"); }

                value = com[0];
            }

            value = value.Replace(Common.GroupSeparator, Common.DecimalSeparator);

            int j = value.IndexOf(Common.DecimalSeparator);
            if (j == -1)
            {
                value = value + Common.DecimalSeparator;
                j = value.Length - 1;   //j = value.IndexOf(Common.DecimalSeparator);
            }

            exponent += j;              // atm.stringvalue.length
            value = value.Remove(value.IndexOf(Common.DecimalSeparator), 1);

            int i = value.Length;
            atm.dataLength = i;
            // i is is even
            if (i % 2 != 0) value += "0";

            j = value.Length >> 1;  //=value.Length/2

            //if (value.Length > atm.mantissa.Length)
            {
                Expand(atm, atm.dataLength/* + 28*/+ 1);
            }

            byte ch;
            bool zflag = true;

            for (i = 0, p = 0; i < j; i++)
            {
                ch = (byte)(10 * value[p] + value[p + 1] - 528);    // 528 = 11 * '0'
                if (ch != 0) zflag = false;

                if ((ch & 0xFF) >= 100 || ((ch & 0xFF) < 100) && (ch / 10 != value[p] - '0'))
                {
                    // Error!, throw new exception
                    throw new Exception("Input string is not a number");
                }

                atm.mantissa[i] = ch;
                atm.mantissa[i + 1] = 0;
                p += 2;
            }

            if (zflag)
            {
                atm.exponent = 0;
                atm.signum = 0;
                atm.dataLength = 1;
                atm.mantissa[0] = 0;
            }
            else
            {
                atm.exponent = exponent;
                atm.signum = sign;
                Normalize(atm);
            }
        }
    } // class
} // namespace
