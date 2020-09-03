using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveryBusiness_App.Data;
using DeliveryBusiness_App.Droid.Data;
using Firebase.Messaging;


[assembly: Xamarin.Forms.Dependency(typeof(FCMSubscription))]

namespace DeliveryBusiness_App.Droid.Data
{
    class FCMSubscription : IFCMSubscription
    {
        public void SubscribeAdmin()
        {
            FirebaseMessaging.Instance.SubscribeToTopic("Admin");
        }

        public void SubscribeCustomer()
        {
            FirebaseMessaging.Instance.SubscribeToTopic("Customer");
        }

        public void SubscribeDriver()
        {
            FirebaseMessaging.Instance.SubscribeToTopic("Driver");
        }

        public void SubscribeStore()
        {
            FirebaseMessaging.Instance.SubscribeToTopic("Store");
        }

        public void UnSubscribeAdmin()
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic("Admin");
        }

        public void UnSubscribeCustomer()
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic("Customer");
        }

        public void UnSubscribeDriver()
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic("Driver");
        }

        public void UnSubscribeStore()
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic("Store");
        }
    }
}