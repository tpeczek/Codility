using System;
using System.Collections.Generic;

//Pi 2012 (based on Codility Blog --> http://blog.codility.com/2012/04/pi-2012-codility-programming.html)
class Solution
{
    #region Methods
    private int[] FindLeftClosestAscendersDistances(int[] A)
    {
        int N = A.Length;
        int[] leftClosestAscendersDistances = new int[N];

        Stack<int> stack = new Stack<int>(N);
        for (int i = 0; i < N; i++)
        {
            while (stack.Count > 0 && A[stack.Peek()] <= A[i])
                stack.Pop();

            if (stack.Count == 0)
                leftClosestAscendersDistances[i] = Int32.MaxValue;
            else
                leftClosestAscendersDistances[i] = i - stack.Peek();

            stack.Push(i);
        }

        return leftClosestAscendersDistances;
    }
    #endregion

    public int[] solution(int[] A)
    {
        //This is where result will be stored
        int[] closestAscendersDistances = null;

        //Sanity check
        if (A != null)
        {
            int N = A.Length;

            int[] leftClosestAscendersDistances = FindLeftClosestAscendersDistances(A);

            Array.Reverse(A);
            int[] rightClosestAscendersDistances = FindLeftClosestAscendersDistances(A);

            closestAscendersDistances = new int[N];
            for (int i = 0; i < N; i++)
            {
                closestAscendersDistances[i] = leftClosestAscendersDistances[i] < rightClosestAscendersDistances[N - i - 1] ? leftClosestAscendersDistances[i] : rightClosestAscendersDistances[N - i - 1];

                if (closestAscendersDistances[i] == Int32.MaxValue)
                    closestAscendersDistances[i] = 0;
            }
        }

        //Return the result
        return closestAscendersDistances;
    }
}