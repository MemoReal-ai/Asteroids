using System;

namespace _Game.Gameplay.Logic.Service
{
    [Serializable]
    public class Data
    {
        public int CurrentScore;
        public int HightScore;


        public void ChangeScore()
        {
            if (CurrentScore > HightScore)
            {
                HightScore = CurrentScore;
                
            }
        }
    }
}
