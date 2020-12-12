using System;
using UnityEngine;

public class SC_Traits : MonoBehaviour {
    public enum Alignment { None, Hero, Villain, Antihero }

    public enum OriginStory { None, Tragic, Inspiring, Family }

    public enum Speciality { None, Magic, HandToHand, Weapons, Robotics, Superhuman, Powers }

    public static T GetRandomTrait<T> () where T : Enum {

        Array a = Enum.GetValues (typeof (T));
        return (T) a.GetValue (new System.Random ().Next (a.Length));

    }

}
