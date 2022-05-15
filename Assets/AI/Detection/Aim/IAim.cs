using UnityEngine;

namespace Assets.AI.Detection.Aim
{
    public interface IAim
    {
        Vector3 Position { get; }

        public void OnComplete();
    }
}
