using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerHealth m_PlayerHealth;
    private PlayerMovement m_PlayerMovement;
    private PlayerCurrency m_PlayerCurrency;
    private PlayerAnimation m_PlayerAnimation;

    [SerializeField] private Transform cameraFocusPoint;

    private void Awake()
    {
        m_PlayerAnimation = GetComponent<PlayerAnimation>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerCurrency = GetComponent<PlayerCurrency>();
        m_PlayerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        transform.SetParent(null, true);
        CinemachineController.Instance.FollowObject(cameraFocusPoint);
    }

    public void ActivatePush(Vector3 agressorPosition)
    {
        m_PlayerMovement.Push(agressorPosition);
    }

    public void ActivateBlink(float duration)
    {
        m_PlayerAnimation.Blink(duration);
    }

    public bool UpdateCurrency(int value)
    {
        if(value > 0)
        {
            m_PlayerCurrency.AddCurrency(value);
            return true;
        }
        
        return m_PlayerCurrency.RemoveCurrency(value);
    }
}
