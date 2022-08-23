using Godot;
using System;

public class Win : Node2D {
    private Label label;
    private Vector2 screenSize;
    private Viewport viewport;

    public override void _Ready() {
        VisualServer.SetDefaultClearColor(new Color(0f, 0f, 0f, 1f));
        Input.MouseMode = Input.MouseModeEnum.Visible;

        label = GetNode<Label>("Label");
        viewport = GetViewport();

        screenSize = viewport.Size;
        ResizeText();
    }

    public override void _Process(Single delta) {
        if (viewport.Size != screenSize) {
            screenSize = viewport.Size;
            ResizeText();
        }
    }

    public void ResizeText() {
        Int32 scale = Mathf.FloorToInt(screenSize.x * 0.2f / 40f);
        label.RectScale = new Vector2((Single) scale, (Single) scale);

        label.RectPosition = new Vector2(
            (label.RectSize.x / 2f) * scale * -1f,
            (label.RectSize.y / 2f) * scale * -1f
        );
    }
}
