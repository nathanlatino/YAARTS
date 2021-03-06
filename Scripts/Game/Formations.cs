﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Formations
{
    /*------------------------------------------------------------------*\
    |*							FORMATIONS
    \*------------------------------------------------------------------*/

    /// <summary>
    /// Compute a list of `n` points evenly spread inside a rectangle
    /// centred at <c>center</c>`. Meant to be used on a <see cref="Selection"/>
    /// by applying to each element the new offset.
    /// </summary>
    /// <param name="n">Selection size</param>
    /// <param name="center">where to center the formation (mouse.position)</param>
    /// <param name="offsetSize">distance between points</param>
    /// <returns><c>List</c> of <c>Vector3</c></returns>
    public static List<Vector3> SquareFormation(int n, Vector3 center, float offsetSize = 1.5f) {
        var lines = Mathf.Round(Mathf.Sqrt(n));
        var size = lines * offsetSize - offsetSize;
        var start = -size / 2;

        var x = start;
        var z = start;

        var offsets = new List<Vector3>();


        for (int i = 1; i <= n; i++) {
            offsets.Add(new Vector3(center.x + x, 0, center.z + z));

            if (i % lines == 0) {
                x = -size / 2;
                z += offsetSize;
            }
            else {
                x += offsetSize;
            }
        }
        return offsets;
    }
}
