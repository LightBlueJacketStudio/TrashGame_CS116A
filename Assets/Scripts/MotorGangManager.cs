using UnityEngine;
using System.Collections;

public class MotorGangManager : MonoBehaviour
{
    // how long NPCs  going stay frozen
    public float howLongEventLasts = 5f;

    // how long to wait between each motor gang attack
    public float timeBetweenEvents = 60f;

    // if true  event will start by itself
    public bool startAutomatically = true;

    // this lets us talk to the NPCManager 
    public NPCManager npcManager;

    private bool eventRunning = false;

    void Awake()
    {
        // if I forget to drag npcManager in Inspector,
        // Unity will try to find it in the scene
        if (npcManager == null)
        {
            npcManager = FindObjectOfType<NPCManager>();
        }
    }

    void Start()
    {
        // if auto mode is on then gonna  start the loop
        if (startAutomatically)
        {
            StartCoroutine(CooldownTimer());
        }
    }

    void Update()
    {
        //  this is press G on keyboard to test the event
        if (Input.GetKeyDown(KeyCode.G) && !eventRunning)
        {
            StartCoroutine(RunEventOnce());
        }
    }

    IEnumerator CooldownTimer()
    {
        while (true)
        {
            // wait X seconds before doing the event again
            yield return new WaitForSeconds(timeBetweenEvents);

            // running  the event
            yield return StartCoroutine(TriggerEvent());
        }
    }

    IEnumerator RunEventOnce()
    {
        //  this is the testing  mode: run event only 1 time
        yield return StartCoroutine(TriggerEvent());
    }

    IEnumerator TriggerEvent()
    {
        // if we don't have npcManager  don't run  the event
        if (npcManager == null)
        {
            Debug.LogWarning("MotorGangManager: npcManager is not set!");
            yield break;
        }

        eventRunning = true;

        Debug.Log("Motor Gang START - freezing NPCs and cars");

        // freeze everything NPCs + cars)
        npcManager.FreezeAllNPCs();

        // keep frozen for event time
        yield return new WaitForSeconds(howLongEventLasts);

        Debug.Log("Motor Gang END - unfreezing NPCs and cars");

        // unfreeze everything
        npcManager.UnfreezeAllNPCs();

        eventRunning = false;
    }
}