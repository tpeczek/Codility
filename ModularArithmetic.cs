using System;

// Implementation of basic modular arithmetic (http://en.wikipedia.org/wiki/Modular_arithmetic) operations
public class ModularArithmetic
{
    #region Modulus
    private long _modulus;

    //The modular arithmetic requires different modulus operation than % operator provides
    private long CongruentModulo(long dividend)
    {
        return (dividend % _modulus + _modulus) % _modulus;
    }
    #endregion

    #region Extended Euclidean Algorithm
    private struct ExtendedEuclideanAlgorithmResult
    {

        public long GreatestCommonDivisor { get; private set; }

        public long X { get; private set; }

        public long Y { get; private set; }

        public ExtendedEuclideanAlgorithmResult(long greatestCommonDivisor, long x, long y)
            : this()
        {
            GreatestCommonDivisor = greatestCommonDivisor;
            X = x;
            Y = y;
        }
    }

    //Recursive Extended Euclidean algorithm (http://en.wikipedia.org/wiki/Extended_Euclidean_algorithm) implementation
    private ExtendedEuclideanAlgorithmResult RecursiveExtendedEuclideanAlgorithm(long dividend, long divisor)
    {
        if (divisor == 0)
            return new ExtendedEuclideanAlgorithmResult(dividend, 1, 0);

        long quotient = dividend / divisor;
        long remainder = (dividend % divisor + divisor) % divisor;

        ExtendedEuclideanAlgorithmResult result = RecursiveExtendedEuclideanAlgorithm(divisor, remainder);

        return new ExtendedEuclideanAlgorithmResult(result.GreatestCommonDivisor, result.Y, result.X - (quotient * result.Y));
    }
    #endregion

    #region Multiplicative Inverse
    //The modular multiplication inverse (http://en.wikipedia.org/wiki/Modular_multiplicative_inverse) is required for dividing in modular arithmetic
    private long ModularMultiplicativeInverse(long a)
    {
        //Find modular multiplication inverse with Extended Euclidean algorithm
        return RecursiveExtendedEuclideanAlgorithm(a, _modulus).X;
    }
    #endregion

    #region Constructor
    public ModularArithmetic(long modulus)
    {
        _modulus = modulus;
    }
    #endregion

    #region Operations
    public long Add(long firstAddend, long secondAddend)
    {
        return CongruentModulo(firstAddend + secondAddend);
    }

    public long Divide(long dividend, long divisor)
    {
        //We can't divide in modular arithmetic, we need to multiply by modular multiplication inverse
        return CongruentModulo(dividend * ModularMultiplicativeInverse(divisor));
    }

    public long Multiply(long multiplicand, long multiplier)
    {
        return CongruentModulo(multiplicand * multiplier);
    }

    public long Subtract(long minuend, long subtrahend)
    {
        return CongruentModulo(minuend - subtrahend + _modulus);
    }
    #endregion
}