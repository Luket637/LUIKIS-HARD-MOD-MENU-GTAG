using BepInEx;
using UnityEngine;
using UnityEngine.XR;

[BepInPlugin("luikis.hardmodmenu.test", "Lukis Hard Mod Menu", "Test")]
public class LukisHardModMenu : BaseUnityPlugin
{
    private bool menuOpen = true;

    private Rect windowRect = new Rect(100, 100, 420, 300);

    private GUIStyle windowStyle;
    private GUIStyle buttonStyle;
    private GUIStyle labelStyle;

    private bool lastYState;

    private void Start()
    {
        windowStyle = new GUIStyle(GUI.skin.window);
        windowStyle.normal.background =
            MakeTexture(new Color(0.45f, 0.45f, 0.45f));

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.background =
            MakeTexture(new Color(0.20f, 0.20f, 0.20f));
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.fontSize = 16;

        labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.normal.textColor = Color.white;
        labelStyle.fontSize = 20;
    }

    private void Update()
    {
        // PC keyboard testing
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }

        // Left controller Y button
        InputDevice leftController =
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftController.isValid &&
            leftController.TryGetFeatureValue(
                CommonUsages.secondaryButton,
                out bool yPressed))
        {
            // Only toggle once when the button is pressed
            if (yPressed && !lastYState)
            {
                ToggleMenu();
            }

            lastYState = yPressed;
        }
    }

    private void ToggleMenu()
    {
        menuOpen = !menuOpen;
    }

    private void OnGUI()
    {
        if (!menuOpen)
            return;

        windowRect = GUI.Window(
            12345,
            windowRect,
            DrawMenu,
            "Lukis Hard Mod Menu",
            windowStyle
        );
    }

    private void DrawMenu(int windowID)
    {
        GUI.Label(
            new Rect(15, 35, 300, 30),
            "TEST",
            labelStyle
        );

        // Empty buttons for now.
        // No mods/features yet.

        GUI.Button(
            new Rect(15, 80, 180, 40),
            "Empty",
            buttonStyle
        );

        GUI.Button(
            new Rect(15, 130, 180, 40),
            "Empty",
            buttonStyle
        );

        // Drag the menu
        GUI.DragWindow(
            new Rect(0, 0, 10000, 30)
        );
    }

    private Texture2D MakeTexture(Color color)
    {
        Texture2D texture = new Texture2D(1, 1);

        texture.SetPixel(0, 0, color);
        texture.Apply();

        return texture;
    }
}
