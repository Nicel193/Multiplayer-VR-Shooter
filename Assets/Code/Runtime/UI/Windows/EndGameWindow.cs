using Code.Runtime.Logic;
using Code.Runtime.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Windows
{
    public class EndGameWindow : MonoBehaviour, IEndGameWindow, IPayloadOpenWindow<Team>
    {
        [SerializeField] private TextMeshProUGUI winTeamText;
        [SerializeField] private Button exitButton;

        private void Awake() =>
            exitButton.onClick.AddListener(ExitInMenu);

        private void OnDestroy() =>
            exitButton.onClick.RemoveListener(ExitInMenu);


        public void Open(Team winTeam)
        {
            gameObject.SetActive(true);

            winTeamText.text = $"Win team: {winTeam.ToString()}";
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void ExitInMenu()
        {
        }
    }
}