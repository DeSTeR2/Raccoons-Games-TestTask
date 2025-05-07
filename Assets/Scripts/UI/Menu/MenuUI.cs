using Infrastructure.Event;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Menu
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        private MenuEvents _menuEvents;

        private void Start()
        {
            playButton.onClick.AddListener(() => _menuEvents.loadPlayScene.OnEvent?.Invoke());
        }

        [Inject]
        public void Construct(AllEvents menuEvents)
        {
            _menuEvents = menuEvents.Find<MenuEvents>();
        }
    }
}