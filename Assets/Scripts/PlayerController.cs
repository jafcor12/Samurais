using StarterAssets;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoState<PlayerController>
{

    //[SerializeField]
    //CinemachineVirtualCamera aimVirtualCamera;

    //[SerializeField]
    //GameObject aimPanel;

    [SerializeField]
    int damage;

    [SerializeField]
    public int health;

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
    bool _hitState;

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
        if (health <= 0)
        {
            Die();
            if (_hitState)
            {
                return;
            }
        }
        if (_swordState != _inputs.sword)
        {
            _swordState = _inputs.sword;
            _animator.SetBool("sword", _swordState);
        }
    }

    void Die()
    {
        _hitState = true;
        _animator.SetBool("PlayerHit", true);

        StartCoroutine(WaitDelete());
    }

    public CharacterController GetcharacterController()
    {
        return _characterController;
    }

    public bool HandleDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            return false;
        else
            return true;
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

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

