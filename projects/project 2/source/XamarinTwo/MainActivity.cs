using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Java.IO;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using Android.Content.PM;
using System;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
namespace XamarinTwo{
    [Activity(Label = "XamarinTwo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity{
        private ImageView _imageView;
        int height;
        int width;
        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.myButton);
            if (IsThereAnAppToTakePictures()){
                CreateDirectoryForPictures();
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                button.Click += TakeAPicture;
            }
            Button secondButton= FindViewById<Button>(Resource.Id.mySecondButton);
            secondButton.Click+= delegate{EditAndApplyImage(width,height);};
            // Get our button from the layout resource,
            // and attach an event to it
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
        private void CreateDirectoryForPictures(){
            App._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures),
                "CameraAppDemo");
            if (!App._dir.Exists()){
                App._dir.Mkdirs();
            }
        }
        private bool IsThereAnAppToTakePictures(){
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        private void TakeAPicture(object sender, EventArgs eventArgs){
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data){
            base.OnActivityResult(requestCode, resultCode, data);
            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.
            height = Resources.DisplayMetrics.HeightPixels;
            width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null){
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }
            GC.Collect(); //remove java's bitmap
        }
        private void EditAndApplyImage(int width, int height){
            App.bitmap = App._file.Path.EditedBitmap(width, height);
            if(App.bitmap != null){
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap=null;
            }
            GC.Collect();
        }
    }
}
public static class App{
    public static File _file;
    public static File _dir;
    public static Bitmap bitmap;
}
public static class BitmapHelpers{
    public static Bitmap EditedBitmap(this string fileName, int width, int height){
        Bitmap different=App._file.Path.LoadAndResizeBitmap(width,height);
        Bitmap abfusque=different.Copy(Bitmap.Config.Argb8888, true);
        for (int x=0;x<100;x++){
            for(int y=0;y<100;y++){
                abfusque.SetPixel(x,y,Color.Argb(255,55,55,55));
            }
        }
        return abfusque;
    }
    public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height){
        // First we get the the dimensions of the file on disk
        BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
        BitmapFactory.DecodeFile(fileName, options);
        // Next we calculate the ratio that we need to resize the image by
        // to fit the requested dimensions.
        int outHeight = options.OutHeight;
        int outWidth = options.OutWidth;
        int inSampleSize = 1;
        if (outHeight > height || outWidth > width){
            inSampleSize = (outWidth > outHeight)?(outHeight / height):(outWidth / width);
        }
        // Now we will load the image and have BitmapFactory resize it for us.
        options.InSampleSize = inSampleSize;
        options.InJustDecodeBounds = false;
        Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
        return resizedBitmap;
    }
}