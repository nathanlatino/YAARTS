using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitivesFactory
{
    private static PrimitivesFactory Instance;

    private readonly RectangleFactory rectangleFactory;

    private PrimitivesFactory() {
        rectangleFactory = RectangleFactory.getInstance();
    }

    public static PrimitivesFactory getInstance() {
        return Instance ?? (Instance = new PrimitivesFactory());
    }

    public Texture2D getRectangleTexture() {
        return rectangleFactory.GetRectangleTexture();
    }
}
