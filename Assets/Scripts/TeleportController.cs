using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    bool _isTeleporting = false;

    Transform _destination;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            if (_isTeleporting)
            {
                return;
            }

            _isTeleporting = true;
            PortalController controller = other.GetComponent<PortalController>();
            _destination = controller.GetDestination();

            CharacterController character = PlayerController.GetInstance().GetcharacterController();
            character.enabled = false;
            character.transform.position = _destination.position;
            character.enabled = true;
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.position == _destination.position)
    //    {
    //        _isTeleporting = false;
    //    }
    //}
}
