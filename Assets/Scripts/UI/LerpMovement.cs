using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public GameObject TargetPos;
    public GameObject InitPos;
    
    public GameObject LoadOutPos;

    private float speed = 0.05f;

    private bool Toggle = false;

    // Update is called once per frame
    void Update()
    {
        

        if (Toggle)
        {
            
            LoadOutPos.transform.position = Vector2.Lerp(LoadOutPos.transform.position, TargetPos.transform.position, speed);

        }
        else if (!Toggle)
        {
            LoadOutPos.transform.position = Vector2.Lerp(LoadOutPos.transform.position, InitPos.transform.position, speed);
        }
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
