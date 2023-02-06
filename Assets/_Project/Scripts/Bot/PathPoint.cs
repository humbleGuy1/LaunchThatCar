using UnityEngine;

namespace Bots
{
    public class PathPoint: MonoBehaviour
    {
        [field: SerializeField] public int Index { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bot bot))
                bot.SetPointReached(Index);
        }
    }
}
