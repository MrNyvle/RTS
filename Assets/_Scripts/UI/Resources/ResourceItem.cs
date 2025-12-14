using _Scripts.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Resources
{
    public class ResourceItem : MonoBehaviour
    {
        public Image image;
        public TMP_Text text;
        
        public void SetItemUI(EResource resource , int quantity)
        {
            text.text = quantity.ToString();
            image.sprite = UiManager.Instance.iconsUI.GetResourceSprite(resource);
        }
    }
}