using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTileManager : MonoBehaviour
{
    [SerializeField]
    private PlayerControl player;

    [SerializeField]
    private GameObject tile;

    [SerializeField]
    private GameObject obstacle;

    [SerializeField]
    private int tileCount = 90;

    [SerializeField]
    private int seed = 8008135;

    [SerializeField]
    private int gridSize = 100;

    [SerializeField]
    [Range(0.001f, 40)]
    private float timeFactor = 1;

    [SerializeField]
    [Range(0, 10)]
    private float bumpScale;

    private float _curBumpSpeed;

    //todo: remove this: change to time factor is not consistently good.
    // [SerializeField]
    // [Range(0, 1)]
    // private float difficultyStep = 0.1f;
    //
    // [SerializeField]
    // private float difficultyLength = 7f;
    //
    // private float _curTimeInLevel;
    // private float _curPercentDifficulty;
    // private float _curSmoothT;
    //
    // private bool _updateDifficulty;
    // private float _oldTimeFactor;
    // private float _targetTimeFactor;
    public int TileCount => tileCount;

    public int Seed => seed;

    public float TimeFactor => timeFactor;

    public float BumpScale => bumpScale;


    // Start is called before the first frame update
    void Start()
    {
        // todo: play with the grid size?
        Perlin.GRID_SIZE = gridSize;
        float size = Camera.main.orthographicSize * (32f / 9f);
        var xScale = size / tileCount;

        for (var index = -TileCount / 2; index <= TileCount / 2; index++)
        {
            float localScaleX = index * xScale;
            var cur = Instantiate(tile, new Vector3(localScaleX, 0, 0), Quaternion.identity, transform);
            var localScale = cur.transform.localScale;
            cur.transform.localScale = new Vector3(xScale, localScale.y, localScale.z);
            cur.name = $"Tile {index + TileCount / 2}";
            cur.SetActive(true);
        }
    }

    private void Update()
    {
        if (!player.Moving) // todo: switch to gameEnded
            return;

        bumpScale = Mathf.SmoothDamp(bumpScale, 10, ref _curBumpSpeed, 60f, 0.3f);

        //
        // if (_updateDifficulty)
        // {
        //     _curSmoothT += Time.deltaTime;
        //     timeFactor = Mathf.SmoothStep(_oldTimeFactor, _targetTimeFactor, _curSmoothT);
        //
        //     if (timeFactor <= _targetTimeFactor)
        //     {
        //         _updateDifficulty = false;
        //     }
        //     return;
        // }
        //
        // _curTimeInLevel += Time.deltaTime;
        // if (_curTimeInLevel > difficultyLength)
        // {
        //     int stepsTaken = Mathf.RoundToInt(_curTimeInLevel / difficultyLength);
        //     _curTimeInLevel -= stepsTaken * difficultyLength;
        //     _curPercentDifficulty += stepsTaken * difficultyStep;
        //     _updateDifficulty = true;
        //     _curSmoothT = 0f;
        //     _oldTimeFactor = timeFactor;
        //     _targetTimeFactor = Mathf.SmoothStep(40, 1, _curPercentDifficulty);
        // }
    }
}