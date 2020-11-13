using UnityEngine;

namespace Freelook
{
    public class CamFreeLook : MonoBehaviour
    {
        private PlayerInteractions _input;

        public float speed;
        public float sensitivity;

        private float _camX;
        private float _camY;

        public float minMaxYLook;
        
        
        private void Awake()
        {
            _input = new PlayerInteractions();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }
        void Update()
        {
            Move();
            Look();
        }

        void Move()
        {
            var vec = _input.Player.Movement.ReadValue<Vector2>();

            var transform1 = transform;
            var position = transform1.position;
            position += (transform1.forward * vec.y * speed) * Time.deltaTime;
            position += (transform1.right * vec.x * speed) * Time.deltaTime;
            transform1.position = position;
        }

        void Look()
        {
            var xAngle = _input.Player.Look.ReadValue<Vector2>().y * sensitivity;
            var yAngle = _input.Player.Look.ReadValue<Vector2>().x * sensitivity;
            _camX -= xAngle;
            _camY += yAngle;
            _camX = Mathf.Clamp(_camX, -minMaxYLook, minMaxYLook);
            transform.localEulerAngles = new Vector3(_camX, _camY, 0f);
        }
        
        
    }
}
