using Godot;
using System;

public class MainMenu : Node2D
{
    private Button exit;
    private Sprite sprite;

    public override void _Ready()
    {
        exit = GetNode<Button>("ExitButton");
        sprite = GetNode<Sprite>("Sprite");

        if (OS.GetName() == "HTML5")
        {
            exit.QueueFree();
        }
    }

    public override void _Process(Single delta)
    {
        sprite.RotationDegrees += 1f;
    }

    public void OnStartButtonPressed()
    {
        GetTree().ChangeScene("res://scenes/main.tscn");
    }

    public void OnLicensesButtonPressed()
    {
        Error error = OS.ShellOpen(OS.GetExecutablePath().GetBaseDir() + "/THIRD_PARTY_LICENSES.txt");
        if (error != Error.Ok)
        {
            OS.ShellOpen("https://github.com/doesnotsitproperly/cube-prison-2/blob/main/THIRD_PARTY_LICENSES.txt");
        }
    }

    public void OnExitButtonPressed()
    {
        GetTree().Notification(NotificationWmQuitRequest);
    }
}
