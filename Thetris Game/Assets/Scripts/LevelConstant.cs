using System.Collections;
using System.Collections.Generic;

public static class LevelConstant
{
    public static readonly int[,] scoreForLineAmount = new int[4, 2]
    {
        {0, 40 }, {1, 100}, {2 ,300}, {3 , 1200}
    };

    public static readonly int[,] frameForLevel = new int[30,2] {
        { 0, 48 } , { 1, 43}, { 2 ,38 }, {3 , 33} , {4 ,28}, {5 , 23}, {6, 18}, {7 ,13},
        { 8, 8 }, {9 , 6}, { 10 , 5}, {11 , 5}, {12 , 5} ,{13 , 4}, {14 , 4},{15 , 4 },
        { 16, 3} ,{17 , 3} ,{18 , 3} ,{19 , 2},{20 , 2}, {21 , 2}, {22 , 2}, {23 , 2}, {24 , 2} ,
        {25 , 2}, {26 , 2} ,{27 , 2} ,{28 , 2}, {29 , 1} 
    };

    public static int getScoreAmount(int level, int lineAmount)
    {
        int score = scoreForLineAmount[lineAmount, 1] * (level + 1);
        return score;
    }

    public static int getFrameAmount(int level)
    {
        return frameForLevel[level, 1];
    }
}
