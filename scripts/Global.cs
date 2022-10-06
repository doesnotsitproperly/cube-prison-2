using Godot;
using System;

public class Global : Node
{
    public static PlatformButtons Buttons;
    public static Single Volume;

    public override void _Ready()
    {
        GD.Randomize();

        if (OS.GetName() == "HTML5")
        {
            InputEventKey escape = new InputEventKey();
            escape.PhysicalScancode = (UInt32)KeyList.Escape;
            InputMap.ActionEraseEvent("quit", escape);

            InputEventKey quoteLeft = new InputEventKey();
            quoteLeft.PhysicalScancode = (UInt32)KeyList.Quoteleft;
            InputMap.ActionAddEvent("quit", quoteLeft);
        }
        else
        {
            ConfigFile config = new ConfigFile();
            if (new File().FileExists("user://settings.cfg"))
            {
                config.Load("user://settings.cfg");
                Volume = (Single)config.GetValue("settings", "volume", 0.2f);
            }
            else
            {
                config.SetValue("settings", "volume", 0.2f);
                config.Save("user://settings.cfg");
                Volume = 0.2f;
            }
        }
    }

    public static SpatialMaterial RandomColor()
    {
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

    public enum PlatformButtons
    {
        Nintendo,
        PlayStation,
        Xbox
    }
}
