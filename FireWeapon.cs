using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 1f;
    public Camera cam;
    public KeyCode shoot = KeyCode.Mouse0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {

        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

        if (hit.transform.tag == "Enemy")
        {
            hit.collider.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
        }
    }
}
