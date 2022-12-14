using Godot;

public class FourRoom : Spatial
{
    public override void _Ready()
    {
        SpatialMaterial color = Global.RandomColor();
        GetNode<Column>("Column1").SetMaterial(color);
        GetNode<Column>("Column2").SetMaterial(color);
        GetNode<Column>("Column3").SetMaterial(color);
        GetNode<Column>("Column4").SetMaterial(color);
    }
}
