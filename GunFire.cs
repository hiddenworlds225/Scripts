using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunFire : MonoBehaviour
{
    //Objects
    public GameObject prefab;
    private Animator anim;
    public Transform spawn;

    //Floats for spawn
    public float Damage;
    public float velocity;
    public float timeToSpawn;
    private float currentTime;

    //Ammo Reserves
    public float magazineHold;
    public float currentAmmo;
    public float reserveCapacity;
    public float currentReserve;

    //sound
    public AudioClip fire;
    private TMP_Text ammoCount;
    private AudioSource gunSoundFire;

    //keycodes
    private KeyCode shoot = KeyCode.Mouse0;
   // private KeyCode aim = KeyCode.Mouse1;
    private KeyCode reload = KeyCode.R;

    //Reload
    public bool empty;
    public bool canShoot;
    public float reloadTime = 1f;
    public bool canRelaod;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ammoCount = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<TMP_Text>();
        gunSoundFire = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        checkAmmo();


        ammoCount.SetText(currentAmmo + " / " + currentReserve);
        currentTime += Time.deltaTime;
        if (currentTime >= timeToSpawn && Input.GetKeyDown(shoot) && canShoot)
        {
            if (currentAmmo <= 1)
            {
                Shoot(true);
            }
            else
            {
                Shoot(false);
            }

        }

        if (currentAmmo <= 0 && canRelaod || Input.GetKeyDown(reload) && canRelaod)
        {
            canShoot = false;
            anim.SetBool("Empty", true);
            StartCoroutine(Reload());

            
        }



    }

    void Shoot(bool setEmpty)
    {
        if (setEmpty)
        {
            anim.SetTrigger("FireToEmpty");
            anim.SetBool("Empty", true);
        }
        else
        {
            anim.SetTrigger("Fired");
        }
        gunSoundFire.PlayOneShot(fire);
        var dev = Instantiate(prefab, spawn.transform.position, spawn.transform.rotation);

        var Bullet = dev.GetComponent<Bullet_Data>();

        Bullet.damage = Damage;
        Bullet.velocity = velocity;
        currentAmmo--;
        dev = null;
        currentTime = 0;
    }

    IEnumerator Reload()
    {
        anim.SetTrigger("Reload");



        float tempAmmo = magazineHold - currentAmmo;
        currentAmmo += tempAmmo;
        currentReserve -= tempAmmo;
        canShoot = true;
        yield return new WaitForSeconds(reloadTime);
    }

    void checkAmmo()
    {
        if (currentAmmo > 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        if(currentReserve > 0)
        {
            canRelaod = true;
        }
        else
        {
            canRelaod = false;
        }
    }


}
