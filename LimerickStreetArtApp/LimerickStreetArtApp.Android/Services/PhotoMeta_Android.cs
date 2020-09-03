using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LimerickStreetArtApp.Data;

namespace LimerickStreetArtApp.Droid.Services
{
    class PhotoMeta_Android : IPhotoMeta
    {
        public string GetPhotoDate(string path)
        {
            if (!File.Exists(path))
            {
               
                return "0";
            }
            ExifInterface exif = new ExifInterface(path);
            return exif.GetAttribute(ExifInterface.TagDatetime);
        }
    }
}