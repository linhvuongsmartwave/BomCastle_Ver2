using System.Collections;
using UnityEngine;

public class BomControl : MonoBehaviour
{
    [Header("Bom")]
    bool isPushBom = false;
    public int bomRemaining;
    public GameObject bomPrefabs;
    public LayerMask effectLayer;
    public float bomFuseTime = 2.5f;

    [Header("Effect")]
    public int radius;
    public float duration = 1f;

    private GameObject iconPushBom;

    private void Start()
    {
        radius = 1;
        bomRemaining = 1;
        bomFuseTime = 2.5f;
        iconPushBom = GameObject.Find("ShoesFalse");
    }

    public void ShowIconPushBom()
    {
        iconPushBom.SetActive(false);
    }

    public void HideIconPushBom()
    {
        iconPushBom.SetActive(true);
    }

    public void PutBom()
    {
        if (bomRemaining > 0) StartCoroutine(PlaceBom());
    }

    public IEnumerator PlaceBom()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Collider2D bomAlready = Physics2D.OverlapBox(position, Vector2.one / 2f, 0, LayerMask.GetMask(Const.bom));
        if (bomAlready != null) yield break;
        GameObject bom = Instantiate(bomPrefabs, position, Quaternion.identity);
        bomRemaining--;
        AudioManager.Instance.CoolDown();
        yield return new WaitForSeconds(bomFuseTime);

        StartCoroutine(VibrateCamera(0.2f, 0.07f));
        AudioManager.Instance.BomExp();
        RfHolder.Instance.Vibrate();
        position = bom.transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = ObjectPooling.Instance.GetPooledObject("effectplayer").GetComponent<Explosion>();
        explosion.transform.position = position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.gameObject.SetActive(true);
        explosion.SetActiveRenderer(explosion.start);
        StartCoroutine(DeactivateAfter(explosion.gameObject, duration));
        Explode(position, Vector2.up, radius);
        Explode(position, Vector2.down, radius);
        Explode(position, Vector2.left, radius);
        Explode(position, Vector2.right, radius);

        Destroy(bom);
        bomRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;
        position += direction;
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0, effectLayer))
            return;

        Explosion explosion = ObjectPooling.Instance.GetPooledObject("effectplayer").GetComponent<Explosion>();
        explosion.transform.position = position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.gameObject.SetActive(true);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        StartCoroutine(DeactivateAfter(explosion.gameObject, duration));

        Explode(position, direction, length - 1);
    }

    private IEnumerator DeactivateAfter(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectPooling.Instance.ReturnPooledObject(obj);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPushBom && collision.gameObject.layer == LayerMask.NameToLayer(Const.bom)) collision.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.pushBom))
        {
            isPushBom = true;
            Destroy(collision.gameObject);
            ShowIconPushBom();
        }
    }

    IEnumerator VibrateCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originalPosition;
    }
}
