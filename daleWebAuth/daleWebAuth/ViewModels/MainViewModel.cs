using daleWebAuth.Pages.Other;
using daleWebAuth.Services;
using daleWebAuth.ViewModels.Base;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace daleWebAuth.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;

        public MainViewModel()
        {

        }
        public MainViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }


        ICommand _authorizeCommand;
        public ICommand AuthorizeCommand => _authorizeCommand ??= new AsyncCommand(ExecuteAuthorizeCommand);

        public async Task ExecuteAuthorizeCommand()
        {
            var externalUrl = _accountService.CreateAuthorizationRequest();

            try
            {
                var codeResult = await WebAuthenticator.AuthenticateAsync(new Uri(externalUrl), new Uri(GlobalSettings.Instance.Callback));
                var code = codeResult.Properties["code"];

                if (!string.IsNullOrEmpty(code))
                {
                    var loginResult = await _accountService.ExchangeCodeForToken(code);
                    if(loginResult.Item1 == helpers.Common.CallStatus.Success)
                    {
                        await NavigationService.PushAsync(new SuccessPage());
                    }
                    else
                    {
                        await DialogService.AlertAsync("Authentication is not successful, something went wrong", "Error", "Ok");
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Login canceled.");

                //await DialogService.AlertAsync("Login canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message}");

                //await DialogService.AlertAsync($"Failed: {ex.Message}");
            }
        }

        ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ??= new AsyncCommand(ExecuteLoginCommand);

        public async Task ExecuteLoginCommand()
        {
            await NavigationService.PushAsync(new LoginPage());
        }
    }
}
