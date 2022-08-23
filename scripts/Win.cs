using Godot;

public class Win : Node2D {
    public override void _Ready() {
        VisualServer.SetDefaultClearColor(new Color(0f, 0f, 0f, 1f));
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }
}
