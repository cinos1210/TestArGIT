using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsBall : MonoBehaviour
{
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void EnableRigidbody(bool enable)
    {
        _rigidbody.constraints = enable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    }

}
