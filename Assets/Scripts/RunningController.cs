using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

#if  UNITY_ANDROID && !UNITY_EDITOR
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
#if UNITY_ANDROID && !UNITY_EDITOR
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
#if UNITY_ANDROID && !UNITY_EDITOR
            if (plugin != null)
            {
                float[] sensorValue = plugin.Call<float[]>("getSensorValues", "stepcounter");

                if (sensorValue != null)
                {
                    if (stepCount != (int)sensorValue[0])
                    {
                        //Dispatcher.RunOnMainThread(() => {
                        //povCamera.transform.position = povCamera.transform.position + povCamera.transform.forward;
                        rb.AddForce(camera.transform.forward * 150);
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
#if UNITY_EDITOR
        // Testing for Unity Editor
        if (Input.GetButtonDown("Fire1"))
             rb.AddForce(camera.transform.forward*40);
        if(Input.GetKeyDown(KeyCode.A))
            rb.AddForce(camera.transform.right * -40);
        if (Input.GetKeyDown(KeyCode.D))
            rb.AddForce(camera.transform.right * 40);
#endif

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            restartGame();
            camera.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
