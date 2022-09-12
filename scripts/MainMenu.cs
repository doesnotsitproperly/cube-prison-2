using Godot;
using System;

public class MainMenu : Node2D
{
    private Button exit;
    private CheckBox checkBoxNintendo;
    private CheckBox checkBoxPlayStation;
    private CheckBox checkBoxXbox;
    private Sprite sprite;

    public override void _Ready()
    {
        exit = GetNode<Button>("ExitButton");
        checkBoxNintendo = GetNode<CheckBox>("CheckBoxes/CheckBoxNintendo");
        checkBoxPlayStation = GetNode<CheckBox>("CheckBoxes/CheckBoxPlayStation");
        checkBoxXbox = GetNode<CheckBox>("CheckBoxes/CheckBoxXbox");
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
        if (checkBoxNintendo.Pressed)
        {
            Global.Buttons = Global.PlatformButtons.Nintendo;
        }
        else if (checkBoxPlayStation.Pressed)
        {
            Global.Buttons = Global.PlatformButtons.PlayStation;
        }
        else
        {
            Global.Buttons = Global.PlatformButtons.Xbox;
        }

        GetTree().ChangeScene("res://scenes/main.tscn");
    }

    public void OnLicensesButtonPressed()
    {
        String filePath = OS.GetExecutablePath().GetBaseDir() + "/THIRD_PARTY_LICENSES.txt";
        if (new File().FileExists(filePath))
        {
            OS.ShellOpen(filePath);
        }
        else
        {
            OS.ShellOpen("https://github.com/doesnotsitproperly/cube-prison-2/blob/main/THIRD_PARTY_LICENSES.txt");
        }
    }

    public void OnExitButtonPressed()
    {
        GetTree().Notification(NotificationWmQuitRequest);
    }

    public void OnCheckBoxNintendoToggled(Boolean on)
    {
        if (on)
        {
            checkBoxPlayStation.Pressed = false;
            checkBoxXbox.Pressed = false;
        }
    }

    public void OnCheckBoxPlayStationToggled(Boolean on)
    {
        if (on)
        {
            checkBoxNintendo.Pressed = false;
            checkBoxXbox.Pressed = false;
        }
    }

    public void OnCheckBoxXboxToggled(Boolean on)
    {
        if (on)
        {
            checkBoxNintendo.Pressed = false;
            checkBoxPlayStation.Pressed = false;
        }
    }
}
