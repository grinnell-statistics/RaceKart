using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public static float TopSpeedFloat;
    public static float AccelerationFloat;
    public static float AccelerationCurveFloat;
    public static float SuspensionFloat;
    public static float CoastingDragFloat ;
    public static float SteerFloat ;
    public static float GripFloat;
    public static float GravityFloat;
    // Start is called before the first frame update
    void Start()
    {
        TopSpeedFloat = float.Parse(Variables.TopSpeedString);
        AccelerationFloat = float.Parse(Variables.AccelerationString);
        AccelerationCurveFloat = float.Parse(Variables.AccelerationCurveString);
        SuspensionFloat= float.Parse(Variables.SuspensionString);
        CoastingDragFloat = float.Parse(Variables.CoastingDragString);
        SteerFloat = float.Parse(Variables.SteerString);
        GripFloat = float.Parse(Variables.GripString);
        GravityFloat = float.Parse(Variables.GravityString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
