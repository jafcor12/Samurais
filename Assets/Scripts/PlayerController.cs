using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoState<PlayerController>
{

    //[SerializeField]
    //CinemachineVirtualCamera aimVirtualCamera;

    //[SerializeField]
    //GameObject aimPanel;

    [SerializeField]
    int damage;

    //[SerializeField]
    //float normalSensitivity = 1.0f;

    //[SerializeField]
    //float aimSensitivity = 0.5f;

    StarterAssetsInputs _inputs;
    //ThirdPersonController _personController;
    CharacterController _characterController;

    Animator _animator;

    //bool _gunState;

    bool _swordState;

    protected override void Awake()
    {
        base.Awake();

        _inputs = GetComponent<StarterAssetsInputs>();
        //_personController = GetComponent<ThirdPersonController>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_swordState != _inputs.sword)
        {
            _swordState = _inputs.sword;
            _animator.SetBool("sword", _swordState);
        }
    }

    public CharacterController GetcharacterController()
    {
        return _characterController;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAgentController enemy = other.GetComponent<EnemyAgentController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }


}
