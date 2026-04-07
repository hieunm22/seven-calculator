/// -------- ToujoursEnBeta
/// Author & Copyright : Peter Luschny
/// License: LGPL version 3.0 or (at your option)
/// Creative Commons Attribution-ShareAlike 3.0
/// Comments mail to: peter(at)luschny.de
/// Created: 2010-03-01
  
namespace Sharith.Math.Primes
{
    using Sharith.Math.MathUtils;
 
    /// <summary>
    ///  The interface for a prime number sieve.
    ///  Much of its functionality is given through PrimeCollections.
    ///
    /// @version cs-2004-09-02
    /// @author Peter Luschny
    /// </summary>
    public interface IPrimeSieve
    {
        /// <summary>
        /// Checks if a given number is prime.
        /// </summary>
        /// <param name="cand">The number the primality of which is to be checked.</param>
        /// <returns>True iff the given number is prime.</returns>
        bool IsPrime(int cand);
 
        /// <summary>
        /// Gives the PrimeCollection of a subrange of the sieve.
        /// It is the collection of all prime numbers p, 
        /// such that low &lt;= p &lt;= high.
        /// Note: If the range [low, high] is not included in the range 
        /// of the sieve, this function might throw an ArgumentOutOfRangeException.
        /// </summary>
        /// <param name="low">The low limit of the enumaration interval.</param>
        /// <param name="high">The high limit of the enumeration interval.</param>
        /// <returns>The enumeration of the prime numbers between
        /// low and high.</returns>
        IPrimeCollection GetPrimeCollection(long low, long high);
    }
}
