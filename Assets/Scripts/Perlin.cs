using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Perlin
{
    /**
     * Grid cells size
     */
    public static int GridSize = 20;

    public static Vector2 SeedVector = 8008135 * Vector2.one;

    /**
     * Number used in pseudo random generation
     */
    private static double MAGIC_NUMBER = 43758.5453123;

    private static Vector2 _gradientVectorX = new Vector2(127.1f, 311.7f);

    private static Vector2 _gradientVectorY = new Vector2(269.5f, 183.3f);

    /**
     * Returns a pseudo-random float between -1 and 1 for a given float c
     *
     * @param c seed
     * @return float in range [-1,1]
     */
    private static float PseudoRandom(float c)
    {
        float num = (float) (MAGIC_NUMBER * Mathf.Sin(c));
        num -= Mathf.Floor(num);
        return (float) (-1.0 + 2.0 * num);
    }

    // Returns a pseudo-random float2 with components between -1 and 1 for a given float2 c 
    private static Vector2 PseudoRandom2D(Vector2 c)
    {
        c = new Vector2(
            Vector2.Dot(c, _gradientVectorX),
            Vector2.Dot(c, _gradientVectorY)
        );
        return new Vector2(PseudoRandom(c.x), PseudoRandom(c.y));
    }

    /**
     * Interpolates between a and b using quintic interpolation at the given ratio t.
     *
     * @param a float
     * @param b float
     * @param t float in the range [0,1]
     * @return interpolated float in range [a,b]
     */
    private static float QuinticInterpolation(float a, float b, float t)
    {
        float u = (float) ((6.0 * t * t - 15.0 * t + 10.0) * t * t * t); // Quintic interpolation
        return a + u * (b - a);
    }

    // Interpolates a given array v of 4 float2 values using biquintic interpolation
    // at the given ratio t (a float2 with components between 0 and 1)
    private static float BiquinticInterpolation(List<float> v, Vector2 t)
    {
        // Interpolate in the x direction
        float x1 = QuinticInterpolation(v[0], v[1], t.x);
        float x2 = QuinticInterpolation(v[2], v[3], t.x);

        // Interpolate in the y direction and return
        return QuinticInterpolation(x1, x2, t.y);
    }

    /**
     * Returns the value of a 1D Perlin noise function at the given coordinate x, using random seed, with grid
     * size {@value #GRID_SIZE}
     *
     * @param x    coordinate to calculate for
     * @param seed random seed
     * @return float in range [-{@value #BUMP_SCALE}, {@value #BUMP_SCALE}]
     */
    public static float Perlin1D(float x, int seed)
    {
        float low = Mathf.Floor(x / GridSize);
        float high = low + 1;
        float distance = Mathf.Abs((x % GridSize) / GridSize);
        return QuinticInterpolation(
            PseudoRandom(seed + low) * distance,
            PseudoRandom(seed + high) * (1 - distance),
            distance
        );
    }

    public static float Perlin2D(Vector2 c)
    {
        c /= GridSize;
        Vector2 cINT0 = new Vector2(Mathf.Floor(c.x), Mathf.Floor(c.y));
        Vector2 cINT1 = cINT0 + Vector2.right;
        Vector2 cINT2 = cINT0 + Vector2.up;
        Vector2 cINT3 = cINT0 + Vector2.one;
        List<float> dotProduct = new List<float>
        {
            Vector2.Dot(c - cINT0, PseudoRandom2D(SeedVector + cINT0)),
            Vector2.Dot(c - cINT1, PseudoRandom2D(SeedVector + cINT1)),
            Vector2.Dot(c - cINT2, PseudoRandom2D(SeedVector + cINT2)),
            Vector2.Dot(c - cINT3, PseudoRandom2D(SeedVector + cINT3))
        };

        return BiquinticInterpolation(dotProduct, c - cINT0);
    }
}