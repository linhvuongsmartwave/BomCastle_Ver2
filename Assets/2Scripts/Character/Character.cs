using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [HideInInspector] public Animator anm;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public float speedMove;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public int currentHealth;
//    public Joystick movementJoystick;
    public CharacterData characterData;


    public virtual void Awake()
    {
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = characterData.health;
        currentHealth = maxHealth;
        speedMove = characterData.speedMove;
    }
}
