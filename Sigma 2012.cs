using System;
using System.Collections.Generic;

//Sigma 2012
class Solution
{
    public int solution(int[] H)
    {
        //This is where result will be stored
        int minimumNumberOfBlocks = 0;

        //Sanity check
        if (H != null && H.Length > 0 && H.Length <= 100000)
        {
            int N = H.Length;

            //We will truck the rectangles we currently have on stack
            Stack<int> currentBlocks = new Stack<int>(N);
            //And the height of the wall
            int currentHeight = 0;

            //Going through entire wall length
            for (int i = 0; i < N; i++)
            {
                //Until we will not achieve the desired wall height
                while (currentHeight != H[i])
                {
                    //If the current height is 0
                    if (currentHeight == 0)
                    {
                        //We push one block with desired height
                        currentBlocks.Push(H[i]);
                        currentHeight = H[i];
                        minimumNumberOfBlocks++;
                    }
                    //If current height is less than desired
                    else if (currentHeight < H[i])
                    {
                        //We push one block with missing height
                        currentBlocks.Push(H[i] - currentHeight);
                        currentHeight = H[i];
                        minimumNumberOfBlocks++;
                    }
                    //If current height is greater than desired
                    else
                    {
                        //We will be removing block until we will get height lower or equal to desired
                        int topBlock = currentBlocks.Pop();
                        currentHeight = currentHeight - topBlock;
                    }
                }
            }
        }
        else
            throw new ArgumentException("A non-empty array no bigger than 100,000 elements must be given", "H");

        //Return the result
        return minimumNumberOfBlocks;
    }
}