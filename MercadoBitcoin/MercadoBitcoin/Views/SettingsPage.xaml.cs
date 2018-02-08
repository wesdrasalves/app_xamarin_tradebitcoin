using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoBitcoin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Dotend.MBTrade.DTO;
using Dotend.MBTrade;

namespace MercadoBitcoin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		//OrdersViewModel viewModel;
		public SettingsPage()
        {
			InitializeComponent ();
			//BindingContext = viewModel = OrdersViewModel.Instance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.ListBuyCoin == null || viewModel.ListBuyCoin.Count == 0)
            //    viewModel.LoadDataCommad.Execute(null);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
             
        }
    }
}