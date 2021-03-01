using RTL;
using UnityEngine;

public class RTLGUIDemo : MonoBehaviour
{

    // The original text
    public string InputText = "السلام العلیک - سلام خوش آمدید - שלום - ہیلو - העלא - 0123456789";

    // Number format in converted text (1,2,3 or ١,٢,٣)
    public NumberFormat NumberFormat = NumberFormat.Arabic;

    // Right to left text element width
    public int TextWidth = 460;

    // Used only for demo project
    public GUISkin Skin;

    // Direction of right to left text after being converted
    public ConvertDirection ConvertDirection = ConvertDirection.Forward;

    // Placeholder for converted text
    string convertedText = "";

    // Values to setup elements in OnGUI function
    float x = 350;
    float y = 0;
    Rect rtlRect = new Rect();

    // GUI element states to check if values are changed in GUI events
    string prevInputText = "";
    int prevWidth = 0;

    void OnGUI()
    {
        // Set GUI Skin
        GUI.skin = Skin;

        // Note:
        // Skin.customStyles[0] -> Style for right to left texts in this demo
        // Skin.customStyles[1] -> Style for left to right texts in this demo (default)

        GUI.color = Color.white;

        // Draw plugin version info
        y = 4;
        GUI.Label(new Rect(x, y, 600, 20), "RTL Plugin 6.0.4 | Right to left text converter (Arabic, Hebrew, Farsi, Urdu, Yiddish, Kurdish and Pashto)", Skin.customStyles[1]);


        GUI.color = Color.white;
        y += 32;
        GUI.Label(new Rect(x, y, 200, 25), "Unity GUI:", Skin.customStyles[1]);

        // The original text input
        InputText = GUI.TextArea(new Rect(x + 80, y, 460, 95), InputText);


        y += 100;
        GUI.Label(new Rect(x, y, 200, 25), "RTL:", Skin.customStyles[1]);
        rtlRect = new Rect(x + 80, y, TextWidth, 95);

        if (GUI.changed || prevInputText != InputText || prevWidth != TextWidth)
        {
            // Calling the function to update RTL value
            UpdateRTLText();
        }

        GUI.Box(rtlRect, "");
        GUI.Box(rtlRect, "");

        // Draw the converted text
        GUI.Label(rtlRect, convertedText, Skin.customStyles[0]);

        y += 100;

        // Draw RTL options on the screen to let users change RTL settings in game view mode at runtime
        GUI.Label(new Rect(x, y, 200, 25), "Options:", Skin.customStyles[1]);

        GUI.Label(new Rect(x + 98, y + 2, 400, 25), "Number Format:", Skin.customStyles[1]);
        if (GUI.Button(new Rect(x + 220, y, 100, 25), "Arabic (١٢٣)"))
        {
            NumberFormat = RTL.NumberFormat.Arabic;
            UpdateRTLText();
        }
        if (GUI.Button(new Rect(x + 324, y, 100, 25), "English (123)"))
        {
            NumberFormat = RTL.NumberFormat.English;
            UpdateRTLText();
        }
        if (GUI.Button(new Rect(x + 428, y, 100, 25), "Context"))
        {
            NumberFormat = RTL.NumberFormat.Context;
            UpdateRTLText();
        }

        y += 30;
        GUI.Label(new Rect(x + 98, y + 2, 400, 25), "Convert Direction:", Skin.customStyles[1]);
        if (GUI.Button(new Rect(x + 220, y, 100, 25), "Forward"))
        {
            ConvertDirection = RTL.ConvertDirection.Forward;
            UpdateRTLText();
        }
        if (GUI.Button(new Rect(x + 324, y, 100, 25), "Backward"))
        {
            ConvertDirection = RTL.ConvertDirection.Backward;
            UpdateRTLText();
        }

        y += 30;
        GUI.Label(new Rect(x + 98, y + 2, 400, 25), "Text Width:", Skin.customStyles[1]);
        string newW = GUI.TextField(new Rect(x + 220, y, 100, 25), TextWidth.ToString());
        int.TryParse(newW, out TextWidth);


        GUI.color = new Color(0.7f, 1f, 0.98f);

        y += 52;
        GUI.Label(new Rect(x, y, 600, 20), "RTL Plugin supports new UI elements as well as OnGUI function. Read more: http://www.heygamers.com", Skin.customStyles[1]);

        y += 22;
        GUI.Label(new Rect(x, y, 600, 20), "Also supports: Word wrapping, math formulas, mixed LTR and RTL languages, parentheses and punctuations ...", Skin.customStyles[1]);

    }

    /// <summary>
    /// Update method called by OnGUI in order to convert text to RTL
    /// </summary>
    private void UpdateRTLText()
    {
        // Setup the font used to render rigth to left text
        UnityFontInfo fontInfo = new UnityFontInfo();
        fontInfo.Font = Skin.customStyles[0].font;
        fontInfo.FontSize = Skin.customStyles[0].fontSize;
        fontInfo.FontStyle = Skin.customStyles[0].fontStyle;

        // Convert and word wrap the RTL text
        convertedText = RTLService.ConvertWordWrap(InputText, TextWidth, fontInfo, NumberFormat, ConvertDirection);

        // Write the result to debug
        Debug.Log(convertedText);

        // Update GUI states
        prevWidth = TextWidth;
        prevInputText = InputText;
    }
}
