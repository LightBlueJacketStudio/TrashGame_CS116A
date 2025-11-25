using UnityEngine;
using UnityEngine.UI;   //  this is for Image
using System.Collections;

public class HallucinationManager : MonoBehaviour
{
    // how long the hallucination stays on screen
    public float howLongEventLasts = 10f;

    // how long to wait before next hallucination
    public float timeBetweenEvents = 70f;

    // if true, it will run automatically in a loop
    public bool startAutomatically = true;

    // this is our purple UI Image on the Canvas
    public Image hallucinationOverlay;

    private bool eventRunning = false;

    void Start()
    {
        // start loop automatically if checkbox is on
        if (startAutomatically)
        {
            StartCoroutine(CooldownTimer());
        }
    }

    void Update()
    {
        //  this is the TEST KEY:
        // press H to manually start the hallucination
        if (Input.GetKeyDown(KeyCode.H) && !eventRunning)
        {
            StartCoroutine(RunEventOnce());
        }
    }

    // loop for auto mode wait, run event, and  repeat
    IEnumerator CooldownTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenEvents);
            yield return StartCoroutine(TriggerEvent());
        }
    }

    // used when we just want to test it one time
    IEnumerator RunEventOnce()
    {
        yield return StartCoroutine(TriggerEvent());
    }

    // main event logic
    IEnumerator TriggerEvent()
    {
        if (hallucinationOverlay == null)
        {
            Debug.LogWarning("HallucinationManager: overlay is not set!");
            yield break;
        }

        eventRunning = true;
        Debug.Log("Hallucination START");

        // fade overlay in (0 → 0.6 alpha)
        yield return StartCoroutine(FadeOverlay(0f, 0.6f, 0.5f));

        // keep hallucination active
        yield return new WaitForSeconds(howLongEventLasts);

        // fade overlay out (0.6 → 0 alpha)
        yield return StartCoroutine(FadeOverlay(0.6f, 0f, 0.5f));

        Debug.Log("Hallucination END");

        eventRunning = false;
    }

    // helper that slowly changes overlay transparency
    IEnumerator FadeOverlay(float from, float to, float duration)
    {
        float t = 0f;
        Color baseColor = hallucinationOverlay.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / duration);
            hallucinationOverlay.color = new Color(baseColor.r, baseColor.g, baseColor.b, a);
            yield return null;
        }
    }
}
