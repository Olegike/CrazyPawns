using CrazyPawns.GameAssets.Board;
using CrazyPawns.GameAssets.Cell;
using CrazyPawns.GameAssets.Common;
using CrazyPawns.GameAssets.Line;
using CrazyPawns.GameAssets.Pawn;
using CrazyPawns.GameAssets.UI;
using UnityEngine;
using Zenject;

namespace CrazyPawn.GameAssets.Installers
{
    [CreateAssetMenu(fileName = "Installer", menuName = "Installers/Installer")]
    public class Installer : ScriptableObjectInstaller<Installer>
    {
        [SerializeField]
        private Board _board;

        [SerializeField]
        private Cell _cell;

        [SerializeField]
        private PawnController _pawnController;

        [SerializeField]
        private Pawn _pawn;

        [SerializeField]
        private Line _line;

        [SerializeField]
        private ClickHandler _clickHandler;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Board>().FromComponentInNewPrefab(_board).AsSingle().NonLazy();
            Container.BindMemoryPool<Cell, CellPool>().FromComponentInNewPrefab(_cell);
            Container.BindInterfacesAndSelfTo<PawnController>().FromComponentInNewPrefab(_pawnController).AsSingle().NonLazy();
            Container.BindMemoryPool<Pawn, PawnPool>().FromComponentInNewPrefab(_pawn);
            Container.BindMemoryPool<Line, LinePool>().FromComponentInNewPrefab(_line);
            Container.BindInterfacesAndSelfTo<ClickHandler>().FromComponentInNewPrefab(_clickHandler).AsSingle().NonLazy();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsCached();
        }
    }
}
