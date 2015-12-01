namespace Bandit.DesignViewModels
{
    using Prism.Windows.Mvvm;

    public class MainPageDesignViewModel : ViewModelBase
    {
        private BandsManagerDesignViewModel bandsManagerDesignViewModel;

        public MainPageDesignViewModel(BandsManagerDesignViewModel bandsManagerDesignViewModel)
        {
            BandsManagerDesignViewModel = bandsManagerDesignViewModel;
        }

        public BandsManagerDesignViewModel BandsManagerDesignViewModel
        {
            get
            {
                return bandsManagerDesignViewModel;
            }

            set
            {
                SetProperty(ref bandsManagerDesignViewModel, value);
            }
        }
    }
}