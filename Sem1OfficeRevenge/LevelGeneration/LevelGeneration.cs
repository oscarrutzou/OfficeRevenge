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
        private List<Texture2D> textures = new List<Texture2D>();
        Room lobbyRoom;

        private float randomRotation;
        private int randomNum;

        private Room previousRoom;
        private int tempX;
        private int tempY;

        //private Texture2D currentTexture;

        public void GenerateWorld()
        {
            //Generate random rotation
            randomRotation = RandomRotation();
            Random rnd = new Random();

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

            for (int i = 0; i < 30; i++)
            {
                Room room = new Room(textures[rnd.Next(0, 4)], randomRotation);

                if (randomRotation < 0 && room.texture.Name == "Rooms\\room3" || randomRotation > MathHelper.Pi && room.texture.Name == "Rooms\\room5")
                {
                    room.texture = textures[rnd.Next(0, 2)];
                }
                else
                {
                    Global.currentScene.Instantiate(room);
                    rooms.Add(room);
                    room.position = previousRoom.position;

                    MoveRoom(room, randomRotation);
                }
            }
        }

        public void RemoveRooms()
        {
            if (rooms.Count != 0)
            {
                foreach (Room room in rooms)
                {
                     room.isRemoved = true;
                }
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
            tempRoom.position.Y -= tempRoom.texture.Height * 1;
            previousRoom = tempRoom;
        }

        private void MoveDown(Room tempRoom) 
        { 
           tempRoom.position.Y += tempRoom.texture.Height * 1; 
           previousRoom = tempRoom; 
        }

        private void MoveLeft(Room tempRoom)
        {
            tempRoom.position.X -= tempRoom.texture.Width * 1; 
            previousRoom = tempRoom; 
        }

        private void MoveRight(Room tempRoom)
        {
            tempRoom.position.X += tempRoom.texture.Width * 1; 
            previousRoom = tempRoom; 
        }

        public float RandomRotation()
        {
            Random rnd = new Random();

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
