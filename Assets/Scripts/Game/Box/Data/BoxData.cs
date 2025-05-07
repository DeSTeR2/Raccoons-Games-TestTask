using System;
using UnityEngine;

namespace Game.Box
{
    [Serializable]
    public class BoxData
    {
        public Color color;
        public int number;
        [Range(0, 100)] public int spawnPercent;

        public BoxData(BoxData data)
        {
            color = data.color;
            number = data.number * 2;
        }
    }
}