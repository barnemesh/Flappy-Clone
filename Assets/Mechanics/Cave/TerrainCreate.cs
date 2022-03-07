using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private int tileCount = 8;

    [SerializeField]
    private float initial0 = -4;

    [SerializeField]
    private int seed = 8008135;

    private List<GameObject> _tiles = new List<GameObject>();
    
    private int currentIndicated = 0;

    private float xScale;

    private float moveCur;

    // Start is called before the first frame update
    void Start()
    {
        
        xScale = tile.transform.localScale.x;
        for (var index = 0; index < tileCount; index++)
        {
            var cur = Instantiate(tile);
            cur.SetActive(true);
            _tiles.Add(cur);
            float localScaleX = (initial0 + index) * xScale;
            cur.transform.position = new Vector2(localScaleX, Perlin.perlin1D(localScaleX, seed));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCur > xScale)
        {
            moveCur -= xScale;
            // move tile to end
            // advance index
            // handle high framerate
        }
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.right;
        moveCur += 1;
    }
}