using System;

//Tau 2012 (based on Codility Blog --> http://blog.codility.com/2012/07/tau-2012-codility-programming.html)
class Solution
{
    private int[][] Transpose(int[][] C, int M, int N)
    {
        int[][] transposedC = new int[N][];

        for (int i = 0; i < N; i++)
        {
            transposedC[i] = new int[M];
            for (int j = 0; j < M; j++)
            {
                transposedC[i][j] = C[j][i];
            }
        }

        return transposedC;
    }

    public int solution(int[][] C)
    {
        //This is where result will be stored
        int maximumProfit = 0;

        if (C != null && C.Length > 0 && C.Length <= 100 && C[0] != null && C[0].Length > 0 && C[0].Length <= 100)
        {
            int M = C.Length;
            int N = C[0].Length;

            if (M > N)
            {
                C = Transpose(C, M, N);
                M = C.Length;
                N = C[0].Length;
            }

            //The value of precalcualtedProfits[i][j] will be profit of a rectangle [0..i-1]x[0..j-1]
            int[][] precalcualtedProfits = new int[M + 1][];
            precalcualtedProfits[0] = new int[N + 1];
            for (int i = 1; i < M + 1; i++)
            {
                precalcualtedProfits[i] = new int[N + 1];
                for (int j = 1; j < N + 1; j++)
                    precalcualtedProfits[i][j] = precalcualtedProfits[i - 1][j] + precalcualtedProfits[i][j - 1] - precalcualtedProfits[i - 1][j - 1] + C[i - 1][j - 1];
            }

            for (int i = 0; i < M; i++)
            {
                for (int k = i + 1; k < M + 1; k++)
                {
                    int minimumInnerRectangleProfit  = 0;                   //Minimum profit of a rectangle [i..k-1]x[0..j-1]
                    int minimumInnerRectangleRow = 0;                       //Maximum such j
                    int maximumMinimumInnerRectangleRemainderProfit = 0;    //Maximum profit of a rectangle [i..k-1]x[j'..j-1]

                    int maximumInnerRectangleProfit  = 0;                   //Maximum profit of a rectangle [i..k-1]x[0..j-1]
                    int maximumInnerRectangleRow = 0;                       //Minimum such j
                    int minimumMaximumInnerRectangleRemainderProfit = 0;    //Minimum profit of a rectangle [i..k-1]x[j'..j-1]
        
                    int minimumOuterRectangleProfit = 0;                    //Minimum profit of a rectangle [0..i-1,k..M-1]x[0..j-1]
                    int minimumOuterRectangleRow = 0;                       //Maximum such j
                    int maximumMinimumOuterRectangleReminderProfit = 0;     //Maximum profit of a rectangle [0..i-1,k..M-1]x[j'..j-1]
            
                    int maximumOuterRectangleProfit = 0;                    //Maximum profit of a rectangle [0..i-1,k..M-1]x[0..j-1]
                    int maximumOuterRectangleRow = 0;                       //Minimum such j
                    int minimumMaximumOuterRectangleReminderProfit = 0;     //Minimum profit of a rectangle [0..i-1,k..M-1]x[j'..j-1]

                    int innerRectangleProfit = 0, outerRectangleProfit = 0;
                    for (int j = 1; j < N + 1; j++)
                    {
                        innerRectangleProfit = precalcualtedProfits[k][j] - precalcualtedProfits[i][j];
                        outerRectangleProfit = precalcualtedProfits[M][j] - innerRectangleProfit;

                        if (innerRectangleProfit <= minimumInnerRectangleProfit)
                        {
                            minimumInnerRectangleProfit  = innerRectangleProfit;
                            minimumInnerRectangleRow = j;
                        }

                        if (innerRectangleProfit - minimumInnerRectangleProfit > maximumMinimumInnerRectangleRemainderProfit)
                            maximumMinimumInnerRectangleRemainderProfit = innerRectangleProfit - minimumInnerRectangleProfit;
            
                        if (innerRectangleProfit > maximumInnerRectangleProfit)
                        {
                            maximumInnerRectangleProfit  = innerRectangleProfit;
                            maximumInnerRectangleRow = j;
                        }

                        if (innerRectangleProfit - maximumInnerRectangleProfit < minimumMaximumInnerRectangleRemainderProfit)
                            minimumMaximumInnerRectangleRemainderProfit = innerRectangleProfit - maximumInnerRectangleProfit;



                        if (outerRectangleProfit <= minimumOuterRectangleProfit)
                        {
                            minimumOuterRectangleProfit = outerRectangleProfit;
                            minimumOuterRectangleRow = j;
                        }

                        if (outerRectangleProfit - minimumOuterRectangleProfit > maximumMinimumOuterRectangleReminderProfit)
                            maximumMinimumOuterRectangleReminderProfit = outerRectangleProfit - minimumOuterRectangleProfit;
                
                        if (outerRectangleProfit > maximumOuterRectangleProfit)
                        {
                            maximumOuterRectangleProfit  = outerRectangleProfit;
                            maximumOuterRectangleRow = j;
                        }

                        if (outerRectangleProfit - maximumOuterRectangleProfit < minimumMaximumOuterRectangleReminderProfit)
                            minimumMaximumOuterRectangleReminderProfit = outerRectangleProfit - maximumOuterRectangleProfit;
                    }
                    maximumProfit = Math.Max(maximumProfit, Math.Max(Math.Max(maximumMinimumInnerRectangleRemainderProfit, maximumMinimumOuterRectangleReminderProfit), Math.Max(innerRectangleProfit - minimumMaximumInnerRectangleRemainderProfit, outerRectangleProfit - minimumMaximumOuterRectangleReminderProfit)));
                }
            }
        }

        //Return the result
        return maximumProfit;
    }
}