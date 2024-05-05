using Code.Runtime.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private PlayerData playerData;

        private void Update()
        {
            float healthPercentage = (float)playerData.Health / playerData.MAXHealth;
            healthImage.fillAmount = healthPercentage;
            
            ammoText.text = $"{playerData.Ammo}/{playerData.MAXAmmo} Ammo";
        }
    }
}