using Godot;
using System;

public class Global : Node {
    public override void _Ready() {
        OS.WindowMaximized = true;
        GD.Randomize();
    }

    public static SpatialMaterial RandomColor() {
        String[] colors = new String[] {
            "res://resources/sweetie_16/color_02.tres",
            "res://resources/sweetie_16/color_03.tres",
            "res://resources/sweetie_16/color_04.tres",
            "res://resources/sweetie_16/color_05.tres",
            "res://resources/sweetie_16/color_06.tres",
            "res://resources/sweetie_16/color_07.tres",
            "res://resources/sweetie_16/color_08.tres",
            "res://resources/sweetie_16/color_09.tres",
            "res://resources/sweetie_16/color_10.tres",
            "res://resources/sweetie_16/color_11.tres",
            "res://resources/sweetie_16/color_12.tres",
            "res://resources/sweetie_16/color_13.tres"
        };
        return ResourceLoader.Load<SpatialMaterial>(colors[GD.Randi() % colors.Length]);
    }
}
