using Windows.UI.Xaml;

namespace BehaviorsForTen
{
    /// <summary>
    /// Please use this as a base class for your custom action
    /// If you are planning on using DependencyObjectCollection as a dependency property on your custom action,
    /// Please re-instantiate the DependecyCollection in a parameterless CTOR of your custom action.
    /// </summary>
    public abstract class ActionBase : DependencyObject, IAction
    {
        public abstract object Execute(object sender, object parameter);
    }
}
