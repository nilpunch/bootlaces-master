using UnityEngine;

namespace BootlacesMaster
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;

        public Vector3 Position => transform.position;

        public float Radius => _radius;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}