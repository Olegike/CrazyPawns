using CrazyPawns.GameAssets.Line;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Pawn
{
    public class PawnController : MonoBehaviour
    {
        [Inject]
        private PawnControllerConfig _config;

        [Inject]
        private PawnPool _pawnPool;

        [Inject]
        private Board.Board _board;

        [SerializeField]
        private Transform _pawnsTransform;

        [SerializeField]
        private LineController _lineController;

        private List<Pawn> _pawns = new();
        private Camera _camera;
        private Socket.Socket _selectedSocketForConnection;
        private Pawn _selectedPawnForConnection;

        private void Start()
        {
            _camera = Camera.main;
            GeneratePawns();
        }

        private void GeneratePawns()
        {
            var positionsInCircle = _board.CellsPositions.Where(pos => CheckDistanceInCircle(pos, _config.InitialZoneRadius)).ToList();
            for (var i = 0; i < _config.InitialPawnCount; i++)
            {
                var randomIndex = Random.Range(0, positionsInCircle.Count);
                var pawn = _pawnPool.Spawn(new PawnConfig(positionsInCircle[randomIndex], _pawnsTransform));             
                AddPawnListeners(pawn);
                _pawns.Add(pawn);
            }
        }

        private bool CheckDistanceInCircle(Vector3 pos, float radius) => pos.x * pos.x + pos.y * pos.y + pos.z * pos.z < radius * radius;

        private void MovePawn(Pawn pawn)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_board.Plane.Raycast(ray, out var distance))
            {
                pawn.transform.position = ray.GetPoint(distance);
                pawn.CanBeDeleted = Mathf.Abs(pawn.transform.position.x) > _board.Bound || Mathf.Abs(pawn.transform.position.z) > _board.Bound;
                _lineController.UpdateLinePositions(pawn.Sockets);
            }
        }

        private void MovePawnFinished(Pawn pawn)
        {
            if (pawn.CanBeDeleted)
            {
                RemovePawnListeners(pawn);
                _lineController.DespawnLines(pawn.Sockets);
                _pawnPool.Despawn(pawn);
                _pawns.Remove(pawn);
                pawn.transform.parent = _pawnsTransform;
            }
        }

        private void StartHighlightSockets(List<Pawn> pawns) => pawns.ForEach(pawn => pawn.HighlightSockets(true));

        private void StopHighlightSockets() => _pawns.ForEach(pawn => pawn.HighlightSockets(false));

        private void SocketConnectionStarted(Pawn selectedPawn, Socket.Socket selectedSocket)
        {
            _selectedSocketForConnection = selectedSocket;
            _selectedPawnForConnection = selectedPawn;
            StartHighlightSockets(_pawns.Where(pawn => pawn != selectedPawn).ToList());
        }

        private void SocketConnectionSucceed(Pawn selectedPawn, Socket.Socket selectedSocket)
        {
            if (_selectedSocketForConnection != null 
                && _selectedPawnForConnection != null 
                && !_selectedPawnForConnection.Sockets.Contains(selectedSocket))
            {
                _lineController.SpawnLine(selectedSocket, _selectedSocketForConnection);
                StopHighlightSockets();
                _selectedSocketForConnection = null;
                _selectedPawnForConnection = null;
            }
        }

        private void AddPawnListeners(Pawn pawn)
        {
            pawn.OnMovePawn += MovePawn;
            pawn.OnMovePawnFinished += MovePawnFinished;
            pawn.OnSocketConnectionStarted += SocketConnectionStarted;
            pawn.OnSocketConnectionSucceed += SocketConnectionSucceed;
            pawn.OnSocketConnectionFinished += StopHighlightSockets;
        }

        private void RemovePawnListeners(Pawn pawn)
        {
            pawn.OnMovePawn -= MovePawn;
            pawn.OnMovePawnFinished -= MovePawnFinished;
            pawn.OnSocketConnectionStarted -= SocketConnectionStarted;
            pawn.OnSocketConnectionSucceed -= SocketConnectionSucceed;
            pawn.OnSocketConnectionFinished -= StopHighlightSockets;
        }

        private void OnDestroy() => _pawns.ForEach(pawn => RemovePawnListeners(pawn));
    }
}
