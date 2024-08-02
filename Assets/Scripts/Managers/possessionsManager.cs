using System.Collections;
using TMPro;
using UnityEngine;

public class PossessionsManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public int InitialAmountOfMoney = 200;
    public TextMeshProUGUI MoneyErrorText;

    private int _money;

    private void Start()
    {
        if (MoneyText == null)
        {
            Debug.LogError("Missing Money Text!", gameObject);
        }
        if (MoneyErrorText == null)
        {
            Debug.LogError("Missing Money Error Text!", gameObject);
        }

        InitializeMoney();
    }

    private void InitializeMoney()
    {
        _money = InitialAmountOfMoney;
        MoneyErrorText.text = string.Empty;
        RefreshMoneyDisplay();
    }

    private void RefreshMoneyDisplay()
    {
        MoneyText.text = $"{_money} €";
    }

    public bool TrySpendMoney(uint amount, string boughtItemName = null)
    {
        if (_money >= amount)
        {
            _money -= (int)amount;
            RefreshMoneyDisplay();
            return true;
        }

        StartCoroutine(ShowMessage(MoneyErrorText, $"Need {amount} €{(boughtItemName == null ? string.Empty : $" to buy {boughtItemName}")}"));
        return false;
    }

    private IEnumerator ShowMessage(TextMeshProUGUI text, string message)
    {
        text.text = message;
        yield return new WaitForSeconds(2f);
        text.text = string.Empty;
    }
}
