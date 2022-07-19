using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Com.Houdini.OneTap
{
    public class pauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        private bool disconnecting = false;
        public GameObject pauseMenuUI;
        public GameObject pauseMenuButton;

        public void Resume()
        {
            if (disconnecting) return;
            pauseMenuUI.SetActive(false);
            pauseMenuButton.SetActive(true);
            //Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            if (disconnecting) return;
            pauseMenuUI.SetActive(true);
            pauseMenuButton.SetActive(false);
            // Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void loadMenu()
        {
            disconnecting = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Menu");
        }

        public void quitGame()
        {
            disconnecting = true;
            PhotonNetwork.LeaveRoom();
            Debug.Log("Quitting...");
            Application.Quit();
        }
    }

}