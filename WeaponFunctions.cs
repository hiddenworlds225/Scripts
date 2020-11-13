using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class WeaponFunctions : MonoBehaviour
    {
        public bool noWeapons = true;
        public int currentWeapon;
        public bool equipmentSwitch;
        public bool oneWeapon;
        public GameObject[] weapons;

        public GameObject[] equipment;

        private PlayerInteractions _input;
        private int _previousWeapon;
        private float _yScroll;


        private void Awake()
        {
            _input = new PlayerInteractions();
        }

        private void Start()
        {
            equipment = new GameObject[2];
            weapons = new GameObject[2];
        }

        private void Update()
        {
            _yScroll = _input.Player.SwitchEquipment.ReadValue<Vector2>().y;

            if (_yScroll != 0) SwitchEquipment(_yScroll);

            if (Keyboard.current.bKey.wasPressedThisFrame) DropCurrentWeapon();
        }

        private void SwitchEquipment(float y)
        {
            switch (y)
            {
                case 120f:
                    currentWeapon--;
                    SwitchItem(-1);
                    break;
                case -120f:
                    currentWeapon++;
                    SwitchItem(1);
                    break;
            }
        }

        private void SwitchItem(int sv)
        {
            if (noWeapons || oneWeapon) return;


            //disable all items
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }

            //double check
            for (var n = 0; n < 2; n++)
            {
                //invert switch if over values
                if (currentWeapon > 1)
                {
                    currentWeapon = 0;
                    equipmentSwitch = !equipmentSwitch;
                }

                if (currentWeapon < 0)
                {
                    currentWeapon = 1;
                    equipmentSwitch = !equipmentSwitch;
                }

                switch (equipmentSwitch)
                {
                    case true:

                        for (var i = 0; i < weapons.Length; i++)
                        {
                            if (weapons[i] != null && i == currentWeapon)
                            {
                                weapons[i].SetActive(true);
                            }

                            if (weapons[i] == null && i == currentWeapon)
                            {
                                currentWeapon += sv;
                            }
                        }

                        break;

                    case false:
                        for (var i = 0; i < equipment.Length; i++)
                        {
                            if (equipment[i] != null && i == currentWeapon)
                            {
                                equipment[i].SetActive(true);
                            }

                            if (equipment[i] == null && i == currentWeapon)
                            {
                                currentWeapon += sv;
                            }
                        }


                        break;
                }
            }
        }

        public void EquipGroundWeapon(Transform @object)
        {
            if (@object.GetComponent<WeaponData>() == null)
            {
                Debug.Log("Marked as equipment, but there's no weapon data!");
                return;
            }


            var wd = @object.GetComponent<WeaponData>();

            if (weapons[0] != null && weapons[1] != null && wd.equipmentType == WeaponData.EquipmentType.Weapon) return;
            if (equipment[0] != null && equipment[1] != null &&
                wd.equipmentType == WeaponData.EquipmentType.Equipment) return;
            wd.equipped = true;
            var transform1 = transform;
            var item = Instantiate(@object, transform1.position, transform1.rotation);
            item.name = item.name.Remove(item.name.IndexOf("(", StringComparison.Ordinal));

            noWeapons = false;
            item.transform.parent = transform;
            AddRemoveEquipment(item.gameObject, "Add");
            Destroy(@object.gameObject);
        }

        private void DropCurrentWeapon()
        {
            var transform1 = transform;
            if (noWeapons) return;


            switch (equipmentSwitch)
            {
                case true:
                    var wd = weapons[currentWeapon].transform.GetComponent<WeaponData>();
                    wd.equipped = false;
                    var droppedWeapon = Instantiate(weapons[currentWeapon], transform1.position, transform1.rotation);
                    droppedWeapon.name =
                        droppedWeapon.name.Remove(droppedWeapon.name.IndexOf("(", StringComparison.Ordinal));
                    AddRemoveEquipment(droppedWeapon, "Remove");
                    break;
                case false:
                    var wd2 = equipment[currentWeapon].transform.GetComponent<WeaponData>();
                    wd2.equipped = false;
                    var droppedEquipment =
                        Instantiate(equipment[currentWeapon], transform1.position, transform1.rotation);
                    droppedEquipment.name =
                        droppedEquipment.name.Remove(droppedEquipment.name.IndexOf("(", StringComparison.Ordinal));
                    AddRemoveEquipment(droppedEquipment, "Remove");
                    break;
            }
        }

        private void AddRemoveEquipment(GameObject item, string ar)
        {
            var wd = item.GetComponent<WeaponData>();
            var rb = item.GetComponent<Rigidbody>();
            var col = item.GetComponent<Collider>();

            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(false);
            }

            oneWeapon = transform.childCount == 1;


            switch (ar)
            {
                case "Add":
                {
                    switch (wd.equipmentType)
                    {
                        case WeaponData.EquipmentType.Weapon:
                        {
                            if (weapons[1] != null && weapons[2] != null) return;

                            equipmentSwitch = true;

                            var i = 0;
                            foreach (var wp in weapons)
                            {
                                if (wp == null)
                                {
                                    weapons[i] = item.gameObject;
                                    item.SetActive(true);
                                    currentWeapon = i;
                                    break;
                                }

                                i++;
                            }

                            break;
                        }
                        case WeaponData.EquipmentType.Equipment:
                        {
                            if (equipment[1] != null && equipment[2] != null) return;
                            equipmentSwitch = false;

                            var i = 0;
                            foreach (var eq in equipment)
                            {
                                if (eq == null)
                                {
                                    equipment[i] = item.gameObject;
                                    item.SetActive(true);
                                    currentWeapon = i;
                                    break;
                                }

                                i++;
                            }

                            break;
                        }
                    }
                }

                    SwitchItem(currentWeapon);
                    col.enabled = false;
                    rb.isKinematic = true;
                    break;
                case "Remove":

                    switch (wd.equipmentType)
                    {
                        case WeaponData.EquipmentType.Weapon:
                        {
                            Destroy(weapons[currentWeapon]);
                            weapons[currentWeapon] = null;

                            break;
                        }
                        case WeaponData.EquipmentType.Equipment:
                        {
                            Destroy(equipment[currentWeapon]);
                            equipment[currentWeapon] = null;

                            break;
                        }
                    }

                    rb.isKinematic = false;
                    col.enabled = true;
                    currentWeapon++;
                    SwitchItem(1);
                    break;
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