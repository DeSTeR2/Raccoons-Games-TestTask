using System;
using UnityEngine;

namespace Infrastructure.DIContainer
{
    [Serializable]
    public class BoxPositions
    {
        public Transform parent;
        public Transform startPosition;

        public Transform leftMoveBound;
        public Transform rightMoveBound;
    }
}