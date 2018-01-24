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
            int numOfButtons = 10;
            Button[] buttons= new Button[numOfButtons];
            for (int i = 0; i < numOfButtons; i++){
                int id;
                switch (i) {
                    case 0:
                        id = Resource.Id.buttonZero;
                        break;
                    case 1:
                        id = Resource.Id.buttonOne;
                        break;
                    case 2:
                        id = Resource.Id.buttonTwo;
                        break;
                    case 3:
                        id = Resource.Id.buttonThree;
                        break;
                    case 4:
                        id = Resource.Id.buttonFour;
                        break;
                    case 5:
                        id = Resource.Id.buttonFive;
                        break;
                    case 6:
                        id = Resource.Id.buttonSix;
                        break;
                    case 7:
                        id = Resource.Id.buttonSeven;
                        break;
                    case 8:
                        id = Resource.Id.buttonEight;
                        break;
                    case 9:
                        id = Resource.Id.buttonNine;
                        break;
                    default:
                        id = 0;
                        break;
                }
                buttons[i] = FindViewById<Button>(id);
            }
            //Button[] buttons = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate {
                label.Text = $"You clicked {count} times.";
                button.Text = string.Format("{0} clicks!", count++);
            };
            buttons[0].Click += delegate{
                count+=0;
            };
            buttons[1].Click += delegate{
                count+=1;
            };
            buttons[2].Click += delegate{
                count+=2;
            };
            buttons[3].Click += delegate{
                count+=3;
            };
            buttons[4].Click += delegate{
                count+=4;
            };
            buttons[5].Click += delegate{
                count+=5;
            };
            buttons[6].Click += delegate{
                count+=6;
            };
            buttons[7].Click += delegate{
                count+=7;
            };
            buttons[8].Click += delegate{
                count+=8;
            };
            buttons[9].Click += delegate{
                count+=9;
            };
        }
        
        
    }
}

