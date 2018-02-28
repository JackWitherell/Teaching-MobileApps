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
namespace XamarinThree{
    [Activity(Label = "Cloud Cam", MainLauncher = true, Icon = "@mipmap/icon")]public class MainActivity : Activity{
        private ImageView _imageView;
        int height;
        int width;
        TextView [] text = new TextView[10];
        List<string> words = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            
            text[0] = FindViewById<TextView>(Resource.Id.textView1);
            text[1] = FindViewById<TextView>(Resource.Id.textView2);
            text[2] = FindViewById<TextView>(Resource.Id.textView3);
            text[3] = FindViewById<TextView>(Resource.Id.textView4);
            text[4] = FindViewById<TextView>(Resource.Id.textView5);
            text[5] = FindViewById<TextView>(Resource.Id.textView6);
            text[6] = FindViewById<TextView>(Resource.Id.textView7);
            text[7] = FindViewById<TextView>(Resource.Id.textView8);
            text[8] = FindViewById<TextView>(Resource.Id.textView9);
            text[9] = FindViewById<TextView>(Resource.Id.textView10);

            setText("");

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
            Button cloudApi= FindViewById<Button>(Resource.Id.CloudApiButton);
            cloudApi.Click+= delegate{words.Clear();apiwork(App._file.Path.LoadAndResizeBitmap(128, 128));setText(words);};
        }
        private void setText(String a){
            text[0].Text = a;
            text[1].Text= text[2].Text=text[3].Text=text[4].Text=text[5].Text=text[6].Text=
                text[7].Text=text[8].Text=text[9].Text="";
        }
        private void setText(List<string> g){
            for (int i=0; i<g.Count;i++){
                text[i].Text=g[i];
            }
            for(int i=g.Count;i<10;i++){
                text[i].Text="";
            }
        }
        private void AllocateDirectory(){
            App._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures),
                "JWCameraEditor");
            if (!App._dir.Exists()){
                App._dir.Mkdirs();
            }
        }
        private bool CamActivityExists(){ //isthereanapptotakepictures
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
        private void apiwork(Android.Graphics.Bitmap bitmap){
            string bitmapString = "";
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, stream);

                var bytes = stream.ToArray();
                bitmapString = System.Convert.ToBase64String(bytes);
            }

            //credential is stored in "assets" folder
            string credPath = "google_api.json";
            Google.Apis.Auth.OAuth2.GoogleCredential cred;

            //Load credentials into object form
            using (var stream = Assets.Open(credPath))
            {
                cred = Google.Apis.Auth.OAuth2.GoogleCredential.FromStream(stream);
            }
            cred = cred.CreateScoped(Google.Apis.Vision.v1.VisionService.Scope.CloudPlatform);

            // By default, the library client will authenticate 
            // using the service account file (created in the Google Developers 
            // Console) specified by the GOOGLE_APPLICATION_CREDENTIALS 
            // environment variable. We are specifying our own credentials via json file.
            var client = new Google.Apis.Vision.v1.VisionService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApplicationName = "subtle-isotope-190917",
                HttpClientInitializer = cred
            });

            //set up request
            var request = new Google.Apis.Vision.v1.Data.AnnotateImageRequest();
            request.Image = new Google.Apis.Vision.v1.Data.Image();
            request.Image.Content = bitmapString;

            //tell google that we want to perform label detection
            request.Features = new List<Google.Apis.Vision.v1.Data.Feature>();
            request.Features.Add(new Google.Apis.Vision.v1.Data.Feature() { Type = "LABEL_DETECTION" });
            var batch = new Google.Apis.Vision.v1.Data.BatchAnnotateImagesRequest();
            batch.Requests = new List<Google.Apis.Vision.v1.Data.AnnotateImageRequest>();
            batch.Requests.Add(request);

            //send request.  Note that I'm calling execute() here, but you might want to use
            //ExecuteAsync instead
            var apiResult = client.Images.Annotate(batch).Execute();

            if (bitmap != null)
            {
                _imageView.SetImageBitmap(bitmap);
                _imageView.Visibility = Android.Views.ViewStates.Visible;
                bitmap = null;
            }

            foreach (var item in apiResult.Responses[0].LabelAnnotations){
                words.Add(item.Description);
            }
            System.GC.Collect();
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