using Godot;
using System;

public class GUI : CanvasLayer
{
    public override void _Ready()
    {
        Label label = GetNode<Label>("Label");
        String text = label.Text;

        if (OS.GetName() == "HTML5")
        {
            text = text.Replace("ESC", "tilde");
        }

        if (Global.Buttons == Global.PlatformButtons.Nintendo)
        {
            text = text.Replace("A button", "B button");
        }
        else if (Global.Buttons == Global.PlatformButtons.PlayStation)
        {
            text = text.Replace("A button", "cross button");
        }

        label.Text = text;
    }
}
