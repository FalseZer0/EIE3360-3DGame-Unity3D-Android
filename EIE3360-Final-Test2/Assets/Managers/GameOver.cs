using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text CapText;

    private void OnEnable()
    {
        float time = PlayerHealth.TimePassed; 
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        int best = PlayerPrefs.GetInt("BestTime", 0);
        if (time > best)
        {
            PlayerPrefs.SetInt("BestTime", (int)time);
        }
        int bests = PlayerPrefs.GetInt("BestTime", 0);
        int minutesB = (int)bests / 60;
        int secondsB = (int)bests % 60;
        string message="";
        if (minutes < 1) message += "“It doesn’t matter how slow you go as long as you don’t stop.” — Confucius";
        else if (minutes ==1) message += "“You must do the thing you think you cannot do.” — Eleanor Roosevelt. Try again";
        else if(minutes>=2) message += "WOW! “In the middle of difficulty lies opportunity.” — Albert Einstein";
        message += $"\n Best score is <color=#E7834F>{minutesB}</color>  minutes and <color=#E7834F>{secondsB}</color> seconds"; 

        CapText.text =
            $"You held up for <color=#E7834F>{minutes}</color> minutes <color=#E7834F>{seconds}</color> seconds.\n{message}";
    }

}
