using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin {
    /**
     * Grid cells size
     */
    private static int GRID_SIZE = 30;

    /**
     * Noise amplitude scale
     */
    private static int BUMP_SCALE = 50;
    /**
     * Number used in pseudo random generation
     */
    private static double MAGIC_NUMBER = 43758.5453123;

    /**
     * remove constructor
     */
    private Perlin() {
    }

    /**
     * Returns a pseudo-random float between -1 and 1 for a given float c
     *
     * @param c seed
     * @return float in range [-1,1]
     */
    private static float pseudoRandom(float c) {
        float num = (float) (MAGIC_NUMBER * Mathf.Sin(c));
        num -= Mathf.Floor(num);
        return (float) (-1.0 + 2.0 * num);
    }

    /**
     * Interpolates between a and b using quintic interpolation at the given ratio t.
     *
     * @param a float
     * @param b float
     * @param t float in the range [0,1]
     * @return interpolated float in range [a,b]
     */
    private static float quinticInterpolation(float a, float b, float t) {
        float u = (float) ((6.0 * t * t - 15.0 * t + 10.0) * t * t * t); // Quintic interpolation
        return a + u * (b - a);
    }

    /**
     * Returns the value of a 1D Perlin noise function at the given coordinate x, using random seed, with grid
     * size {@value #GRID_SIZE}
     *
     * @param x    coordinate to calculate for
     * @param seed random seed
     * @return float in range [-{@value #BUMP_SCALE}, {@value #BUMP_SCALE}]
     */
    public static float perlin1D(float x, int seed) {
        float low = (float) Mathf.Floor(x / GRID_SIZE);
        float high = low + 1;
        float distance = Mathf.Abs((x % GRID_SIZE) / GRID_SIZE);
        return BUMP_SCALE * quinticInterpolation(
            pseudoRandom(seed + low) * distance,
            pseudoRandom(seed + high) * (1 - distance),
            distance
        );
    }
}
