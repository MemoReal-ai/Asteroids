using System;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace _Game.Gameplay.Logic.Service
{
    public class SceneHandler
    {
        public event Action OnSceneRestart;
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            OnSceneRestart?.Invoke();
        }

        public void SceneTransition(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void SceneTransition(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
   
    }
}
