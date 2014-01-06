using System;

//Phi 2012 (based on Codility Blog --> http://blog.codility.com/2012/10/phi-2012-codility-programming.html)
class Solution
{
    #region Fields
    private const long _modulus = 10000007;
    private ModularArithmetic _modularArithmetic = null;
    #endregion

    #region Constructor
    public Solution()
    {
        //Because potential numbers might be very big we will use modular arithmetic
        _modularArithmetic = new ModularArithmetic(_modulus);
    }
    #endregion

    #region Methods
    private bool IsFeasibleBitVector(int x)
    {
        bool isFeasibleBitVector = true;

        while(x > 0)
        {
            if (x % 2 == 1)
                isFeasibleBitVector = !isFeasibleBitVector;
            else if (!isFeasibleBitVector)
                break;

            x = x / 2;
        }
        
        return isFeasibleBitVector;
    }

    private long[,] GetPossibleCombinationsMatrix(int M)
    {
        int matrixDimension = (int)Math.Pow(2, M);
        long[,] possibleCombinationsMatrix = new long[matrixDimension, matrixDimension];

        for (int i = 0; i < matrixDimension; i++)
        {
            if (IsFeasibleBitVector(i))
            {
                for (int j = 0; j < matrixDimension; j++)
                {
                    if (IsFeasibleBitVector(j) && (i & j) == 0)
                        possibleCombinationsMatrix[i, j] = 1;
                }
            }
        }

        return possibleCombinationsMatrix;
    }

    private long[,] GetIdentityMatrix(int size)
    {
        long[,] identityMatrix = new long[size, size];

        for (int i = 0; i < size; i++)
            identityMatrix[i, i] = 1;

        return identityMatrix;
    }

    private long[,] MultiplyMatrices(long[,] multiplicand, long[,] multiplier)
    {
        int length = multiplicand.GetLength(0);
        long[,] product = new long[length, length];

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                long currentProduct = 0;

                for (int k = 0; k < length; k++)
                    currentProduct = _modularArithmetic.Add(currentProduct, _modularArithmetic.Multiply(multiplicand[i, k], multiplier[k, j]));

                product[i, j] = currentProduct;
            }
        }

        return product;
    }

    private long[,] PowerMatrix(long[,] @base, long power)
    {
        long[,] product = GetIdentityMatrix(@base.GetLength(0));

        if (power == 0)
            product = GetIdentityMatrix(@base.GetLength(0));
        else if (power % 2 == 0)
            product = PowerMatrix(MultiplyMatrices(@base, @base), power / 2);
        else
            product = MultiplyMatrices(PowerMatrix(@base, power - 1), @base);

        return product;
    }
    #endregion
    
    public int solution(int N, int M)
    {
        if (N < 1 || N > 1000000)
            throw new ArgumentOutOfRangeException("N", "N must be an integer within the range [1..1,000,000].");

        if (M < 1 || M > 7)
            throw new ArgumentOutOfRangeException("M", "M must be an integer within the range [1..7].");

        return (int)PowerMatrix(GetPossibleCombinationsMatrix(M), N)[0, 0];
    }
}