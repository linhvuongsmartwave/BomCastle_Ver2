//using UnityEngine;

//public class ClickDetector : MonoBehaviour
//{
//    public LoadMapWithTools loadMapWithTools;

//    private void OnValidate()
//    {
//        if (loadMapWithTools == null)
//        {
//            loadMapWithTools = FindObjectOfType<LoadMapWithTools>();
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButton(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
//            if (hit.collider != null)
//            {
//                loadMapWithTools.selectedObject = hit.transform.gameObject;
//            }
//        }
//    }
//}