
namespace _Game.Firebase
{
    public interface IServiceAnalytics
    {
        void InvokeStartGame();
        void InvokeStats(string dataStatsSDK);
        void InvokeLaserShoot();
    }
}
