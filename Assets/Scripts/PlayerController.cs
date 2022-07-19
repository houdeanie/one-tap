using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


namespace Com.Houdini.OneTap
{
    //[RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
    {
        Rigidbody rb;

        [SerializeField]
        private float speed = 20f;
        [SerializeField]
        private float lookSensitivity = 1f;
        [SerializeField]
        private Camera cam;
        public int max_health = 1;
        public GameObject cameraParent;
        public GameObject weapon;
        private Manager manager;
        private int current_Health;

        private GameObject leftMButton;
        private GameObject rightMButton;
        private GameObject fMButton;
        private GameObject bMButton;

        private GameObject leftLButton;
        private GameObject rightLButton;
        private GameObject upLButton;
        private GameObject downLButton;

        private Text ui_username;

        [HideInInspector] public ProfileData playerProfile;
        public TextMeshPro playerUsername;

        /*bool moveLeft = false;
        bool moveRight = false;
        bool moveForward = false;
        bool moveBackward = false;
        
        bool lookLeft = false;
        bool lookRight = false;
        bool lookUp = false;
        bool lookDown = false;*/
        float horizontalMove = 0;
        float verticalMove = 0;
        float horizontalLook = 0;
        float verticalLook = 0;

        private Vector3 moveVelocity = Vector3.zero;
        private Vector3 rotateVector = Vector3.zero;
        private Vector3 rotateCameraVector = Vector3.zero;
        // private PlayerMotor motor;

        private float aimAangle;

        public void OnPhotonSerializeView(PhotonStream p_stream, PhotonMessageInfo p_message)
        {
            if (p_stream.IsWriting)
            {
                p_stream.SendNext((int)(weapon.transform.localEulerAngles.x*100f));
            }
            else
            {
                aimAangle = (int)p_stream.ReceiveNext()/ 100f;

            }
        }

        void Start()
        {
            current_Health = max_health;
            manager = GameObject.Find("Manager").GetComponent<Manager>();
            cameraParent.SetActive(photonView.IsMine);
            if (!photonView.IsMine)
            {
                gameObject.layer = 10;
            }

            if (photonView.IsMine)
            {
                ui_username = GameObject.Find("HUD/PlayerInfoDisplay/Text").GetComponent<Text>();
                ui_username.text = Launcher.myProfile.username;
                photonView.RPC("SyncProfile", RpcTarget.All, Launcher.myProfile.username, Launcher.myProfile.level, Launcher.myProfile.xp);
            }
            rb = GetComponent<Rigidbody>();




            /*if (photonView.IsMine)
            {
                leftMButton = GameObject.Find("HUD/Movement/LeftButton");
                leftMButton.GetComponent<Button>().onClick.AddListener(PointerDownLeft);
                rightMButton = GameObject.Find("HUD/Movement/RightButton");
                rightMButton.GetComponent<Button>().onClick.AddListener(PointerDownRight);
                fMButton = GameObject.Find("HUD/Movement/ForwardButton");
                fMButton.GetComponent<Button>().onClick.AddListener(PointerDownForward);
                bMButton = GameObject.Find("HUD/Movement/BackwardButton");
                bMButton.GetComponent<Button>().onClick.AddListener(PointerDownBackward);

                leftLButton = GameObject.Find("HUD/View/LeftButtonV");
                leftLButton.GetComponent<Button>().onClick.AddListener(PointerDownLLeft);
                rightLButton = GameObject.Find("HUD/View/RightButtonV");
                rightLButton.GetComponent<Button>().onClick.AddListener(PointerDownLRight);
                upLButton = GameObject.Find("HUD/View/UpButton");
                upLButton.GetComponent<Button>().onClick.AddListener(PointerDownLUp);
                downLButton = GameObject.Find("HUD/View/DownButton");
                downLButton.GetComponent<Button>().onClick.AddListener(PointerDownLDown);
            }*/
        }


        private void Update()
        {
            if (!photonView.IsMine)
            {
                RefreshMultiplayerState();
                return;
            }
            //Debug.Log();
            // calc 3d vector movement
            /*float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * xMove;
            Vector3 moveVertical = transform.forward * zMove;

            // final movement vector
            Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

            // apply movement
            Move(velocity);

            // calc rotation as 3d vector, for turning around
            float yRotate = Input.GetAxisRaw("Mouse X");

            Vector3 rotation = new Vector3(0f, yRotate, 0f) * lookSensitivity;

            // apply rotation
            Rotate(rotation);

            // calc camera rotation as 3d vector, for turning around
            float xRotate = Input.GetAxisRaw("Mouse Y");

            Vector3 cameraRotation = new Vector3(xRotate, 0f, 0f) * lookSensitivity;

            // apply camera rotation
            RotateCamera(cameraRotation);*/
            if (!pauseMenu.GameIsPaused)
            {
                Movement();
                CameraLook();



                Vector3 moveHorizontal = transform.right * horizontalMove;
                Vector3 moveVertical = transform.forward * verticalMove;
                Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
                Move(velocity);

                Vector3 rotation = new Vector3(0f, horizontalLook, 0f) * lookSensitivity;
                Vector3 cameraRotation = new Vector3(verticalLook, 0f, 0f) * lookSensitivity;
                Rotate(rotation);
                RotateCamera(cameraRotation);

            }


            // testing damage
            if (Input.GetKeyDown(KeyCode.U))
            {
                TakeDamage(500);
            }
        }

        void Movement()
        {
            if (MLeft.moveLeft)
            {
                horizontalMove = -speed;
            }
            else if (MRight.moveRight)
            {
                horizontalMove = speed;
            }
            else
            {
                horizontalMove = 0;
            }

            if (MFor.moveForward)
            {
                verticalMove = speed;
            }
            else if (MBack.moveBackwards)
            {
                verticalMove = -speed;

            }
            else
            {
                verticalMove = 0;
            }
        }

        void CameraLook()
        {
            if (LLeft.lookLeft)
            {
                horizontalLook = -1;
            }
            else if (LRight.lookRight)
            {
                horizontalLook = 1;
            }
            else
            {
                horizontalLook = 0;
            }

            if (LUp.lookUp)
            {
                verticalLook = 1;
            }
            else if (LDown.lookDown)
            {
                verticalLook = -1;
            }
            else
            {
                verticalLook = 0;
            }
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine) return;
            if (!pauseMenu.GameIsPaused)
            {
                PerformRotation();
                PerformMovement();
                /*Debug.Log("testing horizontal then vetical move");
                Debug.Log(horizontalMove);
                Debug.Log(verticalMove);*/
                rb.velocity = new Vector3(horizontalMove * Time.deltaTime, rb.velocity.y, verticalMove * Time.deltaTime);

                /*Debug.Log("testing horizontal then vetical lookssssssss");

                Debug.Log(horizontalLook);
                Debug.Log(verticalLook);*/
            }
        }

        public void Move(Vector3 velocity)
        {
            moveVelocity = velocity;
        }

        public void Rotate(Vector3 rotation)
        {
            rotateVector = rotation;
        }

        public void RotateCamera(Vector3 rotating)
        {
            rotateCameraVector = rotating;
        }

        // perform rotation
        void PerformRotation()
        {
            if (cam != null)
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateVector));
                cam.transform.Rotate(-rotateCameraVector);
                weapon.transform.Rotate(-rotateCameraVector);
            }
        }

        void PerformMovement()
        {
            if (moveVelocity != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
            }
        }

        //[PunRPC]
        public void TakeDamage(int p_damage)
        {
            if (photonView.IsMine)
            {
                current_Health -= p_damage;
                Debug.Log(current_Health);
                if (current_Health < 0)
                {
                    manager.Spawn();
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }

        // refreshed the enemy multiplayer steps by syncing their vertical rotation
        private void RefreshMultiplayerState()
        {
            // make sure the enemy username tag is always oriented to our direction
            // float myRotation = ;

            // playerUsername.text = Launcher.myProfile.username;

            float cacheCurrentAimAngle = weapon.transform.localEulerAngles.y;
            Quaternion targetRotation = Quaternion.identity * Quaternion.AngleAxis(aimAangle, Vector3.right);
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, Time.deltaTime * 8f);
            Vector3 finalRotation = weapon.transform.localEulerAngles;
            finalRotation.y = cacheCurrentAimAngle;
            weapon.transform.localEulerAngles = finalRotation;
        }

        [PunRPC]
        private void SyncProfile(string p_username, int p_level, int p_xp)
        {
            playerProfile = new ProfileData(p_username, p_level, p_xp);
            playerUsername.text = playerProfile.username;
        }
    }
}
