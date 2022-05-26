using System;
using UnityEngine;

namespace Assets.AI.Detection.Aim
{
    public class PrimaryAim : IAim
    {
        public event Action<PrimaryAim> OnCompleted;

        public Vector3 Position { get; private set; }

        public PrimaryAim(Vector3 position)
        {
            Position = position;
        }

        public void OnComplete()
        {
            OnCompleted?.Invoke(this);
        }
    }
}
