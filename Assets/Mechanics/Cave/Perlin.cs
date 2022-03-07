using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin
{
    /**
     * Grid cells size
     */
    private static int GRID_SIZE = 15;

    /**
     * Number used in pseudo random generation
     */
    private static double MAGIC_NUMBER = 43758.5453123;

    /**
     * remove constructor
     */
    private Perlin()
    {
    }

    /**
     * Returns a pseudo-random float between -1 and 1 for a given float c
     *
     * @param c seed
     * @return float in range [-1,1]
     */
    private static float pseudoRandom(float c)
    {
        float num = (float) (MAGIC_NUMBER * Mathf.Sin(c));
        num -= Mathf.Floor(num);
        return (float) (-1.0 + 2.0 * num);
    }
    
    // Returns a psuedo-random float2 with componenets between -1 and 1 for a given float2 c 
    private static Vector2 random2(Vector2 c)
    {
        c = new Vector2(Vector2.Dot(c, new Vector2(127.1f, 311.7f)), Vector2.Dot(c, new Vector2(269.5f, 183.3f)));
        return new Vector2(pseudoRandom(c.x), pseudoRandom(c.y));
        // var num = 43758.5453123f * new Vector2(Mathf.Sin(c.x), Mathf.Sin(c.y));
        // num = new Vector2(num.x - Mathf.Floor(num.x), num.y - Mathf.Floor(num.y));
        // Vector2 v = -Vector2.one + 2.0f * num;
        // return v;
    }

    /**
     * Interpolates between a and b using quintic interpolation at the given ratio t.
     *
     * @param a float
     * @param b float
     * @param t float in the range [0,1]
     * @return interpolated float in range [a,b]
     */
    private static float quinticInterpolation(float a, float b, float t)
    {
        float u = (float) ((6.0 * t * t - 15.0 * t + 10.0) * t * t * t); // Quintic interpolation
        return a + u * (b - a);
    }
    
    // Interpolates a given array v of 4 float2 values using biquintic interpolation
    // at the given ratio t (a float2 with components between 0 and 1)
    private static float biquinticInterpolation(List<float> v, Vector2 t)
    {
        // Interpolate in the x direction
        float x1 = quinticInterpolation(v[0], v[1], t.x);
        float x2 = quinticInterpolation(v[2], v[3], t.x);

        // Interpolate in the y direction and return
        return quinticInterpolation(x1, x2, t.y);
    }

    /**
     * Returns the value of a 1D Perlin noise function at the given coordinate x, using random seed, with grid
     * size {@value #GRID_SIZE}
     *
     * @param x    coordinate to calculate for
     * @param seed random seed
     * @return float in range [-{@value #BUMP_SCALE}, {@value #BUMP_SCALE}]
     */
    public static float perlin1D(float x, int seed)
    {
        float low = Mathf.Floor(x / GRID_SIZE);
        float high = low + 1;
        float distance = Mathf.Abs((x % GRID_SIZE) / GRID_SIZE);
        return quinticInterpolation(
            pseudoRandom(seed + low) * distance,
            pseudoRandom(seed + high) * (1 - distance),
            distance
        );
    }

    public static float Perlin2D(Vector2 c, int seed)
    {
        // Your implementation
        Vector2 c_int0 = new Vector2(Mathf.Floor(c.x), Mathf.Floor(c.y));
        Vector2 c_int1 = c_int0 + Vector2.right;
        Vector2 c_int2 = c_int0 + Vector2.up;
        Vector2 c_int3 = c_int0 + Vector2.one;

        List<float> dot_product = new List<float>{
            Vector2.Dot(c - c_int0, random2(c_int0)),
            Vector2.Dot(c - c_int1, random2(c_int1)),
            Vector2.Dot(c - c_int2, random2(c_int2)),
            Vector2.Dot(c - c_int3, random2(c_int3))
        };

        return biquinticInterpolation(dot_product, c - c_int0);
    }
}