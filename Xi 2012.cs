using System;

//Xi 2012 (based on Codility Blog --> http://blog.codility.com/2012/02/xi-2012-codility-programming.html)
class Solution
{
    #region Fields
    private const char _zero = '0';
    private const char _one = '1';

    private long[] _precalutadeSparseIntegersCounts;

    private const long _modulus = 1000000007;
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
    //The method to pre-calculate the number of K-sparse integers smaller than 2^I
    private void PrecaluteSparseIntegersCounts(int I, int K)
    {
        //K-sparse numbers smaller than 2^I can be divided into two groups: 
        //- numbers smaller than 2^I-1 (there are F[I-1] of them)
        //- numbers smaller than 2^I but not smaller than 2^I-1, their binary representations contain 1 at the position representing 2^I-1 and 0s at the positions representing 2^I-2, 2^I-3, …, 2^I-K-1 (there are F[I-K-1] of them)
            
        //Above can be described as following recursive equation:
        //F[I] = F[I-1] + F[I-K-1]  for I > 0
        //F[I] = 1                  for I <= 0

        _precalutadeSparseIntegersCounts = new long[I];

        _precalutadeSparseIntegersCounts[0] = 1;

        for (int i = 1; i <= K && i < I; i++)
            _precalutadeSparseIntegersCounts[i] = _precalutadeSparseIntegersCounts[i - 1] + 1;

        for (int i = K + 1; i < I; i++)
            _precalutadeSparseIntegersCounts[i] = _modularArithmetic.Add(_precalutadeSparseIntegersCounts[i - 1], _precalutadeSparseIntegersCounts[i - K - 1]);
    }

    //The method to increase a binary representation of an integer (stored in a string) by 1.
    private string IncrementBinaryRepresentation(string N)
    {
        char[] C = N.ToCharArray();
        for (int i = C.Length - 1; i >= 0; i--)
        {
            if (C[i] == _one)
                C[i] = _zero;
            else if (C[i] == _zero)
            {
                C[i] = _one;
                return new String(C);
            }
            else
                throw new ArgumentException("N can consists only of the characters '0' and/or '1'.", "N");
        }
        return "1" + new String(C);
    }

    //The method to find the number of K-sparse integers smaller than a given positive integer.
    private long CountKSparseIntegersBelowInteger(string N, int K)
    {
        //The problem is easy to solve for integers witch are K-sparse, so we need to check if the input integer is K-sparse
        //N = _zero + N;

        int zerosCount = K, shiftingIndex = 0, i = 0;
        while (i < N.Length)
        {
            if (N[i] == _one)
            {
                //If there are two consecutive 1s separated by fewer than K 0s
                if (zerosCount < K)
                    //The integer is not K-sparse
                    break;
                //If the two consecutive 1s are separated by at least K 0s
                else if (zerosCount > K)
                    //We mark current position as possible shift index
                    shiftingIndex = i;

                zerosCount = 0;
            }
            else
                zerosCount++;

            i++;
        }
            
        //If the integer is not K-sparse
        if (i < N.Length)
            //We need to shift the K-spare part of it up and set all lower bits to 0.
            N = N.Substring(0, shiftingIndex) + _one + new String(_zero, N.Length - shiftingIndex);

        return CountKSparseIntegersBelowSparseInteger(N, K);
    }

    //The method to find the number of K-sparse integers smaller than a given K-sparse positive integer.
    private long CountKSparseIntegersBelowSparseInteger(string N, int K)
    {
        //K-sparse numbers smaller than N can be divided into two groups:
        //- K-sparse numbers smaller than 2^I (we have those pre-calculated in _precalutadeSparseIntegersCounts)
        //- K-sparse numbers smaller than N but not smaller than 2^I, the binary representations of such numbers contain 1 at the position representing 2^I and 0s at the positions representing 2^I-1, 2^I-2, …, 2^I-K, the remaining bits represent a number smaller than N - 2^I

        long sparseIntegersCount = 0;

        //Taking into consideration above description, the result is a sum of values of _precalutadeSparseIntegersCounts[I] for values of I in which the binary representation of N contains 1 at the position representing 2^I
        for (int i = 0; i < N.Length; i++)
        {
            if (N[i] == _one)
                sparseIntegersCount = _modularArithmetic.Add(sparseIntegersCount, _precalutadeSparseIntegersCounts[N.Length - 1 - i]);
        }

        return sparseIntegersCount;
    }
    #endregion

    public int solution(string S, string T, int K)
    {
        //Sanity check
        if (String.IsNullOrEmpty(S) || S.Length > 300000 || S.StartsWith("0"))
            throw new ArgumentException("S length must be within the range [1..300,000] and it can't have leading zeros.", "S");

        //Sanity check
        if (String.IsNullOrEmpty(T) || T.Length > 300000 || T.StartsWith("0"))
            throw new ArgumentException("T length must be within the range [1..300,000] and it can't have leading zeros.", "T");

        //Sanity check
        if (K < 1 && K > 30)
            throw new ArgumentException("K must be within the range [1..30]", "K");

        PrecaluteSparseIntegersCounts(T.Length + 2, K);

        //The number of K-sparse integers in the range [A..B] is equal to the number of K-sparse integers smaller than B+1 minus the number of K-sparse integers smaller than A.
        return (int)_modularArithmetic.Subtract(CountKSparseIntegersBelowInteger(IncrementBinaryRepresentation(T), K), CountKSparseIntegersBelowInteger(S, K));
    }
}