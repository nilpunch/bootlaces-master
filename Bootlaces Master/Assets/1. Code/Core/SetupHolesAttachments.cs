using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public class SetupHolesAttachments : MonoBehaviour
    {
        [SerializeField] private float _castRadius = 0.25f;

        private void Start()
        {
            Hole[] holes = FindObjectsOfType<Hole>();

            foreach (var hole in holes)
            {
                var results = Physics.OverlapSphere(hole.Position, _castRadius);

                LaceHandle freeHandle = results.Select(target => target.GetComponent<LaceHandle>())
                    .Where(handle => handle != null && handle.gameObject.activeInHierarchy && handle.Detached)
                    .OrderBy(handle => Vector3.Distance(handle.Position, hole.Position))
                    .FirstOrDefault();
                
                if (freeHandle != null)
                    hole.InitialAttach(freeHandle);
            }
        }
    }
}