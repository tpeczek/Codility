using System;

//Eta 2011
class Solution
{
    public int solution(int[] A)
    {
        //With the incremental construction or some other methods you can prove that graphs build according to the rules in task can have only three Hamiltonian paths
        int hamiltonianRoutesCount = 3;

        int N = A.Length;
        int M = (N / 2) + 1;

        //We will store the number of town occurences in the entire route
        int[] townOccurancesInRoute = new int[M];
        //We will store the number of how many times the road has been taken (we will look at roads from "left to right")
        int[,] townRoadsTaken = new int[M, 3];

        //We need to check if any of the rules has been violeted
        for (int i = 0; i < N && hamiltonianRoutesCount == 3; i++)
        {
            townOccurancesInRoute[A[i]]++;
            int roadBeginningTown = A[i];
            int roadDestinationTown = A[(i + 1) % N];

            //Each road must connect distinct towns
            if (roadBeginningTown == roadDestinationTown)
                hamiltonianRoutesCount = -2;
            //We always want to look at road from "letf to right"
            else
            {
                if (roadBeginningTown > roadDestinationTown)
                {
                    roadBeginningTown = roadDestinationTown;
                    roadDestinationTown = A[i];
                }

                for (int j = 0; j < 3; j++)
                {
                    //If the town doesn't have the road defined at its j exit
                    if (townRoadsTaken[roadBeginningTown, j] == 0)
                        //We mark a new road from this town to destination town as visited once
                        townRoadsTaken[roadBeginningTown, j] = roadDestinationTown;
                    //If the road defined at towns j exit is the road to the destination town and it has been visited once
                    else if (townRoadsTaken[roadBeginningTown, j] == roadDestinationTown)
                        //We mark a road from this town to destination town as visited twice (by negating the destination town id)
                        townRoadsTaken[roadBeginningTown, j] = -roadDestinationTown;
                    //If the road defined at towns j exit is the road to the destination town and it has been visited twice
                    else if (townRoadsTaken[roadBeginningTown, j] == -roadDestinationTown)
                    {
                        //We have an error, each road must be taken exactly twice
                        hamiltonianRoutesCount = -2;
                        break;
                    }
                }
            }
        }

        //If everything has been good so far we need to do some more checking
        if (hamiltonianRoutesCount == 3)
        {
            for (int i = 0; i < M; i++)
            {
                //Each town must be visited either exactly once or exactly thrice
                if (townOccurancesInRoute[i] != 1 && townOccurancesInRoute[i] != 3)
                {
                    hamiltonianRoutesCount = -2;
                    break;
                }

                //Each road must be taken exactly twice
                for (int j = 0; j < 3; j++)
                {
                    if (townRoadsTaken[i, j] > 0)
                    {
                        hamiltonianRoutesCount = -2;
                        break;
                    }
                }
            }
        }

        //Return the result
        return hamiltonianRoutesCount;
    }
}