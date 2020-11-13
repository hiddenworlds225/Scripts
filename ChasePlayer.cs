using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    
    private NavMeshAgent enemy;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        
    }

    void Chase()
    {
        Vector3 target = player.transform.position;
        enemy.SetDestination(target);
    }

    
   
}
