using _Game.Logic.MetaService.FirebaseService;
using Zenject;

namespace _Game.Logic.Infrastructure.EntryPoints
{
    public class EntryPointProject : IInitializable
    {
        private readonly IFirebaseServiceAnalytics _firebaseService;

        public EntryPointProject(IFirebaseServiceAnalytics firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void Initialize()
        {
            _firebaseService.TrackStartGame();
        }
    }
}