using System;
using _Game.Firebase;
using Zenject;

namespace _Game.MainMenu.Logic.Infrastructure
{
    public class EntryPointProject : IInitializable
    {
        private readonly IServiceAnalitics _service;

        public EntryPointProject(IServiceAnalitics service)
        {
            _service = service;
        }

        public void Initialize()
        {
            _service.InvokeStartGame();
        }
        
    }
}