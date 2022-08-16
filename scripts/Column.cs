using Godot;

public class Column : StaticBody {
    public void SetMaterial(SpatialMaterial material) {
        GetNode<MeshInstance>("MeshInstance").MaterialOverride = material;
    }
}
