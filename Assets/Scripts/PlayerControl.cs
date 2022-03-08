using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speedImpulse = 9f;

    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float xVelocity = 8f;

    [SerializeField]
    private TMP_Text scoreTMP;

    [SerializeField]
    private TMP_Text messageTMP;

    private Rigidbody2D _myRb;

    private ParticleSystem _myPs;

    private bool _moving;

    private bool _gameEnded;

    public bool Moving => _moving;

    // Start is called before the first frame update
    void Start()
    {
        _myRb = GetComponent<Rigidbody2D>();
        _myPs = GetComponent<ParticleSystem>();
        messageTMP.text = "Press Space to start";
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameEnded)
            return;

        if (!Moving)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //refactor: merge this and normal Space
                _moving = true;
                messageTMP.text = "";
                _myRb.velocity = new Vector2(xVelocity, speedImpulse);
                _myPs.Emit(1);
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myRb.velocity = new Vector2(xVelocity, speedImpulse);
            _myPs.Emit(1);
        }

        scoreTMP.text = $"Score\n{Mathf.RoundToInt(transform.position.x)}";
    }

    void FixedUpdate()
    {
        if (!Moving)
            return;


        if (_myRb.velocity.y < -maxSpeed)
        {
            _myRb.velocity = new Vector2(xVelocity, -maxSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_gameEnded)
            return;

        _myPs.Emit(3);
        _gameEnded = true;
        _moving = false;
        _myRb.velocity = Vector2.zero;
        _myRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        messageTMP.text = $"Game Over!\nFinal Score: {Mathf.RoundToInt(transform.position.x)}";
        // todo: end game: restart key
    }
}