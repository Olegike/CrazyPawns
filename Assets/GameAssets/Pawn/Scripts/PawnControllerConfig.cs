using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    [CreateAssetMenu(menuName = "CrazyPawn/PawnControllerConfig", fileName = "PawnControllerConfig")]
    public class PawnControllerConfig : ScriptableObject
    {
        [SerializeField] 
        private float _initialZoneRadius = 10f;

        [SerializeField]
        private int _initialPawnCount = 7;

        [SerializeField]
        private Material _deleteMaterial;

        [SerializeField]
        private Material _activeConnectorMaterial;

        public float InitialZoneRadius => _initialZoneRadius;

        public int InitialPawnCount => _initialPawnCount;

        public Material DeleteMaterial => _deleteMaterial;

        public Material ActiveConnectorMaterial => _activeConnectorMaterial;
    }
}
