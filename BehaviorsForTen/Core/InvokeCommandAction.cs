namespace BehaviorsForTen.Core
{
    using System.Windows.Input;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public sealed class InvokeCommandAction : DependencyObject, IAction
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter", 
                typeof(object), 
                typeof(InvokeCommandAction), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(InvokeCommandAction), 
            new PropertyMetadata(null));

        public static readonly DependencyProperty InputConverterLanguageProperty =
            DependencyProperty.Register(
                "InputConverterLanguage", 
                typeof(string), 
                typeof(InvokeCommandAction), 
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InputConverterParameterProperty =
            DependencyProperty.Register(
                "InputConverterParameter", 
                typeof(object), 
                typeof(InvokeCommandAction), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty InputConverterProperty = DependencyProperty.Register(
            "InputConverter", 
            typeof(IValueConverter), 
            typeof(InvokeCommandAction), 
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

        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }

            set
            {
                this.SetValue(CommandParameterProperty, value);
            }
        }

        public IValueConverter InputConverter
        {
            get
            {
                return (IValueConverter)this.GetValue(InputConverterProperty);
            }

            set
            {
                this.SetValue(InputConverterProperty, value);
            }
        }

        public string InputConverterLanguage
        {
            get
            {
                return (string)this.GetValue(InputConverterLanguageProperty);
            }

            set
            {
                this.SetValue(InputConverterLanguageProperty, value);
            }
        }

        public object InputConverterParameter
        {
            get
            {
                return this.GetValue(InputConverterParameterProperty);
            }

            set
            {
                this.SetValue(InputConverterParameterProperty, value);
            }
        }

        public object Execute(object sender, object parameter)
        {
            if (Command == null)
            {
                return false;
            }

            object parameter2;
            if (CommandParameter != null)
            {
                parameter2 = this.CommandParameter;
            }
            else
            {
                if (this.InputConverter != null)
                {
                    parameter2 = this.InputConverter.Convert(
                        parameter, 
                        typeof(object), 
                        this.InputConverterParameter, 
                        this.InputConverterLanguage);
                }
                else
                {
                    parameter2 = parameter;
                }
            }

            if (!this.Command.CanExecute(parameter2))
            {
                return false;
            }

            this.Command.Execute(parameter2);
            return true;
        }
    }
}