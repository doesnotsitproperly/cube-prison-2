using Godot;

public class GUI : CanvasLayer
{
    public override void _Ready()
    {
        if (OS.GetName() == "HTML5")
        {
            Label label = GetNode<Label>("Label");
            label.Text = label.Text.Replace("ESC", "tilde");
        }
    }
}
