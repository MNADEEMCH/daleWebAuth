using daleWebAuth.Pages.Other;
using daleWebAuth.ViewModels.Base;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace daleWebAuth.ViewModels.Other
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {

        }

        ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ??= new AsyncCommand(ExecuteLoginCommand);

        public async Task ExecuteLoginCommand()
        {
            await NavigationService.PushAsync(new OTPPage());
        }
    }
}
