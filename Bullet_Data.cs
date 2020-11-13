using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Data : MonoBehaviour
{
    public float damage;
    public float velocity;
    public float timeToDecay;
    private float decayTime;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        decayTime += Time.deltaTime;
        transform.position += transform.forward * Time.deltaTime * velocity;
        if(decayTime >= timeToDecay)
        {
            Destroy(gameObject);
        }
    }

    //Destroy on contact

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            EnemyHealth EnemyHealth = col.collider.GetComponent<EnemyHealth>();
            Debug.Log("Hit");
            EnemyHealth.takeDamage(damage);

            Destroy(gameObject);

        }
        Debug.Log(col.gameObject.name);
        Destroy(gameObject);
    }
}
