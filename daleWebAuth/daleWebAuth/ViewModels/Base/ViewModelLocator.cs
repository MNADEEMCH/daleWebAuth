using daleWebAuth.Services;
using System;
using System.Globalization;
using System.Reflection;
using TinyIoC;
using Xamarin.Forms;

namespace daleWebAuth.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static readonly TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();
            _container.Register<MainViewModel>();
            _container.Register<IAccountService, AccountService>();
            // View models - by default, TinyIoC will register concrete classes as multi-instance.

            
        }


        public static bool UseMockService { get; set; }

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static void UpdateDependencies(bool useMockServices)
        {
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null) return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.").Replace("Page", "View");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName =
                string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null) return;
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}