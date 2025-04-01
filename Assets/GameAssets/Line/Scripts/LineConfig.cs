using CrazyPawns.GameAssets.Pawn.Socket;
using UnityEngine;

namespace CrazyPawns.GameAssets.Line
{
    public struct LineConfig
    {
        public Transform Parent;
        public Socket StartSocket;
        public Socket EndSocket;

        public LineConfig(Transform parent, Socket startSocket, Socket endSocket)
        {
            Parent = parent;
            StartSocket = startSocket;
            EndSocket = endSocket;
        }
    }
}
