using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ListEnemy", menuName = "ListEnemy")]
public class ListEnemy : ScriptableObject
{

    public List<GameObject> enemies;

}
