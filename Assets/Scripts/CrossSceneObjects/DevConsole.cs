using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevConsole : MonoBehaviour
{
    public static DevConsole Instance { get; private set; }
    public static bool IsOpen { get; private set; }
    public static bool IsInvincible { get; private set; }

    [Header("Appearance")]
    [SerializeField] private int consoleHeight = 300;
    [SerializeField] private int maxLogLines = 100;
    [SerializeField] private KeyCode toggleKey = KeyCode.BackQuote; // ~ key

    private string inputText = "";
    private readonly List<string> logLines = new List<string>();
    private Vector2 scrollPos;
    private GUIStyle logStyle;
    private GUIStyle inputStyle;
    private bool stylesInitialized;

    // command name -> action taking arg array
    private readonly Dictionary<string, Action<string[]>> commands =
        new Dictionary<string, Action<string[]>>(StringComparer.OrdinalIgnoreCase);

    // history navigation
    private readonly List<string> history = new List<string>();
    private int historyIndex = -1;

    public void Close()
    {
        Time.timeScale = 1f;
        IsOpen = false;
        inputText = "";
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        RegisterDefaultCommands();
    }

    private void Update()
    {
        // Only keep non-toggle stuff here, e.g. history nav if you want it outside OnGUI.
        // Toggle/Escape handling moves into OnGUI below.
    }

    private void NavigateHistory(int direction)
    {
        if (history.Count == 0) return;
        historyIndex = Mathf.Clamp(historyIndex + direction, 0, history.Count - 1);
        inputText = history[historyIndex];
    }

    private void OnGUI()
    {
        Event e = Event.current;

        // Handle open/close BEFORE the early-return, and BEFORE GUI.TextField
        // gets a chance to consume/type the key.
        if (e.type == EventType.KeyDown)
        {
            bool isToggleKeyEvent =
                e.keyCode == toggleKey || e.character == '`' || e.character == '~';

            if (isToggleKeyEvent)
            {
                // Only flip state on the "real" keydown; silently swallow the
                // character-echo event that follows it so it never reaches the field.
                if (e.keyCode == toggleKey)
                {
                    IsOpen = !IsOpen;
                    if (IsOpen) inputText = "";
                }
                e.Use();
                return;
            }

            if (IsOpen && e.keyCode == KeyCode.Escape)
            {
                IsOpen = false;
                e.Use();
                return;
            }
        }

        if (!IsOpen) {
            Time.timeScale = 1f;
            return;
        }

        Time.timeScale = 0f;

        InitStyles();

        // Log area
        Rect logRect = new Rect(10, 5, Screen.width - 20, consoleHeight - 40);
        scrollPos = GUI.BeginScrollView(logRect, scrollPos,
            new Rect(0, 0, logRect.width - 20, Mathf.Max(logLines.Count * 18, logRect.height)));

        for (int i = 0; i < logLines.Count; i++)
        {
            GUI.Label(new Rect(0, i * 18, logRect.width - 20, 18), logLines[i], logStyle);
        }
        GUI.EndScrollView();

        // Input field
        GUI.SetNextControlName("ConsoleInput");
        Rect inputRect = new Rect(10, consoleHeight - 30, Screen.width - 20, 24);

        bool submit = false;
        if (e.type == EventType.KeyDown && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter))
        {
            submit = true;
            e.Use();
        }

        inputText = GUI.TextField(inputRect, inputText, inputStyle);
        GUI.FocusControl("ConsoleInput");

        if (submit && !string.IsNullOrWhiteSpace(inputText))
        {
            SubmitCommand(inputText.Trim());
            inputText = "";
        }        
    }

    private void InitStyles()
    {
        if (stylesInitialized) return;
        logStyle = new GUIStyle(GUI.skin.label) { fontSize = 14, normal = { textColor = Color.white } };
        inputStyle = new GUIStyle(GUI.skin.textField) { fontSize = 14 };
        stylesInitialized = true;
    }

    private void SubmitCommand(string raw)
    {
        Log("> " + raw);
        history.Add(raw);
        historyIndex = history.Count;

        // split respecting simple quoted args: cmd "some arg" arg2
        var parts = SplitArgs(raw);
        if (parts.Length == 0) return;

        string cmdName = parts[0];
        string[] args = parts.Skip(1).ToArray();

        if (commands.TryGetValue(cmdName, out var action))
        {
            try
            {
                action.Invoke(args);
            }
            catch (Exception ex)
            {
                Log($"<color=red>Error: {ex.Message}</color>");
            }
        }
        else
        {
            Log($"<color=orange>Unknown command: {cmdName}</color>");
        }
    }

    private static string[] SplitArgs(string input)
    {
        var result = new List<string>();
        var current = "";
        bool inQuotes = false;

        foreach (char c in input)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }
            if (c == ' ' && !inQuotes)
            {
                if (current.Length > 0) { result.Add(current); current = ""; }
                continue;
            }
            current += c;
        }
        if (current.Length > 0) result.Add(current);
        return result.ToArray();
    }

    public void Log(string message)
    {
        logLines.Add(message);
        if (logLines.Count > maxLogLines) logLines.RemoveAt(0);
        scrollPos.y = float.MaxValue; // auto-scroll to bottom
    }

    public void RegisterCommand(string name, Action<string[]> callback)
    {
        commands[name] = callback;
    }

    private void RegisterDefaultCommands()
    {
        RegisterCommand("help", args =>
        {
            Log("Available commands: " + string.Join(", ", commands.Keys));
        });

        RegisterCommand("clear", args => logLines.Clear());

        RegisterCommand("loadlevel", args =>
        {
            if (args.Length < 1)
            {
                Log("<color=orange>Usage: loadlevel <sceneName></color>");
                return;
            }
            Log($"Loading scene: {args[0]}");
            OpenScene(args[0]);
        });

        RegisterCommand("iddqd", args =>
        {
            IsInvincible = !IsInvincible;
            Log($"Invincible mode {(IsInvincible ? "ON" : "OFF")}");
            return;
        });

        RegisterCommand("quit", args => MySceneManager.instance.QuitToWindows());
    }

    private void OpenScene(string level)
    {
        // force close DevConsole
        Close();

        switch (level) {
            case "darklobby": 
                MySceneManager.instance.OpenScene(GameScene.DARKLOBBY);
                break;
            case "lobby": 
                MySceneManager.instance.OpenScene(GameScene.LOBBY);
                break;
            case "jazz": 
                MySceneManager.instance.OpenScene(GameScene.JAZZ);
                break;
            case "boss": 
                MySceneManager.instance.OpenScene(GameScene.JAZZ_BOSS);
                break;
            case "tutorial": 
                MySceneManager.instance.OpenScene(GameScene.TUTORIAL);
                break;
            default:
                Log($"Error: Scene `{level}` not found");
                break;
        }
    }
}