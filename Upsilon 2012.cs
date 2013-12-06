using System;

//Upsilon 2012 (based on Cartesian tree --> http://en.wikipedia.org/wiki/Cartesian_tree)
class Solution
{
    public int solution(int[] A)
    {
        //This is where result will be stored
        int maximumSequenceLength = 0;
        
        //Sanity check
        if (A != null && A.Length > 0 && A.Length <= 100000)
        {
            int N = A.Length;
            
            int[] guardedInput = new int[N + 1];
            int[] rootsStack = new int[N + 2];
            int[] heightsStack = new int[N + 2];
            int stackPointer = 1;
            
            //We need to add a guard at the end of input table and initialize the stacks
            for (int i = 0; i < N; i++)
            {
                guardedInput[i] = A[i];
                rootsStack[i] = Int32.MaxValue;
                heightsStack[i] = 0;
            }
            
            guardedInput[N] = Int32.MaxValue - 1;
            rootsStack[N] = Int32.MaxValue;
            heightsStack[N] = 0;
            rootsStack[N + 1] = Int32.MaxValue;
            heightsStack[N + 1] = 0;
            
            //We iterate through the guarded input table
            for (int i = 0; i <= N; i++)
            {
                //If current element is greater then the one at the top of the stack
                if (guardedInput[i] > rootsStack[stackPointer])
                {
                    //We have new root and all elements smaller than this will form its left sub-tree
                    while (guardedInput[i] > rootsStack[stackPointer - 1])
                    {
                        //We need to merge the two top items on the stack (by computing new height)
                        heightsStack[stackPointer - 1] = Math.Max(heightsStack[stackPointer - 1], heightsStack[stackPointer] + 1);
                        //End we need to pop one from the stuck
                        stackPointer--;
                    }
                    
                    //We put the new root to the stack
                    rootsStack[stackPointer] = guardedInput[i];
                    heightsStack[stackPointer]++;
                }
                //Otherwise current element is a leaf
                else
                {
                    stackPointer++;
                    rootsStack[stackPointer] = guardedInput[i];
                    heightsStack[stackPointer] = 0;
                }
            }
            
            maximumSequenceLength = heightsStack[stackPointer];
        }
        
        //Return the result
        return maximumSequenceLength;
    }
}