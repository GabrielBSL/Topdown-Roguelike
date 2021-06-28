using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float invunerabilityAfterHit;

    private float invunerabilityTimer;
    [SerializeField] private int m_CurrentHealht;

    private PlayerController m_Controller;
    
    private void Awake()
    {
        m_Controller = GetComponent<PlayerController>();
        m_CurrentHealht = health;
    }

    private void Start()
    {
        GameController.Instance.InsertNewHeart(health, true);
    }

    public void ReceiveHit(int damage, Vector3 agressorPosition)
    {
        if(Time.time < invunerabilityTimer) return;
        invunerabilityTimer = Time.time + invunerabilityAfterHit;
        
        m_CurrentHealht -= damage;
        GameController.Instance.UpdateHeartContainers(damage, false);

        if (m_CurrentHealht <= 0)
        {
            ActivateDeath();
        }
        else
        {
            HitFeedback(agressorPosition);
        }
    }

    private void HitFeedback(Vector3 agressorPosition)
    {
        m_Controller.ActivatePush(agressorPosition);
        m_Controller.ActivateBlink(invunerabilityAfterHit);
        CinemachineController.Instance.StartShake(10, .1f);
    }

    private void ActivateDeath()
    {
        Destroy(gameObject);
    }
}
