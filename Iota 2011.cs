using System;
using System.Collections.Generic;

//Iota 2011
class Solution
{
    public int solution(int[] A)
    {
        //This is where result will be stored
        int shortestAdjacentSequenceLength = 0;

        //Sanity check
        if (A != null && A.Length > 0)
        {
            int N = A.Length;
            int firstElement = A[0];
            int lastElement = A[N - 1];

            //First possible edge case - first element is the same as last (also a single element situation)
            if (firstElement == lastElement)
                shortestAdjacentSequenceLength = 1;
            else
            {
                //Second possible edge case - array containing two different elements (could go through the main algorithm but why waste time)
                shortestAdjacentSequenceLength = 2;
                if (N > 2)
                {
                    //All adjacent elements of given element
                    Dictionary<int, HashSet<int>> adjacentElements = new Dictionary<int, HashSet<int>> { { firstElement, new HashSet<int> { A[1] } }, { lastElement, new HashSet<int> { A[N - 2] } } };
                    //Has the given element been already added to any sequence
                    Dictionary<int, bool> elementConsideredForSequence = new Dictionary<int, bool> { { firstElement, true }, { lastElement, false } };
                    for (int i = 1; i < N - 1; i++)
                    {
                        if (!adjacentElements.ContainsKey(A[i]))
                        {
                            adjacentElements.Add(A[i], new HashSet<int>());
                            elementConsideredForSequence.Add(A[i], false);
                        }
                        adjacentElements[A[i]].Add(A[i - 1]);
                        adjacentElements[A[i]].Add(A[i + 1]);
                    }

                    //We will keep candidates for our sequence separated between current and next ones
                    List<int> currentAdjacentSequencesCandidates = new List<int> { firstElement };
                    List<int> nextAdjacentSequencesCandidates = new List<int>();

                    //We will go until we reach the last element
                    while (!elementConsideredForSequence[lastElement])
                    {
                        //If we have run out of the candidates in "current" collection we need to move to the "next" element and examine candidates on that position
                        if (currentAdjacentSequencesCandidates.Count == 0)
                        {
                            shortestAdjacentSequenceLength++;
                            List<int> tempDdjacentSequencesCandidates = currentAdjacentSequencesCandidates;
                            currentAdjacentSequencesCandidates = nextAdjacentSequencesCandidates;
                            nextAdjacentSequencesCandidates = tempDdjacentSequencesCandidates;
                        }

                        //We take the candidate out ot the "current" collection
                        int currentCandidate = currentAdjacentSequencesCandidates[0];
                        currentAdjacentSequencesCandidates.RemoveAt(0);

                        //We go through all adjacent elements of current candidate
                        foreach (int possibleCandidate in adjacentElements[currentCandidate])
                        {
                            //If this element hasn't been considered for any sequence yet
                            if (!elementConsideredForSequence[possibleCandidate])
                            {
                                //We add it as one of next possible candidates
                                elementConsideredForSequence[possibleCandidate] = true;
                                nextAdjacentSequencesCandidates.Add(possibleCandidate);
                            }
                        }
                    }
                }
            }
        }

        //Return the result
        return shortestAdjacentSequenceLength;
    }
}
