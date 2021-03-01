using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Graph;

[RequireComponent(typeof(Dropdown))]
public class GradientManager : MonoBehaviour
{
    private Gradient viridis;
    private Gradient plasma;
    private Gradient inferno;
    private Gradient magma;
    private Gradient cividis;
    private Gradient flame;
    private GradientColorScheme currentSelectedGradient;
    private Dropdown paletteDropdwn;

    public ColorPalette palette;
    public ColorMapper colorMapper;

    private enum GradientColorScheme {
        VIRIDIS, 
        PLASMA,
        INFERNO, 
        MAGMA, 
        CIVIDIS, 
        FLAME
    };

    // Start is called before the first frame update
    void Awake()
    {
        paletteDropdwn = GetComponent<Dropdown>();
        InitialiseGradients();
        paletteDropdwn.onValueChanged.AddListener(delegate {
            ChangeGradient(paletteDropdwn);
        });
        palette.colorGradient = GetCurrentGradient();
    }

    public void ChangeGradient(Dropdown newGradient) {
        currentSelectedGradient = (GradientColorScheme)newGradient.value;
        palette.colorGradient = GetCurrentGradient();
        colorMapper.SendMessage("RemapColors", palette.colorGradient);
    }

    public Gradient GetCurrentGradient() {
        switch (currentSelectedGradient) {
            case GradientColorScheme.VIRIDIS:
                return viridis;
            case GradientColorScheme.PLASMA:
                return plasma;
            case GradientColorScheme.INFERNO:
                return inferno;
            case GradientColorScheme.MAGMA:
                return magma;
            case GradientColorScheme.CIVIDIS:
                return cividis;
            case GradientColorScheme.FLAME:
                return flame;
            default:
                return flame;
        }
    }

    private void InitialiseGradients()
    {
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color32(255, 255, 0, 1);
        colorKey[0].time = 0f;
        colorKey[1].color = new Color32(205, 0, 0, 1);
        colorKey[1].time = 1f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        flame = new Gradient();
        flame.SetKeys(colorKey, alphaKey);

        // Set Viridis Gradient
        viridis = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color32(243, 233, 28, 1);
        colorKey[0].time = 1.0f;
        colorKey[1].color = new Color32(147, 219, 53, 1);
        colorKey[1].time = 0.86f;
        colorKey[2].color = new Color32(61, 195, 108, 1);
        colorKey[2].time = 0.71f;
        colorKey[3].color = new Color32(34, 162, 135, 1);
        colorKey[3].time = 0.57f;
        colorKey[4].color = new Color32(52, 127, 142, 1);
        colorKey[4].time = 0.43f;
        colorKey[5].color = new Color32(67, 91, 141, 1);
        colorKey[5].time = 0.29f;
        colorKey[6].color = new Color32(79, 48, 127, 1);
        colorKey[6].time = 0.14f;
        colorKey[7].color = new Color32(72, 0, 84, 1);
        colorKey[7].time = 0.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        viridis.SetKeys(colorKey, alphaKey);

        // Set Plasma Gradient
        plasma = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color32(228, 250, 21, 1);
        colorKey[0].time = 1.0f;
        colorKey[1].color = new Color32(246, 189, 39, 1);
        colorKey[1].time = 0.86f;
        colorKey[2].color = new Color32(238, 137, 73, 1);
        colorKey[2].time = 0.71f;
        colorKey[3].color = new Color32(216, 91, 105, 1);
        colorKey[3].time = 0.57f;
        colorKey[4].color = new Color32(186, 47, 138, 1);
        colorKey[4].time = 0.43f;
        colorKey[5].color = new Color32(146, 0, 166, 1);
        colorKey[5].time = 0.29f;
        colorKey[6].color = new Color32(98, 0, 164, 1);
        colorKey[6].time = 0.14f;
        colorKey[7].color = new Color32(47, 0, 135, 1);
        colorKey[7].time = 0.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        plasma.SetKeys(colorKey, alphaKey);

        // Set Inferno Gradient
        inferno = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color32(245, 255, 163, 1);
        colorKey[0].time = 1.0f;
        colorKey[1].color = new Color32(242, 194, 35, 1);
        colorKey[1].time = 0.86f;
        colorKey[2].color = new Color32(239, 125, 21, 1);
        colorKey[2].time = 0.71f;
        colorKey[3].color = new Color32(208, 72, 67, 1);
        colorKey[3].time = 0.57f;
        colorKey[4].color = new Color32(158, 40, 100, 1);
        colorKey[4].time = 0.43f;
        colorKey[5].color = new Color32(105, 15, 111, 1);
        colorKey[5].time = 0.29f;
        colorKey[6].color = new Color32(48, 7, 84, 1);
        colorKey[6].time = 0.14f;
        colorKey[7].color = new Color32(1, 0, 4, 1);
        colorKey[7].time = 0.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        inferno.SetKeys(colorKey, alphaKey);

        // Set Magma Gradient
        magma = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color32(255, 222, 152, 1);
        colorKey[0].time = 1.0f;
        colorKey[1].color = new Color32(255, 151, 98, 1);
        colorKey[1].time = 0.86f;
        colorKey[2].color = new Color32(251, 76, 92, 1);
        colorKey[2].time = 0.71f;
        colorKey[3].color = new Color32(197, 34, 122, 1);
        colorKey[3].time = 0.57f;
        colorKey[4].color = new Color32(137, 15, 133, 1);
        colorKey[4].time = 0.43f;
        colorKey[5].color = new Color32(84, 0, 125, 1);
        colorKey[5].time = 0.29f;
        colorKey[6].color = new Color32(39, 10, 83, 1);
        colorKey[6].time = 0.14f;
        colorKey[7].color = new Color32(9, 5, 28, 1);
        colorKey[7].time = 0.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        magma.SetKeys(colorKey, alphaKey);

        // Set Cividis Gradient
        cividis = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color32(251, 223, 0, 1);
        colorKey[0].time = 1.0f;
        colorKey[1].color = new Color32(226, 203, 55, 1);
        colorKey[1].time = 0.86f;
        colorKey[2].color = new Color32(184, 177, 93, 1);
        colorKey[2].time = 0.71f;
        colorKey[3].color = new Color32(157, 148, 113, 1);
        colorKey[3].time = 0.57f;
        colorKey[4].color = new Color32(118, 118, 118, 1);
        colorKey[4].time = 0.43f;
        colorKey[5].color = new Color32(82, 90, 111, 1);
        colorKey[5].time = 0.29f;
        colorKey[6].color = new Color32(43, 65, 111, 1);
        colorKey[6].time = 0.14f;
        colorKey[7].color = new Color32(0, 44, 104, 1);
        colorKey[7].time = 0.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        cividis.SetKeys(colorKey, alphaKey);
    }


}
