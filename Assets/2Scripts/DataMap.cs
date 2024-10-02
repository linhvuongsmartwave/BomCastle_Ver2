using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataMap", menuName = "DataLevel/DataMap")]
public class DataMap : ScriptableObject {
    public int level;
    public GameObject prefabMap;
}
