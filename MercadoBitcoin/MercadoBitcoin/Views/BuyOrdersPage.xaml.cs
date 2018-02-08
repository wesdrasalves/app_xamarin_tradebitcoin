using MercadoBitcoin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MercadoBitcoin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuyOrdersPage : ContentPage
	{
        OrdersViewModel viewModel;
        public BuyOrdersPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = OrdersViewModel.Instance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ListBuyCoin == null || viewModel.ListBuyCoin.Count == 0)
                viewModel.LoadDataCommad.Execute(null);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            viewModel.isOnFilter = !viewModel.isOnFilter;
        }
    }
}