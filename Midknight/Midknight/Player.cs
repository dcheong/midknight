using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Midknight
{
    public class Player
    {

        public Vector2 _pos;
        public Vector2 _worldpos;
        public Texture2D _texture;
        public bool moving;
        public bool snapped;
        public Vector2 movingdir;
        public int transition;
        public int steps;
        private int framecount = 3;
        private int framerows = 4;
        public int timeperFrame = 5;
        private int framex = 0;
        private int framey = 1;
        public int counter = 0;

        public Player()
        {
            _worldpos = Vector2.Zero;
            _pos = Vector2.Zero;
            snapped = true;
            movingdir = Vector2.Zero;
            moving = false;
            steps = 8;
            
        }
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public Vector2 worldPos
        {
            get { return _worldpos; }
            set { _worldpos = value;}
        }
        public Texture2D Tex
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public void calcTile()
        {
            float newx = (float)Math.Round(_pos.X/32);
            float newy = (float)Math.Round(_pos.Y/32);
            worldPos = (new Vector2(newx, newy));
        }
        public void Update(int[,] world) {   
          calcTile();
          
          if (Keyboard.GetState().IsKeyDown(Keys.Right) && moving == false)
            {
                framey = 3;
                if(world[((int)_worldpos.X)+1,(int)_worldpos.Y]!=3)
                {
                    moving = true;
                    movingdir.X=4;
                    transition = 0;
                }
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && moving == false)
            {
                framey = 2;
                if (world[((int)_worldpos.X) - 1, (int)_worldpos.Y] != 3)
                {
                    moving = true;
                    movingdir.X=-4;
                    transition = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && moving == false)
            {
                framey = 0;
                if (world[((int)_worldpos.X), ((int)_worldpos.Y)-1] != 3)
                {
                    moving = true;
                    movingdir.Y=-4;
                    transition = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && moving == false)
            {
                framey = 1;
                if (world[((int)_worldpos.X), ((int)_worldpos.Y) + 1] != 3)
                {
                    moving = true;
                    movingdir.Y=4;
                    transition = 0;
                }
            }
 
            if(moving==true)
            {
                if(transition<steps)
                {
                    _pos = _pos + movingdir;
                    transition++;
                }
                else
                {
                    calcTile();
                    moving = false;
                    movingdir = Vector2.Zero;
                    
                }
            }
            UpdateFrame();
            }
        public void UpdateFrame()
        {
            if(moving)
            {
                counter++;
                if(counter%timeperFrame == 0)
                {
                    counter = 0;
                    framex++;
                    if (framex > 2)
                    {
                        framex = 0;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int FrameHeight = _texture.Height / framerows;
            int FrameWidth = _texture.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * framex, FrameHeight * framey, FrameWidth, FrameHeight);
            spriteBatch.Draw(_texture, _pos, sourcerect, Color.White);
        }
    }
}
