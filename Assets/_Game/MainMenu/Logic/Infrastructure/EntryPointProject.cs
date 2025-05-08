using System;
using _Game.Firebase;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class EntryPointProject : IInitializable
    {
        private readonly IServiceSDK _service;

        public EntryPointProject(IServiceSDK service)
        {
            _service = service;
        }

        public void Initialize()
        {
            _service.InvokeStartGame();
        }
        
    }
}