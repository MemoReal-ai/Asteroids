
namespace _Game.Gameplay.Logic.Infrastructure
{
    public class EnemyDiedSignal
    {
        public int Reward { get; private set; }

        public EnemyDiedSignal(int reward)
        {
            Reward = reward;
        }
    }
}