using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleVisible : MonoBehaviour
{
    public void makeInvisible()
    {
         this.gameObject.SetActive(false);
    }

    public void makeVisible()
    {
        this.gameObject.SetActive(true);
    }
}
