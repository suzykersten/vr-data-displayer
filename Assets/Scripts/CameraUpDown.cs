using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraUpDown : MonoBehaviour
{

    public float speed = 25f;

    private bool doMoveUp = true;
    private bool doMoveDown = false;

    private float startY;

    public void setMoveUp(bool value)
    {
        doMoveUp = value;
        doMoveDown = !value;
    }
    
    public void setMoveDown(bool value)
    {
        doMoveUp = !value;
        doMoveDown = value;
    }

    private void Start()
    {
        startY = transform.position.y;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {

            if (doMoveUp)
            {
                float vertical = speed * Time.deltaTime;

                this.transform.Translate(0, vertical, 0);
            }

            if (doMoveDown && this.transform.position.y > startY)
            {
                float vertical = -speed * Time.deltaTime;

                this.transform.Translate(0, vertical, 0);
            }

        }
        

    }

}
