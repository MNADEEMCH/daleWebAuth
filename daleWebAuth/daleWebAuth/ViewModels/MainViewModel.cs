using daleWebAuth.Services;
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
    public class MainViewModel : BaseViewModel
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
                var codeResult = await WebAuthenticator.AuthenticateAsync(new Uri(externalUrl), new Uri("daleWebAuth://"));
                var code = codeResult.Properties["code"];

                if (!string.IsNullOrEmpty(code))
                {
                    var loginResult = await _accountService.ExchangeCodeForToken(code);

                    //var authResult = await _accountService.AuthServerUserInfo(loginResult.Item2?.AccessToken);
                    //if (authResult.Item1 == CallStatus.Success)
                    //{
                    //    await ProcessLoginRequest(loginResult.Item2, authResult.Item2);
                    //}
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
    }
}
