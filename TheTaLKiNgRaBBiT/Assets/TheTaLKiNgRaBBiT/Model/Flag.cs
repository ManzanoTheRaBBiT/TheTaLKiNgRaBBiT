using System.Collections;
using System.Collections.Generic;

/**
 * @author TheRaBBiT
 */

/**
 * The Flag class works as a container for every variable on your game that you want to save
 * Easy to set and to get, the purpose is easily read a choice made by the player that you consider important
 * Using with integer values, you can read the value of the specific dialogue and update your game with it's value
 * Read the flag, and call a different scene, or set a different tree on the Trigger class based on the choices made by the player
 */
public static class Flag {

    public static Dictionary<string , int> FlagsLib = new Dictionary<string, int>();

    public static void SetFlag(string flag , int answer){
		FlagsLib[flag] = answer;
    }

    public static int GetFlag(string flag){
        if(!FlagsLib.ContainsKey(flag)){
            return 0;
        }
        return FlagsLib[flag];
    }

}
