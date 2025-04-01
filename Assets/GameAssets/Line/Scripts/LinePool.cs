using Zenject;

namespace CrazyPawns.GameAssets.Line
{
    public class LinePool : MonoMemoryPool<LineConfig, Line>
    {
        protected override void Reinitialize(LineConfig config, Line line) => line.Reinitialize(config);
    }
}
