using UnityEngine;

namespace BootlacesMaster.UI
{
    public class Cheats : MonoBehaviour
    {
        [SerializeField] private GameObject _root = null;
        
        private bool _touchesTriggered;

        private PointerInputSplitter _inputSplitter;
        
        private void Start()
        {
            _root.SetActive(false);
            _inputSplitter = FindObjectOfType<PointerInputSplitter>();
        }

        private void Update()
        {
            // PC
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _root.SetActive(_root.activeSelf == false);

                _inputSplitter.enabled = _root.activeSelf == false;

                return;
            }
        
            // Android
            if (Input.touchCount >= 3 && _touchesTriggered == false)
            {
                _touchesTriggered = true;
                _root.SetActive(_root.activeSelf == false);
                
                _inputSplitter.enabled = _root.activeSelf == false;
                
                return;
            }
        
            if (Input.touchCount == 0 && _touchesTriggered)
            {
                _touchesTriggered = false;
            }
        }
    }
}