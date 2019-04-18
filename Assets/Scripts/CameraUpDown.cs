using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 pos = this.transform.position;
        pos.y += vertical;
        this.transform.position = pos;

        this.transform.Rotate(horizontal / 2, 0, 0);

    }
}
