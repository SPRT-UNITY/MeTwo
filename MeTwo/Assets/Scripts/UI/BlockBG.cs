using UnityEngine;
using UnityEngine.EventSystems;

public class BlockBG : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // BG 클릭 시, 가장 최근의 팝업을 지우기
        Managers.UI.ClosePopupUI();
    }
}
