using System;

//Gamma 2011 (based on Manacher’s Algorithm --> http://leetcode.com/2011/11/longest-palindromic-substring-part-ii.html)
class Solution {
    public int solution(string S) {
        //This is where result will be stored
        int palindromicSlicesCount = 0;

        //Sanity check
        if (!String.IsNullOrEmpty(S))
        {
            //In order to be able to handle palindromes of odd and even lengths in the same way, we insert special character (#) between letters as well as delimiters.
            //i.g. 'baababa' becomes '^#b#a#a#b#a#b#a#$'
            S = "^#" + String.Join("#", Array.ConvertAll(S.ToCharArray(), c => c.ToString())) + "#$";
            int N = S.Length;

            //This is where the length of the longest palindrome centered at given index will be stored
            int[] P = new int[N];
            //This is where the index of current palindrome center will be stored
            int C = 0;
            //This is where the index of right edge of the palindrome most to the right will be stored
            int R = 0;

            //For every character index in the string
            for (int i = 1; i < N - 1; i++)
            {
                //We need to find the mirrored index i’ around the palindrome’s center C
                int iprime = C - (i - C);

                //If the palindrome most to the right reaches further then current index
                if (R > i)
                    //The initial palindrome length is calculated as minimum of 'distance between current index and the index of right edge of the palindrome most to the right' and 'the lenght of the longest palindrome centered at i’'
                    P[i] = Math.Min(R-i, P[iprime]);
                else
                    P[i] = 0;

                //Now we try to expand palindrome beyond the initial length
                while (S[i + 1 + P[i]] == S[i - 1 - P[i]])
                    P[i]++;

                //We can get the count of plaindormes around current index by dividing the lenght of the longest one by 2 (fixed point division)
                palindromicSlicesCount += P[i]/2;
                //If the number of palindromic slices is above 100,000,000 we should return -1
                if (palindromicSlicesCount > 100000000)
                {
                    palindromicSlicesCount = -1;
                    break;
                }

                //If the palindrome expands beyond R
                if (i + P[i] > R)
                {
                    //We need to adjust C and R
                    C = i;
                    R = i + P[i];
                }
            }
        }

        //Return the result
        return palindromicSlicesCount;
    }
}