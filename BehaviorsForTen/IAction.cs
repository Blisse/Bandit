using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorsForTen
{
    public interface IAction
    {
        object Execute(object sender, object parameter);
    }
}
