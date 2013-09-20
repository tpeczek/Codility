using System;

//Lambda 2011 (based on Codility solution --> http://blog.codility.com/2011/11/lambda-2011-certificate-solution.html)
class Solution
{
    public int solution(int[] T)
    {
        //This is where result will be stored
        int lowestPeripheralityRouter = -1;

        //Sanity check
        if (T != null && T.Length > 0)
        {
            int N = T.Length;

            //We will treat the routers network as a tree, first we calculate a degree of every node
            int[] routersDegrees = new int[N];
            for (int i = 0; i < N; i++)
            {
                if (T[i] != i)
                    routersDegrees[T[i]]++;
            }

            //Now we will order the nodes from leaves to root
            int routersOrderingIndex = 0;
            int[] routersOrdering = new int[N];

            //Nodes with degree 0 are leaves
            for (int i = 0; i < N; i++)
            {
                if (routersDegrees[i] == 0)
                {
                    routersOrdering[routersOrderingIndex++] = i;
                }
            }

            //When all descends of node has been order the node itself can be ordered
            for (int i = 0; i < N; i++)
            {
                int parentRouter = T[routersOrdering[i]];
                routersDegrees[parentRouter]--;
                if (routersDegrees[parentRouter] == 0)
                {
                    routersOrdering[routersOrderingIndex++] = parentRouter;
                }
            }

            int[] routersSubtreesLengths = new int[N];
            for (int i = 0; i < N; i++)
                routersSubtreesLengths[i] = 1;

            int[] routersTotalPathsLengths = new int[N];

            //First we treat every node as root and caculate the size of the subtree with this route and the total length of paths going down from this route
            for (int i = 0; i < N; i++)
            {
                int rootRouterDescendant = routersOrdering[i];
                int rootRouter = T[rootRouterDescendant];
                routersSubtreesLengths[rootRouter] = routersSubtreesLengths[rootRouter] + routersSubtreesLengths[rootRouterDescendant];
                routersTotalPathsLengths[rootRouter] = routersTotalPathsLengths[rootRouter] + routersTotalPathsLengths[rootRouterDescendant] + routersSubtreesLengths[rootRouterDescendant];
            }

            //Now we need to increase the total path lengths with paths going up from this route
            for (int i = N - 2; i >= 0; i--)
            {
                int rootRouter = routersOrdering[i];
                int rootRouterParent = T[rootRouter];
                routersTotalPathsLengths[rootRouter] = routersTotalPathsLengths[rootRouterParent] - routersSubtreesLengths[rootRouter] + N - routersSubtreesLengths[rootRouter];
            }

            //Now we just need to find the shortest path
            lowestPeripheralityRouter = 0;
            for (int i = 0; i < N; i++)
            {
                if (routersTotalPathsLengths[i] < routersTotalPathsLengths[lowestPeripheralityRouter])
                    lowestPeripheralityRouter = i;
            }
        }

        //Return the result
        return lowestPeripheralityRouter;
    }
}