using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarGraph : MonoBehaviour
{
    public GameObject model = null;
    public TextAsset text = null;

    public int modelsPerRow;
    public float heightBetweenRows;

    public float widthBetweenBars = 2f;
    public float paddingInBars = .05f;


    private bool madeBarGraph = false;

    


    // Start is called before the first frame update
    void Start()
    {
       if (model == null)
        {
            throw new System.ArgumentNullException("No model for bar graph");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (!madeBarGraph)
        {
           DataFile dataFile = new DataFile(text);

            List < DataPoint > dataPoints = dataFile.getData();

            float startx, starty, startz, x, y, z;

            startx = x = this.transform.position.x;
            starty = y = this.transform.position.y;
            startz = z = this.transform.position.z;

            foreach(DataPoint point in dataPoints)
            {
                point.value = point.value / 9;
                int row = 0;

                for (int i = 0; i < point.value; i++)
                {


                    GameObject instance = Instantiate(model);
                    instance.isStatic = true;

                    instance.transform.parent = this.transform;
                    instance.transform.position = new Vector3(x, y, z);

                    x -= paddingInBars;
                    
                    row++;

                    if (row != 0 && row % modelsPerRow == 0)
                    {
                        x = startx;
                        z -= paddingInBars;
                    }

                    //check for at end of square plane
                    if (row == (modelsPerRow * modelsPerRow))
                    {
                        x = startx;
                        z = startz;
                        y +=heightBetweenRows;
                        row = 0;
                    }
                }

                x = startx + widthBetweenBars;
                startx = x;
                y = starty;
                z = startz;
            }
        }

        madeBarGraph = true;
    }
}
