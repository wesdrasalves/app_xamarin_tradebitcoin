using Dotend.MBTrade;
using MercadoBitcoin.Helpers;
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
	public partial class PINAccess : ContentPage
	{
		public PINAccess ()
		{
			InitializeComponent ();
            Access();
        }

        async void Access()
        {


            if (!await PCLHelper.ArquivoExisteAsync(Settings.FILE_STORE))
            {
                await PCLHelper.CriarArquivo(Settings.FILE_STORE);
            }

            string _data = await PCLHelper.ReadAllTextAsync(Settings.FILE_STORE);

            if(_data == "")
            {
                await Navigation.PushModalAsync(new SettingsPage());
            }

        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            string _data = await PCLHelper.ReadAllTextAsync(Settings.FILE_STORE);

            if (_data == "")
            {
                await Navigation.PushModalAsync(new SettingsPage());
            }else
            {
                string[] _arrData = _data.Split(new string[] { Settings.SEPARATOR_DATA }, StringSplitOptions.RemoveEmptyEntries);
                MBTAPI.RenewInstance(_arrData[1], _arrData[0], myPIN.Text);
                Application.Current.MainPage = new Main();
            }
        }
    }
}