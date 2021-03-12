using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace daleWebAuth.Services.Navigation
{
    public interface INavigationService
    {
        Task PushAsync(Page page, object parameter);
        Task PushAsync(Page page);
        Task PushModalAsync(Page page, object parameter, bool animation);
    }
}
