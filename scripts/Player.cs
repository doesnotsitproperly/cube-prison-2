using Godot;
using System;

public class Player : KinematicBody {
    [Export] public Int32 Speed;
    [Export] public Single MouseSensitivity;

    private RayCast rayCast;
    private Spatial pivot;
    private Vector3 velocity;

    public override void _Ready() {
        rayCast = GetNode<RayCast>("Pivot/Camera/RayCast");
        pivot = GetNode<Spatial>("Pivot");
        velocity = new Vector3();

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Process(Single delta) {
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
    }

    public override void _PhysicsProcess(Single delta) {
        velocity = new Vector3();

        velocity.x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        velocity.z = Input.GetActionStrength("backward") - Input.GetActionStrength("forward");

        velocity = velocity.Rotated(Vector3.Up, Rotation.y);
        velocity = velocity.Normalized() * Speed;
        velocity = MoveAndSlide(velocity, Vector3.Up);
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
            InputEventMouseMotion mouseMotion = (InputEventMouseMotion) @event;

            RotateY(-mouseMotion.Relative.x * MouseSensitivity);
            pivot.RotateX(-mouseMotion.Relative.y * MouseSensitivity);

            pivot.Rotation = new Vector3(
                Mathf.Clamp(pivot.Rotation.x, -1.2f, 1.2f),
                pivot.Rotation.y,
                pivot.Rotation.z
            );
        }
    }
}
