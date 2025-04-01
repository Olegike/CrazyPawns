using CrazyPawns.GameAssets.Pawn.Socket;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Line
{
    public class LineController : MonoBehaviour
    {
        [Inject]
        private LinePool _linePool;

        private List<Line> _lines = new();

        public void SpawnLine(Socket firstSocket, Socket secondSocket)
        {
            var line = _linePool.Spawn(new LineConfig(transform, firstSocket, secondSocket));
            _lines.Add(line);
        }

        public void UpdateLinePositions(IReadOnlyList<Socket> sockets)
        {
            var targetLines = SelectLines(sockets);
            targetLines.ForEach(line => line.UpdateLinePositions());
        }

        public void DespawnLines(IReadOnlyList<Socket> sockets)
        {
            var targetLines = SelectLines(sockets);

            foreach (var line in targetLines)
            {
                _linePool.Despawn(line);
                _lines.Remove(line);
                line.transform.parent = transform;
            }
        }

        private List<Line> SelectLines(IReadOnlyList<Socket> sockets) => _lines.Where(line => sockets.Any(socket => CheckSocketInLine(line, socket))).ToList();

        private bool CheckSocketInLine(Line line, Socket socket) => socket == line.LineConfig.StartSocket || socket == line.LineConfig.EndSocket;
    }
}
