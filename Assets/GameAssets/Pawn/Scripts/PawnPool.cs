using Zenject;

namespace CrazyPawns.GameAssets.Pawn
{
    public class PawnPool : MonoMemoryPool<PawnConfig, Pawn>
    {
        protected override void Reinitialize(PawnConfig config, Pawn pawn) => pawn.Reinitialize(config);
    }
}
