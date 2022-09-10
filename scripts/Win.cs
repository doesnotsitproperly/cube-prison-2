using Godot;

public class Win : Node2D
{
    public override void _Ready()
    {
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").VolumeDb = GD.Linear2Db(Global.Volume);

        Input.MouseMode = Input.MouseModeEnum.Visible;
    }
}
