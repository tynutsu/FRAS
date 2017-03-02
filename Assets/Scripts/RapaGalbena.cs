﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Characters.FirstPerson;

public class RapaGalbena : MonoBehaviour {

    public GameObject FPS;      // should be draggeed in inspector and point to the FPS Controller
    public GameObject Car;      // should be draggeed in inspector and point to the car
    private GameObject Player;
    public CarUserControl CarInput;
    private Transform resetPosition;
    public List<GameObject> orderOfUI = new List<GameObject>();
    public GameObject HardLeft;
    public GameObject HardRight;
    public GameObject MediumLeft;
    public GameObject MediumRight;
    public GameObject EasyRight;
    private GameObject activeHUDDirection;
    public Text RaceTime;
    public Text Result;
    private float timer = 0.0f;
    private bool timerActive = false;
    public bool raceFinished = false;
    public Image image;
    public float alpha;

    // Use this for initialization
    void Start () {
        switch (PlayerPrefs.GetInt("GameMode"))
        {
            case 0:
                Car.gameObject.SetActive(true);
                FPS.gameObject.SetActive(false);
                Player = Car.gameObject;
                break;
            case 1:
                Car.gameObject.SetActive(false);
                FPS.gameObject.SetActive(true);
                Player = FPS.gameObject;
                break;
            default: Debug.Log("Should not arrive here, error when switching PlayerPrefs \"GameMode\"");
                break;
        }
        resetPosition = Player.transform;
        orderOfUI.Add(MediumLeft);
        orderOfUI.Add(HardRight);
        orderOfUI.Add(MediumLeft);
        orderOfUI.Add(HardLeft);
        orderOfUI.Add(MediumRight);
        orderOfUI.Add(HardLeft);
        orderOfUI.Add(HardLeft);
        orderOfUI.Add(EasyRight);
        alpha = image.color.a;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.UnloadLevel(1);
            Application.LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Pressed R");
            Player.transform.rotation = resetPosition.rotation;
            Player.transform.position = resetPosition.position;
        }

        if (timerActive)
        {
            timer += Time.deltaTime;
            RaceTime.text = "Race Time: " + getRaceTime();
        }
        if (raceFinished)
        {
            CarInput.finishRace();
            Color color = image.color;
            color.a += 0.01f;
            image.color = color;
        }
    }

    public void updateResetPosition(Transform t)
    {
        resetPosition = t;
    }

    public void Enable(GameObject ui)
    {
        if (orderOfUI.Count == 0)
        {
            return;
        }
        if (ui == orderOfUI[0])
        {
            orderOfUI[0].SetActive(true);
        }
    }

    public void Disable(GameObject ui)
    {
        if (orderOfUI.Count == 0)
        {
            return;
        }
        if (ui == orderOfUI[0] && orderOfUI[0].activeSelf)
        {
            orderOfUI[0].SetActive(false);
            orderOfUI.RemoveAt(0);
        }
    }

    public void startTimer()
    {
        timerActive = true;
    }

    public void stopTimer()
    {
        timerActive = false;
        if (orderOfUI.Count > 1)
        {
            Result.color = new Color(255, 0, 0);
            Result.text = "Invalid Race, \n You did not follow directions\n Press ESC to return to menu";
        }
        else
        {
            Result.text = "Race Completed " + getRaceTime() + "\n Press ESC to return to menu";
        }
        Result.gameObject.active = true;
        raceFinished = true;
    }

    private string getRaceTime()
    {
        var mm = (int) timer / 60;
        var ss = (int) timer % 60;
        var mmm =(int) ((timer - Mathf.Floor(timer)) *1000);
        string milliseconds = "";
        if (mmm < 10)
        {
            milliseconds = "00" + mmm.ToString();
        } else
        {
            if (mmm < 100)
            {
                milliseconds = "0" + mmm.ToString();
            } else
            {
                milliseconds = mmm.ToString();
            }
        }
        return mm.ToString() + ":" + (ss < 10 ? "0" + ss.ToString() : ss.ToString()) + ":" + milliseconds;
    }
}
