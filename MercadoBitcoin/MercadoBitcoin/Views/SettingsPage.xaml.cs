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
using MercadoBitcoin.Helpers;

namespace MercadoBitcoin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        string _pageBack = string.Empty;
		//OrdersViewModel viewModel;
		public SettingsPage()
        {
			InitializeComponent ();
			//BindingContext = viewModel = OrdersViewModel.Instance;
        }

        public SettingsPage(string pPageBack)
        {
            InitializeComponent();

            _pageBack = pPageBack;
            //BindingContext = viewModel = OrdersViewModel.Instance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.ListBuyCoin == null || viewModel.ListBuyCoin.Count == 0)
            //    viewModel.LoadDataCommad.Execute(null);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            string _data = "{0}{1}{2}";

            _data = string.Format(_data, PublicKey.Text, Settings.SEPARATOR_DATA, PrivateKey.Text);

            bool _return = await PCLHelper.WriteTextAllAsync(Settings.FILE_STORE, _data);

            await Navigation.PopModalAsync();
        }

        async void Sair_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Clear_Clicked(object sender, EventArgs e)
        {
            await PCLHelper.DeleteFile(Settings.FILE_STORE);
            await Navigation.PopModalAsync();
        }
    }
}