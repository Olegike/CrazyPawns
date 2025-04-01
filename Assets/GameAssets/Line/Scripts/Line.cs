using UnityEngine;

namespace CrazyPawns.GameAssets.Line
{
    public class Line : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _lineRenderer;

        private LineConfig _config;

        public LineConfig LineConfig => _config;

        public void Reinitialize(LineConfig config)
        {
            _config = config;
            transform.parent = config.Parent;
            UpdateLinePositions();
        }

        public void UpdateLinePositions()
        {
            _lineRenderer.SetPosition(0, _config.StartSocket.transform.position);
            _lineRenderer.SetPosition(1, _config.EndSocket.transform.position);
        }
    }
}
