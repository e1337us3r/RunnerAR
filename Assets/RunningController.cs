using System.Collections.Generic;
using UnityEngine;

public class RunningController : MonoBehaviour
{
    public GameObject camera;
    private AndroidJavaObject plugin;
    Rigidbody rb;
    //Thread childThread;

    int stepCount = 0;

    void Start()
    {
        
        rb = this.gameObject.GetComponent<Rigidbody>();

#if  UNITY_ANDROID
        plugin = new AndroidJavaClass("jp.kshoji.unity.sensor.UnitySensorPlugin").CallStatic<AndroidJavaObject>("getInstance");
        plugin.Call("setSamplingPeriod", 50 * 1000); // refresh sensor 50 mSec each
        plugin.Call("startSensorListening", "stepcounter");


        /*ThreadStart childref = new ThreadStart(CheckSensor);
        childThread = new Thread(childref);
        childThread.Start();*/

        InvokeRepeating("CheckSensor", 2.0f, 0.08f);
#endif
    }


    void OnApplicationQuit()
    {
#if UNITY_ANDROID
        if (plugin != null)
        {
            plugin.Call("terminate");
            plugin = null;
        }

#endif
    }



    void CheckSensor()
    {
        /*while (true)
        {
            Thread.Sleep(100);*/
            
            Debug.Log("Check sensor while start");
#if UNITY_ANDROID
            if (plugin != null)
            {
                float[] sensorValue = plugin.Call<float[]>("getSensorValues", "stepcounter");

                if (sensorValue != null)
                {
                    if (stepCount != (int)sensorValue[0])
                    {
                        //Dispatcher.RunOnMainThread(() => {
                        //povCamera.transform.position = povCamera.transform.position + povCamera.transform.forward;
                        rb.AddForce(camera.transform.forward * 250);
                        //});
                        stepCount = (int)sensorValue[0];
                    }

                    Debug.Log("sensorValue:" + string.Join(",", new List<float>(sensorValue).ConvertAll(i => i.ToString()).ToArray()));
                }
            }
#endif
            Debug.Log("Check sensor while end");
        //}
    }

    // Update is called once per frame
    void Update()
    {

        // Unity editörü üzerinden test etmek için
       /* if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("fire");
            rb.AddForce(camera.transform.forward*250);

        }*/
    }


}
