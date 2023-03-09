using NaughtyAttributes;
using UnityEngine;

namespace Assets.Script.Tools
{
    public class ChangeAnchors : MonoBehaviour
    {
        [Button]
        public void ChangeAnchor()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            RectTransform parentTransform = rectTransform.parent.GetComponent<RectTransform>();
            float width = parentTransform.rect.width;
            float height = parentTransform.rect.height;
            
            Vector3 position = rectTransform.localPosition;
            Vector2 size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;
            rectTransform.localPosition = position;
            rectTransform.sizeDelta = size;
            float xMin = Mathf.Abs((rectTransform.anchoredPosition.x - (rectTransform.sizeDelta.x / 2)) / width);
            float yMin = Mathf.Abs((rectTransform.anchoredPosition.y - (rectTransform.sizeDelta.y / 2)) / height);
            float xMax = Mathf.Abs((rectTransform.anchoredPosition.x + (rectTransform.sizeDelta.x / 2)) / width);
            float yMax = Mathf.Abs((rectTransform.anchoredPosition.y + (rectTransform.sizeDelta.y / 2)) / height);
            rectTransform.anchorMin = new Vector2(xMin, yMin);
            rectTransform.anchorMax = new Vector2(xMax, yMax);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
        }
    }
}
