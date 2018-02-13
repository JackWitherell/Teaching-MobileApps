## Post Mortem
After finishing this project I felt accomplished in a way much unlike the first project for this class. In the first project, I was able to write a lot of code for a complex program that had lots of useful features to me. This program on the other hand had a strange, esoteric effect on my ability to write strong, meaningful code. I spent more time wrestling with the android API than I did getting the more complex parts of the program working.

I knew I would have the same kinds of issues working on a camera-activity program that I've had when attempting complex program development in the past. The intent system along with the image storage system was esoteric and strange and I spent most of the time reading it and trying to process what most of it meant. It seems like the abstraction layer android is built on requires even more abstraction to properly handle tasks I would implement in completely different ways.

I spent so much time wrestling with the system trying to comprehend it that about a day before the assignment was due I made a commit called "Dumped Entire Codebase" and tossed all of my changes past the base Camera recipe tutorial on Xamarin's website.

When I got working again and ignored the stuff past the tutorial, I started working on the core of the program, the filters.

These weren't hard to do and I implemented an old image filter I had made a while back in a different language. This was easy to implement but I had to put in more edge cases to protect against color out of bounds errors.

Overall this project was really cool and I wish I had enough patience to create a save image subroutine, but due to the way I set this up (and completely disregarded a scaling algorithm, the pictures would have been tiny and almost unusable.

I think that this assignment would have been made easier if there was a more streamlined way of implementing image capture between xamarin and android studio. It was very difficult to build and develop the app with only an intermediate level of coding experience and rare brushes with expert topics. The abstraction involved in these kinds of library implementations take a lot out of a person and it was very difficult to get done.

I'm only so glad of the result as I am glad to overcome one of the harder parts of mobile app development.