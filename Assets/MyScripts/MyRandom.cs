using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class MyRandom
{
    private static int excludeLastRandNum;
    private static bool firstRun = true;

    public static int RandomWithExclusion(int min, int max)
    {
        int result;
        //Don't exclude if this is first run.
        if (firstRun)
        {
            //Generate normal random number
            result = UnityEngine.Random.Range(min, max);
            excludeLastRandNum = result;
            firstRun = false;
            return result;
        }

        //Not first run, exclude last random number with -1 on the max
        result = UnityEngine.Random.Range(min, max - 1);
        //Apply +1 to the result to cancel out that -1 depending on the if statement
        result = (result < excludeLastRandNum) ? result : result + 1;
        excludeLastRandNum = result;
        return result;
    }
}