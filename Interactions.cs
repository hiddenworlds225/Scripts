using System;
using UnityEngine;

namespace Player
{
    public class Interactions : MonoBehaviour
    {
        private Camera _cam;
        private PlayerInteractions _input;
        private WeaponFunctions _functions;
        private HudController _hud;

        public float interactDistance;

        private void Awake()
        {
            _input = new PlayerInteractions();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _cam = GetComponentInChildren<Camera>();
            _functions = GetComponentInChildren<WeaponFunctions>();
            _hud = transform.GetComponent<HudController>();
        }

        // Update is called once per frame
        void Update()
        {

            RaycastHit hit;
             
            if (!Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, interactDistance)) return;
            
            if (hit.transform.gameObject.CompareTag("Equipment"))
            {
                if (hit.transform.GetComponent<WeaponData>() != null)
                {
                    _hud.interact.text = $"pickup {hit.transform.name}";
                }
                
                if (_input.Player.Interact.triggered) _functions.EquipGroundWeapon(hit.transform);

            }

            if (hit.transform.gameObject.CompareTag("Interact"))
            {
                _hud.interact.text = "Interact";
                if (_input.Player.Interact.triggered) throw new NotImplementedException();

            }


        }

        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }

    }
}