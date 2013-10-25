using System;

//Omicron 2012 (based on Codility Blog --> http://blog.codility.com/2012/03/omicron-2012-codility-programming.html)
class Solution
{
    #region Fields
    private long[,] _fibonacciMatrix = new long[,] { { 1, 1 }, { 1, 0 } };
    private long[,] _powMatrix = new long[,] { { 1, 0 }, { 0, 1 } };

    private const long _modulus = 10000103;
    private const long _modulusPeriod = 20000208;
    private ModularArithmetic _modularArithmetic = null;
    #endregion

    #region Constructor
    public Solution()
    {
        //Because potential numbers might be very big we will use modular arithemtic
        _modularArithmetic = new ModularArithmetic(_modulus);
    }
    #endregion

    #region Methods
    private long[,] MultiplyMatrices(long[,] multiplicand, long[,] multiplier)
    {
        long[,] product = new long[2, 2];

        product[0,0] = _modularArithmetic.Add(_modularArithmetic.Multiply(multiplicand[0,0], multiplier[0,0]), _modularArithmetic.Multiply(multiplicand[0,1], multiplier[1,0]));
        product[0,1] = _modularArithmetic.Add(_modularArithmetic.Multiply(multiplicand[0,0], multiplier[0,1]), _modularArithmetic.Multiply(multiplicand[0,1], multiplier[1,1])); 
        product[1,0] = _modularArithmetic.Add(_modularArithmetic.Multiply(multiplicand[1,0], multiplier[0,0]), _modularArithmetic.Multiply(multiplicand[1,1], multiplier[1,0]));
        product[1,1] = _modularArithmetic.Add(_modularArithmetic.Multiply(multiplicand[1,0], multiplier[0,1]), _modularArithmetic.Multiply(multiplicand[1,1], multiplier[1,1]));

        return product;
    }

    private long[,] PowerMatrix(long[,] @base, long power)
    {
        long[,] product;

        if (power == 0)
            product = _powMatrix;
        else if (power % 2 == 0)
            product = PowerMatrix(MultiplyMatrices(@base, @base), power / 2);
        else
            product = MultiplyMatrices(PowerMatrix(@base, power - 1), @base);

        return product;
    }

    private long Fibonacci(long n)
    {
        return PowerMatrix(_fibonacciMatrix, n)[0, 1];
    }

    private long PowerModModulusPeriod(long @base, long power, long modulusPeriod)
    {
        long product;

        if (power == 0)
            product = 1;
        else if (power % 2 == 0)
            product = PowerModModulusPeriod((@base * @base) % modulusPeriod, power / 2, modulusPeriod);
        else
            product = (@base * PowerModModulusPeriod(@base, power - 1, modulusPeriod)) % modulusPeriod;

        return product;
    }
    #endregion

    public int solution(int N, int M)
    {
        long powerFibonacci = 0;

        if (N < 0)
            throw new ArgumentOutOfRangeException("N", "N must be a non-negative integer.");

        if (M < 0)
            throw new ArgumentOutOfRangeException("M", "M must be a non-negative integer.");

        if (N == 0 && M != 0)
            powerFibonacci = 0;
        else if (N == 1 || M == 0)
            powerFibonacci = 1;
        else
            powerFibonacci = Fibonacci(PowerModModulusPeriod(N, M, _modulusPeriod));

        return (int)powerFibonacci;
    }
}