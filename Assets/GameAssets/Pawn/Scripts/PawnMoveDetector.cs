using CrazyPawns.GameAssets.Common;
using System;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Pawn
{
    public class PawnMoveDetector : MonoBehaviour
    {
        public event Action OnMove;
        public event Action OnMoveFinished;

        [Inject]
        private CameraController _cameraController;

        private void OnMouseDrag()
        {
            _cameraController.IgnoreClicks = true;
            OnMove?.Invoke();
        }

        private void OnMouseUp()
        {
            _cameraController.IgnoreClicks = false;
            OnMoveFinished?.Invoke();
        }
    }
}
