using Android.App;
using Android.Widget;
using Android.OS;

namespace XamarinOne
{
    [Activity(Label = "Programming Calculator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity{
        
        private Vibrator myVib;
 
 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            myVib = (Vibrator)GetSystemService(VibratorService);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            string ToBinaryString(int val){
                string hexval = val.ToString("X");
                string final = "";
                for (int i=0; i < hexval.Length; i++){
                    switch (hexval[i]){
                        case '0':
                            { final = string.Concat(final, hexval.Length==1?"0":(i==0?"":"0000")); break; }
                        case '1':
                            { final = string.Concat(final, i==0?"1":"0001"); break; }
                        case '2':
                            { final = string.Concat(final, i==0?"10":"0010"); break; }
                        case '3':
                            { final = string.Concat(final, i==0?"11":"0011"); break; }
                        case '4':
                            { final = string.Concat(final, i==0?"100":"0100"); break; }
                        case '5':
                            { final = string.Concat(final, i==0?"101":"0101"); break; }
                        case '6':
                            { final = string.Concat(final, i==0?"110":"0110"); break; }
                        case '7':
                            { final = string.Concat(final, i==0?"111":"0111"); break; }
                        case '8':
                            { final = string.Concat(final, "1000"); break; }
                        case '9':
                            { final = string.Concat(final, "1001"); break; }
                        case 'A':
                            { final = string.Concat(final, "1010"); break; }
                        case 'B':
                            { final = string.Concat(final, "1011"); break; }
                        case 'C':
                            { final = string.Concat(final, "1100"); break; }
                        case 'D':
                            { final = string.Concat(final, "1101"); break; }
                        case 'E':
                            { final = string.Concat(final, "1110"); break; }
                        case 'F':
                            { final = string.Concat(final, "1111"); break; }
                        default:
                            { break; }
                    }
                }
                return final;
            }
 
            TextView lBin = FindViewById<TextView>(Resource.Id.Bin);
            TextView lDec = FindViewById<TextView>(Resource.Id.Dec);
            TextView lHex = FindViewById<TextView>(Resource.Id.Hex);
 
            // Get our button from the layout resource,
            // and attach an event to it
            bool memheld = false;
            bool newb=true;
            int valueOne = 0;
            int valueTwo = 0;
            string mode="DEC";
            string sign="NUL";
 
            Button buttonCE         = FindViewById<Button>(Resource.Id.button1);
            Button buttonCL         = FindViewById<Button>(Resource.Id.button2);
            Button buttonMultiply   = FindViewById<Button>(Resource.Id.button3);
            Button buttonDivide     = FindViewById<Button>(Resource.Id.button4);
            Button buttonBackspace  = FindViewById<Button>(Resource.Id.button5);
            Button button7          = FindViewById<Button>(Resource.Id.button6);
            Button button8          = FindViewById<Button>(Resource.Id.button7);
            Button button9          = FindViewById<Button>(Resource.Id.button8);
            Button buttonA          = FindViewById<Button>(Resource.Id.button9);
            Button buttonB          = FindViewById<Button>(Resource.Id.button10);
            Button button4          = FindViewById<Button>(Resource.Id.button11);
            Button button5          = FindViewById<Button>(Resource.Id.button12);
            Button button6          = FindViewById<Button>(Resource.Id.button13);
            Button buttonC          = FindViewById<Button>(Resource.Id.button14);
            Button buttonD          = FindViewById<Button>(Resource.Id.button15);
            Button button1          = FindViewById<Button>(Resource.Id.button16);
            Button button2          = FindViewById<Button>(Resource.Id.button17);
            Button button3          = FindViewById<Button>(Resource.Id.button18);
            Button buttonE          = FindViewById<Button>(Resource.Id.button19);
            Button buttonF          = FindViewById<Button>(Resource.Id.button20);
            Button button0          = FindViewById<Button>(Resource.Id.button21);
            Button buttonNegate     = FindViewById<Button>(Resource.Id.button22);
            Button buttonPlus       = FindViewById<Button>(Resource.Id.button23);
            Button buttonMinus      = FindViewById<Button>(Resource.Id.button24);
            Button buttonEquals     = FindViewById<Button>(Resource.Id.button25);
            Button buttonBIN        = FindViewById<Button>(Resource.Id.buttonA);
            Button buttonDEC        = FindViewById<Button>(Resource.Id.buttonB);
            Button buttonHEX        = FindViewById<Button>(Resource.Id.buttonC);

            buttonCE.Click         += delegate {
                myVib.Vibrate(30);
                if(memheld){
                    valueTwo=0;
                    lBin.Text = ToBinaryString(valueTwo);
                    lDec.Text = valueTwo.ToString();
                    lHex.Text = valueTwo.ToString("X");
                }
                else{
                    valueOne=0;
                    lBin.Text = ToBinaryString(valueOne);
                    lDec.Text = valueOne.ToString();
                    lHex.Text = valueOne.ToString("X");
                }
            }; //Finished
            buttonCL.Click         += delegate {
                myVib.Vibrate(30);
                valueOne = 0;
                valueTwo = 0;
                lBin.Text = ToBinaryString(valueOne);
                lDec.Text = valueOne.ToString();
                lHex.Text = valueOne.ToString("X");
                sign="NUL";
                newb=true;
            }; //Finished
            buttonMultiply.Click   += delegate { 
                myVib.Vibrate(30);
                
                if(newb){
                    valueTwo=0;
                    newb=false;
                }

                if(memheld){
                    //equals
                }
                else{
                    memheld=true;
                    sign="MUL";
                }
            };
            buttonDivide.Click     += delegate {
                myVib.Vibrate(30);
                
                if(newb){
                    valueTwo=0;
                    newb=false;
                }

                if(memheld){
                    //equals
                }
                else{
                    memheld=true;
                    sign="DIV";
                }
            };
            buttonBackspace.Click  += delegate {
                myVib.Vibrate(30);
                if(mode!="BIN"){
                    if(memheld){
                        valueTwo/=(mode=="DEC")?10:16;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne/=(mode=="DEC")?10:16;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
                else{
                    if(memheld){
                        valueTwo/=2;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne/=2;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            button7.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=7;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=7;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            button8.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=8;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=8;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            button9.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=9;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=9;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            buttonA.Click          += delegate {
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(mode=="HEX"){
                    myVib.Vibrate(30);
                    if(memheld){
                        valueTwo*=16;
                        valueTwo+=10;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=16;
                        valueOne+=10;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            buttonB.Click          += delegate {
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(mode=="HEX"){
                    myVib.Vibrate(30);
                    if(memheld){
                        valueTwo*=16;
                        valueTwo+=11;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=16;
                        valueOne+=11;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            button4.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=4;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=4;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            button5.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=5;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=5;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            button6.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=6;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=6;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            buttonC.Click          += delegate {
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(mode=="HEX"){
                    myVib.Vibrate(30);
                    if(memheld){
                        valueTwo*=16;
                        valueTwo+=12;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=16;
                        valueOne+=12;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            buttonD.Click          += delegate {
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(mode=="HEX"){
                    myVib.Vibrate(30);
                    if(memheld){
                        valueTwo*=16;
                        valueTwo+=13;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=16;
                        valueOne+=13;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            button1.Click          += delegate {
                myVib.Vibrate(30);
                
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(memheld){
                    if(mode!="BIN"){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=1;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueTwo*=2;
                        valueTwo+=1;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                }
                else{
                    if(mode!="BIN"){
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=1;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                    else{
                        valueOne*=2;
                        valueOne+=1;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            button2.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=2;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=2;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            button3.Click          += delegate {
                if(mode!="BIN"){
                    myVib.Vibrate(30);

                    if(newb){
                        valueTwo=0;
                        sign="NUL";
                    }
                
                    if(memheld){
                        valueTwo*=(mode=="DEC")?10:16;
                        valueTwo+=3;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=(mode=="DEC")?10:16;
                        valueOne+=3;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    
                    }
                }
            };
            buttonE.Click          += delegate {
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(mode=="HEX"){
                    myVib.Vibrate(30);
                    if(memheld){
                        valueTwo*=16;
                        valueTwo+=14;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=16;
                        valueOne+=14;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            buttonF.Click          += delegate {
                if(newb){
                    valueTwo=0;
                    sign="NUL";
                }

                if(mode=="HEX"){
                    myVib.Vibrate(30);
                    if(memheld){
                        valueTwo*=16;
                        valueTwo+=15;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueOne*=16;
                        valueOne+=15;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            button0.Click          += delegate {
                myVib.Vibrate(30);
                if(memheld){
                    if(mode!="BIN"){
                        valueTwo*=(mode=="DEC")?10:16;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    }
                    else{
                        valueTwo*=2;
                        lBin.Text = ToBinaryString(valueTwo);
                        lDec.Text = valueTwo.ToString();
                        lHex.Text = valueTwo.ToString("X");
                    };
                }
                else{
                    if(mode!="BIN"){
                        valueOne*=(mode=="DEC")?10:16;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                    else{
                        valueOne*=2;
                        lBin.Text = ToBinaryString(valueOne);
                        lDec.Text = valueOne.ToString();
                        lHex.Text = valueOne.ToString("X");
                    }
                }
            };
            buttonNegate.Click     += delegate {
                myVib.Vibrate(30);
                if(memheld){
                    valueTwo=0-valueTwo;
                    lBin.Text = ToBinaryString(valueTwo);
                    lDec.Text = valueTwo.ToString();
                    lHex.Text = valueTwo.ToString("X");
                }
                else{
                    valueOne=0-valueOne;
                    lBin.Text = ToBinaryString(valueOne);
                    lDec.Text = valueOne.ToString();
                    lHex.Text = valueOne.ToString("X");
                }
            };
            buttonPlus.Click       += delegate {
                myVib.Vibrate(30);
                
                if(newb){
                    valueTwo=0;
                    newb=false;
                }

                if(memheld){
                    //equals
                }
                else{
                    memheld=true;
                    sign="ADD";
                }
            };
            buttonMinus.Click      += delegate {
                myVib.Vibrate(30);
                
                if(newb){
                    valueTwo=0;
                    newb=false;
                }

                if(memheld){
                    //equals
                }
                else{
                    memheld=true;
                    sign="SUB";
                }
            };
            buttonEquals.Click     += delegate {
                if(sign=="ADD"){
                    valueOne+=valueTwo;
                    memheld=false;
                    lBin.Text = ToBinaryString(valueOne);
                    lDec.Text = valueOne.ToString();
                    lHex.Text = valueOne.ToString("X");
                    newb=true;
                }
                else if(sign=="SUB"){
                    valueOne-=valueTwo;
                    memheld=false;
                    lBin.Text = ToBinaryString(valueOne);
                    lDec.Text = valueOne.ToString();
                    lHex.Text = valueOne.ToString("X");
                    newb=true;
                }
                else if(sign=="MUL"){
                    valueOne*=valueTwo;
                    memheld=false;
                    lBin.Text = ToBinaryString(valueOne);
                    lDec.Text = valueOne.ToString();
                    lHex.Text = valueOne.ToString("X");
                    newb=true;
                }
                else if(sign=="DIV"){
                    valueOne/=valueTwo;
                    memheld=false;
                    lBin.Text = ToBinaryString(valueOne);
                    lDec.Text = valueOne.ToString();
                    lHex.Text = valueOne.ToString("X");
                    newb=true;
                }
                else if(sign=="NUL"){

                }
            };
            buttonBIN.Click        += delegate{
                mode="BIN";
            };
            buttonDEC.Click        += delegate{
                mode="DEC";
            };
            buttonHEX.Click        += delegate{
                mode="HEX";
            };
           
           
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

