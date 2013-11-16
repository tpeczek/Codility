using System;

//Rho 2012 (based on Codility Blog --> http://blog.codility.com/2012/05/rho-2012-codility-programming.html)
class Solution
{
    #region Methods
    //Method looking for next possible value in the sequence
    private bool SearchDivision(int[] sequence, int index, int A)
    {
        if (sequence[index] == A)
            return true;
        else if (index + 1 >= sequence.Length)
            return false;
        else if (!CanReach(sequence.Length - index - 1, sequence[index], A))
            return false;

        for (int i = index; i > -1; i--)
        {
            for (int j = i; j > -1; j--)
            {
                if ((sequence[i] * 2 < sequence[index]) || (sequence[i] + sequence[j] <= sequence[index]))
                    break;

                if (sequence[i] + sequence[j] <= A)
                {
                    sequence[index + 1] = sequence[i] + sequence[j];
                    if (SearchDivision(sequence, index + 1, A))
                        return true;
                }
            }
        }

        return false;
    }

    //Method which checks if we can go from start value to target value within given number of steps assuming that we are doubling value on every step.
    private bool CanReach(int steps, int start, int target)
    {
        for (int i = 0; i < steps; i++)
            start *= 2;

        return start >= target;
    }
    #endregion

    public int[] solution(int A)
    {
        //This is where result will be stored
        int[] shortestSequence = null;

        //Sanity check
        if (A > 0 && A <= 600)
        {
            //The shortest sequence will never be longer than value below
            int maxSequenceLength = 2 * (int)Math.Floor(Math.Log(A, 2));

            for (int i = 1; i <= maxSequenceLength; i++)
            {
                shortestSequence = new int[i];
                shortestSequence[0] = 1;

                if (SearchDivision(shortestSequence, 0, A))
                    break;
            }
        }
        else
            throw new ArgumentOutOfRangeException("A", "The parameter must be an integer within the range [1..600].");

        //Return the result
        return shortestSequence;
    }
}