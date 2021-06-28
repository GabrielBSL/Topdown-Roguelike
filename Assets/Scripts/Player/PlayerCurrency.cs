using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    private int m_CurrentCurrency;

    public void AddCurrency(int value)
    {
        if (value <= 0) return;
        m_CurrentCurrency += value;

        GameController.Instance.UpdateCurrencyText(m_CurrentCurrency);
    }

    public bool RemoveCurrency(int value)
    {
        if (m_CurrentCurrency - value < 0) return false;

        m_CurrentCurrency -= value;
        GameController.Instance.UpdateCurrencyText(m_CurrentCurrency);

        return true;
    }
}
