using System.Collections.Generic;

namespace Assets.AI.Detection.Aim
{
    public class PatrolAimProvider : IAimProvider
    {
        private Queue<PatrolAim> _aims;

        public PatrolAimProvider(params PatrolAim[] aims)
        {
            _aims = new Queue<PatrolAim>(aims);
        }

        public IAim getAim()
        {
            var first = _aims.Peek();
            if (first.Achieved)
            {
                _aims.Dequeue();
                first.Reset();
                _aims.Enqueue(first);
            }
            return _aims.Peek();
        }
    }
}
