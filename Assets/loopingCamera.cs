using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopingCamera : MonoBehaviour
{
    bool m_isActive = false;
    Transform m_activatedAnchor;
    Transform m_hallAnchorLeft;

    [SerializeField] Transform playerCameraTransform;

    void Update()
    {
        if (!m_isActive)
            return;

        /*if(m_invert)
        {
            Vector3 nonWorldSpace = m_activatedAnchor.position - playerCameraTransform.position;
            Vector3 newPos = new Vector3(startHallAnchorRight.position.x + nonWorldSpace.x, startHallAnchorRight.position.y - nonWorldSpace.y, startHallAnchorRight.position.z + nonWorldSpace.z);
            this.transform.position = newPos;

            Vector3 newRot = playerCameraTransform.rotation.eulerAngles;
            newRot.y += 180;

            this.transform.rotation = Quaternion.Euler(newRot);
        }*/
        
        this.transform.position = m_hallAnchorLeft.position - (m_activatedAnchor.position - playerCameraTransform.position);
        this.transform.rotation = playerCameraTransform.rotation;
    }

    public void ActivateLoop(Transform activatedTrigger, Transform corriderAnchor)
    {
        m_isActive = true;
        m_activatedAnchor = activatedTrigger;
        m_hallAnchorLeft = corriderAnchor;
    }

    public void Deactivate()
    {
        m_isActive = false;
    }

    public void ReverseLoop()
    {
        Transform temp = m_activatedAnchor;

        m_activatedAnchor = m_hallAnchorLeft;
        m_hallAnchorLeft = temp;

        m_isActive = true;
    }
}
