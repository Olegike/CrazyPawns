using CrazyPawns.GameAssets.Common;
using CrazyPawns.GameAssets.UI;
using System;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Pawn.Socket
{
    public class Socket : MonoBehaviour
    {
        private const float ClickDeltaTime = 0.2f;
        private static bool IsDragging = false;

        public event Action<Socket> OnConnectionStarted;
        public event Action<Socket> OnConnectionSucceed;
        public event Action OnConnectionFinished;

        [Inject]
        private ClickHandler _clickHandler;

        [Inject]
        private CameraController _cameraController;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private Material _defaultMaterial;

        [SerializeField]
        private Material _deleteMaterial;

        [SerializeField]
        private Material _availableMaterial;

        private Camera _camera;
        private bool _canBeDeleted = false;
        private float _downClickTime;

        public bool CanBeDeleted
        {
            get => _canBeDeleted;
            set
            {
                if (_canBeDeleted != value)
                {
                    _canBeDeleted = value;
                    UpdateMaterial();
                }
            }
        }

        private void Start() => _camera = Camera.main;

        public void Reinitialize() => CanBeDeleted = false;

        public void Highlight(bool highlight) => _meshRenderer.material = highlight ? _availableMaterial : _defaultMaterial;

        private void UpdateMaterial() => _meshRenderer.material = _canBeDeleted ? _deleteMaterial : _defaultMaterial;

        private void ConnectionFinished() => OnConnectionFinished?.Invoke();

        private void OnMouseDown()
        {
            _cameraController.IgnoreClicks = true;
            _downClickTime = Time.time;
        }

        private void OnMouseDrag()
        {
            if (Time.time - _downClickTime >= ClickDeltaTime)
            {
                IsDragging = true;
                OnConnectionStarted?.Invoke(this);
            }
        }

        private void OnMouseUp()
        {
            if (IsDragging)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit) && hit.transform.TryGetComponent<Socket>(out var socket))
                {
                    OnConnectionSucceed?.Invoke(socket);
                }

                ConnectionFinished();
                IsDragging = false;
            }
            else
            {
                OnConnectionSucceed?.Invoke(this);

                if (!_clickHandler.IsActive)
                {
                    _clickHandler.ActivateButton(ConnectionFinished);
                    OnConnectionStarted?.Invoke(this);
                }
            }

            _cameraController.IgnoreClicks = false;
        }
    }
}
