using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public struct BodySocket
{
    public XRSocketInteractor SocketInteractor;
    [Range(0.01f, 1f)] public float heightRatio;
}

public class BodySocketInventory : MonoBehaviour
{
    [field:SerializeField] public BodySocket[] BodySockets { get; private set; }
    
    [SerializeField] private GameObject hmd;
    [SerializeField] private CharacterController characterController;

    private Vector3 _currentHMDlocalPosition;
    private Quaternion _currentHMDRotation;
    
    private void Update()
    {
        _currentHMDlocalPosition = hmd.transform.localPosition;
        _currentHMDRotation = hmd.transform.rotation;
        foreach (var bodySocket in BodySockets)
        {
            UpdateBodySocketHeight(bodySocket);
        }

        UpdateSocketInventory();
    }

    private void UpdateBodySocketHeight(BodySocket bodySocket)
    {
        float playerHeight = hmd.transform.position.y - characterController.bounds.min.y;
        float socketHeight = playerHeight * bodySocket.heightRatio;
        
        bodySocket.SocketInteractor.gameObject.transform.localPosition = new Vector3(
            bodySocket.SocketInteractor.gameObject.transform.localPosition.x,
            socketHeight,
            bodySocket.SocketInteractor.gameObject.transform.localPosition.z);
    }

    private void UpdateSocketInventory()
    {
        transform.localPosition = new Vector3(_currentHMDlocalPosition.x, 0, _currentHMDlocalPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, _currentHMDRotation.y, transform.rotation.z,
            _currentHMDRotation.w);
    }
}