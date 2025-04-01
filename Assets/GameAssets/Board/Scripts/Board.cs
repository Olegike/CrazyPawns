using CrazyPawns.GameAssets.Cell;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Board
{
    public class Board : MonoBehaviour
    {
        [Inject]
        private BoardConfig _config;

        [Inject]
        private CellPool _cellPool;

        [SerializeField]
        private Material _blackCellMaterial;

        [SerializeField]
        private Material _whiteCellMaterial;

        [SerializeField]
        private Vector3 _normal = Vector3.up;

        private Cell.Cell[,] _cells;

        public List<Vector3> CellsPositions { get; private set; } = new();

        public Plane Plane { get; private set; }

        public float Bound { get; private set; }

        private void Start()
        {
            Plane = new Plane(_normal, 0.0f);
            GenerateCells();
            Bound = _config.CheckerBoardSize * _config.CellSize / 2;
        }

        private void GenerateCells()
        {
            var boardSize = _config.CheckerBoardSize;
            _cells = new Cell.Cell[boardSize, boardSize];
            for (var i = 0; i < boardSize; i++)
            {
                for (var j = 0; j < boardSize; j++)
                {
                    var xPos = CalcPos(i);
                    var zPos = CalcPos(j);
                    var position = new Vector3(xPos, 0.0f, zPos);
                    var cellConfig = new CellConfig(transform, position, (i + j) % 2 == 0 ? _blackCellMaterial : _whiteCellMaterial);
                    var cell = _cellPool.Spawn(cellConfig);
                    CellsPositions.Add(cell.transform.position);
                    _cells[i,j] = cell;
                }
            }
        }

        private float CalcPos(int index)
        {
            var shift = _config.CellSize % 2 == 0 ? 0.0f : _config.CellSize / 2;
            return shift - ((float)_config.CheckerBoardSize / 2 - index) * _config.CellSize;
        }
    }
}
