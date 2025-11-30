using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PointsManager : MonoBehaviour
{

    public Text trashScore;
    public Text healthPoints;

    int trashNum = 0;
    int healthNum = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trashScore.text = "Trash Collected: " + trashNum.ToString();
        healthPoints.text = "Health: " + healthNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
