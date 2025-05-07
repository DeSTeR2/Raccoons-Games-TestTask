using Infrastructure.Event;
using Infrastructure.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Level events", menuName = "Event/Type/LevelEvents")]
public class LevelEvent : Events
{
    public EventSO gameSceneLoaded;
}