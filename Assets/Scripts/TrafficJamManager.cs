using UnityEngine;
using System.Collections;

public class TrafficJamManager : MonoBehaviour
{
    // how long cars  gonna stay on the stopped
    public float howLongEventLasts = 15f;

    // time between each  intraffic jam
    public float timeBetweenEvents = 45f;

    // if true traffic jam will run in a loop automatically
    public bool startAutomatically = true;

    private bool eventRunning = false;

    void Start()
    {
        // start the loop when game begins ( it runs only if checkbox is on)
        if (startAutomatically)
        {
            StartCoroutine(CooldownLoop());
        }
    }

    void Update()
    {
        //  this is TEST KEY:
        // press T to start a traffic jam manually
        if (Input.GetKeyDown(KeyCode.T) && !eventRunning)
        {
            StartCoroutine(RunEventOnce());
        }
    }

    // auto mode: wait some time, then run the event, repeat
    IEnumerator CooldownLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenEvents);
            yield return StartCoroutine(TriggerEvent());
        }
    }

    // only one time (for testing with T key)
    IEnumerator RunEventOnce()
    {
        yield return StartCoroutine(TriggerEvent());
    }

    // main logic for traffic jam
    IEnumerator TriggerEvent()
    {
        eventRunning = true;

        Debug.Log("Traffic Jam START");

        // find all cars in the scene
        CarMovement[] allCars = FindObjectsOfType<CarMovement>();

        // stop / freeze all cars
        foreach (CarMovement car in allCars)
        {
            car.SetFrozen(true);   // I already use this in NPCManager
        }

        // keep cars stopped for some seconds
        yield return new WaitForSeconds(howLongEventLasts);

        // unfreeze cars  for back to normal driving
        foreach (CarMovement car in allCars)
        {
            car.SetFrozen(false);
        }

        Debug.Log("Traffic Jam END");

        eventRunning = false;
    }
}
