using TMPro;
using UnityEngine;

namespace _Game.MainMenu.Logic.UI
{
    public class ViewScore : MonoBehaviour
    {
        public TextMeshProUGUI LastSessionScore;
        public TextMeshProUGUI HighScore;


        public void SetScoreLastSession(string score)
        {
            LastSessionScore.text = " Last session score " + score;
        }

        public void SetHighScore(string highScore)
        {
            HighScore.text = " High score " + highScore;
        }
    }
}