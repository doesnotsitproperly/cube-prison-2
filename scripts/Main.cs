using Godot;
using System;
using System.Collections.Generic;

public class Main : Spatial {
    private Direction direction = Direction.Left;
    private List<Vector2> walls = new List<Vector2>();
    private Vector3[] roomOrigins = new Vector3[8];

    public override void _Ready() {
        PackedScene[] roomScenes = new PackedScene[] {
            ResourceLoader.Load<PackedScene>("res://scenes/rooms/three_room.tscn"),
            ResourceLoader.Load<PackedScene>("res://scenes/rooms/four_room.tscn"),
            ResourceLoader.Load<PackedScene>("res://scenes/rooms/five_room.tscn")
        };

        PackedScene doorwayScene = ResourceLoader.Load<PackedScene>("res://scenes/doorway.tscn");
        PackedScene wallScene = ResourceLoader.Load<PackedScene>("res://scenes/wall.tscn");

        roomOrigins[0] = Vector3.Zero;
        Spatial firstDoorway = doorwayScene.Instance<Spatial>();
        firstDoorway.Translation = new Vector3(roomOrigins[0].x, roomOrigins[0].y, roomOrigins[0].z - 17);
        walls.Add(new Vector2(firstDoorway.Translation.x, firstDoorway.Translation.z));
        AddChild(firstDoorway);
        PlaceWalls(wallScene, roomOrigins[0]);

        for (Byte i = 1; i <= 7; i++) {
            Spatial room = roomScenes[GD.Randi() % roomScenes.Length].Instance<Spatial>();
            room.Translation = roomOrigins[i - 1];

            Spatial doorway = doorwayScene.Instance<Spatial>();

            Vector3 translation = room.Translation;
            if (direction == Direction.Forward) {
                if (GD.Randi() % 2 == 0) {
                    // Place left
                    direction = Direction.Left;

                    translation.x -= 34;

                    doorway.Translation = new Vector3(translation.x + 17, translation.y, translation.z);
                    doorway.RotationDegrees = new Vector3(0f, 90f, 0f);
                    walls.Add(new Vector2(doorway.Translation.x, doorway.Translation.z));

                    PlaceWalls(wallScene, room.Translation);
                } else {
                    // Place right
                    direction = Direction.Right;

                    translation.x += 34;

                    doorway.Translation = new Vector3(translation.x - 17, translation.y, translation.z);
                    doorway.RotationDegrees = new Vector3(0f, 90f, 0f);
                    walls.Add(new Vector2(doorway.Translation.x, doorway.Translation.z));

                    PlaceWalls(wallScene, room.Translation);
                }
            } else {
                // Place forward
                direction = Direction.Forward;

                translation.z -= 34;

                doorway.Translation = new Vector3(translation.x, translation.y, translation.z + 17);
                walls.Add(new Vector2(doorway.Translation.x, doorway.Translation.z));

                PlaceWalls(wallScene, room.Translation);
            }
            room.Translation = translation;

            AddChild(room);
            roomOrigins[i] = room.Translation;

            AddChild(doorway);
        }

        PlaceWalls(wallScene, roomOrigins[7]);
    }

    private void PlaceWalls(PackedScene wall, Vector3 position) {
        // Front wall
        PlaceWall(
            wall,
            new Vector3(position.x, position.y + 9, position.z - 17),
            new Vector3(90f, 0f, 0f)
        );
        // Back wall
        PlaceWall(
            wall,
            new Vector3(position.x, position.y + 9, position.z + 17),
            new Vector3(90f, 0f, 0f)
        );
        // Left wall
        PlaceWall(
            wall,
            new Vector3(position.x - 17, position.y + 9, position.z),
            new Vector3(90f, 90f, 0f)
        );
        // Right wall
        PlaceWall(
            wall,
            new Vector3(position.x + 17, position.y + 9, position.z),
            new Vector3(90f, 90f, 0f)
        );
    }

    private void PlaceWall(PackedScene wall, Vector3 position, Vector3 rotation) {
        Boolean place = true;

        for (Int32 i = 0; i < walls.Count; i++) {
            if (walls[i] == new Vector2(position.x, position.z)) {
                place = false;
            }
        }

        if (place) {
            StaticBody wallInstance = wall.Instance<StaticBody>();
            wallInstance.Translation = position;
            wallInstance.RotationDegrees = rotation;
            AddChild(wallInstance);
            walls.Add(new Vector2(
                wallInstance.Translation.x,
                wallInstance.Translation.z
            ));
        }
    }

    enum Direction {
        Left,
        Right,
        Forward
    }
}
