using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettings : MonoBehaviour
{
    public enum difficulties {Rookie, Easy, Normal, Hard};
    public difficulties difficulty = difficulties.Rookie;
    public Text difficultyLabel;

    public void SetDifficulty()
    {
        if (difficulty == difficulties.Rookie) {
            PlayerPrefs.SetString("Difficulty", "Rookie");
            difficultyLabel.text = "Difficulty: Rookie";
        }
        else if (difficulty == difficulties.Easy) {
            PlayerPrefs.SetString("Difficulty", "Easy");
            difficultyLabel.text = "Difficulty: Easy";
        }
        else if (difficulty == difficulties.Normal) {
            PlayerPrefs.SetString("Difficulty", "Normal");
            difficultyLabel.text = "Difficulty: Normal";
        }
        else if (difficulty == difficulties.Hard) {
            PlayerPrefs.SetString("Difficulty", "Hard");
            difficultyLabel.text = "Difficulty: Hard";
        }
    }
}
