using Acr.UserDialogs;
using daleWebAuth.Services;
using daleWebAuth.Services.Navigation;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace daleWebAuth.ViewModels.Base
{
    public abstract class ViewModelBase : BaseViewModel
    {
        protected IUserDialogs DialogService { get => ViewModelLocator.Resolve<IUserDialogs>(); }
        protected INavigationService NavigationService { get => ViewModelLocator.Resolve<INavigationService>(); }
        protected AccountService AccountService { get => ViewModelLocator.Resolve<AccountService>(); }

        protected ViewModelBase()
        {
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task OnAppearing()
        {
            return Task.FromResult(false);
        }

        public virtual Task OnDisappearing()
        {
            return Task.FromResult(false);
        }
    }
}
