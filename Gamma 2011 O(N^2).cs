using System;

//Gamma 2011
class Solution {
    public int solution(string S) {
        //This is where result will be stored
        int palindromicSlicesCount = 0;

        //Sanity check
        if (!String.IsNullOrEmpty(S))
        {
            //In order to be able to handle palindromes of odd and even lengths in the same way, we insert special character (#) between letters.
            //i.g. 'baababa' becomes 'b#a#a#b#a#b#a'
            S = String.Join("#", Array.ConvertAll(S.ToCharArray(), c => c.ToString()));
            int N = S.Length;
            
            //For every potential palindorme center
            for (int i = 1; i < N - 1; i++)
            {
                //Expand the palindrome starting by first no '#' character and steping by 2
                for (int j = 2 - i%2; i - j >= 0 && i + j < N; j = j + 2)
                {
                    //If characters are the same
                    if (S[i - j] == S[i + j])
                    {
                        //Increase the number of palindromic slices (if it is above 100,000,000 we should return -1)
                        if (++palindromicSlicesCount > 100000000)
                            return -1;
                    }
                    else
                        break;
                }
            }
        }

        //Return the result
        return palindromicSlicesCount;
    }
}
