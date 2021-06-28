using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusEnemy : Enemy
{
    private enum AnimList
    {
        walk_up,
        walk_down,
        walk_left,
        walk_right
    }

    private AnimList m_CurrentAnimation;
    
    [SerializeField] private float minimalDistanceToPlayer;
    [SerializeField] private float attackSpeed;

    private bool m_IsAttacking;

    void FixedUpdate()
    {
        if(m_IsAttacking || Player == null) return;
        
        if ((transform.position - Player.position).sqrMagnitude > Mathf.Pow(minimalDistanceToPlayer, 2))
        {
            Vector2 direction = (Player.position - transform.position).normalized * speed;
            m_rb.velocity = direction;
        }
        else
        {
            if (Time.time > attackDelayTimer)
            {
                attackDelayTimer = Time.time + attackDelay;
                StartCoroutine(ChargeAttack());
            }
            else
            {
                m_rb.velocity = Vector2.zero;
            }
        }

        AnimateEnemy();
    }

    private void AnimateEnemy()
    {
        if (Mathf.Abs(m_rb.velocity.x) > Mathf.Abs(m_rb.velocity.y))
        {
            if(m_rb.velocity.x > 0) ChangeAnimation(AnimList.walk_right);
            else ChangeAnimation(AnimList.walk_left);
        }
        else
        {
            if(m_rb.velocity.y > 0) ChangeAnimation(AnimList.walk_up);
            else ChangeAnimation(AnimList.walk_down);
        }
    }

    IEnumerator ChargeAttack()
    {
        m_IsAttacking = true;
        
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = Player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            m_rb.position = Vector2.Lerp(originalPosition, 
                targetPosition, formula);
            
            yield return null;
        }

        m_IsAttacking = false;
    }

    private void ChangeAnimation(AnimList animationName)
    {
        if(m_CurrentAnimation == animationName) return;

        m_CurrentAnimation = animationName;
        m_Animation.Play(animationName.ToString());
    }
}
