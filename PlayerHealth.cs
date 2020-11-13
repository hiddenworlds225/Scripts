using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health;
        public float shield;

        public void Start()
        {
            Mathf.Clamp(health, 0, 100);
            Mathf.Clamp(shield, 0, 100);
        }

        public void Damaged(float damaged)
        {
            if (shield > 0)
            {
                if (shield < damaged)
                {
                    damaged -= shield;
                    shield = 0;
                    health -= damaged;

                }
            }
            
            health -= damaged;
        }

        private void Death()
        {
            var rb = GetComponent<Rigidbody>();
            var wd = GetComponent<WeaponData>();

        }

        private void Update()
        {
            if(health < 0) Death();
        }
    }
}