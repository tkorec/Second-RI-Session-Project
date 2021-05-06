using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unock : MonoBehaviour
{
    public bool isFingerPrintSet;
    public bool isLockOpen;
    Light ledDiod;
    public bool timerIsRunning = false;
    public float timeRemaining;
    public int randomNumber;
    public GameObject gameObjectFoundBySearch;
    public float startTime = 0f;
    public float timer = 0f;
    public float holdTime = 5.0f;
    public bool held = false;
    public bool preparedForSetting;
    public int successfullEffort;
    public bool settingTimerIsRunning = false;
    public int settingRandomNumber;
    
    

    // Start is called before the first frame update
    void Start()
    {
        ledDiod = GetComponent<Light>();
        gameObjectFoundBySearch = GameObject.Find("finger_lock");
        isFingerPrintSet = false;
        isLockOpen = false;
        preparedForSetting = false;
        successfullEffort = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            startTime = Time.time;
            timer = startTime;
            if (isFingerPrintSet == false && isLockOpen == false)
            {
                timeRemaining = 1.0f;
                timerIsRunning = true;
                ledDiod.color = Color.blue;
                ledDiod.enabled = !ledDiod.enabled;
                Animation animation = gameObjectFoundBySearch.GetComponent<Animation>();
                animation.Play();
                isLockOpen = true;
            }
            else if (isFingerPrintSet == false && isLockOpen == true && preparedForSetting == false)
            {
                timeRemaining = 1.0f;
                timerIsRunning = true;
                ledDiod.color = Color.blue;
                ledDiod.enabled = !ledDiod.enabled;
            }
            else if (preparedForSetting == true && isFingerPrintSet == false)
            {
                settingRandomNumber = Random.Range(1, 10);
                if (settingRandomNumber > 3)
                {
                    ledDiod.color = Color.green;
                    timeRemaining = 1.0f;
                    settingTimerIsRunning = true;
                    successfullEffort += 1;
                    //isSet(successfullEffort);
                }
                else
                {
                    ledDiod.color = Color.red;
                    timeRemaining = 1.0f;
                    settingTimerIsRunning = true;
                }
            }
            else if (isFingerPrintSet == true)
            {
               timeRemaining = 1.0f;
               timerIsRunning = true;
               randomNumber = Random.Range(1, 10);
               if (randomNumber > 3)
               {
                   ledDiod.color = Color.green;
                   ledDiod.enabled = !ledDiod.enabled;
                   Animation component = gameObjectFoundBySearch.GetComponent<Animation>();
                   component.Play();
               }
               else
               {
                  ledDiod.color = Color.red;
                  ledDiod.enabled = !ledDiod.enabled;
               } 
            }
        } 

        if (Input.GetKey("space") && held == false)
        {
            timer += Time.deltaTime;
            if (timer > (startTime + holdTime))
            {
                held = true;
                ledDiod.color = Color.blue;
                ledDiod.enabled = !ledDiod.enabled;
                preparedForSetting = true;
            }
        }

        if (Input.GetKeyUp("space"))
        {
            held = false;
        } 
            
        
              
        
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("time has run out");
                ledDiod.enabled = !ledDiod.enabled;
            }
        } 

        if (settingTimerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                settingTimerIsRunning = false;
                ledDiod.color = Color.blue;
                isSet(successfullEffort);
            }
        } 
    }

    void isSet(int count)
    {
        if (count == 5)
        {
            isFingerPrintSet = true;
            Debug.Log("Lock is set");
            ledDiod.enabled = !ledDiod.enabled;
        }
    }
}
