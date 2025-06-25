namespace _Game.Logic.MetaService.FirebaseService
{
    public interface IServiceAnalytics
    {
        void TrackStartGame();
        void TrackStatsAfterLose(string dataStatsSDK);
        void TrackLaserShoot();
    }
}
