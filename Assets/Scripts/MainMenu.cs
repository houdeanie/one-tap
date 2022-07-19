using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Houdini.OneTap
{
    public class MainMenu : MonoBehaviour
    {
        public Launcher launcher;
        /*public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/

        private void Start()
        {
            pauseMenu.GameIsPaused = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void JoinMatch()
        {
            Debug.Log("Joining");
            new WaitForSeconds(2f);
            launcher.Join();
        }

        public void CreateGame()
        {
            Debug.Log("Creating");
            new WaitForSeconds(2f);
            launcher.Create();
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            new WaitForSeconds(2f);
            Application.Quit();
        }
    }

}
