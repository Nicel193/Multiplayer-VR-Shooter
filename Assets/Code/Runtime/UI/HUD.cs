using System;
using Code.Runtime.Logic.PlayerSystem;
using Code.Runtime.Logic.WeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NetworkPlayer = Code.Runtime.Logic.PlayerSystem.NetworkPlayer;

namespace Code.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private NetworkPlayer networkPlayer;

        private void Update()
        {
            DisplayHealthText();
            DisplayAmmoText();
        }

        private void DisplayHealthText()
        {
            float health = networkPlayer.PlayerHealth.Health;
            float maxHealth = networkPlayer.PlayerHealth.MAXHealth;

            float healthPercentage = health / maxHealth;
            healthImage.fillAmount = healthPercentage;
        }

        private void DisplayAmmoText()
        {
            ammoText.text = String.Empty;
            
            if(networkPlayer.PlayerWeapon == null) return;
            
            Magazine weaponCurrentMagazine = networkPlayer.PlayerWeapon.CurrentMagazine;

            if (weaponCurrentMagazine != null)
            {
                ammoText.text = $"{weaponCurrentMagazine.AmmoCount}/{weaponCurrentMagazine.MaxAmmoCount} Ammo";
            }
        }
    }
}