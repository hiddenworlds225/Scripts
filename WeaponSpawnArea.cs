using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEquipment
{
    public class WeaponSpawnArea : MonoBehaviour
    {
        private GameObject[] _weapons;

        void Start()
        {
            _weapons = Resources.LoadAll<GameObject>("Weapons");

            Transform transform1;
            var obj = Instantiate(_weapons[Random.Range(0, _weapons.Length)], (transform1 = transform).position, transform1.rotation
                                                                                                                 * Quaternion.Euler(90, 0, 0));

            obj.transform.parent = null;
            obj.transform.name = obj.name.Remove(obj.name.IndexOf("(", StringComparison.Ordinal));
        }

        // Update is called once per frame
        void Update()
        {
            //print(Random.Range(0,_weapons.Length));
        }
    }
}