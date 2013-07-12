using System;

//Beta 2010
class Solution {
    public int solution(int[] A) {
        //This is where result will be stored
        int intersectingDiscs = 0;

        //Sanity check
        if (A != null && A.Length > 0)
        {
            int N = A.Length;

            //We will store information on how many discs starts and ends in every point
            int[] discsStarts = new int[N];
            int[] discsEnds = new int[N];

            //Calculating how many discs starts and ends in every point
            int farthestPossibleDiscsEnd = N - 1;
            for (int i = 0; i < A.Length; i++)
            {
                //If the disc is starting before the 0 point we will treat it as starting in 0 point
                discsStarts[i >= A[i] ? i - A[i] : 0]++;

                //If the disc is ending after the N - 1 point we will treat it as ending in N - 1 point.
                int discEnd = i + A[i];
                //The discEnd < 0 check is for arithmetic overflow prevention
                discsEnds[(discEnd < 0 || discEnd >= N) ? farthestPossibleDiscsEnd : discEnd]++;
            }

            //This will keep track of discs that have started before the current point and will end in this point or after it
            int containingDiscs = 0;
            for (int i = 0; i < N; i++)
            {
                //We increase the number of intersecting discs by:
                //- the number of discs that have been started but not ended before current point multiplied by the number of discs starting at current point (all of them intersect with each other)
                intersectingDiscs += containingDiscs * discsStarts[i];
                //- the number of discs starting at current point multiplied by number of discs starting at current point minus one and this divided by 2 (every disc starting at current point is intersecting with all the others discs starting at current point and we need to avoid counting double intersections)
                intersectingDiscs += (discsStarts[i] * (discsStarts[i] - 1))/2;

                //If the number of intersecting discs is above 10,000,000 we should return -1
                if (intersectingDiscs > 10000000)
                    return -1;

                //We adjust the number of discs started before the current by adding the number of discs starting in current point and substracting the number of discs ending in current point
                //This way we keep the number of discs starting before or in current point which haven't end yet
                containingDiscs += discsStarts[i] - discsEnds[i];
            }
        }

        //Return the result
        return intersectingDiscs;
    }
}