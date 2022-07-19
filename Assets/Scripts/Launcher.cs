using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Com.Houdini.OneTap
{
    [System.Serializable]
    public class ProfileData
    {
        public string username;
        public int level;
        public int xp;

        public ProfileData()
        {
            this.username = "";
            this.level = 1;
            this.xp = 0;
        }

        public ProfileData(string u, int l, int x)
        {
            this.username = u;
            this.level = l;
            this.xp = x;
        }
    }

    public class Launcher : MonoBehaviourPunCallbacks
    {
        public InputField usernameField;
        public static ProfileData myProfile = new ProfileData();

        public void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            myProfile = Data.LoadProfile();
            if (!string.IsNullOrEmpty(myProfile.username))
            {
                usernameField.text = myProfile.username;
            }
            Connect();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected");
            //Join();
            PhotonNetwork.JoinLobby();
            base.OnConnectedToMaster();
            //base.OnConnected();
        }

        public override void OnJoinedRoom()
        {
            StartGame();
            base.OnJoinedRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Create();
            base.OnJoinRandomFailed(returnCode, message);
        }
        public void Connect()
        {
            Debug.Log("Trying to connect");
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Join()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void Create()
        {
            PhotonNetwork.CreateRoom("");
        }

        public void StartGame()
        {
            if (string.IsNullOrEmpty(usernameField.text))
            {
                myProfile.username = "GUEST_" + Random.Range(1000, 9999);
            }
            else
            {
                myProfile.username = usernameField.text;
            }
            
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                //Data.SaveProfile(myProfile);
                PhotonNetwork.LoadLevel(1);
            }
        }
    }

}
