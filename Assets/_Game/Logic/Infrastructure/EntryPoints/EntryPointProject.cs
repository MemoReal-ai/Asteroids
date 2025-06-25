using _Game.Logic.MetaService.FirebaseService;
using Zenject;

namespace _Game.Logic.Infrastructure.EntryPoints
{
    public class EntryPointProject : IInitializable
    {
        private readonly IServiceAnalytics _firebaseService;

        public EntryPointProject(IServiceAnalytics firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void Initialize()
        {
            _firebaseService.TrackStartGame();
        }
    }
}