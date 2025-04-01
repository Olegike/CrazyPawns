using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Common
{
    public class CameraController : MonoBehaviour
    {
        private const float MinDistToPlane = 1.0f;

        [Inject]
        private Board.Board _board;

        [SerializeField]
        private float _dragSpeed = 50f;

        [SerializeField]
        private float _zoomSpeed = 50f;

        [SerializeField]
        private Camera _camera;

        private Vector3 _dragStartPosition;
        private Vector3 _dragCurrentPosition;

        public bool IgnoreClicks { get; set; } = false;

        private void LateUpdate()
        {
            if (!IgnoreClicks)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (GetMouseWorldPosition(out var mouseWorldposition))
                    {
                        _dragStartPosition = mouseWorldposition;
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    if (GetMouseWorldPosition(out var mouseWorldPosition))
                    {
                        _dragCurrentPosition = mouseWorldPosition;
                        transform.position += (_dragStartPosition - _dragCurrentPosition) * _dragSpeed * Time.deltaTime;
                    }
                }
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                if (GetMouseWorldPosition(out var mouseWorldPosition))
                {
                    if (_board.Plane.GetDistanceToPoint(_camera.transform.position) > MinDistToPlane || Input.mouseScrollDelta.y < 0.0f)
                    {
                        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, mouseWorldPosition, _zoomSpeed * Input.mouseScrollDelta.y * Time.smoothDeltaTime);
                    }
                }
            }
        }

        private bool GetMouseWorldPosition(out Vector3 mouseWorldPosition)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var success = _board.Plane.Raycast(ray, out var entry);
            mouseWorldPosition = success ? ray.GetPoint(entry) : Vector3.zero;

            return success;
        }
    }
}
