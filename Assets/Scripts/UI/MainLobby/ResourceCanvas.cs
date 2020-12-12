using UnityEngine;
using TMPro;
using Developers.Structure;

public class ResourceCanvas : MonoBehaviour, IGameUI
{
    [SerializeField] TextMeshProUGUI DisplayGold = null;
    [SerializeField] TextMeshProUGUI DisplayCash = null;

    public void OnUpdate()
    {
        DisplayGold.SetText ( GameManager.Instance.LocalPlayer.DisplayGold ( ResourceType.Gold ) );
        DisplayCash.SetText ( GameManager.Instance.LocalPlayer.DisplayGold ( ResourceType.Cash ) );
    }
}