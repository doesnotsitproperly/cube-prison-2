using Godot;
using System;

public class Player : KinematicBody {
    [Export]
    public Int32 Speed;
    [Export]
    public Single MouseSensitivity;

    private Spatial pivot;
    private Vector3 velocity;

    public override void _Ready() {
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
    }

    public override void _PhysicsProcess(Single delta) {
        velocity = new Vector3();

        velocity.x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        velocity.z = Input.GetActionStrength("backward") - Input.GetActionStrength("forward");

        velocity = velocity.Normalized() * Speed;
        velocity = MoveAndSlide(velocity, Vector3.Up);
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
            InputEventMouseMotion mouseMotion = (InputEventMouseMotion) @event;

            RotateY(-mouseMotion.Relative.x * MouseSensitivity);
            pivot.RotateX(-mouseMotion.Relative.y * MouseSensitivity);

            ClampX(pivot.Rotation, -1.2f, 1.2f);
        }
    }

    private void ClampX(Vector3 vector, Single min, Single max) {
        if (vector.x > max) {
            vector = new Vector3(max, vector.y, vector.z);
        } else if (vector.x < min) {
            vector = new Vector3(min, vector.y, vector.z);
        }
    }
}
