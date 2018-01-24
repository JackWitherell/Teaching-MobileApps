using Android.App;
using Android.Widget;
using Android.OS;

namespace XamarinOne
{
    [Activity(Label = "PostFix Calculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private Vibrator myVib;
 
 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            myVib = (Vibrator)GetSystemService(VibratorService);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
 
            // Get our button from the layout resource,
            // and attach an event to it
 
            Button buttonCE = FindViewById<Button>(Resource.Id.button1);
            Button buttonC = FindViewById<Button>(Resource.Id.button2);
            Button buttonBackspace = FindViewById<Button>(Resource.Id.button3);
            Button buttonLeftParen = FindViewById<Button>(Resource.Id.button4);
            Button buttonDivide = FindViewById<Button>(Resource.Id.button5);
            Button button7 = FindViewById<Button>(Resource.Id.button6);
            Button button8 = FindViewById<Button>(Resource.Id.button7);
            Button button9 = FindViewById<Button>(Resource.Id.button8);
            Button buttonRightParen = FindViewById<Button>(Resource.Id.button9);
            Button buttonMultiply = FindViewById<Button>(Resource.Id.button10);
            Button button4 = FindViewById<Button>(Resource.Id.button11);
            Button button5 = FindViewById<Button>(Resource.Id.button12);
            Button button6 = FindViewById<Button>(Resource.Id.button13);
            Button buttonNAV = FindViewById<Button>(Resource.Id.button14);
            Button buttonMinus = FindViewById<Button>(Resource.Id.button15);
            Button button1 = FindViewById<Button>(Resource.Id.button16);
            Button button2 = FindViewById<Button>(Resource.Id.button17);
            Button button3 = FindViewById<Button>(Resource.Id.button18);
            Button buttonNA = FindViewById<Button>(Resource.Id.button19);
            Button buttonPlus = FindViewById<Button>(Resource.Id.button20);
            Button buttonNegate = FindViewById<Button>(Resource.Id.button21);
            Button buttonDot = FindViewById<Button>(Resource.Id.button22);
            Button button0 = FindViewById<Button>(Resource.Id.button23);
            Button buttonNAG = FindViewById<Button>(Resource.Id.button24);
            Button buttonEquals = FindViewById<Button>(Resource.Id.button25);
            buttonCE.Click += delegate { myVib.Vibrate(30); };
            buttonC.Click += delegate { myVib.Vibrate(30); };
            buttonBackspace.Click += delegate { myVib.Vibrate(30); };
            buttonLeftParen.Click += delegate { myVib.Vibrate(30); };
            buttonDivide.Click += delegate { myVib.Vibrate(30); };
            button7.Click += delegate { myVib.Vibrate(30); };
            button8.Click += delegate { myVib.Vibrate(30); };
            button9.Click += delegate { myVib.Vibrate(30); };
            buttonRightParen.Click += delegate { myVib.Vibrate(30); };
            buttonMultiply.Click += delegate { myVib.Vibrate(30); };
            button4.Click += delegate { myVib.Vibrate(30); };
            button5.Click += delegate { myVib.Vibrate(30); };
            button6.Click += delegate { myVib.Vibrate(30); };
            buttonNAV.Click += delegate { myVib.Vibrate(30); };
            buttonMinus.Click += delegate { myVib.Vibrate(30); };
            button1.Click += delegate { myVib.Vibrate(30); };
            button2.Click += delegate { myVib.Vibrate(30); };
            button3.Click += delegate { myVib.Vibrate(30); };
            buttonNA.Click += delegate { myVib.Vibrate(30); };
            buttonPlus.Click += delegate { myVib.Vibrate(30); };
            buttonNegate.Click += delegate { myVib.Vibrate(30); };
            buttonDot.Click += delegate { myVib.Vibrate(30); };
            button0.Click += delegate { myVib.Vibrate(30); };
            buttonNAG.Click += delegate { myVib.Vibrate(30); };
            buttonEquals.Click += delegate { myVib.Vibrate(30); };
 
            /*<Button
              android:id="@+id/myButton"
              android:layout_width="match_parent"
              android:layout_height="wrap_content"
              android:text="@string/hello" />
            */
            //Button button = FindViewById<Button>(Resource.Id.myButton);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

