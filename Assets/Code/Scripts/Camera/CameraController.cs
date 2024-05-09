using Code.Scripts.PLayer;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Camera 
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Vector3 _offset;

        private PlayerMovementController _playerMovementController;

        [Inject]
        public void Construct(PlayerMovementController playerMovementController)
        {
            _playerMovementController = playerMovementController;
        }

        private void Update()
        {
            _camera.transform.position = _playerMovementController.transform.position + _offset;
        }
    }
}