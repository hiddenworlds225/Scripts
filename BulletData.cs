using UnityEngine;

namespace Universal
{
    public class BulletData : MonoBehaviour
    {
        public float damage;
        public float timeToDecay;
        private float _decayTime;
        private Rigidbody _rb;
        public float velocity;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }


        // Update is called once per frame
        void Update()
        {
            _decayTime += Time.deltaTime;
            _rb.position += transform.forward * Time.deltaTime * velocity;
            if (_decayTime >= timeToDecay)
            {
                Destroy(this);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                Debug.Log("I hit!");
                Destroy(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}