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
            Button chooseImage = FindViewById<Button>(Resource.Id.choosePhoto);
            chooseImage.Click+= delegate{
                var imageIntent = new Intent();
                imageIntent.SetType ("image/*");
                imageIntent.SetAction (Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(imageIntent, "Select Photo"), 1012);
            };
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
            StartActivityForResult(intent, 1011);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data){
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode==1011){
                Uri contentUri = Uri.FromFile(App._file);
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);
            }
            if(requestCode==1012){
                Uri contentUri = data.Data;
                String filePath = GetPathToImage(contentUri);

                App._file = new File(filePath);
            }
            if(resultCode==Result.Ok){
                height = Resources.DisplayMetrics.HeightPixels;
                width = _imageView.Height;
                App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
                if (App.bitmap != null){
                    _imageView.SetImageBitmap(App.bitmap);
                    App.bitmap = null;
                }
                GC.Collect();
            }
        }
        private string GetPathToImage(Android.Net.Uri uri){
            string doc_id = "";
            using (var c1 = ContentResolver.Query (uri, null, null, null, null)) {
                c1.MoveToFirst ();
                String document_id = c1.GetString (0);
                doc_id = document_id.Substring (document_id.LastIndexOf (":") + 1);
            }
            string path = null;
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ManagedQuery(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] {doc_id}, null)){
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
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

public static class Pixl{ //Custom class made for handling a pixel, one at a time
    public static int r;
    public static int g;
    public static int b;
    public static void loadPixel(int val){
        r=(val>>16)&0xFF;
        g=(val>>8)&0xFF;
        b=val&0xFF;
    }
    public static void addErr(int err){
        r=(r+err)>255?255:(r+err)<0?0:(Pixl.r+err);
        g=(g+err)>255?255:(g+err)<0?0:(Pixl.g+err);
        r=(b+err)>255?255:(b+err)<0?0:(Pixl.b+err);
    }
}
public static class BitmapHelpers{
    public static Bitmap EditedBitmap(this string fileName, int width, int height){
        
        Bitmap different=App._file.Path.LoadAndResizeBitmap(width,height);
        Bitmap imgCopy=different.Copy(Bitmap.Config.Argb8888, true);

        int[]pix = new int[imgCopy.Width*imgCopy.Height];
        different.GetPixels(pix,0,imgCopy.Width,0,0,imgCopy.Width,imgCopy.Height);
        for(int x=0; x<imgCopy.Width; x++){
            for(int y=0; y<imgCopy.Height;y++){
                int currentIndex=(y*imgCopy.Width)+x;
                Pixl.loadPixel(pix[currentIndex]);
                Pixl.r=(pix[currentIndex]>>16)&0xFF;
                Pixl.g=(pix[currentIndex]>>8)&0xFF;
                Pixl.b=pix[currentIndex]&0xFF;
                float aver =((Pixl.r+Pixl.g+Pixl.b)/3);
                int cap=((int)((aver/255)*3.99))*85;
                int err=(int)(aver)-cap;
                int newerr;
                int fcolor = (cap<<16)|(cap<<8)|(cap);
                pix[currentIndex]=fcolor;
                if(imgCopy.Width!=(x+1)){//checking if on right edge of board
                    Pixl.loadPixel(pix[currentIndex+1]);
                    newerr=(int)(err*(7/16.0));
                    Pixl.addErr(newerr);
                    pix[currentIndex+1]=(Pixl.r<<16)|(Pixl.g<<8)|(Pixl.b);
                }
                if(y<imgCopy.Height-1){ //checking if not at the bottom
                    if(x>0){ //checking if on left edge of board
                        Pixl.loadPixel(pix[currentIndex-1+imgCopy.Width]);
                        newerr=(int)(err*(3/16.0));
                        Pixl.addErr(newerr);
                        pix[currentIndex-1+imgCopy.Width]=(Pixl.r<<16)|(Pixl.g<<8)|(Pixl.b);
                    }
                    //if not at bottom
                    Pixl.loadPixel(pix[currentIndex+imgCopy.Width]);
                    newerr=(int)(err*(5/16.0));
                    Pixl.addErr(newerr);
                    pix[currentIndex+imgCopy.Width]=(Pixl.r<<16)|(Pixl.g<<8)|(Pixl.b);
                    if(imgCopy.Width!=(x+1)){//checking if not on right edge of board
                        Pixl.loadPixel(pix[currentIndex+1+imgCopy.Width]);
                        newerr=(int)(err*(1/16.0));
                        Pixl.addErr(newerr);
                        pix[currentIndex+1+imgCopy.Width]=(Pixl.r<<16)|(Pixl.g<<8)|(Pixl.b);
                    }
                }
            }
        }
        imgCopy.SetPixels(pix,0,imgCopy.Width,0,0,imgCopy.Width,imgCopy.Height);
        return imgCopy;
    }
    public static Bitmap Inferno(this string fileName, int width, int height){
        Bitmap different=App._file.Path.LoadAndResizeBitmap(width,height);
        Bitmap imgCopy=different.Copy(Bitmap.Config.Argb8888, true);
        for (int x=0;x<imgCopy.Width;x++){
            for(int y=0;y<imgCopy.Height;y++){
                var aj= new Color(imgCopy.GetPixel(x,y));
                imgCopy.SetPixel(x,y,Color.Argb(255,aj.R-30,aj.G-10,aj.B+10));
            }
        }
        return imgCopy;
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