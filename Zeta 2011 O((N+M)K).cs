using System;

//Zeta 2011
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

            //For every ball
            for (int i = 1; i <= K; i++)
            {
                //We start from left top corner of the board
                int switchRowIndex = 0;
                int switchColumnIndex = 0;

                //With ball rolling to the bottom
                int ballDirection = _bottomDirection;

                //Until the ball will not exit the board
                while (switchRowIndex < N && switchColumnIndex < M)
                {
                    int switchDirection = A[switchRowIndex][switchColumnIndex];
                    //If the current ball switch doesn't have neutral direction
                    if (switchDirection != _neutralDirection)
                    {
                        //We change the direction of the ball based on switch value
                        ballDirection = switchDirection;
                        //And negate this value
                        A[switchRowIndex][switchColumnIndex] = (-1) * switchDirection;
                    }

                    //Based on the direction of the ball we move it to the next switch
                    if (ballDirection == _bottomDirection)
                        switchRowIndex++;
                    else
                        switchColumnIndex++;
                }

                //If the ball has exited the board through bottom edge of the bottom-right switch
                if (switchRowIndex == N && switchColumnIndex == M - 1)
                    //We increase the result
                    result++;
            }
        }

        return result;
    }
}