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
    [Activity(Label = "Bitty Cam", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity{
        private ImageView _imageView;
        int height;
        int width;
        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.myButton);
            if (CamActivityExists()){
                AllocateDirectory();
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                button.Click += TakeAPicture;
            }
            Button secondButton= FindViewById<Button>(Resource.Id.mySecondButton);
            secondButton.Click+= delegate{height=128; width=128; EditAndApplyImage(width,height,true);};
            Button thirdButton= FindViewById<Button>(Resource.Id.myThirdButton);
            thirdButton.Click+= delegate{height=128; width=128; EditAndApplyImage(width,height,false);};
        }
        private void AllocateDirectory(){
            App._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures),
                "JWCameraEditor");
            if (!App._dir.Exists()){
                App._dir.Mkdirs();
            }
        }
        private bool CamActivityExists(){
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
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            height = Resources.DisplayMetrics.HeightPixels;
            width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null){
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }
            GC.Collect();
        }
        private void EditAndApplyImage(int width, int height, bool a){
            if(a){
                App.bitmap = App._file.Path.Inferno(width, height);
            }
            else{
                App.bitmap = App._file.Path.EditedBitmap(width, height);
            }
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
        
        for(int x=0; x<abfusque.Width; x++){
            for(int y=0; y<abfusque.Height; y++){
                var colortemp=new Color(abfusque.GetPixel(x,y));
                float aver=((colortemp.R+colortemp.G+colortemp.B)/3);
                int aa=(int)((aver/255)*3.99);
                aa*=85;
                int err=(int)(aver)-aa;
                int newerr;
                if(abfusque.Width!=(x+1)){
                    var rightpix=new Color(abfusque.GetPixel(x+1,y));
                    newerr=(int)(err*(7/16.0));
                    abfusque.SetPixel(x+1,y,
                        Color.Argb(255,
                        (rightpix.R+newerr)>255?255:(rightpix.R+newerr)<0?0:(rightpix.R+newerr),
                        (rightpix.G+newerr)>255?255:(rightpix.G+newerr)<0?0:(rightpix.G+newerr),
                        (rightpix.B+newerr)>255?255:(rightpix.B+newerr)<0?0:(rightpix.B+newerr)));
                }
                if(x>0&&y<abfusque.Height-1){
                    var rightpix=new Color(abfusque.GetPixel(x-1,y+1));
                    newerr=(int)(err*(3/16.0));
                    abfusque.SetPixel(x-1,y+1,
                        Color.Argb(255,
                        (rightpix.R+newerr)>255?255:(rightpix.R+newerr)<0?0:(rightpix.R+newerr),
                        (rightpix.G+newerr)>255?255:(rightpix.G+newerr)<0?0:(rightpix.G+newerr),
                        (rightpix.B+newerr)>255?255:(rightpix.B+newerr)<0?0:(rightpix.B+newerr)));
                }
                if(y<abfusque.Height-1){
                    var rightpix=new Color(abfusque.GetPixel(x,y+1));
                    newerr=(int)(err*(5/16.0));
                    abfusque.SetPixel(x,y+1,
                        Color.Argb(255,
                        (rightpix.R+newerr)>255?255:(rightpix.R+newerr)<0?0:(rightpix.R+newerr),
                        (rightpix.G+newerr)>255?255:(rightpix.G+newerr)<0?0:(rightpix.G+newerr),
                        (rightpix.B+newerr)>255?255:(rightpix.B+newerr)<0?0:(rightpix.B+newerr)));
                }
                if(abfusque.Width!=(x+1)&&y<abfusque.Height-1){
                    var rightpix=new Color(abfusque.GetPixel(x+1,y+1));
                    newerr=(int)(err*(1/16.0));
                    abfusque.SetPixel(x+1,y+1,
                        Color.Argb(255,
                        (rightpix.R+newerr)>255?255:(rightpix.R+newerr)<0?0:(rightpix.R+newerr),
                        (rightpix.G+newerr)>255?255:(rightpix.G+newerr)<0?0:(rightpix.G+newerr),
                        (rightpix.B+newerr)>255?255:(rightpix.B+newerr)<0?0:(rightpix.B+newerr)));
                }
                abfusque.SetPixel(x,y,Color.Argb(255,aa,aa,aa));
            }
        }
        return abfusque;
    }
    public static Bitmap Inferno(this string fileName, int width, int height){
        Bitmap different=App._file.Path.LoadAndResizeBitmap(width,height);
        Bitmap abfusque=different.Copy(Bitmap.Config.Argb8888, true);
        for (int x=0;x<abfusque.Width;x++){
            for(int y=0;y<abfusque.Height;y++){
                var aj= new Color(abfusque.GetPixel(x,y));
                abfusque.SetPixel(x,y,Color.Argb(255,aj.R-30,aj.G-10,aj.B+10));
            }
        }
        return abfusque;
    }
    public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height){
        BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
        BitmapFactory.DecodeFile(fileName, options);
        int outHeight = options.OutHeight;
        int outWidth = options.OutWidth;
        int inSampleSize = 1;
        if (outHeight > height || outWidth > width){
            inSampleSize = (outWidth > outHeight)?(outHeight / height):(outWidth / width);
        }
        options.InSampleSize = inSampleSize;
        options.InJustDecodeBounds = false;
        Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
        return resizedBitmap;
    }
}