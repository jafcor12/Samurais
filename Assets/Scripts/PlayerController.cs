using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoState<PlayerController>
{

    [SerializeField]
    CinemachineVirtualCamera aimVirtualCamera;

    [SerializeField]
    GameObject aimPanel;

    [SerializeField]
    int damage;

    [SerializeField]
    float normalSensitivity = 1.0f;

    [SerializeField]
    float aimSensitivity = 0.5f;

    StarterAssetsInputs _inputs;
    ThirdPersonController _personController;
    CharacterController _characterController;

    Animator _animator;

    bool _gunState;

    bool _swordState;

    protected override void Awake()
    {
        base.Awake();

        _inputs = GetComponent<StarterAssetsInputs>();
        _personController = GetComponent<ThirdPersonController>();
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

        if (_gunState != _inputs.gun)
        {
            _gunState = _inputs.gun;
            _animator.SetBool("Gun", _gunState);

            aimVirtualCamera.gameObject.SetActive(_gunState);
            aimPanel.SetActive(_gunState);

            _personController.SetRotateOnMove(!_gunState);

            if (_gunState)
            {
                _personController.SetSensitivity(aimSensitivity);

                Vector3 mouseWorldPosition = Vector3.zero;
                Vector2 screenCenterPoint =
                    new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

                Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 2000.0f))
                {
                    mouseWorldPosition = raycastHit.point;
                }

                Vector3 worldAimTarget = mouseWorldPosition;
                //worldAimTarget.y = transform.position.y;

                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                transform.forward = 
                    Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 15.0f);


            }
            else
            {
                _personController.SetSensitivity(normalSensitivity);
            }
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
