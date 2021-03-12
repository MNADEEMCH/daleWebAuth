using daleWebAuth.Pages;
using daleWebAuth.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace daleWebAuth.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected NavigationPage NavigationPage
        {
            get
            {
                if (_App?.MainPage is MainPage)
                {
                    return new NavigationPage(new MainPage());
                }

                return _App?.MainPage as NavigationPage;
            }
        }

        private INavigation _Navigation => NavigationPage.Navigation;


        private Application _App => Application.Current;

        #region INavigation implementation

        public async Task SetRootPageAsync(Page page)
        {
            await PopToRootAsync();
            _App.MainPage = page;
        }

        public void RemovePage(Page page)
        {
            _Navigation?.RemovePage(page);
        }

        public void InsertPageBefore(Page page, Page before)
        {
            _Navigation?.InsertPageBefore(page, before);
        }

        public async Task PushAsync(Page page)
        {
            if (NavigationStack.Any())
            {
                if (NavigationStack.LastOrDefault().GetType() == page.GetType())
                    return;
            }
            var task = _Navigation?.PushAsync(page);
            if (task != null)
                await task;
        }

        public Page GetCurrentPage()
        {
            var page = _Navigation?.NavigationStack[(_Navigation?.NavigationStack.Count ?? 0) - 1];
            return page;
        }

        public async Task<Page> PopAsync()
        {
            var task = _Navigation?.PopAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PopToRootAsync()
        {
            var task = _Navigation?.PopToRootAsync();
            if (task != null)
                await task;
        }

        public async Task PushModalAsync(Page page)
        {
            var task = _Navigation?.PushModalAsync(page);
            if (task != null)
                await task;
        }

        public async Task<Page> PopModalAsync()
        {
            var task = _Navigation?.PopModalAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PushAsync(Page page, bool animated)
        {
            var task = _Navigation?.PushAsync(page, animated);
            if (task != null)
                await task;
        }

        public async Task<Page> PopAsync(bool animated)
        {
            var task = _Navigation?.PopAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PopToRootAsync(bool animated)
        {
            var task = _Navigation?.PopToRootAsync(animated);
            if (task != null)
                await task;
        }

        public async Task PushModalAsync(Page page, bool animated)
        {
            var task = _Navigation?.PushModalAsync(page, animated);
            if (task != null)
                await task;
        }

        public async Task<Page> PopModalAsync(bool animated)
        {
            var task = _Navigation?.PopModalAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PushAsync(Page page, object parameter)
        {
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            await PushAsync(page);
        }

        public async Task PushModalAsync(Page page, object parameter, bool animation)
        {
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            await PushModalAsync(page, animation);
        }

        public IReadOnlyList<Page> NavigationStack => _Navigation?.NavigationStack;
        public IReadOnlyList<Page> ModalStack => _Navigation?.ModalStack;

        #endregion INavigation implementation
    }
}
