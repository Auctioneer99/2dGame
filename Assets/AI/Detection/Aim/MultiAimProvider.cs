using UnityEngine;

namespace Assets.AI.Detection.Aim
{
    public class MultiAimProvider : IAimProvider
    {
        private IAimProvider _secondaryProvider;

        private PrimaryAim _primaryAim;

        public MultiAimProvider(IAimProvider provider)
        {
            _secondaryProvider = provider;
        }

        public IAim getAim()
        {
            return _primaryAim == null ? _secondaryProvider.getAim() : _primaryAim;
        }

        public void AddPrimaryAim(Vector3 position)
        {
            if (_primaryAim != null)
            {
                _primaryAim.OnCompleted -= OnCompleted;
            }
            var a = new PrimaryAim(position);
            a.OnCompleted += OnCompleted;
            _primaryAim = a;
        }

        private void OnCompleted(PrimaryAim aim)
        {
            _primaryAim = null;
        }
    }
}
