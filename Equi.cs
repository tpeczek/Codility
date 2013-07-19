using System;

//Equi
class Solution {
    public int solution(int[] A) {
        //This is where result will be stored
        int equilibriumIndex = -1;

        //Sanity check
        if (A != null && A.Length > 0)
        {
            int N = A.Length;

            //We will store the elements sum in Int64 to avoid arithmetic overflow
            Int64 lowerIndicesElementsSum = 0;
            //We will pre-calculate the sum of all elements as initial value for sum of elements with higher indices
            Int64 higherIndicesElementsSum = 0;
            for (int i = 0;i < N;i++)
            {
                higherIndicesElementsSum += A[i];
            }

            //For every index in A array
            for (int i = 0;i < N;i++)
            {
                //First we should subtract the current element value from sum of elements with higher indices
                higherIndicesElementsSum -= A[i];

                //If the sum of elements with lower indices is equal to sum of elements with higher indices
                if (lowerIndicesElementsSum == higherIndicesElementsSum)
                {
                    //We have found the equilibrium index
                    equilibriumIndex = i;
                    break;
                }

                //If not we should add current element value to sum of elements with lower indices and go to the next iteration
                lowerIndicesElementsSum += A[i];
            }
        }

        //Return the result
        return equilibriumIndex;
    }
}