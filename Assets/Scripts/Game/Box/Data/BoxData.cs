using System;
using Infrastructure.StateMachine.States;
using UnityEngine;

namespace Game.Box
{
    [Serializable]
    public class BoxData
    {
        public Color color;
        public int number;
        [Range(0, 100)] public int spawnPercent;

        private Material Material;
        private static readonly int BoxColor = Shader.PropertyToID("_BoxColor");
        public BoxData(BoxData data)
        {
            color = data.color;
            number = data.number * 2;
        }

        public Material GetMaterial(Material material)
        {
            if (Material == null)
            {
                Material = new Material(material);
                Material.SetColor(BoxColor, color);
            }

            return Material;
        }
    }
}