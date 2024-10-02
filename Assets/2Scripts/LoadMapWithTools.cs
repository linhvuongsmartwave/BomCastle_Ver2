//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEditor;

//public class LoadMapWithTools : MonoBehaviour
//{
//    [Header("Prefabs")] public GameObject wood;
//    public GameObject nail;
//    public GameObject hole;
//    public GameObject item1;
//    public GameObject item2;
//    public GameObject item3;
//    public GameObject item4;
//    public GameObject item5;


//    [Header("Parent Spawn")] public GameObject parentSpawn;
//    public Transform parentSpawnTransform;

//    [Header("Select Object")] public Button woodButton;
//    public Button nailButton;
//    public Button holeButton;
//    public Button btnItem1;
//    public Button btnItem2;
//    public Button btnItem3;
//    public Button btnItem4;
//    public Button btnItem5;

//    [Header("Map Tools")] public Button renMap;
//    public Button createObj;
//    public Button saveMap;
//    public Button deleteObj;
//    public Button clearAll;

//    [Header("Map Size")] public int rows;
//    public int columns;

//    [Header("Current Object")] public GameObject currentObject;
//    public bool isMapRendered;
//    public GameObject selectedObject;

//    [Header("Rendered Objects")] private List<GameObject> renderedObjects = new List<GameObject>();
//    [Header("Map Name")] public InputField mapNameInputField;
//    private int lastValidInteger = 0;

//    [Header("DataMap Controller")] public DataMapController dataMapController;

//    private void Start()
//    {
//        woodButton.onClick.AddListener(() => currentObject = wood);
//        nailButton.onClick.AddListener(() => currentObject = nail);
//        holeButton.onClick.AddListener(() => currentObject = hole);
//        btnItem1.onClick.AddListener(() => currentObject = item1);
//        btnItem2.onClick.AddListener(() => currentObject = item2);
//        btnItem3.onClick.AddListener(() => currentObject = item3);
//        btnItem4.onClick.AddListener(() => currentObject = item4);
//        btnItem5.onClick.AddListener(() => currentObject = item5);

//        renMap.onClick.AddListener(RenMap);
//        saveMap.onClick.AddListener(SaveMap);
//        clearAll.onClick.AddListener(ClearAll);
//        deleteObj.onClick.AddListener(DeleteSelectedObject);
//        createObj.onClick.AddListener(CreateObjectAtMousePosition);

//        mapNameInputField.onValueChanged.AddListener(ValidateIntegerInput);
//    }

//    private void RenMap()
//    {
//        if (isMapRendered) return;

//        float offsetX = columns / 2f;
//        float offsetY = rows / 2f;

//        for (int i = 0; i < rows; i++)
//        {
//            for (int j = 0; j < columns; j++)
//            {
//                Vector3 spawnPosition = parentSpawnTransform.position + new Vector3(j - offsetX, -i + offsetY, 0);
//                GameObject obj = Instantiate(currentObject, spawnPosition, Quaternion.identity, parentSpawn.transform);
//                renderedObjects.Add(obj);
//            }
//        }

//        isMapRendered = true;
//    }

//    private void SaveMap()
//    {
//        if (string.IsNullOrEmpty(mapNameInputField.text))
//        {
//            Debug.Log("Map name is required");
//            return;
//        }
//        string directoryPath = "Assets/Prefabs/Maps";
//        if (!AssetDatabase.IsValidFolder(directoryPath))
//        {
//            AssetDatabase.CreateFolder("Assets/Prefabs", "Maps");
//        }

//        string prefabPath = directoryPath + "/" + mapNameInputField.text + ".prefab";
//        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(parentSpawn, prefabPath);

//        if (prefab == null)
//        {
//            Debug.Log("Failed to save map");
//            return;
//        }

//        Debug.Log("Map saved successfully at " + prefabPath);

//        DataMap newDataMap = ScriptableObject.CreateInstance<DataMap>();
//        newDataMap.prefabMap = prefab;
//        newDataMap.level = lastValidInteger;

//        string dataMapDirectoryPath = "Assets/Data/Maps";
//        if (!AssetDatabase.IsValidFolder(dataMapDirectoryPath))
//        {
//            AssetDatabase.CreateFolder("Assets/Data", "Maps");
//        }

//        string dataMapPath = dataMapDirectoryPath + "/Map" + mapNameInputField.text + ".asset";
//        AssetDatabase.CreateAsset(newDataMap, dataMapPath);
//        Debug.Log("DataMap saved successfully at " + dataMapPath);

//        dataMapController.dataMaps.Add(newDataMap);
//        ClearAll();
//        isMapRendered = false;
//    }

//    private void DeleteSelectedObject()
//    {
//        if (selectedObject != null)
//        {
//            renderedObjects.Remove(selectedObject);
//            Destroy(selectedObject);
//            selectedObject = null;
//        }
//    }

//    private void CreateObjectAtMousePosition()
//    {
//        Debug.Log("aaaa");
//        GameObject obj = Instantiate(currentObject, selectedObject.transform.position, Quaternion.identity,
//          parentSpawn.transform);
//        renderedObjects.Add(obj);
//    }
//    public void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            CreateObjectAtMousePosition();
//        }
//    }

//    public void ClearAll()
//    {
//        foreach (GameObject obj in renderedObjects)
//        {
//            Destroy(obj);
//        }

//        renderedObjects.Clear();
//        isMapRendered = false;
//    }

//    private void ValidateIntegerInput(string newValue)
//    {
//        if (int.TryParse(newValue, out int result))
//        {
//            lastValidInteger = result;
//        }
//        else
//        {
//            mapNameInputField.text = lastValidInteger.ToString();
//        }
//    }
//}