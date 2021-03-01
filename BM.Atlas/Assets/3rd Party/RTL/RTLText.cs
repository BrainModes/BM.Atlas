using RTL;
using UnityEngine;
using UnityEngine.UI;

// RTLText is using Text component to render the converted text
[RequireComponent(typeof(Text))]
public class RTLText : MonoBehaviour
{
    public Text textComponent;

    [TextArea]
    // The original text
    public string originalText;

    [Header("RTL Settings")]
    // Number format in converted text (1,2,3 or ١,٢,٣)
    public NumberFormat numberFormat = NumberFormat.Context;

    // Direction of right to left text after being converted
    public ConvertDirection convertDirection = ConvertDirection.Forward;

    // wordwrap right to left text. If enabled, wordWrapWidth should be set correctly.
    public bool wordWrap = true; // For single line and short texts set it to false.
    
    // Right to left text element width
    public float wordWrapWidth = 130; // You can dynamically set this to rectTransform.rect.width of Text component

    public void Start()
    {
        // Find the target text component
        textComponent = GetComponent<Text>();

        // Un comment following to set wordwrap width dynamically to Text component's rectTransform.rect.width 
        wordWrapWidth = textComponent.rectTransform.rect.width;
    }
    public void Convert()
    {
        if (wordWrap)
        {
            // RTL handles word wrapping so disable Unity word wrapping
            textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;

            // Get the current font information
            UnityFontInfo fontInfo = new UnityFontInfo();
            fontInfo.Font = textComponent.font;
            fontInfo.FontSize = textComponent.fontSize;
            fontInfo.FontStyle = textComponent.fontStyle;

            // Convert and apply right to left word wrapping
            textComponent.text = RTLService.ConvertWordWrap(originalText, wordWrapWidth, fontInfo, numberFormat, convertDirection);
        }
        else
        {
            // Convert the text to right to left
            textComponent.text = RTLService.Convert(originalText, numberFormat, convertDirection);
        }
    }
}
