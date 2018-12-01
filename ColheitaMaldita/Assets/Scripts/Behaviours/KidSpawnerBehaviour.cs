using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawnerBehaviour : MonoBehaviour 
{
    public KidBehaviour kidPrefab;

    public KidBehaviour SendKidTo(Vector3 pos)
    {
        var go = Instantiate(kidPrefab, transform);
        go.GoToTarget(pos);
        return go;
    }

}
