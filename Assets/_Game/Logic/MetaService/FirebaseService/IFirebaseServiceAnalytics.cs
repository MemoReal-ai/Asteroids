namespace _Game.Logic.MetaService.FirebaseService
{
    public interface IFirebaseServiceAnalytics
    {
        void TrackStartGame();
        void TrackStatsAfterLose(string dataStatsSDK);
        void TrackLaserShoot();
    }
}
