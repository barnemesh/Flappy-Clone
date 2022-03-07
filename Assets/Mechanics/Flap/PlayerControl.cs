using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float forceAdded = 9f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float xVelocity = 8f;

    private Rigidbody2D _myRB;

    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                moving = true;
                _myRB.velocity = new Vector2(-xVelocity, 0);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myRB.velocity = new Vector2(-xVelocity, forceAdded);
        }
    }

    void FixedUpdate()
    {
        if (!moving)
        {
            return;
        }

        if (_myRB.velocity.y < -maxSpeed)
        {
            _myRB.velocity = new Vector2(-xVelocity, -maxSpeed);
        }
    }
}