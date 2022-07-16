using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public GameObject TargetPos;
    public GameObject InitPos;
    
    public GameObject LoadOutPos;

    public float speed;
    private float elapsedTime;

    private bool isMoving;

    

    private bool Toggle = false;

    // Update is called once per frame
    void Update()
    {
        
        /*
        if (!isMoving)
        {
            elapsedTime = 0f;
        }
        */
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / speed;

        if (Toggle)
        {
            LoadOutPos.transform.position = Vector3.Lerp(InitPos.transform.position, TargetPos.transform.position, percentageComplete);

        }
        else if (!Toggle)
        {
            LoadOutPos.transform.position = Vector3.Lerp(LoadOutPos.transform.position, InitPos.transform.position, percentageComplete);
        }
        //Debug.Log(Toggle);
        //Debug.Log(elapsedTime);
        Debug.Log(percentageComplete);
    }
    
    public void LoadOutSliding()
    {
        if (Toggle)
        {
            Toggle = false;
        }
        else if (!Toggle)
        {
            Toggle = true;
        }
    }




}
