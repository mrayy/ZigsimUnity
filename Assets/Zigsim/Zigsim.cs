//Simple wrapper for Zig-Sim
//Written by MHD Yamen Saraiji - yamen@myamens.com



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

public delegate void PacketReceivedHandler(object sender, string PacketData);

public class Zigsim : MonoBehaviour {
	UdpClient client;
	public int Port  = 50000;
	private IPEndPoint mRemoteIpEndPoint;
	private Socket     mListener;
	private byte[]     mReceiveBuffer;
	private string     mPacket;
	private const int  kMaxSubPacketSize       = 1400;

	bool isDone=false;
	Thread _udpGrabber;

	public event PacketReceivedHandler PacketNotification;

	// Use this for initialization
	void Start () {
		mReceiveBuffer = new byte[kMaxSubPacketSize];
		mPacket        = System.String.Empty;

		mRemoteIpEndPoint = new IPEndPoint(IPAddress.Any, Port);
		mListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		mListener.Bind(mRemoteIpEndPoint);

		mListener.Blocking          = false;
		mListener.ReceiveBufferSize = 128*1024;

		_udpGrabber = new Thread (new ThreadStart (UDPUpdate));
		_udpGrabber.Start ();

	}

	void OnDestroy()
	{
		isDone=true;
		mListener.Close ();
		_udpGrabber.Join ();
	}

	void Update()
	{
		if(data.sensordata!=null)
		transform.rotation = data.sensordata.quaternion;
	}

	void UDPUpdate()
	{
		while (!isDone) {
			
			UDPRead ();
		}

	}
	public void UDPRead()
	{
		try
		{
			int bytesReceived = mListener.Receive(mReceiveBuffer);

			mPacket = Encoding.ASCII.GetString(mReceiveBuffer, 0, bytesReceived);


			//== ok packet has been created from sub packets and is complete ==--
			Parse(mPacket);
			//== notify listeners ==--
			if(PacketNotification!=null)
				PacketNotification(this, mPacket);

		}
		catch(System.Exception ex)
		{}
	}


	public ZigJSON data=new ZigJSON();

	public bool Parse(string message)
	{
		JsonUtility.FromJsonOverwrite(message,data);

		//cheap way..
		if (message.Contains ("accel")) {
			data.sensordata.hasAccel = true;
		}else data.sensordata.hasAccel = false;

		if (message.Contains ("gyro")) {
			data.sensordata.hasGyro = true;
		}else data.sensordata.hasGyro = false;

		if (message.Contains ("gps")) {
			data.sensordata.hasGPS = true;
		}else data.sensordata.hasGPS = false;

		if (message.Contains ("quaternion")) {
			data.sensordata.hasQuaternion = true;
		}else data.sensordata.hasQuaternion = false;

		if (message.Contains ("compass")) {
			data.sensordata.hasCompass = true;
		}else data.sensordata.hasCompass = false;

		if (message.Contains ("gravity")) {
			data.sensordata.hasGravity = true;
		}else data.sensordata.hasGravity = false;

		if (message.Contains ("touch")) {
			data.sensordata.hasTouch = true;
		}else data.sensordata.hasTouch = false;

		if (message.Contains ("pressure")) {
			data.sensordata.hasPressure = true;
		}else data.sensordata.hasPressure = false;

		if (message.Contains ("miclevel")) {
			data.sensordata.hasMiclevel = true;
		}else data.sensordata.hasMiclevel = false;

		return true;
	}

}
