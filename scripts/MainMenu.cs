using Godot;
using System;

public class MainMenu : Node2D {
    private Sprite sprite;

    public override void _Ready() {
        sprite = GetNode<Sprite>("Sprite");
    }

    public override void _Process(Single delta) {
        sprite.RotationDegrees += 1f;
    }

    public void OnButtonPressed() {
        GetTree().ChangeScene("res://scenes/main.tscn");
    }
}
