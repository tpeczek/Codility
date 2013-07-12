using System;

//Alpha 2010
class Solution {
    public int solution(int[] A) {
        //This is where result will be stored
        int firstCoveringPrefix = 0;

        //Sanity check
        if (A != null && A.Length > 0)
        {
            //Because each element of array A is an integer within the range [0..A.Length-1] we can track the occurences in bool array of a same length
            //Important: Default value of bool in .Net is false
            bool[] occurences = new bool[A.Length];

            //For every element in A array
            for (int i = 0; i < A.Length; i++)
            {
                //If the element haven't occured yet
                if (!occurences[A[i]])
                {
                    //Mark its occurence in tracking array
                    occurences[A[i]] = true;
                    //Change the covering prefix to current index as this is first occurance of this element
                    firstCoveringPrefix = i;
                }
            }
        }

        //Return the result
        return firstCoveringPrefix;
    }
}