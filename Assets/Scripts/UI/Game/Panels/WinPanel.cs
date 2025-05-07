using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

namespace UI.Game.Panels
{
    public class WinPanel : Panel
    {
        private GameStateMachine _gameStateMachine;

        protected override void Start()
        {
            closeBtn.onClick.AddListener(MoveToMenuState);
        }

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public override void Open()
        {
            if (gameObject.activeInHierarchy) return;

            base.Open();
            SoundManager.instance.PlaySound(SoundType.Win);
        }

        private void MoveToMenuState()
        {
            _gameStateMachine.Enter<MenuState>();
        }
    }
}