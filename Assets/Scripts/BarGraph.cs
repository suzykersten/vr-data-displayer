using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarGraph : MonoBehaviour
{
    public GameObject model = null;
    public TextAsset text = null;

    public GameObject GvrPointer = null;

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

            int columnNumber = 0;

            foreach(DataPoint point in dataPoints)
            {
                //holds all the data for this object
                GameObject column = new GameObject();
                column.name = "Col " + columnNumber++ + ": data-" + point.value ;
                column.transform.parent = this.transform;
                column.transform.position = new Vector3(x, y, z);

                //generate a tooltip
                if (GvrPointer != null)
                {
                    GameObject tooltip = new GameObject();
                    TextMesh textMesh = tooltip.AddComponent<TextMesh>();

                    textMesh.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    textMesh.GetComponent<Renderer>().material = textMesh.font.material;
                    textMesh.color = Color.black;
                    textMesh.text = point.name;

                    tooltip.transform.parent = GvrPointer.transform;
                    tooltip.transform.position = new Vector3(0, 0, 20);

                    //make a script to toggle the visibility of the tooltip
                    tooltip.AddComponent<toggleVisible>();
                    tooltip.SetActive(false);


                    //make box collider for tag output
                    GameObject tag = new GameObject();
                    tag.name = "col tag";
                    tag.transform.parent = column.transform;
                    tag.transform.position = new Vector3(x, y, z);

                    tag.AddComponent<BoxCollider>();

                    BoxCollider tagCollider = tag.GetComponent<BoxCollider>();

                    Bounds modelBounds = model.GetComponent<MeshFilter>().sharedMesh.bounds;

                    //size of the bar
                    float tagColliderHeight = ((modelBounds.size.y * model.transform.localScale.y)) * point.value
                        + heightBetweenRows / 2 * point.value
                        - heightBetweenRows;

                    tagCollider.size = new Vector3(modelBounds.size.x * model.transform.localScale.x,
                            tagColliderHeight,
                            modelBounds.size.z * model.transform.localScale.z);
                    tagCollider.center = new Vector3(0, tagColliderHeight / 2.0f, 0);


                    //make tooltip visible on gaze over
                    EventTrigger tagTrigger =  tag.AddComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerEnter;
                    entry.callback.AddListener((data) => { tooltip.GetComponent<toggleVisible>().makeVisible(); });
                    tagTrigger.triggers.Add(entry);

                    //and remove tooltip on gaze leave
                    EventTrigger.Entry entry2 = new EventTrigger.Entry();
                    entry2.eventID = EventTriggerType.PointerExit;
                    entry2.callback.AddListener((data) => { tooltip.GetComponent<toggleVisible>().makeInvisible(); });
                    tagTrigger.triggers.Add(entry2);

                }
                
               
               
                int row = 0;

                for (int i = 0; i < point.value; i++)
                {

                    //make the object for this increment of the dataset
                    GameObject instance = Instantiate(model);
                    instance.isStatic = true;

                    instance.transform.parent = column.transform;
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
