using daleWebAuth.Pages;
using daleWebAuth.ViewModels.Base;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace daleWebAuth.ViewModels.Other
{
    public class VerifyAnswersViewModel : ViewModelBase
    {
        public VerifyAnswersViewModel()
        {

        }

        ICommand _verifyCommand;
        public ICommand DoneCommand => _verifyCommand ??= new AsyncCommand(ExecuteDoneCommand);

        public async Task ExecuteDoneCommand()
        {
            await NavigationService.PushAsync(new Pages.Other.SuccessPage());
        }
    }
}
