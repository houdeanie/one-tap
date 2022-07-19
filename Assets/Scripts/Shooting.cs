using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Com.Houdini.OneTap
{
    public class Shooting : MonoBehaviourPunCallbacks
    {
        public AudioClip gunshotsound;
        public float pitchRandom;
        public AudioSource sfx;
        public GameObject bulletHolePrefab;
        public LayerMask canbeshot;
        public float coolDownTime = 2f;
        public int damage = 10;

        private Transform UIreload;

        private GameObject shootButton;

        private float currentCoolDown = 0f;

        void Start()
        {
            currentCoolDown = 0f;
            if (photonView.IsMine)
            {
                UIreload = GameObject.Find("HUD/ShootButton/Reload").transform;

                shootButton = GameObject.Find("HUD/ShootButton");
                Button buttondeets = shootButton.GetComponent<Button>();
                shootButton.GetComponent<Button>().onClick.AddListener(NoParamaterOnclick);
            }

        }

        void Update()
        {
            if (!photonView.IsMine) return;
            
            if (currentCoolDown > 0)
            {
                refreshReload();
                currentCoolDown -= Time.deltaTime;
            }
        }

        private void NoParamaterOnclick()
        {
            Debug.Log("Button clicked with no parameters");
            if (!pauseMenu.GameIsPaused)
            {
                if (currentCoolDown <= 0)
                {
                    photonView.RPC("Shoot", RpcTarget.All);
                }
            }
        }
        void refreshReload()
        {
            float t_reload_ratio = (float)currentCoolDown / (float)coolDownTime;
            UIreload.localScale = new Vector3(t_reload_ratio, 1, 1);
        }

        [PunRPC]
        void Shoot()
        {
            if (!pauseMenu.GameIsPaused)
            {
                currentCoolDown = coolDownTime;
                Transform t_camera = transform.Find("Camera");

                // sounds
                sfx.Stop();
                sfx.clip = gunshotsound;
                sfx.pitch = 1 - pitchRandom + Random.Range(-0.2f, 0.2f);
                sfx.Play();
                RaycastHit t_hit = new RaycastHit();
                if (Physics.Raycast(t_camera.position, t_camera.forward, out t_hit, 1000f, canbeshot))
                {
                    GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                    t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                    Destroy(t_newHole, 5f);

                    if (photonView.IsMine)
                    {
                        if (t_hit.collider.gameObject.layer == 10)
                        {
                            t_hit.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, damage);
                        }
                    }
                }
            }


        }

        [PunRPC]
        private void TakeDamage(int p_damage)
        {
            GetComponent<PlayerController>().TakeDamage(p_damage);
        }
    }
}


