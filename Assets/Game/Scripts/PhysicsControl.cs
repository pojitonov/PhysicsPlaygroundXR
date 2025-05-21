using UnityEngine;

public class PhysicsControl : MonoBehaviour
{
    [SerializeField] private Transform rootObject;
    
    public void EnablePhysics()
    {
        foreach (Transform child in rootObject)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
            }
        }
    }
    
    public void DisablePhysics()
    {
        foreach (Transform child in rootObject)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = true;
            }
        }
    }
    
    public void ChangeDamping(float value)
    {
        foreach (Transform child in rootObject)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearDamping = value;
            }
        }
    }
    
    public void JointAnchorY(float value)
    {
        foreach (Transform child in rootObject)
        {
            if (child.TryGetComponent<HingeJoint>(out var joint))
            {
                joint.anchor = new Vector3(0f, value, 0f);
            }
        }
    }
}
