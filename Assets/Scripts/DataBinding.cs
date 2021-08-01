using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * Assume that you have created the class 'Data' for some reason and you are processing some data inside it. The output is float 'data'.
 * Then you realized you have to show this variable on the screen via a UnityEngine.Text, using the class 'TextSetter' you have written.
 * It updates the text whenever the data changes.
 * Data and TextSetter classes have no means of communication. They can't use references of each other.
 * In addition, you liked the TextSetter class a lot and want to use it in different places with different types of data later on. You want to generalize your technique.
 * 
 * Writing a global manager class that handles the classes below is not an option.
 * 
 * Static access to data classes is not the answer.
 * 
 * How would you solve this? Is there a behavioural pattern that seems to be the answer?
 * 
 * You can implement anything you wish.
 * 
 * Your solution doesn't actually have to work, just make sure your solution and intentions are clear conceptually.
 * 
 */


//Answers:  Observer pattern will solve this problem
public interface IObserver
{
    // Receive update from subject
    void Update(ISubject subject, float data);
}

public interface ISubject
{   
    // Attach an observer to the subject.
    void Attach(IObserver observer);

    // Detach an observer from the subject.
    void Detach(IObserver observer);

    // Notify all observers about an event.
    void Notify();
}
public class Data : ISubject
{
    private float data = 0f;

    private List<IObserver> _observers = new List<IObserver>();

    // The subscription management methods.
    public void Attach(IObserver observer)
    {
        Console.WriteLine("Subject: Attached an observer.");
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
        Console.WriteLine("Subject: Detached an observer.");
    }

    // Trigger an update in each subscriber.
    public void Notify()
    {
        Console.WriteLine("Subject: Notifying observers...");

        foreach (var observer in _observers)
        {
            observer.Update(this, data);
        }
    }

    public void UpdateTrigger()
    {
        Update();
    }

    private void Update()
    {
        data += Time.deltaTime * 5f;
        Notify();
    }
}

public class TextSetter : IObserver
{
    [SerializeField]
    private Text text;
    public void Update(ISubject subject, float data)
    {
        Debug.Log("TextSetter is also informed " + data);
        text.text = data.ToString();
    }

    
}
