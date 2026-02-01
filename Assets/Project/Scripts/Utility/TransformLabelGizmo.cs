using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class TransformLabelGizmo : MonoBehaviour
{
    [Header("Label")]
    [SerializeField]
    private bool _showLabel = true;

    [SerializeField]
    private bool _onlyWhenSelected = false;

    [SerializeField, TextArea]
    private string _customText = "";

    [SerializeField]
    private int _fontSize = 12;

    [SerializeField]
    private Color _textColor = Color.yellow;

    [SerializeField]
    private bool _scaleWithDistance = true;

    [SerializeField]
    private float _verticalOffset = 1.25f;

    [SerializeField]
    private bool _drawLeaderLine = true;

    [Header("Background")]
    [SerializeField]
    private bool _drawBackground = true;

    [SerializeField]
    private Color _backgroundColor = new(0.0f, 0.0f, 0.0f, 0.45f);

    [SerializeField]
    private bool _drawBorder = true;

    [SerializeField]
    private Color _borderColor = new(1.0f, 1.0f, 1.0f, 0.25f);

    [SerializeField]
    private float _borderThickness = 1.0f;

    [SerializeField]
    private Vector2 _padding = new(6.0f, 3.0f);

    [Header("Culling")]
    [SerializeField]
    private bool _cullWhenBehindCamera = true;

    [SerializeField]
    private bool _cullWhenOffscreen = true;

    [Header("Wrapping")]
    [SerializeField]
    private bool _wrapText = true;

    [SerializeField]
    private float _maxLabelWidth = 220.0f; // pixels; 0 or less = no cap

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!_showLabel || _onlyWhenSelected)
            return;

        DrawLabel();
    }

    private void OnDrawGizmosSelected()
    {
        if (!_showLabel)
            return;

        DrawLabel();
    }

    private void DrawLabel()
    {
        string label = string.IsNullOrEmpty(_customText) ? gameObject.name : _customText;

        float offset = _verticalOffset;

        if (_scaleWithDistance)
        {
            float handleSize = HandleUtility.GetHandleSize(transform.position);
            offset *= handleSize * 0.2f;
        }

        Vector3 worldPos = transform.position + Vector3.up * offset;

        Camera cam = Camera.current;

        if (cam == null)
            return;

        Vector3 vp = cam.WorldToViewportPoint(worldPos);

        if (_cullWhenBehindCamera && vp.z <= 0.0f)
            return;

        if (_cullWhenOffscreen)
        {
            if (vp.x < 0.0f || vp.x > 1.0f || vp.y < 0.0f || vp.y > 1.0f)
                return;
        }

        GUIStyle style = new(EditorStyles.boldLabel)
        {
            normal =
            {
                textColor = _textColor
            },
            fontSize = _fontSize,
            alignment = TextAnchor.MiddleCenter,
            richText = true,
            clipping = TextClipping.Overflow,
            wordWrap = _wrapText
        };

        GUIContent content = new(label);

        Vector2 textSize = style.CalcSize(content);

        float width = textSize.x + (_padding.x * 2.0f);
        float height = textSize.y + (_padding.y * 2.0f);

        if (_wrapText && _maxLabelWidth > 0.0f)
        {
            if (width > _maxLabelWidth)
            {
                float contentWidth = Mathf.Max(1.0f, _maxLabelWidth - (_padding.x * 2.0f));
                float contentHeight = style.CalcHeight(content, contentWidth);

                width = _maxLabelWidth;
                height = contentHeight + (_padding.y * 2.0f);
            }
        }

        Handles.BeginGUI();

        Vector3 guiPoint = HandleUtility.WorldToGUIPoint(worldPos);

        Rect rect = new(
            guiPoint.x - (width * 0.5f),
            guiPoint.y - height,
            width,
            height
        );

        if (_drawBackground)
        {
            if (_drawBorder && _borderThickness > 0.0f)
            {
                Rect borderRect = new(
                    rect.x - _borderThickness,
                    rect.y - _borderThickness,
                    rect.width + (_borderThickness * 2.0f),
                    rect.height + (_borderThickness * 2.0f)
                );

                EditorGUI.DrawRect(borderRect, _borderColor);
            }

            EditorGUI.DrawRect(rect, _backgroundColor);
        }

        Rect textRect = new(
            rect.x + _padding.x,
            rect.y + _padding.y,
            rect.width - (_padding.x * 2.0f),
            rect.height - (_padding.y * 2.0f)
        );

        GUI.Label(textRect, content, style);

        Handles.EndGUI();

        if (_drawLeaderLine)
        {
            Handles.color = _textColor;
            Handles.DrawLine(transform.position, worldPos);
        }
    }
#endif
}