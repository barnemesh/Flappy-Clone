using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    float numTiles = 60;

    [SerializeField]
    [Range(1,25)]
    private float timeFactor = 5;
    private int seed = 8008135;

    private void Start()
    {
        SetHeightByNoise(transform.position.x);
    }

    void FixedUpdate()
    {
        var localScaleX = transform.position.x;
        SetHeightByNoise(localScaleX);
    }

    private void SetHeightByNoise(float localScaleX)
    {
        var p = Perlin.Perlin2D(new Vector2(localScaleX, Time.time / timeFactor));
        transform.position = new Vector2(localScaleX, p);
    }

    private void OnBecameInvisible()
    {
        var transform1 = transform;
        float localScaleX = transform1.position.x + numTiles * transform1.localScale.x;
        SetHeightByNoise(localScaleX);
    }
}