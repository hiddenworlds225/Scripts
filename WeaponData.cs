using System;
using Player;
using Enemy;
using UnityEngine;
using UnityEngine.InputSystem;
using Universal;

public class WeaponData : MonoBehaviour
{
    public HudController hudController;
    public AmmoHold ammoHold;

    private PlayerInteractions _inputs;

    private Transform _be;

    public bool equipped;

    private bool _preint;

    public enum EquipmentType
    {
        Weapon,
        Equipment
    }

    public enum AmmoType
    {
        Rifle,
        Pistol,
        Shotgun,
        Sniper,
        None
    }

    public enum WeaponBehavior
    {
        Auto,
        Semi,
        Bolt,
        None
    }

    public enum EquipmentBehavior
    {
        Grenade,
        Ied,
        Tactical,
        Other,
        None
    }

    public EquipmentType equipmentType;
    public WeaponBehavior weaponBehavior;
    public EquipmentBehavior equipmentBehavior;
    public AmmoType ammoType;

    public bool canStack;
    public int damage;
    public int maxCapacity;
    public int currentCapacity;

    public float reloadTime;
    public float rateOfFire;
    public float velocity;

    private float _timeToFire;
    private float _nextTimeToFire;

    public GameObject bullet;

    private bool _autoFire;

    private void Awake()
    {
        if (transform.parent != null && transform.parent.GetComponent<EnemyWeaponSystem>() != null)
        {
            Debug.Log("This is an AI!");
        }

        if (transform.parent != null && transform.parent.GetComponent<WeaponFunctions>())
        {
            _inputs = new PlayerInteractions();
        }
    }

    private void Start()
    {
        _be = transform.GetChild(0);

        Physics.IgnoreLayerCollision(9, 10);

        _timeToFire = 1 / (rateOfFire / 60);


        if (!equipped) return;
        
        ammoHold = transform.GetComponentInParent<AmmoHold>();
        
        if (transform.parent != null && transform.parent.GetComponent<WeaponFunctions>())
        {
            hudController = GameObject.FindWithTag("Player").GetComponent<HudController>();

            hudController.weaponData = this;

            switch (ammoType)
            {
                case AmmoType.Pistol:
                    hudController.ammoHold = ammoHold.pistolAmmo;
                    hudController.currentAmmo = currentCapacity;
                    hudController.index = 2;
                    break;
                case AmmoType.Rifle:
                    hudController.ammoHold = ammoHold.rifleAmmo;
                    hudController.currentAmmo = currentCapacity;
                    hudController.index = 1;
                    break;
                case AmmoType.Shotgun:
                    hudController.ammoHold = ammoHold.shotgunAmmo;
                    hudController.index = 0;
                    break;
                case AmmoType.Sniper:
                    hudController.ammoHold = ammoHold.sniperAmmo;
                    break;
                case AmmoType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        _preint = true;
    }


    private void Update()
    {
        if (currentCapacity > 1) Shoot();

        if (transform.parent == null || transform.parent.GetComponent<WeaponFunctions>() == null) return;
        if (_inputs.Player.Reload.triggered)
        {
            StartCoroutine(ammoHold.Reload(this, reloadTime));
        }

        if (hudController != null)
        {
            UpdateUI();
        }

        if (currentCapacity < 1)
        {
            StartCoroutine(ammoHold.Reload(this, reloadTime));
        }
    }

    private void Shoot()
    {
        if (weaponBehavior == WeaponBehavior.Auto && Time.time >= _nextTimeToFire && _autoFire)
        {
            Fire();
        }

        if (weaponBehavior == WeaponBehavior.Semi && _inputs.Player.Fire.triggered)
        {
            Fire();
        }
    }

    private void Fire()
    {
        var transform1 = _be.transform;
        var copiedBullet = Instantiate(bullet, transform1.position, transform1.rotation);

        var bulletData = copiedBullet.GetComponent<BulletData>();
        bulletData.velocity = velocity;
        bulletData.damage = damage;

        currentCapacity -= 1;

        _nextTimeToFire = Time.time + _timeToFire;
    }


    public void UpdateUI()
    {
        hudController.currentAmmo = currentCapacity;

        switch (ammoType)
        {
            case AmmoType.Pistol:
                hudController.ammoHold = ammoHold.pistolAmmo;
                hudController.index = 2;
                break;
            case AmmoType.Rifle:
                hudController.ammoHold = ammoHold.rifleAmmo;
                hudController.index = 1;
                break;
            case AmmoType.Shotgun:
                hudController.ammoHold = ammoHold.shotgunAmmo;
                hudController.index = 0;
                break;
            case AmmoType.Sniper:
                hudController.ammoHold = ammoHold.sniperAmmo;
                break;
            case AmmoType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDestroy()
    {
        _preint = false;
        if (!equipped && hudController != null) return;
        hudController = null;
    }

    private void OnEnable()
    {
        if (transform.parent == null || transform.parent.GetComponent<EnemyWeaponSystem>() != null) return;
        if (equipped) _inputs.Enable();

        _inputs.Player.Fire.started += ctx => _autoFire = true;
        _inputs.Player.Fire.canceled += ctx => _autoFire = false;

        if (!equipped && hudController != null || !_preint) return;
        hudController = GameObject.FindWithTag("Player").GetComponent<HudController>();
        hudController.weaponData = this;
        UpdateUI();
    }

    private void OnDisable()
    {
        if (transform.parent == null || transform.parent.GetComponent<WeaponFunctions>() == null) return;
        _inputs.Disable();
        if (!equipped && hudController != null) return;
        hudController = null;
    }
}