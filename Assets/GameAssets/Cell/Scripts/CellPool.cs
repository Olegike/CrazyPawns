using Zenject;

namespace CrazyPawns.GameAssets.Cell
{
    public class CellPool : MonoMemoryPool<CellConfig, Cell>
    {
        protected override void Reinitialize(CellConfig config, Cell cell) => cell.Reinitialize(config);
    }
}
