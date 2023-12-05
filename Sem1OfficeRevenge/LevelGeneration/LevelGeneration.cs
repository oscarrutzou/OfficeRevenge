using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sem1OfficeRevenge
{
    public class LevelGeneration
    {
        private List<Room> rooms = new List<Room>();
        private List<Room> tempRooms = new List<Room>();
        private List<Texture2D> textures = new List<Texture2D>();
        private List<CivillianEnemy> CivEnemies = new List<CivillianEnemy>();
        private CivillianEnemy civEnm;
        Random rnd = new Random();

        Room lobbyRoom;

        private float randomRotation;
        private int randomNum;

        private Room previousRoom;
        private int tempX;
        private int tempY;
        private bool intersects = false;
        private int scale = 5;
        //private Texture2D currentTexture;

        public void GenerateWorld()
        {
            intersects = false;

            //Generate random rotation
            randomRotation = RandomRotation();
            
            //Add textures to list
            textures.Add(GlobalTextures.textures[TextureNames.TileMap2]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap4]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap3]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap5]);
          

            //Generate first lobby room
            lobbyRoom = new Room(GlobalTextures.textures[TextureNames.TileMap1], randomRotation);
            Global.currentScene.Instantiate(lobbyRoom);
            rooms.Add(lobbyRoom);

            previousRoom = lobbyRoom;

            for (int i = 0; i < 25; i++)
            {
                Room room = new Room(textures[rnd.Next(0, 4)], randomRotation);

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

                //Global.currentScene.Instantiate(room);
                //room.position = previousRoom.position;

                //MoveRoom(room, randomRotation);

                if (CheckIntersect(room))
                {
                    RemoveRooms();

                    rooms.Clear();

                    GenerateWorld();

                    break;
                }

                rooms.Add(room);

            }

            //foreach (Room room in rooms)
            //{
            //    for (int i = 0; i < rnd.Next(3, 8); i++)
            //    {
            //        CivEnemies.Add(new CivillianEnemy());
            //        Global.currentScene.Instantiate(CivEnemies[CivEnemies.Count - 1]);
            //        CivEnemies[CivEnemies.Count - 1].position = new Vector2(room.position.X + rnd.Next(-450, 451), room.position.Y + rnd.Next(-450, 451));
            //    }
            //}
        }

        private bool CheckIntersect(Room room)
        {
            foreach (Room roomI in rooms)
            {
                //if (room == roomI)
                //{
                //    break;
                //}

                if (room.collisionBox.Intersects(roomI.collisionBox))
                {
                    intersects = true;
                }
            }
            return intersects;
        }

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

        private void RoomColliders(Room tempRoom, float rotation)
        {
            switch (rotation)
            {
                case 0:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(30, -30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20 , (int)tempRoom.position.Y + 140, 40, 130);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(-30, 30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y + 140, 40, 130);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(175, 300, new Vector2(0, 0));                          
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y + 140, 40, 130);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(250, 300, new Vector2(-30, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y + 140, 40, 130);
                            break;

                        default:
                            
                            break;
                    }
                    break;

                case MathHelper.Pi:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(30, -30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y - 270, 40, 130);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(30, -30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y - 270, 40, 130);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(175, 300, new Vector2(0, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y - 270, 40, 130);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(250, 300, new Vector2(30, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 20, (int)tempRoom.position.Y - 270, 40, 130);
                            break;

                        default:
                            
                            break;
                    }
                    break;

                case MathHelper.PiOver2:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(-30, -30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 230, (int)tempRoom.position.Y - 65, 40, 130);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(-30, -30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 230, (int)tempRoom.position.Y - 65, 40, 130);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(175, 300, new Vector2(0, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 230, (int)tempRoom.position.Y - 65, 40, 130);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(250, 300, new Vector2(0, -30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 230, (int)tempRoom.position.Y - 65, 40, 130);
                            break;

                        default:
                            break;
                    }

                    break;

                case MathHelper.Pi + MathHelper.PiOver2:

                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room3":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(30, 30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 210, (int)tempRoom.position.Y - 130, 40, 130);
                            break;

                        case "Rooms\\room5":
                            tempRoom.SetCollisionBox(240, 240, new Vector2(0, 0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 210, (int)tempRoom.position.Y - 130, 40, 130);
                            break;

                        case "Rooms\\room2":
                            tempRoom.SetCollisionBox(175, 300, new Vector2(0,0));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 210, (int)tempRoom.position.Y - 130, 40, 130);
                            break;

                        case "Rooms\\room4p":
                            tempRoom.SetCollisionBox(250, 300, new Vector2(0, 30));
                            tempRoom.hallwayCol = new Rectangle((int)tempRoom.position.X - 210, (int)tempRoom.position.Y - 130, 40, 130);
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

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

        private void RotateLeft(Room tempRoom)
        {
            randomRotation -= MathHelper.PiOver2;
            tempRoom.rotation = randomRotation;
        }

        private void RotateRight(Room tempRoom)
        {
            randomRotation += MathHelper.PiOver2;
            tempRoom.rotation = randomRotation;
        }
   
        private void MoveUp(Room tempRoom)
        {
            tempRoom.position.Y -= tempRoom.texture.Height * scale;
            previousRoom = tempRoom;
        }

        private void MoveDown(Room tempRoom) 
        { 
           tempRoom.position.Y += tempRoom.texture.Height * scale; 
           previousRoom = tempRoom; 
        }

        private void MoveLeft(Room tempRoom)
        {
            tempRoom.position.X -= tempRoom.texture.Width * scale; 
            previousRoom = tempRoom; 
        }

        private void MoveRight(Room tempRoom)
        {
            tempRoom.position.X += tempRoom.texture.Width * scale; 
            previousRoom = tempRoom; 
        }

        public float RandomRotation()
        {
            randomNum = rnd.Next(0, 4);

            switch (randomNum)
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
