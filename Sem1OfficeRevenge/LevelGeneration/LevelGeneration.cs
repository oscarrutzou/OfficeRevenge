﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sem1OfficeRevenge
{
    public class LevelGeneration
    {
        private List<Room> rooms = new List<Room>();
        private List<Room> tempRooms = new List<Room>();
        private List<Texture2D> textures = new List<Texture2D>();
        private List<GenericEnemy> Enemies = new List<GenericEnemy>();
        private CivillianEnemy civEnm;
        Random rnd = new Random();

        Room lobbyRoom;
        public Room elevator;

        private int scale = 5;
        private float randomRotation;
        private int randomNum;

        private Room previousRoom;
        private int tempX;
        private int tempY;
        private bool intersects = false;
        public bool doneGenerating = false;

        public void GenerateWorld()
        {
            intersects = false;

            //Generate random rotation
            randomRotation = RandomRotation();
            
            //Add textures to list
            textures.Add(GlobalTextures.textures[TextureNames.TileMap2]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap4]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap3]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap3]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap5]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap5]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap6]);

            //Generate first lobby room
            lobbyRoom = new Room(GlobalTextures.textures[TextureNames.TileMap1], randomRotation);
            lobbyRoom.SetCollisionBox(300, 300, new Vector2(0, 0));
            Global.currentScene.Instantiate(lobbyRoom);
            rooms.Add(lobbyRoom);

            previousRoom = lobbyRoom;

            //Generate Rooms
            for (int i = 0; i < 7; i++)
            {
                Room room = new Room(textures[rnd.Next(0, 6)], randomRotation);

                //if rotation is under pi/2 and room is room3 or rotation is over pi/2 and room is room5, generate new room.
                if (randomRotation < MathHelper.PiOver2 && room.texture.Name == "Rooms\\room3" || randomRotation > MathHelper.PiOver2 && room.texture.Name == "Rooms\\room5")
                {
                    //generate new room, non turning room.
                    room.texture = textures[rnd.Next(0, 2)];
                    Global.currentScene.Instantiate(room);
                    room.position = previousRoom.position;

                    //move room and set collision box
                    MoveRoom(room, randomRotation);
                    RoomColliders(room, randomRotation);
                }
                else
                {
                    //instantiates room
                    Global.currentScene.Instantiate(room);
                    //sets room position to previous room position
                    room.position = previousRoom.position;

                    MoveRoom(room, randomRotation);
                    RoomColliders(room, randomRotation);
                }

                //check if room intersects with other rooms
                if (CheckIntersect(room))
                {
                    RemoveRooms();

                    rooms.Clear();

                    GenerateWorld();

                    intersects = true;

                    break;
                }
                else
                {
                    intersects = false;
                }

                rooms.Add(room);
            }

            //if no rooms intersect, generate civillians and elevator
            if (!intersects)
            {
                foreach (Room room in rooms)
                {
                    for (int i = 0; i < rnd.Next(3, 7); i++)
                    {
                        if (room.texture.Name != "Rooms\\TempLobby1" && room.texture.Name != "Rooms\\Elevator")
                        {
                            Enemies.Add(new CivillianEnemy());
                            Enemies[Enemies.Count - 1].position = new Vector2(room.position.X + rnd.Next(-250, 251), room.position.Y + rnd.Next(-250, 251));
                            Global.currentScene.Instantiate(Enemies[Enemies.Count - 1]);
                            for (int ii = 0; ii < rnd.Next(0, 4); ii++)
                            {
                                Enemies.Add(new CombatEnemy());
                                Global.currentScene.Instantiate(Enemies[Enemies.Count - 1]);
                                Enemies[Enemies.Count - 1].position = new Vector2(room.position.X + rnd.Next(-250, 251), room.position.Y + rnd.Next(-250, 251));
                            }

                        }
                    }
                }

                //generate elevator
                elevator = new Room(GlobalTextures.textures[TextureNames.TileMap6], randomRotation);
                elevator.position = previousRoom.position;
                Global.currentScene.Instantiate(elevator);

                MoveRoom(elevator, randomRotation);
                RoomColliders(elevator, randomRotation);

                doneGenerating = true;
            }
        }

        //Second world generation for after the first level (same as first world generation but with different textures)
        public void GenerateSecondWorld()
        {
            intersects = false;

            //Generate random rotation
            randomRotation = RandomRotation();

            //Add textures to list
            textures.Add(GlobalTextures.textures[TextureNames.TileMap2]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap4]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap3]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap3]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap5]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap5]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap6]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap7]);


            //Generate first lobby room
            lobbyRoom = new Room(GlobalTextures.textures[TextureNames.TileMap7], randomRotation);
            lobbyRoom.SetCollisionBox(100, 75, new Vector2(0, 0));
            Global.currentScene.Instantiate(lobbyRoom);
            rooms.Add(lobbyRoom);

            //set collision box on first room based on rotation
            switch (randomRotation)
            {
                case 0:
                lobbyRoom.SetCollisionBox(100, 75, new Vector2(0, -115 * scale));
                break;

                case MathHelper.Pi:
                lobbyRoom.SetCollisionBox(100, 75, new Vector2(0, 115 * scale));
                break;

                case MathHelper.PiOver2:
                lobbyRoom.SetCollisionBox(100, 75, new Vector2(115 * scale, 0));
                break;

                case MathHelper.Pi + MathHelper.PiOver2:
                lobbyRoom.SetCollisionBox(100, 75, new Vector2(-115 * scale, 0));
                break;

                    default:
                    break;
            }

                previousRoom = lobbyRoom;

                for (int i = 0; i < 7; i++)
                {
                    Room room = new Room(textures[rnd.Next(0, 6)], randomRotation);

                    if (randomRotation < MathHelper.PiOver2 && room.texture.Name == "Rooms\\room3" || randomRotation > MathHelper.PiOver2 && room.texture.Name == "Rooms\\room5")
                    {
                        room.texture = textures[rnd.Next(0, 2)];
                        Global.currentScene.Instantiate(room);
                        room.position = previousRoom.position;

                        MoveRoom(room, randomRotation);
                        RoomColliders(room, randomRotation);
                    }
                    else
                    {
                        Global.currentScene.Instantiate(room);
                        room.position = previousRoom.position;

                        MoveRoom(room, randomRotation);
                        RoomColliders(room, randomRotation);
                    }

                    if (CheckIntersect(room))
                    {
                        RemoveRooms();

                        rooms.Clear();

                        GenerateSecondWorld();

                        intersects = true;

                        break;
                    }
                    else
                    {
                        intersects = false;
                    }
                    rooms.Add(room);
                }

                if (!intersects)
                {
                    foreach (Room room in rooms)
                    {
                        for (int i = 0; i < rnd.Next(3, 8); i++)
                        {
                            if (room.texture.Name != "Rooms\\ElevatorReverse")
                            {
                                Enemies.Add(new CivillianEnemy());
                                Enemies[Enemies.Count - 1].position = new Vector2(room.position.X + rnd.Next(-250, 251), room.position.Y + rnd.Next(-250, 251));
                                Global.currentScene.Instantiate(Enemies[Enemies.Count - 1]);
                            for (int ii = 0; ii < rnd.Next(0, 4); ii++)
                            {
                                Enemies.Add(new CombatEnemy());
                                Global.currentScene.Instantiate(Enemies[Enemies.Count - 1]);
                                Enemies[Enemies.Count - 1].position = new Vector2(room.position.X + rnd.Next(-250, 251), room.position.Y + rnd.Next(-250, 251));
                            }
                        }

                        }
                    }

                    elevator = new Room(GlobalTextures.textures[TextureNames.TileMap6], randomRotation);
                    elevator.position = previousRoom.position;
                    Global.currentScene.Instantiate(elevator);

                    MoveRoom(elevator, randomRotation);
                    RoomColliders(elevator, randomRotation);

                    doneGenerating = true;
                }
        }

        //Check if room intersects with other rooms and returns true or false
        private bool CheckIntersect(Room room)
        {
            foreach (Room roomI in rooms)
            {
                if (room.collisionBox.Intersects(roomI.collisionBox))
                {
                    intersects = true;
                }
            }
            return intersects;
        }

        //Removes all rooms from the scene
        public void RemoveRooms()
        {
            if (rooms.Count >= 0 && rooms.Count <= 25)
            {
                foreach (Room room in rooms)
                {
                     room.isRemoved = true;
                }
            }
        }

        //Sets collision box on room the room and on rotation
        private void RoomColliders(Room tempRoom, float rotation)
        {
            switch (rotation)
            {
                case 0:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(30 * scale, -30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale , (int)tempRoom.position.Y + 100 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(-30 * scale, 30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y + 100 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(165, 290, new Vector2(0, 0));                          
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y + 100 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(240, 290, new Vector2(-30 * scale, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y + 100 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\Elevator":
                            tempRoom.SetCollisionBox(100, 75, new Vector2(0, 115 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y + 100 * scale, 40 * scale, 190 * scale);
                            break;

                        default:
                            break;
                    }
                    break;

                case MathHelper.Pi:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(30 * scale, -30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y - 300 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(30 * scale, -30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y - 300 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(165, 290, new Vector2(0, 0 ));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y - 300 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(240, 290, new Vector2(30 * scale, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y - 300 * scale, 40 * scale, 190 * scale);
                            break;

                        case "Rooms\\Elevator":
                            tempRoom.SetCollisionBox(100, 75, new Vector2(0, -115 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 * scale, (int)tempRoom.position.Y - 300 * scale, 40 * scale, 190 * scale);
                            break;


                        default:
                            
                            break;
                    }
                    break;

                case MathHelper.PiOver2:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(-30 * scale, -30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 300 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(-30 * scale, -30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 300 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(290, 165, new Vector2(0, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 300 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(290, 240, new Vector2(0, -30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 300 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\Elevator":
                            tempRoom.SetCollisionBox(75, 100, new Vector2(-115 * scale, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 300 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        default:
                            break;
                    }

                    break;

                case MathHelper.Pi + MathHelper.PiOver2:

                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(30 * scale, 30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X + 100 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(230, 230, new Vector2(0, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X + 100 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(290, 165, new Vector2(0,0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X + 100 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(290, 240, new Vector2(0, 30 * scale));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X + 100 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            break;

                        case "Rooms\\Elevator":
                            tempRoom.SetCollisionBox(75, 100, new Vector2(110 * scale, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X + 100 * scale, (int)tempRoom.position.Y - 20 * scale, 190 * scale, 40 * scale);
                            
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        //Moves room based on rotation
        private void MoveRoom(Room tempRoom, float rotation)
        {
            //move room up, down, left or right based on random rotation
            switch (rotation)
            {
                case 0:
                    switch (previousRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            RotateLeft(tempRoom);
                            MoveLeft(tempRoom);
                            break;

                        case "Rooms\\room5":
                            RotateRight(tempRoom);
                            MoveRight(tempRoom);
                            break;

                        default:
                            MoveUp(tempRoom);
                            break;
                    }
                    break;

                case MathHelper.Pi:
                    switch (previousRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            RotateLeft(tempRoom);
                            MoveRight(tempRoom);
                            break;

                        case "Rooms\\room5":
                            RotateRight(tempRoom);
                            MoveLeft(tempRoom);
                            break;

                        default:
                            MoveDown(tempRoom);
                            break;
                    }
                    break;

                case MathHelper.PiOver2:
                    switch (previousRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            RotateLeft(tempRoom);
                            MoveUp(tempRoom);
                            break;

                        case "Rooms\\room5":
                            RotateRight(tempRoom);
                            MoveDown(tempRoom);
                            break;

                        default:
                            MoveRight(tempRoom);
                            break;
                    }

                    break;

                case MathHelper.Pi + MathHelper.PiOver2:

                    switch (previousRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            RotateLeft(tempRoom);
                            MoveDown(tempRoom);
                            break;

                        case "Rooms\\room5":
                            RotateRight(tempRoom);
                            MoveUp(tempRoom);
                            break;

                        default:
                            MoveLeft(tempRoom);
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        //Rotates room left
        private void RotateLeft(Room tempRoom)
        {
            randomRotation -= MathHelper.PiOver2;
            tempRoom.rotation = randomRotation;
        }

        //Rotates room right
        private void RotateRight(Room tempRoom)
        {
            randomRotation += MathHelper.PiOver2;
            tempRoom.rotation = randomRotation;
        }
   
        //Moves room up
        private void MoveUp(Room tempRoom)
        {
            tempRoom.position.Y -= tempRoom.texture.Height * scale;
            previousRoom = tempRoom;
        }

        //Moves room down
        private void MoveDown(Room tempRoom) 
        { 
           tempRoom.position.Y += tempRoom.texture.Height * scale; 
           previousRoom = tempRoom; 
        }

        //Moves room left
        private void MoveLeft(Room tempRoom)
        {
            tempRoom.position.X -= tempRoom.texture.Width * scale; 
            previousRoom = tempRoom; 
        }

        //Moves room right
        private void MoveRight(Room tempRoom)
        {
            tempRoom.position.X += tempRoom.texture.Width * scale; 
            previousRoom = tempRoom; 
        }

        //Generates random rotation
        public float RandomRotation()
        {
            switch (rnd.Next(0, 4))
            {

                case 0:
                    randomRotation = 0;
                    break;

                case 1:
                    randomRotation = MathHelper.Pi;
                    break;

                case 2:
                    randomRotation = MathHelper.PiOver2;
                    break;

                case 3:
                    randomRotation = MathHelper.Pi + MathHelper.PiOver2;
                    break;

                default:
                    break;
            }

            return randomRotation;
        }
    }
}
