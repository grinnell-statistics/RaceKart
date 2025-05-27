using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//   Class: Variables
// Purpose: stores variables of car stats
public class Variables : MonoBehaviour
{
    public Text TopSpeed;
    public Text Acceleration;
    public Text AccelerationCurve;
    public Text Suspension;
    public Text CoastingDrag;
    public Text Steer;
    public Text Grip;
    public Text Gravity;
    public static string TopSpeedString;
    public static string AccelerationString;
    public static string AccelerationCurveString;
    public static string SuspensionString;
    public static string CoastingDragString;
    public static string SteerString;
    public static string GripString;
    public static string GravityString;

    public void Select()
    {
        TopSpeedString = TopSpeed.text.ToString();
        AccelerationString = Acceleration.text.ToString();
        AccelerationCurveString = AccelerationCurve.text.ToString();
        SuspensionString = Suspension.text.ToString();
        CoastingDragString = CoastingDrag.text.ToString();
        SteerString = Steer.text.ToString();
        GripString = Grip.text.ToString();
        GravityString = Gravity.text.ToString();
    }
}
