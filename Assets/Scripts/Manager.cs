using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Photon.Pun;

namespace Com.Houdini.OneTap
{
    public class PlayInfo
    {
        //public PlayerData profile;
        public int actor;
        public short kills;
        public short deaths;

        /*public Playerinfo (ProfileData p, int a, short k, short d)
        {
            this.profile = p;
            this.actor = a;
            this.kills = k;
            this.deaths = d;
        }*/
    }
    public class Manager : MonoBehaviour
    {
        public string player_prefab;
        public Transform[] spawn_points;
        // Start is called before the first frame update
        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            Transform t_spawn = spawn_points[Random.Range(0, spawn_points.Length)];
            PhotonNetwork.Instantiate(player_prefab, t_spawn.position, t_spawn.rotation);
        }
    }
}

