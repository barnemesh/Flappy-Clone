using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    [SerializeField]
    private int tileCount = 16;

    // Start is called before the first frame update
    void Start()
    {
        var parent = new GameObject();
        var xScale = tile.transform.localScale.x;
        for (var index = 0; index < tileCount; index++)
        {
            float localScaleX = (-tileCount / 2 + index) * xScale;
            var cur = Instantiate(tile, new Vector3(localScaleX, 0, 0), Quaternion.identity, parent.transform);
            cur.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        transform.position += 0.1f * Vector3.right;
    }
}