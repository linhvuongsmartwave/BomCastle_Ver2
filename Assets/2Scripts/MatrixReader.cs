using UnityEngine;

public class MatrixReader : MonoBehaviour {
  public int levelNumber;
  public int rows;
  public int columns;
  public GameObject parentSpawn;
  public GameObject[] brickPrefabs;

  public void Start() {
    LoadLevel(levelNumber);
  }

  public void LoadLevel(int levelNumber) {
    string fileName = "Level" + levelNumber;
    TextAsset textAsset = Resources.Load<TextAsset>(fileName);
    if (textAsset == null) {
      Debug.Log("File not found or not loaded correctly");
      return;
    }

    string[] lines = textAsset.text.Split('\n');
    rows = lines.Length;
    for (int i = 0; i < lines.Length; i++) {
      string[] cells = lines[i].Split(',');
      if (i == 0) {
        columns = cells.Length;
      }

      for (int j = 0; j < cells.Length; j++) {
        if (int.TryParse(cells[j], out int brickType)) {
          GameObject brick = Instantiate(brickPrefabs[brickType], new Vector3(j, -i, 0), Quaternion.identity);
          brick.transform.SetParent(parentSpawn.transform);
        }
        else {
          Debug.Log($"Cannot parse '{cells[j]}' to an integer");
        }
      }
    }
  }
}