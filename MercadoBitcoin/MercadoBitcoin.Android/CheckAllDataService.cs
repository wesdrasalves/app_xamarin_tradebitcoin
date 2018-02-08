using System;
using Android.App;
using Android.Util;
using Android.Content;
using Android.OS;
using System.Threading;
using Dotend.MBTrade;

namespace MercadoBitcoin.Droid
{

    /// <summary>
    /// This is a sample started service. When the service is started, it will log a string that details how long 
    /// the service has been running (using Android.Util.Log). This service displays a notification in the notification
    /// tray while the service is active.
    /// </summary>
    [Service]
    public class CheckAllDataService : Service
    {
        static readonly string TAG = typeof(CheckAllDataService).FullName;

        bool isStarted;
        Handler handler;
        Action runnable;

        public override void OnCreate()
        {
            base.OnCreate();
            handler = new Handler();

            runnable = new Action(() =>
            {
                this.Check();
                Intent i = new Intent(Constants.NOTIFICATION_BROADCAST_ACTION);
                i.PutExtra(Constants.BROADCAST_MESSAGE_KEY, "BitMessatege");
                Android.Support.V4.Content.LocalBroadcastManager.GetInstance(this).SendBroadcast(i);
                handler.PostDelayed(runnable, Constants.DELAY_BETWEEN_LOG_MESSAGES);
            });
        }

        private void Check()
        {
            string _msg;
            if (MBUtils.Instance.CheckBigDown())
            {
                _msg = "Bit caindo rápido de: {0} para: {1}";
                
                if(TempStorage.Instance.ListBuy != null &&
                    TempStorage.Instance.ListBuy.Count > 0)
                {
                    double _value = TempStorage.Instance.ListBuy[0].Value;

                    string.Format(_msg, MBUtils.Instance.OldValueBuy, _value);
                }

                Notificate("Bitcoin Down", _msg, 1);
            }

            if (MBUtils.Instance.CheckBigUp())
            {
                _msg = "Bit subindo rápido de: {0} para: {1}";

                if (TempStorage.Instance.ListSell != null &&
                    TempStorage.Instance.ListSell.Count > 0)
                {
                    double _value = TempStorage.Instance.ListSell[0].Value;

                    string.Format(_msg, MBUtils.Instance.OldValueSell, _value);
                }

                Notificate("Bitcoin Up", _msg, 2);
            }

            if (MBUtils.Instance.CheckBuy())
            {
                _msg = "Ordem de compra executada.";

                Notificate("Bitcoin Buy", _msg, 3);
            }

            if (MBUtils.Instance.CheckSell())
            {
                _msg = "Ordem de venda executada.";

                Notificate("Bitcoin Sell", _msg, 4);
            }
        }

        private void Notificate(string Title, string Message, int Id)
        {
            NotificationManager _notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            Notification _notification = new Notification.Builder(this)
                .SetContentTitle(Title)
                .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                .SetContentText(Message)
                .SetSmallIcon(Resource.Drawable.logo)
                .Build();

            _notificationManager.Notify(Id, _notification);
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (intent.Action.Equals(Constants.ACTION_START_SERVICE))
            {
                if (isStarted)
                {
                    Log.Info(TAG, "OnStartCommand: The service is already running.");
                }
                else
                {
                    Log.Info(TAG, "OnStartCommand: The service is starting.");
                    handler.PostDelayed(runnable, Constants.DELAY_BETWEEN_LOG_MESSAGES);
                    isStarted = true;
                }
            }
            else if (intent.Action.Equals(Constants.ACTION_STOP_SERVICE))
            {
                Log.Info(TAG, "OnStartCommand: The service is stopping.");
                StopForeground(true);
                StopSelf();
                isStarted = false;

            }
            else if (intent.Action.Equals(Constants.ACTION_RESTART_TIMER))
            {
                Log.Info(TAG, "OnStartCommand: Restarting the timer.");

            }

            return StartCommandResult.Sticky;
        }


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override void OnDestroy()
        {
            // Stop the handler.
            handler.RemoveCallbacks(runnable);

            // Remove the notification from the status bar.
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Cancel(Constants.SERVICE_RUNNING_NOTIFICATION_ID);

            isStarted = false;
            base.OnDestroy();
        }

        /// <summary>
        /// Builds a PendingIntent that will display the main activity of the app. This is used when the 
        /// user taps on the notification; it will take them to the main activity of the app.
        /// </summary>
        /// <returns>The content intent.</returns>
        PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.SetAction(Constants.ACTION_MAIN_ACTIVITY);
            notificationIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);
            notificationIntent.PutExtra(Constants.SERVICE_STARTED_KEY, true);

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }

        /// <summary>
        /// Builds a Notification.Action that will instruct the service to restart the timer.
        /// </summary>
        /// <returns>The restart timer action.</returns>
        Notification.Action BuildRestartTimerAction()
        {
            var restartTimerIntent = new Intent(this, GetType());
            restartTimerIntent.SetAction(Constants.ACTION_RESTART_TIMER);
            var restartTimerPendingIntent = PendingIntent.GetService(this, 0, restartTimerIntent, 0);

            var builder = new Notification.Action.Builder(Resource.Drawable.logo,
                                              GetText(Resource.String.restart_timer),
                                              restartTimerPendingIntent);

            return builder.Build();
        }

        /// <summary>
        /// Builds the Notification.Action that will allow the user to stop the service via the
        /// notification in the status bar
        /// </summary>
        /// <returns>The stop service action.</returns>
        Notification.Action BuildStopServiceAction()
        {
            var stopServiceIntent = new Intent(this, GetType());
            stopServiceIntent.SetAction(Constants.ACTION_STOP_SERVICE);
            var stopServicePendingIntent = PendingIntent.GetService(this, 0, stopServiceIntent, 0);

            var builder = new Notification.Action.Builder(Android.Resource.Drawable.IcMediaPause,
                                                          GetText(Resource.String.stop_service),
                                                          stopServicePendingIntent);
            return builder.Build();

        }
    }
}