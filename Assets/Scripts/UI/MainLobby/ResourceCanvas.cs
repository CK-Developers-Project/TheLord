using UnityEngine;
using TMPro;
using Developers.Structure;

public class ResourceCanvas : MonoBehaviour, IGameUI
{
    [SerializeField] TextMeshProUGUI DisplayText = null;

    // FIXME : 한국어 밖에 없으므로 자원 고정
    string ResourceText = "자원";

    public void OnUpdate()
    {
        DisplayText.SetText ( string.Format ( "{0}: {1}", ResourceText, GameManager.Instance.LocalPlayer.DisplayGold ( ResourceType.Gold ) ) );
    }
}