using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputMap m_InputMap;
    private Vector2 m_Direction;

    private Rigidbody2D m_rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;

    private bool m_IsBeingPushed;
    private Vector3 m_PushDirection;

    // Start is called before the first frame update
    void Awake()
    {
        m_InputMap = new InputMap();
        m_rb = GetComponent<Rigidbody2D>();

        m_InputMap.Player.Movement.performed += ctx => m_Direction = ctx.ReadValue<Vector2>();
        m_InputMap.Player.Movement.canceled += ctx => m_Direction = Vector2.zero;
    }

    private void OnEnable() => m_InputMap.Enable();
    private void OnDisable() => m_InputMap.Disable();

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_IsBeingPushed) 
            m_rb.velocity = m_PushDirection * pushForce;
        else 
            m_rb.velocity = m_Direction * speed;
    }

    public void Push(Vector3 agressorPosition)
    {
        m_PushDirection = (transform.position - agressorPosition).normalized;
        m_IsBeingPushed = true;

        StartCoroutine(DeactivatePush());
    }

    IEnumerator DeactivatePush()
    {
        yield return new WaitForSeconds(pushDuration);
        m_IsBeingPushed = false;
    }
}
