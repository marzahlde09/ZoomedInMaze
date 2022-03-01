using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZoomedInMaze
{
    class Environment
    {
        //global variables i.e. "properties" go here
        public Rectangle[,] rectangles;
        public int[,] textureMap;
        public int[,] collisionMap;
        public int startX;
        public int startY;
        public int size;
        public Texture2D[] textures;


        //constructor - organizes what is needed to CREATE an object from this class
        public Environment(int startX, int startY, int size,
            int[,] textureMap, int[,] collisionMap, Texture2D[] textures)
        {
            this.startX = startX;
            this.startY = startY;
            this.size = size;

            this.textureMap = textureMap;
            this.collisionMap = collisionMap;

            this.rectangles = new Rectangle[
                this.textureMap.GetLength(0), //sets up the number of rows
                this.textureMap.GetLength(1)];//sets up the number of columns

            //nested for loop for filling in rectangles using the startX, startY, and
            //size to figure out the locations for the rectangles
            for (int row = 0; row < this.rectangles.GetLength(0); row++)
            {
                for (int col = 0; col < this.rectangles.GetLength(1); col++)
                {
                    this.rectangles[row, col] = new Rectangle(
                        startX + col * size, startY + row * size, size, size);
                }
            }

            this.textures = textures;
        }

        public int[] collides(Rectangle gameObject)
        {
            //these variables will hold on to the row and column of the first thing
            //the gameObject intersects
            //-1, -1 means we DIDN'T hit anything
            int index1 = -1;
            int index2 = -1;

            for (int row = 0; row < this.rectangles.GetLength(0); row++)
            {
                for (int col = 0; col < this.rectangles.GetLength(1); col++)
                {
                    if (gameObject.Intersects(this.rectangles[row, col])
                        && this.collisionMap[row, col] == 1)
                    {
                        index1 = row;
                        index2 = col;
                        return new int[] { index1, index2 };
                    }
                }
            }

            return new int[] { index1, index2 };
        }

        public void move(int horizontal, int vertical)
        {
            for (int row = 0; row < this.rectangles.GetLength(0); row++)
            {
                for (int col = 0; col < this.rectangles.GetLength(1); col++)
                {
                    rectangles[row, col].X += horizontal;
                    rectangles[row, col].Y += vertical;
                }
            }
        }


        public void draw(SpriteBatch _spriteBatch)
        {
            for (int row = 0; row < this.rectangles.GetLength(0); row++)
            {
                for (int col = 0; col < this.rectangles.GetLength(1); col++)
                {
                    _spriteBatch.Draw(
                        textures[textureMap[row, col]], //picks the right texture from textureMap
                        rectangles[row, col],
                        Color.White);
                }
            }
        }
    }
}
