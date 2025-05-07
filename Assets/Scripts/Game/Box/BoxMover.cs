using Infrastructure.DIContainer;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Box
{
    public class BoxMover : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Box _box;
        private BoxConfig _boxConfig;
        private BoxController _boxController;

        private Vector3 _leftPosition;

        private Camera _mainCamera;
        private float _prefTimeBoxSpawned;
        private Vector3 _rightPosition;
        private float timeAfterBoxSpawned;

        public void OnDrag(PointerEventData eventData)
        {
            if (_box == null) return;

            var screenPos = new Vector3(
                eventData.position.x,
                eventData.position.y,
                _mainCamera.WorldToScreenPoint(_box.transform.position).z
            );

            var worldPos = _mainCamera.ScreenToWorldPoint(screenPos);
            var clampedX = Mathf.Clamp(worldPos.x, _leftPosition.x, _rightPosition.x);
            _box.transform.position = new Vector3(clampedX, _box.transform.position.y, _box.transform.position.z);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            timeAfterBoxSpawned = Time.time - _prefTimeBoxSpawned;
            if (timeAfterBoxSpawned >= _boxConfig.delayBetweenSpawn)
            {
                _box = _boxController.SpawnBox();
                _prefTimeBoxSpawned = Time.time;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_box == null) return;

            _box.ApplyShootForce(_boxConfig.shootingForce * _boxConfig.shootDirection);
            _box = null;
        }

        [Inject]
        public void Construct(BoxConfig boxConfig, BoxPositions boxPositions, BoxController boxController)
        {
            _boxConfig = boxConfig;
            _boxController = boxController;
            _leftPosition = boxPositions.leftMoveBound.position;
            _rightPosition = boxPositions.rightMoveBound.position;
            _mainCamera = Camera.main;
        }
    }
}