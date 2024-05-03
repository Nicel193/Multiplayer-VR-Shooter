using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Code.Runtime.Logic
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float shootForce;
        
        private XRGrabInteractable _xrGrabInteractable;

        private void Awake()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable() =>
            _xrGrabInteractable.activated.AddListener(Shoot);

        private void OnDisable() =>
            _xrGrabInteractable.activated.RemoveListener(Shoot);

        private void Shoot(ActivateEventArgs arg)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            
            rigidbody.AddForce(spawnPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet, 5f);
        }
    }
}