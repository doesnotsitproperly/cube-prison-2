using Godot;
using System;

public class Player : KinematicBody
{
    [Export]
    public Int32 Speed;
    [Export]
    public Single JoystickCameraSensitivity;
    [Export]
    public Single MouseCameraSensitivity;

    private ColorRect healthMeter;
    private RayCast rayCast;
    private Single health;
    private Spatial pivot;

    private TouchScreenButton upButton;
    private TouchScreenButton downButton;
    private TouchScreenButton leftButton;
    private TouchScreenButton rightButton;

    public override void _Ready()
    {
        healthMeter = GetNode<ColorRect>("../GUI/Health");
        rayCast = GetNode<RayCast>("Pivot/Camera/RayCast");
        health = 1f;
        pivot = GetNode<Spatial>("Pivot");

        upButton = GetNode<TouchScreenButton>("../GUI/TouchScreenButtons/TouchScreenButtonUp");
        downButton = GetNode<TouchScreenButton>("../GUI/TouchScreenButtons/TouchScreenButtonDown");
        leftButton = GetNode<TouchScreenButton>("../GUI/TouchScreenButtons/TouchScreenButtonLeft");
        rightButton = GetNode<TouchScreenButton>("../GUI/TouchScreenButtons/TouchScreenButtonRight");

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(Single delta)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
                Input.MouseMode = Input.MouseModeEnum.Visible;
            else
                Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        if (Input.IsActionJustPressed("interact"))
        {
            Node node = (Node)rayCast.GetCollider();
            if (node != null)
                if (node.Name == "Button")
                    GetTree().ChangeScene("res://scenes/win.tscn");
        }

        Vector3 velocity = new Vector3(Input.GetActionStrength("right") - Input.GetActionStrength("left"), 0f, Input.GetActionStrength("backward") - Input.GetActionStrength("forward"));

        if (upButton.IsPressed())
            velocity.z--;
        if (downButton.IsPressed())
            velocity.z++;
        if (leftButton.IsPressed())
            velocity.x--;
        if (rightButton.IsPressed())
            velocity.x++;

        MoveAndSlide(velocity.Rotated(Vector3.Up, Rotation.y).Normalized() * Speed, Vector3.Up);

        MoveCamera(new Vector2(Input.GetActionStrength("camera_down") - Input.GetActionStrength("camera_up"), Input.GetActionStrength("camera_right") - Input.GetActionStrength("camera_left")) * JoystickCameraSensitivity * -1);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            InputEventMouseMotion mouseMotion = (InputEventMouseMotion)@event;
            MoveCamera(new Vector2(mouseMotion.Relative.y, mouseMotion.Relative.x) * MouseCameraSensitivity * -1);
        }
    }

    public void MoveCamera(Vector2 cameraRotation)
    {
        RotateY(cameraRotation.y);
        pivot.RotateX(cameraRotation.x);

        pivot.Rotation = new Vector3(Mathf.Clamp(pivot.Rotation.x, -1.2f, 1.2f), pivot.Rotation.y, pivot.Rotation.z);
    }
}
