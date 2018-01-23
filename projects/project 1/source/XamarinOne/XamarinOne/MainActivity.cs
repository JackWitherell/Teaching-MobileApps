using Android.App;
using Android.Widget;
using Android.OS;

namespace XamarinOne
{
    [Activity(Label = "PostFix Calculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var label = FindViewById<TextView>(id:Resource.Id.textView1);

            Button button = FindViewById<Button>(Resource.Id.myButton);
            Button[] buttons = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate {
                label.Text = $"You clicked {count} times.";
                button.Text = string.Format("{0} clicks!", count++);
            };
        }
    }
}

