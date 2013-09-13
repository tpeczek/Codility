using System;
using System.Collections.Generic;

class Solution
{
    #region Fields
    private ModularArithmetic _modularArithmetic = null;
    private List<long> _factorials = new List<long>() { 1 };
    #endregion

    #region Constructor
    public Solution()
    {
        //Because potential numbers might be very big (worst case 1000000!) we will use modular arithemtic
        _modularArithmetic = new ModularArithmetic(1410000017);
    }
    #endregion

    #region Arithmetic
    //Factorial --> n!
    private long Factorial(int n)
    {
        int k = _factorials.Count - 1;
        //Store all the factorials up to the highest required
        while (k < n)
            _factorials.Add(_modularArithmetic.Multiply(_factorials[k++], k));
        //This way every factorial is calculated only once
        return _factorials[n];
    }

    //Binomial coefficient --> n!/(k!(n - k)!)
    private long BinomialCoefficient(int n, int k)
    {
        long numerator = Factorial(n);
        long denominator = _modularArithmetic.Multiply(Factorial(k), Factorial(n - k));

        return _modularArithmetic.Divide(numerator, denominator); ;
    }
    #endregion

    public int solution(int[] T, int[] D)
    {
        //This is where result will be stored
        long numberOfCombinations = 0;

        //Sanity check
        if (T != null && T.Length > 0 && D != null && D.Length == D.Length)
        {
            int N = T.Length;
            numberOfCombinations = 1;

            //The general algorithm is very simple
            for (int i = 0; i < N; i++)
                //We mutltiply all the binominal coefficients to get the result (thanks to modular arithemtic the result is alread a proper remainder)
                numberOfCombinations = _modularArithmetic.Multiply(numberOfCombinations, BinomialCoefficient(T[i], D[i]));
        }

        //Return the result
        return (int)numberOfCombinations;
    }
}