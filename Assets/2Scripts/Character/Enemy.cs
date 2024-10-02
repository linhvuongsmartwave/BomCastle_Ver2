using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    int bossHp = 3;
    bool isTakeDamage = true;
    public Slider sliderheath;

    public TypeEnemy typeEnemy;
    public enum TypeEnemy
    {
        boss,
        enemy
    }

    [Header("Bom")]
    public GameObject bomPrefabs;
    public float bomFuseTime = 3f;
    public int bomRemaining;

    public LayerMask obstacleLayer;
    public Vector2 movementDirection = Vector2.right;
    Vector2[] randomDirections = new Vector2[] { Vector2.down, Vector2.left, Vector2.right, Vector2.up };

    public override void Awake()
    {
        base.Awake();
        sliderheath.value = currentHealth;
        bomRemaining = 1;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnDestroyedEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.effectPlayer))
        {
            if (isTakeDamage)
            {
                StartCoroutine(nameof(HandleOnTrigger));
                if (typeEnemy == TypeEnemy.boss)
                {
                    bossHp--;
                    sliderheath.value--;
                    if (bossHp <= 0) Destroy(this.gameObject);
                }
                else Destroy(this.gameObject);
            }
        }
    }

    IEnumerator HandleOnTrigger()
    {
        isTakeDamage = false;
        yield return new WaitForSeconds(1f);
        isTakeDamage = true;

    }

    public void PutBom()
    {
        if (bomRemaining > 0)
        {
            if (GameManager.Instance.isPause) StartCoroutine(PlaceBom());
        }
    }

    public IEnumerator PlaceBom()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        Collider2D existingBom = Physics2D.OverlapBox(position, Vector2.one / 2f, 0, LayerMask.GetMask("Bom"));
        if (existingBom != null)
        {
            yield break;
        }
        GameObject bom = ObjectPooling.Instance.GetPooledObject("Bom");
        if (bom != null)
        {
            bom.transform.position = position;
            bomRemaining--;
            yield return new WaitForSeconds(bomFuseTime);
            ObjectPooling.Instance.ReturnPooledObject(bom);
            bomRemaining++;
        }
    }

    public void Update()
    {
        movement = movementDirection;
        anm.SetFloat("Horizontal", movement.x);
        anm.SetFloat("Vertical", movement.y);
        anm.SetFloat("Speed", movement.sqrMagnitude);
        CheckObstacles();
    }

    public void FixedUpdate()
    {
        if (GameManager.Instance.isPause)
            rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }

    private void CheckObstacles()
    {
        Vector2 forward = new Vector2(movement.x, movement.y).normalized;
        float distance = 0.6f;

        RaycastHit2D hitForward = Physics2D.Raycast(new Vector2(rb.position.x, rb.position.y - 0.2f), forward, distance, obstacleLayer);
        if (hitForward.collider != null)
        {
            movementDirection = NewDirection(movementDirection);
            PutBom();
        }
    }

    private Vector2 NewDirection(Vector2 currentDirection)
    {
        List<Vector2> newDirections = new List<Vector2>(randomDirections);
        newDirections.Remove(currentDirection);
        int randomIndex = Random.Range(0, newDirections.Count);
        return newDirections[randomIndex];
    }
}