using System;

//Nu 2011 (based on Codility Blog --> http://blog.codility.com/2012/01/nu-2011-certificate-solution.html)
class Solution
{
    //Algorithm of the five(s) (http://en.wikipedia.org/wiki/Median_of_medians) adjusted to special conditions of this task
    private int AlgorithmOfTheFive(int[] A, int[] B, int P, int Q, int R, int S)
    {
        int sequenceMedian = 0;

        //We pre calculate the length of the target sequence
        int sequenceLength = (Q - P + 1) + (S - R + 1);

        if (sequenceLength <= 5)
        {
            //We will be dealing only with  sequences of odd length, so we know at which index the median will be
            int sequenceMedianIndex = sequenceLength / 2;
            
            //We go only through first half of the new sequence
            for (int sequenceIndex = 0; sequenceIndex <= sequenceMedianIndex; sequenceIndex++)
            {
                //We take element from either A or B depending on their values (this way we can keep the target sequence sorted)
                if (P > Q || (R <= S && B[R] < A[P]))
                {
                    sequenceMedian = B[R];
                    R++;
                }
                else
                {
                    sequenceMedian = A[P];
                    P++;
                }
            }
        }
        else
        {
            int numberOfElementsToDrop = sequenceLength / 4;
            if (A[P + numberOfElementsToDrop] < B [S - numberOfElementsToDrop])
                sequenceMedian = AlgorithmOfTheFive(A, B, P + numberOfElementsToDrop, Q, R, S - numberOfElementsToDrop);
            else
                sequenceMedian = AlgorithmOfTheFive(A, B, P, Q - numberOfElementsToDrop, R + numberOfElementsToDrop, S);
        }

        return sequenceMedian;
    }

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
                int leftSequenceLength = Q[I] - P[I] + 1;
                int rightSequenceLength = S[I] - R[I] + 1;
                //We want to eliminate leftSequenceLength - rightSequenceLength - 1 elements from the first sequence, or rightSequenceLength - leftSequenceLength - 1 from second sequence
                int numberOfElementsToTruncate = Math.Abs(leftSequenceLength - rightSequenceLength) / 2;

                if (leftSequenceLength > rightSequenceLength)
                {
                    if (A[P[I] + numberOfElementsToTruncate] >= B[S[I]])
                        sequencesMedians[I] = A[P[I] + numberOfElementsToTruncate];
                    else if (A[Q[I] - numberOfElementsToTruncate] <= B[R[I]])
                        sequencesMedians[I] = A[Q[I] - numberOfElementsToTruncate];
                    else
                        sequencesMedians[I] = AlgorithmOfTheFive(A, B, P[I] + numberOfElementsToTruncate, Q[I] - numberOfElementsToTruncate, R[I], S[I]);
                }
                else
                {
                    if (B[R[I] + numberOfElementsToTruncate] >= A[Q[I]])
                        sequencesMedians[I] = B[R[I] + numberOfElementsToTruncate];
                    else if (B[S[I] - numberOfElementsToTruncate] <= A[P[I]])
                        sequencesMedians[I] = B[S[I] - numberOfElementsToTruncate];
                    else
                        sequencesMedians[I] = AlgorithmOfTheFive(A, B, P[I], Q[I], R[I] + numberOfElementsToTruncate, S[I] - numberOfElementsToTruncate);
                }

            }
            Array.Sort(sequencesMedians);
            sequencesMediansMedian = sequencesMedians[K / 2];
        }

        //Return the result
        return sequencesMediansMedian;
    }
}