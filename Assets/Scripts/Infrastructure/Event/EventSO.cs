using System;
using UnityEngine;

namespace Infrastructure.Events
{
    [CreateAssetMenu(fileName = "Event SO", menuName = "Event/New event")]
    public class EventSO : ScriptableObject
    {
        public Action OnEvent;
    }
}