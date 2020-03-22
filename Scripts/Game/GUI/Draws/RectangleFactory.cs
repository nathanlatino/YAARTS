using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleFactory
{
    private static RectangleFactory Instance;
    private Texture2D rectangleTexture;

    private RectangleFactory() { }

    public static RectangleFactory getInstance() {
        return Instance ?? (Instance = new RectangleFactory());
    }

    /// <summary>
    /// Flyweight pattern
    /// </summary>
    /// <returns></returns>
    public Texture2D GetRectangleTexture() {
        if (rectangleTexture == null) {
            rectangleTexture = new Texture2D(1, 1);
            rectangleTexture.SetPixel(0, 0, Color.white);
            rectangleTexture.Apply();
        }

        return rectangleTexture;
    }
}
