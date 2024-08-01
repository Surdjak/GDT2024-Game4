using TMPro;
using UnityEngine;

public class PossessionsManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public int InitialAmountOfMoney = 200;

    private int _money;

    private void Start()
    {
        if (MoneyText == null)
        {
            Debug.LogError("Missing Money Text!", gameObject);
        }

        InitializeMoney();
    }

    private void InitializeMoney()
    {
        _money = InitialAmountOfMoney;
        MoneyText.text = $"{_money} €";
    }

    public bool HasEnoughMoney(int amount) => _money >= amount;
    public void SpendMoney(int amount) => _money -= amount;
}
