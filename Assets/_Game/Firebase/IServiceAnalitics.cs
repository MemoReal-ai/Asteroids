
namespace _Game.Firebase
{
    public interface IServiceAnalitics
    {
        void InvokeStartGame();
        void InvokeStats(string dataStatsSDK);
        void InvokeLaserShoot();
    }
}
