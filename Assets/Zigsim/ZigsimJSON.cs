using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Device{
	public string name="unkown";
	public string uuid="unkown";
};

[Serializable]
public class Time{
	public int year;
	public int month;
	public int day;
	public int hour;
	public int minuite;
	public float second;
	public string timeString="";
};

[Serializable]
public class Pressure{
	public float pressure;
	public float altitude;
};

[Serializable]
public class Compass{
	public bool faceup;
	public float angle;
};

[Serializable]
public class GPS{
	public float latitude;
	public float longitude;
};

[Serializable]
public class TouchPoint{
	public float x;
	public float y;
	public float radius;
	public float force;
};

[Serializable]
public class Beacon{
	public string uuid="unkown";
	public int rssi;
	public int major;
	public int minor;
};


[Serializable]
public class MicLevel{
	public float max;
	public float average;
}

[Serializable]
public class SensorData
{

	public bool hasAccel;
	public Vector3 accel;

	public bool hasGyro;
	public Vector3 gyro;

	public bool hasGravity;
	public Vector3 gravity;

	public bool hasQuaternion;
	public Quaternion quaternion;

	public bool hasCompass;
	public Compass compass;

	public bool hasPressure;
	public Pressure pressure;

	public bool hasGPS;
	public GPS gps;

	public bool hasTouch;
	public bool hasTouch2D;
	public bool hasTouchRadius;
	public bool hasTouchForce;
	public TouchPoint[] touch;

	public bool hasMiclevel;
	public MicLevel miclevel;

	public bool hasBeacon;
	public Beacon[] beacons;
}

[Serializable]
public class ZigJSON{
	
	public Device device;
	public Time time;

	public SensorData sensordata;
	public ZigJSON()
	{
	}


};
