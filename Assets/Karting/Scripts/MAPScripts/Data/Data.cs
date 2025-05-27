using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//   Class: Data
// Purpose: contains a structure that store player and game data
public class Data : MonoBehaviour
{
    public struct datum
    {
        public System.DateTime date;
        public string playerID;
        public string groupID;
        public string levelID;
        public string level;
        public int obstacles;
        public string surface;
        public string wheel;
        public string engine;
        public string body;
        public bool win;
        public float time;
        public float topSpeed;
        public float timeTopSpeed;
        public int curve1;
        public float c1;
        public int curve2;
        public float c2;
        public int curve3;
        public float c3;
        public float maxVel;
        public float acceleration;
        public float accelCurve;
        public float suspension;
        public float drag;
        public float steer;
        public float grip;
        public float gravity;
        public int ramps;
        public string challenge1;
        public string challenge2;
        public float restart;
    }
}
