using Infrastructure.Events;
using UnityEngine;

namespace Infrastructure.Event
{
    [CreateAssetMenu(fileName = "Menu events", menuName = "Event/Type/MenuEvents")]
    public class MenuEvents : Events
    {
        public EventSO menuSceneLoaded;
        public EventSO loadPlayScene;
    }
}