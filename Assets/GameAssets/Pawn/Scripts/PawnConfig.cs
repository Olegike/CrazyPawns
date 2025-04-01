using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public struct PawnConfig
    {
        public Vector3 Position;
        public Transform Parent;

        public PawnConfig(Vector3 position, Transform parent)
        {
            Position = position;
            Parent = parent;
        }
    }
}
