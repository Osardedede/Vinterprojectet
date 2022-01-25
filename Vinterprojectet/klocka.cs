using System;
using System.Threading;



public class klocka
{

    public static void tid()
    {
        Timer t = new Timer(TimerCallback, null, 0, 1000);
        Console.ReadLine();
    }

    private static void TimerCallback(Object o)
    {
        Console.WriteLine("In TimerCallback: " + DateTime.Now);
    }
}



// Uses the System.Threading namespace, which allows you access various classes '
// and interfaces that enable multi-threaded programming, like the Timer class (Line 2).

// Creates a Timer object that will call the TimerCallback function every 1000 milliseconds.
//  The null value is for the state parameter, which allows you to pass any information to be used in the callback method. The 0 is for the dueTime variable, which is the time of delay before the callback is called (Line 7).

// Waits for the user to hit Enter, which will stop the timer (Line 8).

// Creates the function TimerCallback, which will be called every 1000 milliseconds from the Timer object.
//  Object o is passed in, since the TimerCallback function requires an object parameter, even if you don’t 
// use it (Line 11).

// Logs the time in the console every second, or 1000 milliseconds (Line 12).


