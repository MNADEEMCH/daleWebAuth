using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace daleWebAuth.Pages.Other
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void IssueDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            IssueDateEnty.Text = e.NewDate.ToString("dd/MM/yyyy");
        }

        private void DoBPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DoBEntry.Text = e.NewDate.ToString("dd/MM/yyyy");
        }

        private void IssueDateEnty_Focused(object sender, FocusEventArgs e)
        {
            IssueDatePicker.Focus();
        }

        private void DoBEntry_Focused(object sender, FocusEventArgs e)
        {
            DoBPicker.Focus();
        }
    }
}