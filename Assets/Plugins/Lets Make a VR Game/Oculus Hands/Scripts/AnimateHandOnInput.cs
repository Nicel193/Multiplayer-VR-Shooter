using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : NetworkBehaviour
{
    private const string TriggerAnimationValueTag = "Trigger";
    private const string GripAnimationValueTag = "Grip";
    
    [SerializeField] private InputActionProperty pinchAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private Animator handAnimator;
    
    private void Update()
    {
        if(!Object.HasStateAuthority) return;

        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat(TriggerAnimationValueTag, triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat(GripAnimationValueTag, gripValue);
    }
}
