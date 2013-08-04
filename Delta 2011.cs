using System;

//Delta 2011 (based on 'Partition with Duplicate Elements' section from following paper http://www.cs.cornell.edu/~wdtseng/icpc/notes/dp3.pdf)
class Solution {
    public int solution(int[] A) {
        //This is where result will be stored
        int result = 0;

        //Sanity check
        if (A != null && A.Length > 0)
        {
            int N = A.Length;


            //First we need a total sum of all elements
            //As we have possibility of multiplying by -1 we will make all values absolute
            int totalSum = 0;
            for(int i = 0;i < N;i++)
            {
                A[i] = Math.Abs(A[i]);
                totalSum += A[i];
            }
            //We will be looking for a subset that adds up to half of the total sum
            //If the total sum is odd the half is rounded down (the best possible solution is 1 instead of 0)
            int halfTotalSum = totalSum / 2;

            //We create an array that will hold information about sums of possible subsets
            //In another words subsetSums[x] will be true if and only if there is a subset that has sum x
            //We are only interested in sums that are equal or less than half of the total sum but in order to avoid checking the sum in the inner loop we add another 100 bools (100 is the maximum value allowed)
            //We also set subsetSums[0] to true – this sum can be always achieved by taking an empty set.
            bool[] subsetSums = new bool[halfTotalSum + 101];
            subsetSums[0] = true;

            //We will keep the location of the rightmost true entry in subsetSums (this is in fact the closest subset sum to half of the total sum), at the beginning this is 0
            int closestSubsetSum = 0;
            //Sorting the array will make highest possible subset sum grow as slowly as possible
            Array.Sort(A);

            int subsequentValueOccurrences = 1;
            //For every value in array
            for(int i = 0;i < N;i++)
            {
                //If the value is 0 we can skip it (doesn't change the sum)
                if(A[i] == 0)
                    continue;
          
                //We can optimize by counting the subsequent value occurrences, skipping the internal loop and multiplying by that number later
                if(i < N-1 && A[i] == A[i+1] )
                {
                    subsequentValueOccurrences++;
                    continue;
                }

                //We start at closest subset sum so far and move left
                for (int j = closestSubsetSum; j >= 0; j--)
                {
                    //We look for the subset sums we have already calculated
                    if(subsetSums[j])
                    {
                        //Furthest subset sum is the value of current subset sum plus current value multiplied by its occurrences
                        int furthestSubsetSum = j + A[i] * subsequentValueOccurrences;
                        //The furthest subset sum can't be higher than the half of the total sum
                        furthestSubsetSum = Math.Min(halfTotalSum, furthestSubsetSum);

                        //We mark all subset sums from current to furthest with value as step, if any of the sums along the way has already been marked we break (we have already been here in one of previous iterations)
                        for(int k = j + A[i];k <= furthestSubsetSum && !subsetSums[k]; k += A[i])
                        {
                            subsetSums[k]=true;
                        }
                    }
                }

                //New closest subset sum is the value of current closest subset sum plus current value multiplied by its occurrences
                closestSubsetSum = closestSubsetSum + A[i] * subsequentValueOccurrences;

                //If the closest subset sum is higher than the half of the total sum
                if (closestSubsetSum > halfTotalSum)
                {
                    closestSubsetSum = halfTotalSum;
                    //We move back to closest subset sum in the scope
                    while(!subsetSums[closestSubsetSum])
                        closestSubsetSum--;
                }

                //If the closest subset sum is equal to the half of the total sum
                if(closestSubsetSum == halfTotalSum)
                    //We have found the optimal solution
                    break;

                subsequentValueOccurrences = 1;
            }

            //Final result is equal to total sum minus two times the closest subset sum
            result = totalSum - 2 * closestSubsetSum;
        }

        //Return the result
        return result;
    }
}