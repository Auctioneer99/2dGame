using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AI.State
{
    public interface IState<T> where T : IStateModel
    {
        public void Initialize(IStateSwitcher<T> changer, T model);
        public void Update();
        public void Enter(IState<T> last);
        public void Exit(IState<T> next);
    }
}
