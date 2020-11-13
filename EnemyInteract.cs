using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyInteract : MonoBehaviour
    {
        
        private Transform _vision;
        private LayerMask _layer;

        private EnemyWeaponSystem _ews;
        

        private void Start()
        {
            _ews = GetComponentInChildren<EnemyWeaponSystem>();
            _vision = transform.GetChild(0);
            _layer = LayerMask.GetMask("Interactable");
        }

        public void Interact(float interactDistance)
        {
            Physics.Raycast(_vision.position, _vision.transform.forward, out var hit, interactDistance);

            if (hit.transform == null)
            {
                Debug.Log("Didn't see it");
                Debug.DrawRay(_vision.position, _vision.forward * interactDistance, Color.red, 5f);
                    return;
            }
            
            if (hit.transform.CompareTag("Equipment"))
            {
                _ews.EquipGroundWeapon(hit.transform);
            }
        }
    }
}
