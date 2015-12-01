using Windows.UI.Xaml;

namespace BehaviorsForTen
{
    public interface IBehavior
    {
        DependencyObject AssociatedObject { get; }
        void Attach(DependencyObject associatedObject);
        void Detach();
    }
}
