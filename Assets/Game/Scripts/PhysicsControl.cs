using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class PhysicsControl : MonoBehaviour
{
    [SerializeField] private Transform rootObjectPhysics;
    [SerializeField] private Transform physicsControls;
    [SerializeField] private List<Transform> defaultButtons;

    private void Start()
    {
        foreach (Transform child in physicsControls.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "ButtonPanelBack")
            {
                var rbp = child.GetComponent<RoundedBoxProperties>();
                if (rbp != null)
                {
                    rbp.Color = new Color32(0, 0, 0, 51);
                    rbp.SendMessage("UpdateMaterialPropertyBlock", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        
        foreach (var button in defaultButtons)
        {
            foreach (Transform child in button.GetComponentsInChildren<Transform>(true))
            {
                if (child.name == "ButtonPanelBack")
                {
                    var rbp = child.GetComponent<RoundedBoxProperties>();
                    if (rbp != null)
                    {
                        rbp.Color = new Color(0f, 1f, 0f, 0.5f);
                        rbp.SendMessage("UpdateMaterialPropertyBlock", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }

    public void EnablePhysics()
    {
        foreach (Transform child in rootObjectPhysics)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
            }
        }
    }

    public void DisablePhysics()
    {
        foreach (Transform child in rootObjectPhysics)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = true;
            }
        }
    }

    public void ChangeDamping(float value)
    {
        foreach (Transform child in rootObjectPhysics)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearDamping = value;
            }
        }
    }

    public void JointAnchorY(float value)
    {
        foreach (Transform child in rootObjectPhysics)
        {
            if (child.TryGetComponent<HingeJoint>(out var joint))
            {
                joint.anchor = new Vector3(0f, value, 0f);
            }
        }
    }

    public void SetActiveState(Transform buttonRoot)
    {
        foreach (Transform child in buttonRoot.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "ButtonPanelBack")
            {
                var rbp = child.GetComponent<RoundedBoxProperties>();
                if (rbp != null)
                {
                    rbp.Color = new Color(0f, 1f, 0f, 0.5f);
                    rbp.SendMessage("UpdateMaterialPropertyBlock", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

    public void SetInactiveState(Transform buttonRoot)
    {
        foreach (Transform child in buttonRoot.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == "ButtonPanelBack")
            {
                var rbp = child.GetComponent<RoundedBoxProperties>();
                if (rbp != null)
                {
                    rbp.Color = new Color32(0, 0, 0, 51);
                    rbp.SendMessage("UpdateMaterialPropertyBlock", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}