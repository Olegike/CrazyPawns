using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public class Pawn : MonoBehaviour
    {
        public event Action<Pawn> OnMovePawn;
        public event Action<Pawn> OnMovePawnFinished;
        public event Action<Pawn, Socket.Socket> OnSocketConnectionStarted;
        public event Action<Pawn, Socket.Socket> OnSocketConnectionSucceed;
        public event Action OnSocketConnectionFinished;

        [SerializeField]
        private PawnMoveDetector _moveDetector;

        [SerializeField]
        private Material _defaultMaterial;

        [SerializeField]
        private Material _deleteMaterial;

        [SerializeField]
        private MeshRenderer _cubeMeshRenderer;

        [SerializeField]
        private List<Socket.Socket> _sockets;

        private bool _canBeDeleted = false;

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

        public IReadOnlyList<Socket.Socket> Sockets => _sockets; 

        public void Reinitialize(PawnConfig config)
        {
            transform.parent = config.Parent;
            transform.position = config.Position;
            _moveDetector.OnMove += MovePawn;
            _moveDetector.OnMoveFinished += MovePawnEnd;
            CanBeDeleted = false;
            foreach (var socket in _sockets)
            {
                socket.Reinitialize();
                socket.OnConnectionStarted += SocketConnectionStarted;
                socket.OnConnectionSucceed += SocketConnectionSucceed;
                socket.OnConnectionFinished += SocketConnectionFinished;
            }
        }

        public void HighlightSockets(bool highlight) => _sockets.ForEach(socket => socket.Highlight(highlight));

        private void MovePawn() => OnMovePawn?.Invoke(this);

        private void MovePawnEnd() => OnMovePawnFinished?.Invoke(this);

        private void UpdateMaterial()
        {
            _cubeMeshRenderer.material = _canBeDeleted ? _deleteMaterial : _defaultMaterial;
            _sockets.ForEach(socket => socket.CanBeDeleted = _canBeDeleted);
        }

        private void SocketConnectionStarted(Socket.Socket socket) => OnSocketConnectionStarted?.Invoke(this, socket);

        private void SocketConnectionSucceed(Socket.Socket socket) => OnSocketConnectionSucceed?.Invoke(this, socket);

        private void SocketConnectionFinished() => OnSocketConnectionFinished?.Invoke();

        private void OnDestroy()
        {
            _moveDetector.OnMove -= MovePawn;
            _moveDetector.OnMoveFinished -= MovePawnEnd;
            foreach (var socket in _sockets)
            {
                socket.OnConnectionStarted -= SocketConnectionStarted;
                socket.OnConnectionSucceed -= SocketConnectionSucceed;
                socket.OnConnectionFinished -= SocketConnectionFinished;
            }
        }
    }
}
