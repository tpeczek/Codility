using System;
using System.Collections.Generic;

//Chi 2012
class Solution
{
    public int[] solution(int[] A, int[] B)
    {
        //Sanity check
        if (A == null || A.Length > 30000)
            throw new ArgumentException("An array no bigger than 30,000 elements must be given", "A");

        //Sanity check
        if (B == null || B.Length > 30000)
            throw new ArgumentException("An array no bigger than 30,000 elements must be given", "B");

        int M = A.Length;
        int N = B.Length;

        //We will find the highest point in the landscape
        int highestLandscapePoint = 0;
        for (int i = 1; i < M; i++)
        {
            if (A[i] > A[highestLandscapePoint])
                highestLandscapePoint = i;
        }

        //We will pre-calculate the positions at with the cannonballs will rest if fired at given height
        Dictionary<int, int> possibleLandscapePoints = new Dictionary<int,int>();
        for (int i = 0; i <= highestLandscapePoint; i++)
        {
            for (int j = possibleLandscapePoints.Count; j <= A[i]; j++)
                possibleLandscapePoints.Add(j, i - 1);
        }

        //For every cannonball shot
        for (int i = 0; i < N; i++)
        {
            //If the shot will have impact on the landscape
            if (B[i] > A[0] && B[i] <= A[highestLandscapePoint])
            {
                //Check at which position the cannonball will rest
                int landscapePoint = possibleLandscapePoints[B[i]];
                
                //Adjust the height of landscape at this position
                A[landscapePoint]++;

                //If the cannonball fired at the new height of this position could rest here
                if (possibleLandscapePoints[A[landscapePoint]] > landscapePoint - 1)
                    //Set up new rest position for cannonball fired at this new height
                    possibleLandscapePoints[A[landscapePoint]] = landscapePoint - 1;
            }
        }

        //Return the result
        return A;
    }
}