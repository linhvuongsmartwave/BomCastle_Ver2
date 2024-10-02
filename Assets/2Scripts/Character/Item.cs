using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float moveDistance ;
    public float moveDuration;
    public float rotationDuration = 1.0f;

    void Start()
    {
        moveDistance = 0.2f;
        moveDuration = 0.5f;
        InvokeRepeating("CheckItem", 1, 1);
    }


    void CheckItem()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Collider2D bomAlready = Physics2D.OverlapBox(position, Vector2.one / 2f, 0, LayerMask.GetMask("Brick"));
        if (bomAlready != null) return;
        else
        {
            transform.DOMoveY(transform.position.y + moveDistance, moveDuration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    transform.DORotate(new Vector3(360, 0, 0), rotationDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear);

                })
                .SetLoops(-1, LoopType.Yoyo);
        }
    }



}
