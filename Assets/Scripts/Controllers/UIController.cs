using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private TextMeshProUGUI currencyText;

    private List<GameObject> heartsList;
    private int lastFull = -1;

    private void Awake()
    {
        heartsList = new List<GameObject>();
    }

    public void InsertHeartIntoUI(int amount, bool full)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newHeart = Instantiate(heart, heartContainer);
            heartsList.Add(newHeart);
        }

        if (full) AddHearts(amount);
    }

    public void ChangeHeartsInUI(int amount, bool add)
    {
        if (add) AddHearts(amount);
        else RemoveHearts(amount);
    }

    private void AddHearts(int amount)
    {
        int lastFullHeartPosition = lastFull;

        for (int i = lastFullHeartPosition + 1; i < lastFullHeartPosition + amount + 1; i++)
        {
            if (i >= heartsList.Count) return;

            GameObject objHeart = heartsList[i];
            objHeart.transform.Find("Full").gameObject.SetActive(true);

            lastFull++;
        }
    }

    private void RemoveHearts(int amount)
    {
        int lastFullHeartPosition = lastFull;

        for (int i = lastFullHeartPosition; i > lastFullHeartPosition - amount; i--)
        {
            if (i < 0) return;

            GameObject objHeart = heartsList[i];
            objHeart.transform.Find("Full").gameObject.SetActive(false);

            lastFull--;
        }
    }

    public void ChangeCoinTextToNewValue(int newValue)
    {
        currencyText.text = newValue.ToString();
    }
}
