using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInteractions _inputs;

        //Unity items
        private Camera _cam;
        private Rigidbody _rb;
        private CapsuleCollider _capsule;
        public LayerMask layer;

        //vars
        private float _currentCameraRotation;
        private float _currentSpeed;
        public float speed;
        public float speedMultiplier;
        public float sensitivity;
        public float jumpForce;

        public float cameraLookLimit;

        private bool _isRunning;

        private void Awake()
        {
            _inputs = new PlayerInteractions();
        }

        private void Start()
        {
            Physics.IgnoreLayerCollision(8, 9);
            _cam = GetComponentInChildren<Camera>();
            _rb = GetComponent<Rigidbody>();
            _capsule = GetComponent<CapsuleCollider>();
            Cursor.lockState = CursorLockMode.Locked;


        }

        private void Update()
        {
            Look();
            Run();
            PlayerJump();
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {

            var vec = _inputs.Player.Movement.ReadValue<Vector2>();
        
            var transform1 = transform;
            var moveForward = transform1.forward * (vec.y * _currentSpeed);
            var moveRight = transform1.right * (vec.x * _currentSpeed);

            _rb.MovePosition(transform1.position + (moveForward + moveRight) * Time.deltaTime);
        }

        private void Look()
        {
            var mouseInput = _inputs.Player.Look.ReadValue<Vector2>();

            // rotate on x axis mouse
            var rotation = new Vector3(0f, mouseInput.x, 0f) * sensitivity;
            _rb.MoveRotation(_rb.rotation * Quaternion.Euler(rotation));
            if (_cam == null) return;
            //rotate on y axis mouse
            var camRotation = mouseInput.y * sensitivity;
            _currentCameraRotation -= camRotation;
            _currentCameraRotation = Mathf.Clamp(_currentCameraRotation, -cameraLookLimit, cameraLookLimit);


            _cam.transform.localEulerAngles = new Vector3(_currentCameraRotation, 0f, 0f);
        }

        private void PlayerJump()
        {
            if (!_inputs.Player.Jump.triggered) return;
            if (Grounded())
            {
                _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void Run()
        {
            if (_inputs.Player.Run.triggered)
            {

                _isRunning = !_isRunning;

            }

            if (_isRunning)
            {
                if (speedMultiplier != 0 && speedMultiplier > 0 && _inputs.Player.Movement.ReadValue<Vector2>().y > 0)
                {
                    _currentSpeed = speed * speedMultiplier;
                }
                else
                {
                    _currentSpeed = speed;
                }


            }
            else
            {
                _currentSpeed = speed;
            }
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
        private void OnEnable()
        {
            _inputs.Enable();
        }

        /*private void ExitApp()
    {
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        }
        else
        {
            Application.Quit();
        }
    }*/

        private bool Grounded()
        {
            var bounds = _capsule.bounds;
            return Physics.CheckCapsule(bounds.center, new Vector3(bounds.center.x, bounds.min.y, bounds.center.z), _capsule.radius * .9f, layer);
        }
    }
}