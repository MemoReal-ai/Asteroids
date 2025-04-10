using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace _Game.Gameplay.Logic.Service
{
    public class SceneHandler
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
