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
        //private Texture2D currentTexture;

        public void GenerateWorld()
        {
            
            
            //Generate random rotation
            randomRotation = RandomRotation();
            Random rnd = new Random();

            //Add textures to list
            textures.Add(GlobalTextures.textures[TextureNames.TileMap2]);
            textures.Add(GlobalTextures.textures[TextureNames.TileMap3]);

            //Generate first lobby room
            lobbyRoom = new Room(GlobalTextures.textures[TextureNames.TileMap1], randomRotation);
            Global.currentScene.Instantiate(lobbyRoom);
            rooms.Add(lobbyRoom);

            previousRoom = lobbyRoom;

            for (int i = 0; i < 2; i++)
            {
                Room room = new Room(textures[rnd.Next(0, 2)], randomRotation);
                Global.currentScene.Instantiate(room);
                rooms.Add(room);
                

                MoveRoom(room, randomRotation);
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
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room2":
                            tempRoom.position.Y -= tempRoom.texture.Height + 120;
                            tempRoom.position.X -= 121;
                            //tempRoom.position.Y -= previousRoom.position.Y;
                            previousRoom = tempRoom;
                            break;

                        case "Rooms\\room3":
                            tempRoom.position.Y -= tempRoom.texture.Height + 80;
                            tempRoom.position.X -= 79;
                            //tempRoom.position.Y -= previousRoom.position.Y;
                            previousRoom = tempRoom;
                            break;

                        default:
                            break;
                    }
                    break;

                case MathHelper.Pi:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room2":
                            tempRoom.position.Y += tempRoom.texture.Height + 120;
                            tempRoom.position.X += 121;
                            previousRoom = tempRoom;
                            break;

                        case "Rooms\\room3":
                            tempRoom.position.Y += tempRoom.texture.Height + 80;
                            tempRoom.position.X += 79;
                            previousRoom = tempRoom;
                            break;

                        default:
                            break;
                    }
                    break;

                case MathHelper.PiOver2:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room2":
                            tempRoom.position.X += tempRoom.texture.Width + 118;
                            tempRoom.position.Y -= 121;
                            previousRoom = tempRoom;
                            break;

                        case "Rooms\\room3":
                            tempRoom.position.X += tempRoom.texture.Width + 80;
                            tempRoom.position.Y -= 79;
                            previousRoom = tempRoom;
                            break;

                        default:
                            break;
                    }
                    break;

                case MathHelper.Pi + MathHelper.PiOver2:
                    switch (tempRoom.texture.Name)
                    {
                        case "Rooms\\room2":
                            tempRoom.position.X -= tempRoom.texture.Width + 118;
                            tempRoom.position.Y += 121;
                            previousRoom = tempRoom;
                            break;

                        case "Rooms\\room3":
                            tempRoom.position.X -= tempRoom.texture.Width + 80;
                            tempRoom.position.Y += 79;
                            previousRoom = tempRoom;
                            break;

                        default:
                            break;

                    }
                    break;

                default:
                    break;
            }
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
