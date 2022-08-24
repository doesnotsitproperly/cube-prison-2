using Godot;
using System;

public class Player : KinematicBody {
    [Export] public Int32 Speed;
    [Export] public Single JoystickCameraSensitivity;
    [Export] public Single MouseCameraSensitivity;

    private ColorRect healthMeter;
    private RayCast rayCast;
    private Single health;
    private Spatial pivot;

    public override void _Ready() {
        healthMeter = GetNode<ColorRect>("../GUI/Health");
        rayCast = GetNode<RayCast>("Pivot/Camera/RayCast");
        health = 1f;
        pivot = GetNode<Spatial>("Pivot");

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Process(Single delta) {
        healthMeter.RectScale = new Vector2(health, 1f);
    }

    public override void _PhysicsProcess(Single delta) {
        if (Input.IsActionJustPressed("quit")) {
            if (Input.MouseMode == Input.MouseModeEnum.Captured) {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            } else {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }
        }

        if (Input.IsActionJustPressed("interact")) {
            Node node = (Node) rayCast.GetCollider();
            if (node != null) {
                if (node.Name == "Button") {
                    GetTree().ChangeScene("res://scenes/win.tscn");
                }
            }
        }

        Vector3 velocity = new Vector3();

        velocity.x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        velocity.z = Input.GetActionStrength("backward") - Input.GetActionStrength("forward");

        velocity = velocity.Rotated(Vector3.Up, Rotation.y);
        velocity = velocity.Normalized() * Speed;
        velocity = MoveAndSlide(velocity, Vector3.Up);

        MoveCamera(
            -(Input.GetActionRawStrength("camera_down") - Input.GetActionStrength("camera_up")) * JoystickCameraSensitivity,
            -(Input.GetActionStrength("camera_right") - Input.GetActionStrength("camera_left")) * JoystickCameraSensitivity
        );
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
            InputEventMouseMotion mouseMotion = (InputEventMouseMotion) @event;
            MoveCamera(
                -mouseMotion.Relative.y * MouseCameraSensitivity,
                -mouseMotion.Relative.x * MouseCameraSensitivity
            );
        }
    }

    public void MoveCamera(Single xRotation, Single yRotation) {
        RotateY(yRotation);
        pivot.RotateX(xRotation);

        pivot.Rotation = new Vector3(
            Mathf.Clamp(pivot.Rotation.x, -1.2f, 1.2f),
            pivot.Rotation.y,
            pivot.Rotation.z
        );
    }
}
