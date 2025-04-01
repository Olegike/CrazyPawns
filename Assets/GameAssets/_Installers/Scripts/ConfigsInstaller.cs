using CrazyPawns.GameAssets.Board;
using CrazyPawns.GameAssets.Pawn;
using UnityEngine;
using Zenject;

namespace CrazyPawn.GameAssets.Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField]
        private BoardConfig _boardConfig;

        [SerializeField]
        private PawnControllerConfig _pawnControllerConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BoardConfig>().FromScriptableObject(_boardConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PawnControllerConfig>().FromScriptableObject(_pawnControllerConfig).AsSingle().NonLazy();
        }
    }
}
