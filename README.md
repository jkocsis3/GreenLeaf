# GreenLeaf Journal

This is a side project of mine I am currently working on.  I am creating a gardening journal to allow gardeners to track the process of thier plants and gardens.  This is intended to be a iOS, UWP, and Android application.  I initially started this project using the Xamarin.Forms methodology, but found it is lacking in a few key areas.  I decided then to create a shared C# code base and implement individual views for each device.

The application has the ability to track genetic information about the plant, take progress reports (including images) whenever needed. I am hoping to institute a very lighweight neural network to help identify plant disease of insect issues.  It can forecast when the plant will shift into each stage of life. Finally, all information is stored in a SQLite database which is schema version controlled to allow future updates to the system without losing any existing data.

I also want to use this project to increase my knowledge of the Android and iOS platforms.
