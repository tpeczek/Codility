using System;
using System.Collections.Generic;

//Mu 2011
class Solution
{
    #region Fields
    private const int _zeroCharacter = (int)'0';
    private const int _nineCharacter = (int)'9';
    private Dictionary<char, int> _digits = new Dictionary<char, int>() { { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 } };
    private ModularArithmetic _modularArithmetic = null;
    private const long _modulus = 1410000017;
    #endregion

    #region Constructor
    public Solution()
    {
        //Because potential numbers might be very big we will use modular arithmetic
        _modularArithmetic = new ModularArithmetic(_modulus);
    }
    #endregion


    public int solution(string S)
    {
        //This is where result will be stored
        long numberOfZeros = 0;

        //Sanity check
        if (!String.IsNullOrEmpty(S))
        {
            int L = S.Length;

            if (L > 1 && S.StartsWith("0"))
                throw new ArgumentException("Parameter can't contain leading zeros", "S");

            //We will store the value of the N in the modular arithmetic form
            long modularArithmeticNumber = 0;
            //We will also store the number of zeros in a decimal representation of N --> Z
            int numberOfZerosInNumber = 0;

            //We will go most to least significant.
            for (int i = 0; i < L; i++)
            {
                if (_zeroCharacter > S[i] || S[i] > _nineCharacter)
                    throw new ArgumentException("Parameter can contain only digits (0-9)", "S");

                //The current digit --> D
                int digit = _digits[S[i]];

                //numberOfZeros(10 * N + D) = 10 * (numberOfZeros(N) - 1) + N - (9 - D)*Z + 1
                numberOfZeros = _modularArithmetic.Subtract(_modularArithmetic.Add(_modularArithmetic.Multiply(10, numberOfZeros), modularArithmeticNumber), _modularArithmetic.Multiply(numberOfZerosInNumber, 9 - digit));
                        
                if (digit == 0)
                    numberOfZerosInNumber += 1;

                //The value of N at the end of the loop is previous value of N multiplied by 10 and increased by current digit
                modularArithmeticNumber = _modularArithmetic.Add(_modularArithmetic.Multiply(10, modularArithmeticNumber), digit);
            }
        }

        //Return the result
        return (int)_modularArithmetic.Add(numberOfZeros, 1);
    }
}