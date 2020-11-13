using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        private EnemyWeaponSystem _ews;
        private EnemyInteract _interact;
        private NavMeshAgent _agent;
        private Transform _vision;
        private GameObject[] _intPoint;

        public string task;

        public float interactDistance;
        public float distanceFromPoint;
        public float scanDistance;

        public GameObject target;

        [Range(0, 180)]
        public int visionDegree;
        
        private void Start()
        {
            _interact = GetComponent<EnemyInteract>();
            _ews = GetComponentInChildren<EnemyWeaponSystem>();
            _agent = GetComponent<NavMeshAgent>();
            _vision = transform.GetChild(0);

            _intPoint = GameObject.FindGameObjectsWithTag("InterestPoint");

            task = "NoTask";
        }

        private void Update()
        {
            switch (task)
            {
                case "NoTask":
                    FindNearestInterestPoint();
                    break;
                case "PathComplete":
                    ScanArea();
                    break;
            }
            
        }

        void FindNearestInterestPoint()
        {
            task = "FindingClosestDestination";
            GameObject closestPoint = null;
            var closestPointDistance = 0f;
            foreach (var points in _intPoint)
            {
                var distance = Vector3.Distance(points.transform.position, transform.position);

                if (closestPoint == null)
                {
                    closestPoint = points;
                    closestPointDistance = distance;
                }

                if (closestPointDistance > distance)
                {
                    closestPoint = points;
                    closestPointDistance = distance;
                }
            }

            task = "DestinationSet";
            if (closestPointDistance != 0)
            {
                StartCoroutine(SetDestination(closestPoint));
            }
        }

        void ScanArea()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanDistance);
            foreach (var item in hitColliders)
            {
                if (item.gameObject.CompareTag("Equipment"))
                {
                    StartCoroutine(EquipItem(item.gameObject));
                }
            }
        }

        IEnumerator EquipItem(GameObject itemTarget)
        {
            task = "Equipping";
            var position = itemTarget.transform.position;
            _agent.destination = position;
            yield return new WaitUntil(() =>
                Vector3.Distance(itemTarget.transform.position, _vision.transform.position) <= interactDistance);
            _agent.destination = transform.position;
            var rotation = Quaternion.LookRotation(position - _vision.transform.position);
            _vision.rotation = rotation;
            _interact.Interact(interactDistance);
            
            
        }

        IEnumerator SetDestination(GameObject itemTarget)
        {
            _agent.destination = itemTarget.transform.position;
            
            yield return new WaitUntil(() =>
                Vector3.Distance(itemTarget.transform.position, transform.position) <= distanceFromPoint);
            
            _agent.destination = transform.position;
            
            task = "PathComplete";
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}