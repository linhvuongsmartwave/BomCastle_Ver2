using System.Collections;
using UnityEngine;

public class BomEnemy : MonoBehaviour
{
    public int radius;
    bool isDelay = true;
    public Explosion effect;
    public float duration = 1f;
    public LayerMask effectLayer;

    void Start()
    {
        radius = 1;
        Invoke(nameof(DelayTimeOndisable), 1f);
    }
 
    private void OnDisable()
    {
        if (!isDelay)
        {
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            Explosion explosion = ObjectPooling.Instance.GetPooledObject("effectenemy").GetComponent<Explosion>();
            explosion.transform.position = position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.gameObject.SetActive(true);
            explosion.SetActiveRenderer(explosion.start);
            Explode(position, Vector2.up, radius);
            Explode(position, Vector2.down, radius);
            Explode(position, Vector2.left, radius);
            Explode(position, Vector2.right, radius);
        }
    }

    void DelayTimeOndisable()
    {
        isDelay=false;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;
        position += direction;
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0, effectLayer))
            return;

        Explosion explosion = ObjectPooling.Instance.GetPooledObject("effectenemy").GetComponent<Explosion>();
        explosion.transform.position = position;
        explosion.transform.rotation = Quaternion.identity;
        explosion.gameObject.SetActive(true);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);

        Explode(position, direction, length - 1);
    }

    private IEnumerator DeactivateAfter(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectPooling.Instance.ReturnPooledObject(obj);
    }
}
