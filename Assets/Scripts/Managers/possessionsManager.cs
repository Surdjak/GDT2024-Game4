using System.Collections;
using TMPro;
using UnityEngine;

public class PossessionsManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public int InitialAmountOfMoney = 200;
    public TextMeshProUGUI MoneyErrorText;
    private int _money;

    public TextMeshProUGUI GrapeText;
    private int _grapes;

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
        if (GrapeText == null)
        {
            Debug.LogError("Missing Grape Text!", gameObject);
        }

        InitializeMoney();
    }

    #region Money

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
    private void RefreshGrapeDisplay()
    {
        GrapeText.text = _grapes.ToString();
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

    #endregion

    #region Grapes

    public void GainGrapes(uint amout)
    {
        _grapes += (int)amout;
        GrapeText.text = _grapes.ToString();
    }
    public void SellAllGrapes()
    {
        _money += _grapes * 1;
        _grapes = 0;
        RefreshMoneyDisplay();
        RefreshGrapeDisplay();
    }

    #endregion

    private IEnumerator ShowMessage(TextMeshProUGUI text, string message)
    {
        text.text = message;
        yield return new WaitForSeconds(2f);
        text.text = string.Empty;
    }
}
