
namespace _Game.Firebase
{
    public interface IServiceSDK
    {
        void InvokeStartGame();
        void InvokeStats(string dataStatsSDK);
        void InvokeLaserShoot();
    }
}
