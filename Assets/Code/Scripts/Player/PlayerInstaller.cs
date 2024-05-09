using Code.Scripts.Camera;
using UnityEngine;
using Zenject;

namespace Code.Scripts.PLayer 
{
    public class PlayerInstaller : MonoInstaller
    {
        private Controls _controls;   

        private void OnEnable()
        {
            _controls?.Enable();
        }

        private void OnDisable()
        {
            _controls?.Disable();
        }

        private void OnDestroy()
        {
            _controls?.Dispose();
        }

        public override void InstallBindings() 
        {
            _controls = new();
            Container.Bind<Controls>().FromInstance(_controls);

            var playerMovementController = GameObject.FindAnyObjectByType<PlayerMovementController>();
            var cameraController = GameObject.FindAnyObjectByType<CameraController>();

            switch (playerMovementController != null) 
            {
                case true:
                    Container.Bind<PlayerMovementController>().FromInstance(playerMovementController);
                    break;
                case false:
                    Debug.Log("There is no PlayerMovementController on the scene");
                    break;
            }

            switch (cameraController != null)
            {
                case true:
                    Container.Bind<CameraController>().FromInstance(cameraController);
                    break;
                case false:
                    Debug.Log("There is no CameraController on the scene");
                    break;
            }
        }
    }
}