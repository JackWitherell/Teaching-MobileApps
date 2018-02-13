## Bitty Cam

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

Bitty Cam is an android powered image effects application that allows you take a picture and apply one of two filters onto it. I wrote it with this GameBoy filter I wrote in another language as a winter break project because I wanted to have the ability to use this filter on the go. The other filter I used was found (stumbled upon) by accident. Just a happy little accident I guess.

Both filters degrade image quality greatly (intentionally) as part of making look like a lower resolution image.

This app does a few complex things (mainly with help from the Xamarin Recipe listed here): https://developer.xamarin.com/recipes/android/other_ux/camera_intent/take_a_picture_and_save_using_camera_app/

After an image is imported, either Inferno or Gameboy can be chosen.

# How does the Gameboy filter work?

[![Gameboy Filter](https://i.imgur.com/rX7zDZ4.png)](https://i.imgur.com/rX7zDZ4.png)

I implemented Floyd-Steinberg Dithering which you can see an example of (along with the tutorial I followed) here: https://www.youtube.com/watch?v=0L2n8Tg2FwI

You can see a video where I originally implemented this dithering method here: https://youtu.be/efzJUmbCNoI?t=8m

# How does the Inferno filter work?

[![Inferno Filter](https://i.imgur.com/CWqjbPS.png)](https://i.imgur.com/CWqjbPS.png)

By subtracting some values from Red and Green, and adding values to Blue while completely disregarding offsets and limits. It really screws with the color values and the result is actually pretty unique.

## System Design

Android 5.0 Targeted
Probably best to use the app in portrait mode.

## Usage

[![Screenshot](https://i.imgur.com/ShB4QjR.png)](https://i.imgur.com/ShB4QjR.png)

When you open the app, you'll be greeted by an ImageView and three buttons.
If you click the uppermost button, you'll be shown the Camera activity. After taking and accepting an image you can click one of the other two buttons below and the effect will be applied to the image.