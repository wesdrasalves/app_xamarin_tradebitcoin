using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Android.Util;
using System;
using System.Threading;

namespace MercadoBitcoin.Droid
{

    [Activity(Label = "MercadoBitcoin.Android", Theme = "@style/MyTheme", MainLauncher = false , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Intent startServiceIntent;
        Intent stopServiceIntent;
                

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            
            LoadApplication(new App());

            startServiceIntent = new Intent(this, typeof(CheckAllDataService));
            startServiceIntent.SetAction(Constants.ACTION_START_SERVICE);

            stopServiceIntent = new Intent(this, typeof(CheckAllDataService));
            stopServiceIntent.SetAction(Constants.ACTION_STOP_SERVICE);

            StartService(startServiceIntent);
        }

    }
}