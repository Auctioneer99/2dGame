using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AI.State
{
    public interface IStateSwitcher<T> where T : IStateModel
    {
        void ChangeState<S>() where S : IState<T>, new();
    }
}
