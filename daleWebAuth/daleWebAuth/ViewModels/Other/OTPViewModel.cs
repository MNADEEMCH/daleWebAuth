using daleWebAuth.Pages.Other;
using daleWebAuth.ViewModels.Base;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace daleWebAuth.ViewModels.Other
{
    public class OTPViewModel : ViewModelBase
    {
        public OTPViewModel()
        {

        }

        ICommand _verifyCommand;
        public ICommand VerifyCommand => _verifyCommand ??= new AsyncCommand(ExecuteVerifyCommand);

        public async Task ExecuteVerifyCommand()
        {
            await NavigationService.PushAsync(new VerifyAnswersPage());
        }
    }
}
