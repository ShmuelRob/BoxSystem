using BoxSystem.Services;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using ManageSystem;
using Models;

namespace BoxSystem.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IConfigItems, ConfigItems>();
            SimpleIoc.Default.Register<LogViewModel>();
            SimpleIoc.Default.Register<INotify<Box>, Notifier>();
            SimpleIoc.Default.Register<ManageSystem<Box>>();
            SimpleIoc.Default.Register<ControlViewModel>();
            SimpleIoc.Default.Register<ProductsViewModel>();
        }

        public IConfigItems Config => ServiceLocator.Current.GetInstance<ConfigItems>();
        public LogViewModel Log => ServiceLocator.Current.GetInstance<LogViewModel>();
        public INotify<Box> Notifier => ServiceLocator.Current.GetInstance<Notifier>();
        public ManageSystem<Box> MS => ServiceLocator.Current.GetInstance<ManageSystem<Box>>();
        public ControlViewModel Control => ServiceLocator.Current.GetInstance<ControlViewModel>();
        public ProductsViewModel Products => ServiceLocator.Current.GetInstance<ProductsViewModel>();

        public static void Cleanup() { }
    }
}