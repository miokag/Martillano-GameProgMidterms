using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI eggText;
    public TextMeshProUGUI chickText;
    public TextMeshProUGUI henText;
    public TextMeshProUGUI roosterText;

    private void Start()
    {
        UpdateEggText(0);
        UpdateChickText(0);
        UpdateHenText(0);
        UpdateRoosterText(0);
    }

    public void UpdateEggText(int count)
    {
        eggText.text = count.ToString();
    }

    public void UpdateChickText(int count)
    {
        chickText.text = count.ToString();
    }

    public void UpdateHenText(int count)
    {
        henText.text = count.ToString();
    }

    public void UpdateRoosterText(int count)
    {
        roosterText.text = count.ToString();
    }
}
