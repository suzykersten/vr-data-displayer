using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stores data from a data csv file
public class DataPoint
{
    public string name;
    public float value;
}

public class DataFile
{
    private List<DataPoint> data;

    public DataFile(TextAsset text)
    {
        data = new List<DataPoint>();
        parseDataFromTextAsset(text);
    }

    public List<DataPoint> getData()
    {
        return data;
    }

    private void parseDataFromTextAsset(TextAsset text)
    {
        string[] lines = text.text.Split('\n');

        foreach(string line in lines)
        {
            string[] values = line.Split(',');

            DataPoint point = new DataPoint();
            point.name = values[0];
            point.value = Convert.ToSingle(values[1]);

            data.Add(point);
        }
    }
}
