namespace BehaviorsForTen.Behaviors
{
    using System.Windows.Input;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    public class SelectionChangedBehavior : BehaviorBase<Selector>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(SelectionChangedBehavior), 
            new PropertyMetadata(null));
        
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        protected override void ElementAttached()
        {
            base.ElementAttached();
            AssociatedElement.SelectionChanged += AssociatedElementSelectionChanged;
        }

        protected override void ElementDetached()
        {
            AssociatedElement.SelectionChanged -= AssociatedElementSelectionChanged;
            base.ElementDetached();
        }

        private void AssociatedElementSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Command.Execute(e);
        }
    }
}