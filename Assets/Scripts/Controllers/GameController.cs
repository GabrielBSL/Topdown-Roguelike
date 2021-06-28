using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private UIController uIController;

    private void Awake()
    {
        Instance = this;
        uIController = FindObjectOfType<UIController>();
    }

    public void InsertNewHeart(int difference, bool full)
    {
        uIController.InsertHeartIntoUI(difference, full);
    }

    public void UpdateHeartContainers(int difference, bool add)
    {
        uIController.ChangeHeartsInUI(difference, add);
    }

    public void UpdateCurrencyText(int value)
    {
        uIController.ChangeCoinTextToNewValue(value);
    }
}