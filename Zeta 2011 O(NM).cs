using System;

//Zeta 2011 (Dynamic Programming)
class Solution
{
    //Directions definitions
    private const int _bottomDirection = -1;
    private const int _rightDirection = 1;
    private const int _neutralDirection = 0;

    public int solution(int[][] A, int K)
    {
        //This is where result will be stored
        int result = 0;
        
        //Sanity check
        if (A != null && A.Length > 0 && K > 0)
        {
            int N = A.Length;
            int M = A[0].Length;

            //We will keep the number of ball exiting the board through bottom edge in every column
            int[] numberOfBallsExitingSwitchBottom = new int[M];

            //First we assume that all the balls exits through first column (as they enter through it)
            numberOfBallsExitingSwitchBottom[0] = K;

            //We will move first through rows than through columns (thanks to that in the end the numberOfBallsExitingBottom will contain numbers for last row)
            for (int switchRowIndex = 0; switchRowIndex < N; switchRowIndex++)
            {
                //We need to keep the number of balls exiting the switch through the right edge
                int numberOfBallsExitingSwitchRight = 0;

                for (int switchColumnIndex = 0; switchColumnIndex < M; switchColumnIndex++)
                {
                    //The number of balls entering current switch is equal to the sum of balls exiting switch through bottom edge in the same column from previous row and the balls exiting switch through right edge in previous column of the same row
                    int numberOfBallsEnteringSwitch = numberOfBallsExitingSwitchBottom[switchColumnIndex] + numberOfBallsExitingSwitchRight;

                    int switchDirection = A[switchRowIndex][switchColumnIndex];
                    //If this switch doesn't change direction of the ball then number of balls exiting through bottom and right edge is already correct (the same as previous switches)
                    if (switchDirection != _neutralDirection)
                    {
                        //If initial direction of this ball switch is "to the right"
                        if (switchDirection == _rightDirection)
                            //Then every even ball entering it will exit through the bottom edge
                            numberOfBallsExitingSwitchBottom[switchColumnIndex] = numberOfBallsEnteringSwitch / 2;
                        //If initial direction of this ball switch is "to the bottom"
                        else
                            //Then every odd ball entering it will exit through the bottom edge
                            numberOfBallsExitingSwitchBottom[switchColumnIndex] = numberOfBallsEnteringSwitch / 2 + numberOfBallsEnteringSwitch % 2;

                        //The number of balls exiting switch through the right edge is equal to number of balls entering minus the balls which have exited through bottom edge
                        numberOfBallsExitingSwitchRight = numberOfBallsEnteringSwitch - numberOfBallsExitingSwitchBottom[switchColumnIndex];
                    }
                }
            }

            //The final result is the number of balls exiting the board trough the bottom edge of the bottom-right switch
            result = numberOfBallsExitingSwitchBottom[M - 1];
        }

        return result;
    }
}