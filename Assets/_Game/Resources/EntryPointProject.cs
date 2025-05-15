using System;
using _Game.Firebase;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class EntryPointProject : IInitializable
    {
        private readonly IServiceAnalytics _service;

        public EntryPointProject(IServiceAnalytics service)
        {
            _service = service;
        }

        public void Initialize()
        {
            _service.InvokeStartGame();
        }
        
    }
}