using Godot;
using System;
using System.Collections.Generic;

public class Main : Spatial
{
    private Direction direction = Direction.Left;
    private List<Vector2> walls = new List<Vector2>();
    private PackedScene wallScene;
    private Vector3[] roomOrigins = new Vector3[8];

    public override void _Ready()
    {
        PackedScene[] roomScenes = new PackedScene[] {
            ResourceLoader.Load<PackedScene>("res://scenes/rooms/three_room.tscn"),
            ResourceLoader.Load<PackedScene>("res://scenes/rooms/four_room.tscn"),
            ResourceLoader.Load<PackedScene>("res://scenes/rooms/five_room.tscn")
        };

        PackedScene doorwayScene = ResourceLoader.Load<PackedScene>("res://scenes/doorway.tscn");
        wallScene = ResourceLoader.Load<PackedScene>("res://scenes/wall.tscn");

        roomOrigins[0] = new Vector3();
        Spatial firstDoorway = doorwayScene.Instance<Spatial>();
        firstDoorway.Translation = new Vector3(roomOrigins[0].x, roomOrigins[0].y, roomOrigins[0].z - 17f);
        walls.Add(new Vector2(firstDoorway.Translation.x, firstDoorway.Translation.z));
        AddChild(firstDoorway);
        PlaceWalls(roomOrigins[0]);

        for (Byte i = 1; i < roomOrigins.Length; i++)
        {
            Spatial room = roomScenes[GD.Randi() % roomScenes.Length].Instance<Spatial>();
            room.Translation = roomOrigins[i - 1];

            Spatial doorway = doorwayScene.Instance<Spatial>();

            Vector3 translation = room.Translation;
            if (direction == Direction.Forward)
            {
                if (GD.Randi() % 2 == 0)
                {
                    // Place left
                    direction = Direction.Left;

                    translation.x -= 34f;

                    doorway.Translation = new Vector3(translation.x + 17f, translation.y, translation.z);
                    doorway.RotationDegrees = new Vector3(0f, 90f, 0f);
                    walls.Add(new Vector2(doorway.Translation.x, doorway.Translation.z));

                    PlaceWalls(room.Translation);
                }
                else
                {
                    // Place right
                    direction = Direction.Right;

                    translation.x += 34f;

                    doorway.Translation = new Vector3(translation.x - 17f, translation.y, translation.z);
                    doorway.RotationDegrees = new Vector3(0f, 90f, 0f);
                    walls.Add(new Vector2(doorway.Translation.x, doorway.Translation.z));

                    PlaceWalls(room.Translation);
                }
            }
            else
            {
                // Place forward
                direction = Direction.Forward;

                translation.z -= 34f;

                doorway.Translation = new Vector3(translation.x, translation.y, translation.z + 17f);
                walls.Add(new Vector2(doorway.Translation.x, doorway.Translation.z));

                PlaceWalls(room.Translation);
            }
            room.Translation = translation;

            AddChild(room);
            roomOrigins[i] = room.Translation;

            AddChild(doorway);
        }

        PlaceWalls(roomOrigins[roomOrigins.Length - 1]);

        wallScene.Dispose();

        Spatial button = ResourceLoader.Load<PackedScene>("res://scenes/button.tscn").Instance<Spatial>();
        button.Translation = new Vector3(roomOrigins[7].x, roomOrigins[7].y + 5f, roomOrigins[7].z - 16.1f);
        AddChild(button);
    }

    private void PlaceWall(Vector3 position, Vector3 rotation)
    {
        Boolean place = true;

        for (Int32 i = 0; i < walls.Count; i++)
        {
            if (walls[i] == new Vector2(position.x, position.z))
            {
                place = false;
                break;
            }
        }

        if (place)
        {
            StaticBody wallInstance = wallScene.Instance<StaticBody>();
            wallInstance.Translation = position;
            wallInstance.RotationDegrees = rotation;
            AddChild(wallInstance);
            walls.Add(new Vector2(wallInstance.Translation.x, wallInstance.Translation.z));
        }
    }

    private void PlaceWalls(Vector3 position)
    {
        // Front wall
        PlaceWall(new Vector3(position.x, position.y + 9f, position.z - 17f), new Vector3(90f, 0f, 0f));
        // Back wall
        PlaceWall(new Vector3(position.x, position.y + 9f, position.z + 17f), new Vector3(90f, 0f, 0f));
        // Left wall
        PlaceWall(new Vector3(position.x - 17f, position.y + 9f, position.z), new Vector3(90f, 90f, 0f));
        // Right wall
        PlaceWall(new Vector3(position.x + 17f, position.y + 9f, position.z), new Vector3(90f, 90f, 0f));
    }

    enum Direction
    {
        Left,
        Right,
        Forward
    }
}
