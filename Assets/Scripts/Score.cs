/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {

    public static void Start() {
        //ResetHighscore();
        if (Bird.GetInstance() != null)
        {
            Bird.GetInstance().OnDied += Bird_OnDied;
        }
    }

    private static void Bird_OnDied(object sender, System.EventArgs e) {
        TrySetNewHighscore(Level.GetInstance().GetPipesPassedCount());
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("highscore");
    }

    public static string GetName()
    {
        return PlayerPrefs.GetString("name");
    }

    public static bool TrySetNewHighscore(int score) {
        int currentHighscore = GetHighscore();
        if (score > currentHighscore) {
            // New Highscore
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            return true;
        } else {
            return false;
        }
    }

    public static bool TrySetNewName(string name)
    {
        int currentHighscore = GetHighscore();
        if (name != GetName())
        {
            // New Highscore
            PlayerPrefs.SetString("name", name);
            PlayerPrefs.SetString("debug", PlayerPrefs.GetString("debug") + " /" + name + "," + currentHighscore);
            Debug.Log(PlayerPrefs.GetString("debug"));
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ResetHighscore() {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetString("name", "");
        PlayerPrefs.SetString("debug", "debug:");
        PlayerPrefs.Save();
    }
}
