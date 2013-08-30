using System; 

//Theta 2011
class Solution
{
    public int solution(int[] D, int[] P, int T)
    {
        //This is where result will be stored
        int cheapestRefillStrategyCost = -1;

        //Sanity check
        if (D != null && D.Length > 0 && P != null && P.Length == D.Length && T > 0)
        {
            int N = D.Length;
            cheapestRefillStrategyCost = 0;

            //For every town we will find farthest reachable town (with full tank).
            int[] farthestReachableTowns = new int[N];
            for (int gas = T, currentTown = 0, farthestReachableTown = 0; currentTown < N; currentTown++)
            {
                //We will go through towns till the gas runs out
                while (farthestReachableTown < N && gas >= D[farthestReachableTown])
                    gas -= D[farthestReachableTown++];

                //If we couldn't reach any town, no valid refill strategy exists
                if (farthestReachableTown == currentTown)
                {
                    cheapestRefillStrategyCost = -1;
                    break;
                }
                //Otherwise we mark the farthest reachable town and add enough gas to reach next town, we will look if we can reach any further with this gas from there.
                else
                {
                    farthestReachableTowns[currentTown] = farthestReachableTown;
                    gas += D[currentTown];
                }
            }

            //If valid refill strategy exists
            if (cheapestRefillStrategyCost == 0)
            {
                //For every town we will find next cheaper town
                int[] cheaperTowns = new int[N];
                cheaperTowns[N - 1] = N;

                //We will go back from town one before last
                for (int currentTown = N - 2; currentTown >= 0; currentTown--)
                {
                    //If gas price in next town is lower than the current town
                    if (P[currentTown + 1] < P[currentTown])
                        //We mark the next town as next cheaper gas station
                        cheaperTowns[currentTown] = currentTown + 1;
                    //Otherwise
                    else
                    {
                        //We look for cheaper gas station among the ones we have already found
                        cheaperTowns[currentTown] = cheaperTowns[currentTown + 1];
                        while (cheaperTowns[currentTown] != N && P[cheaperTowns[currentTown]] >= P[currentTown])
                            cheaperTowns[currentTown] = cheaperTowns[cheaperTowns[currentTown]];

                    }
                }

                //Creating refill strategy
                for (int gas = 0, gasRefill = 0, currentTown = 0, nextTown = 0; currentTown < N && cheapestRefillStrategyCost != -2; currentTown = nextTown)
                {
                    //If next cheaper town is not reachable from this town
                    if (cheaperTowns[currentTown] > farthestReachableTowns[currentTown])
                    {
                        //We fill the full tank
                        gasRefill = T - gas;

                        //And move to the next town
                        nextTown = currentTown + 1;
                        gas = T - D[currentTown];
                    }
                    //If next cheaper town is reachable from this town
                    else
                    {
                        //We calculate how much gas we need to reach it
                        int gasNeeded = 0;
                        for (int passedTown = currentTown; passedTown < cheaperTowns[currentTown]; passedTown++)
                            gasNeeded += D[passedTown];

                        //Make the required refill
                        gasRefill = gas < gasNeeded ? gasNeeded - gas : 0;

                        //And move to that town
                        nextTown = cheaperTowns[currentTown];
                        gas += gasRefill - gasNeeded;
                    }

                    if ((ulong)cheapestRefillStrategyCost + (ulong)P[currentTown] * (ulong)gasRefill > 1000000000)
                        cheapestRefillStrategyCost = -2;
                    else
                        cheapestRefillStrategyCost += P[currentTown] * gasRefill;
                }
            }
        }

        return cheapestRefillStrategyCost;
    }
}