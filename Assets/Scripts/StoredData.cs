using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoredData
{
    public int storedAttempts;
    public float[] storedRecords;
    
    public StoredData (float[] records, int attempts)
    {
        float[] rec = new float[3];
        records.CopyTo(rec, 0);
        storedRecords = rec;
        storedAttempts = attempts;
    }
    public StoredData()
    {
        storedRecords = new float[3];
        storedAttempts = 0;

    }
}
