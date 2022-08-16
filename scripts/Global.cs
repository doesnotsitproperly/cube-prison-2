using Godot;
using System;

public class Global : Node {
    public override void _Ready() {
        GD.Randomize();
    }

    public static SpatialMaterial RandomColor() {
        String[] colors = new String[] {
            "res://resources/sweetie_16/color_2.tres",
            "res://resources/sweetie_16/color_3.tres",
            "res://resources/sweetie_16/color_4.tres",
            "res://resources/sweetie_16/color_5.tres",
            "res://resources/sweetie_16/color_6.tres",
            "res://resources/sweetie_16/color_7.tres",
            "res://resources/sweetie_16/color_8.tres",
            "res://resources/sweetie_16/color_9.tres",
            "res://resources/sweetie_16/color_10.tres",
            "res://resources/sweetie_16/color_11.tres",
            "res://resources/sweetie_16/color_12.tres",
            "res://resources/sweetie_16/color_13.tres"
        };
        return ResourceLoader.Load<SpatialMaterial>(colors[GD.Randi() % colors.Length]);
    }
}
