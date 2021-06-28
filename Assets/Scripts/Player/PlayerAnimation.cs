using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float blinkIntensity;
    
    private Animator m_Animator;
    private Rigidbody2D m_Rb;
    private SpriteRenderer m_Renderer;
    
    private enum Facing
    {
        up,
        down,
        left,
        right
    }

    private enum AnimList
    {
        idle_up,
        idle_down,
        idle_left,
        idle_right,
        walk_up,
        walk_down,
        walk_left,
        walk_right
    }

    private Facing m_Facing;
    private string m_CurrentAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rb = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Facing = Facing.down;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (m_Rb.velocity == Vector2.zero)
        {
            if(m_Facing == Facing.up) PlayAnimation(AnimList.idle_up);
            else if(m_Facing == Facing.down) PlayAnimation(AnimList.idle_down);
            else if(m_Facing == Facing.left) PlayAnimation(AnimList.idle_left);
            else if(m_Facing == Facing.right) PlayAnimation(AnimList.idle_right);
        }
        else
        {
            if (m_Rb.velocity.x > 0.1f)
            {
                PlayAnimation(AnimList.walk_right);
                m_Facing = Facing.right;
            }
            else if (m_Rb.velocity.x < -0.1f)
            {
                PlayAnimation(AnimList.walk_left);
                m_Facing = Facing.left;
            }
            else if (m_Rb.velocity.y > 0.1f)
            {
                PlayAnimation(AnimList.walk_up);
                m_Facing = Facing.up;
            }
            else if (m_Rb.velocity.y < -0.1f)
            {
                PlayAnimation(AnimList.walk_down);
                m_Facing = Facing.down;
            }
        }
    }

    private void PlayAnimation(AnimList animationName)
    {
        if(m_CurrentAnimation == animationName.ToString()) return;

        m_CurrentAnimation = animationName.ToString();
        m_Animator.Play(m_CurrentAnimation);
    }

    public void Blink(float duration)
    {
        StartCoroutine(ApplyBlinkEffect(duration));
    }

    IEnumerator ApplyBlinkEffect(float duration)
    {
        float timePassed = 0;
        while (timePassed < duration)
        {
            timePassed += blinkIntensity;
            m_Renderer.enabled = !m_Renderer.enabled;
            yield return new WaitForSeconds(blinkIntensity);
        }

        m_Renderer.enabled = true;
    }
}
