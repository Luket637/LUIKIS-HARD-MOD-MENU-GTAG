using BepInEx;
using UnityEngine;
using UnityEngine.XR;

[BepInPlugin("luikis.hardmodmenu.test", "Lukis Hard Mod Menu", "Test")]
public class LukisHardModMenu : BaseUnityPlugin
{
    private bool menuOpen = true;
    private bool minimized = false;
    private bool lastYState;

    private Rect windowRect = new Rect(100, 100, 430, 320);

    private GUIStyle windowStyle;
    private GUIStyle titleStyle;
    private GUIStyle versionStyle;
    private GUIStyle buttonStyle;
    private GUIStyle controlButtonStyle;

    private Texture2D grayTexture;
    private Texture2D darkGrayTexture;

    private void Start()
    {
        // =========================
        // COLORS
        // =========================

        // Main panel = GRAY
        grayTexture = MakeTexture(new Color(0.45f, 0.45f, 0.45f));

        // Buttons/header = DARK GRAY
        darkGrayTexture = MakeTexture(new Color(0.20f, 0.20f, 0.20f));

        // =========================
        // MAIN WINDOW
        // =========================

        windowStyle = new GUIStyle(GUI.skin.window);
        windowStyle.normal.background = grayTexture;
        windowStyle.padding = new RectOffset(10, 10, 10, 10);

        // =========================
        // TITLE
        // =========================

        titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontSize = 21;
        titleStyle.fontStyle = FontStyle.Bold;

        // =========================
        // VERSION
        // =========================

        versionStyle = new GUIStyle(GUI.skin.label);
        versionStyle.normal.textColor = Color.white;
        versionStyle.fontSize = 14;

        // =========================
        // NORMAL BUTTONS
        // =========================

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.background = darkGrayTexture;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.hover.background = darkGrayTexture;
        buttonStyle.hover.textColor = Color.white;
        buttonStyle.active.background = darkGrayTexture;
        buttonStyle.active.textColor = Color.white;
        buttonStyle.fontSize = 16;

        // =========================
        // X / MINIMIZE BUTTONS
        // =========================

        controlButtonStyle = new GUIStyle(buttonStyle);
        controlButtonStyle.fontSize = 18;
        controlButtonStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void Update()
    {
        // =========================
        // F1 KEYBOARD TEST
        // =========================

        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }

        // =========================
        // LEFT CONTROLLER Y
        // =========================

        InputDevice leftController =
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftController.isValid &&
            leftController.TryGetFeatureValue(
                CommonUsages.secondaryButton,
                out bool yPressed))
        {
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

        if (menuOpen)
        {
            minimized = false;
        }
    }

    private void OnGUI()
    {
        if (!menuOpen)
            return;

        windowRect.width = 430;
        windowRect.height = minimized ? 65 : 320;

        windowRect = GUI.Window(
            12345,
            windowRect,
            DrawMenu,
            "",
            windowStyle
        );
    }

    private void DrawMenu(int windowID)
    {
        // =========================
        // DARK GRAY HEADER
        // =========================

        GUI.DrawTexture(
            new Rect(0, 0, windowRect.width, 55),
            darkGrayTexture
        );

        // =========================
        // TITLE
        // =========================

        GUI.Label(
            new Rect(15, 10, 290, 32),
            "LUKIS HARD MOD MENU",
            titleStyle
        );

        // =========================
        // MINIMIZE
        // =========================

        if (GUI.Button(
            new Rect(windowRect.width - 75, 12, 30, 30),
            "—",
            controlButtonStyle))
        {
            minimized = !minimized;
        }

        // =========================
        // CLOSE
        // =========================

        if (GUI.Button(
            new Rect(windowRect.width - 40, 12, 30, 30),
            "X",
            controlButtonStyle))
        {
            menuOpen = false;
        }

        // =========================
        // MINIMIZED
        // =========================

        if (minimized)
        {
            GUI.DragWindow(
                new Rect(0, 0, windowRect.width - 80, 55)
            );

            return;
        }

        // =========================
        // VERSION
        // =========================

        GUI.Label(
            new Rect(18, 65, 100, 25),
            "TEST",
            versionStyle
        );

        // =========================
        // EMPTY MENU AREA
        // =========================

        GUI.DrawTexture(
            new Rect(15, 95, windowRect.width - 30, 185),
            grayTexture
        );

        // =========================
        // EMPTY BUTTONS
        // =========================

        GUI.Button(
            new Rect(30, 110, 170, 42),
            "Empty",
            buttonStyle
        );

        GUI.Button(
            new Rect(30, 162, 170, 42),
            "Empty",
            buttonStyle
        );

        GUI.Button(
            new Rect(30, 214, 170, 42),
            "Empty",
            buttonStyle
        );

        // =========================
        // DRAG WINDOW
        // =========================

        GUI.DragWindow(
            new Rect(0, 0, windowRect.width - 80, 55)
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
