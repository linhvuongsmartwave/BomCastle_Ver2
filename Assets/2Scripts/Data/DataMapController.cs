using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataMapController", menuName = "Data/DataMapController")]
public class DataMapController : ScriptableObject {
  public List<DataMap> dataMaps;
}