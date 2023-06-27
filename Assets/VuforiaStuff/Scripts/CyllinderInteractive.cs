using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CyllinderInteractive : MonoBehaviour
{
    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    private Animator _animator;
    private bool _isOpen;
    private int _hash_isOpen = Animator.StringToHash("isOpen");


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            OnOpen.Invoke();
        }else
        {
            OnClose.Invoke();
        }

        _animator.SetBool(_hash_isOpen, _isOpen);

    }

}
