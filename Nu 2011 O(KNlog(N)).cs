using System;

//Nu 2011
class Solution
{
    public int solution(int[] A, int[] B, int[] P, int[] Q, int[] R, int[] S)
    {
        //This is where result will be stored
        int sequencesMediansMedian = 0;

        //Sanity check
        if (A != null && A.Length > 0 && B != null && B.Length > 0 && P != null && Q != null && R != null && S != null && P.Length == Q.Length && P.Length == R.Length && P.Length == S.Length && P.Length != 0)
        {
            int N = A.Length;
            int M = B.Length;
            int K = P.Length;

            int[] sequencesMedians = new int[K];
            for (int I = 0; I < K; I++)
            {
                //We will be merging the sequence by adjusting indexes
                int leftParentIndex = P[I];
                int rightParentIndex = R[I];

                //We pre calculate the length of the target sequence
                int sequenceLength = (Q[I] - P[I] + 1) + (S[I] - R[I] + 1);
                //Because the task is dealing only with  sequences of odd length, we know at which index the median will be
                int sequenceMedianIndex = sequenceLength / 2;
                
                //We go only through first half of the new sequence
                for (int sequenceIndex = 0; sequenceIndex <= sequenceMedianIndex; sequenceIndex++)
                {
                    //We take element from either A or B depending on their values (this way we can keep the target sequence sorted)
                    if (leftParentIndex > Q[I] || (rightParentIndex <= S[I] && B[rightParentIndex] < A[leftParentIndex]))
                    {
                        sequencesMedians[I] = B[rightParentIndex];
                        rightParentIndex++;
                    }
                    else
                    {
                        sequencesMedians[I] = A[leftParentIndex];
                        leftParentIndex++;
                    }
                }
            }
            
            //We sort the sequence created from medians
            Array.Sort(sequencesMedians);
            //And get our result
            sequencesMediansMedian = sequencesMedians[K / 2];
        }

        //Return the result
        return sequencesMediansMedian;
    }
}

