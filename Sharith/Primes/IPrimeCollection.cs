/// -------- ToujoursEnBeta
/// Author & Copyright : Peter Luschny
/// License: LGPL version 3.0 or (at your option)
/// Creative Commons Attribution-ShareAlike 3.0
/// Comments mail to: peter(at)luschny.de
/// Created: 2010-03-01

using System.Collections.Generic;
using Sharith.Math.MathUtils;

namespace Sharith.Math.Primes
{
    /// <summary>
    ///  An interface for enumerating a prime number sieve.
    ///
    ///  <para />An IPrimeCollection is both IEnumerable&lt;int&gt;
    ///  and IDisposable as well as an IEnumerator&lt;int&gt;.
    /// <blockquote><pre>
    ///
    /// <para />To understand the difference between "SieveRange" and "PrimeRange"
    /// better, let us consider the following example:
    ///
    /// <para />If the SieveRange is 10..20, then the PrimeRange is 5..8,
    /// because of the following table
    ///
    /// <para />                   &lt;-     SieveRange           -&gt;
    /// <para />1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
    /// <para />x,1,2,x,3,x,4,x,x, x, 5, x, 6, x, x, x, 7, x, 8, x, x, x, 9
    /// <para />                   &lt;-     PrimeRange           -&gt;
    /// <para />
    /// <para />Thus the smallest prime in the SieveRange is the 5-th prime and
    /// the largest prime in this SieveRange is the 8-th prime.
    /// <para />
    /// <para /></pre></blockquote>
    /// <para />
    /// <para />version cs-2004-09-03
    /// <para />author Peter Luschny
    ///
    /// </summary>
    public interface IPrimeCollection : IEnumerator<long>, IEnumerable<long>
    {
        /// <summary>
        /// Gets the number of primes in the enumeration.
        /// </summary>
        long NumberOfPrimes { get; }
    }
}
