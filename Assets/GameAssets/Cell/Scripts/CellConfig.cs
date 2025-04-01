using UnityEngine;

namespace CrazyPawns.GameAssets.Cell
{
    public struct CellConfig
    {
        public Transform Parent;
        public Vector3 Position;
        public Material Material;

        public CellConfig(Transform parent, Vector3 position, Material material)
        {
            Parent = parent;
            Position = position;
            Material = material;
        }
    }
}
