namespace ProjectName.Extension
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class ColorExtension
    {
        public static Texture2D ToTexture2D(this Color color, int width, int height)
        {
            var pixels = new Color[width * height];
            var texture = new Texture2D(width, height);

            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = color;

            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }
    }
}