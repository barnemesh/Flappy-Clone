using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTileControl : MonoBehaviour
{
    private CaveTileManager _manager;
    private void Start()
    {
        _manager = transform.parent.GetComponent<CaveTileManager>();
        SetHeightByNoise(transform.position.x);
    }

    void FixedUpdate()
    {
        var localScaleX = transform.position.x;
        SetHeightByNoise(localScaleX);
    }

    private void SetHeightByNoise(float localScaleX)
    {
        var p = Perlin.Perlin2D(new Vector2(localScaleX, Time.time / _manager.TimeFactor), _manager.Seed);
        transform.position = new Vector2(localScaleX, _manager.BumpScale * p);
    }

    private void OnBecameInvisible()
    {
        var transform1 = transform;
        float localScaleX = transform1.position.x + (_manager.TileCount) * transform1.localScale.x;
        SetHeightByNoise(localScaleX);
    }
}
