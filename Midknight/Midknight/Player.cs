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


namespace Oki
{
    public class Player
    {
        public Vector2 _pos;
        public Vector2 _worldpos;
        public Texture2D _texture;
        public bool moving;
        public Vector2 movingdir;
        public int transition;
        public int steps;

        public Player()
        {
            _worldpos = Vector2.Zero;
            _pos = Vector2.Zero;
            movingdir = Vector2.Zero;
            moving = false;
            steps = 8;
        }
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public Texture2D Tex
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public void Update(int[,] world)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right)&&moving==false)
            {
                if(world[((int)_worldpos.X)+1,(int)_worldpos.Y]!=3)
                {
                    moving = true;
                    movingdir.X = 4;
                    transition = 0;
                }
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && moving == false)
            {
                if (world[((int)_worldpos.X) - 1, (int)_worldpos.Y] != 3)
                {
                    moving = true;
                    movingdir.X = -4;
                    transition = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && moving == false)
            {
                if (world[((int)_worldpos.X), ((int)_worldpos.Y)-1] != 3)
                {
                    moving = true;
                    movingdir.Y = -4;
                    transition = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && moving == false)
            {
                if (world[((int)_worldpos.X), ((int)_worldpos.Y) + 1] != 3)
                {
                    moving = true;
                    movingdir.Y = 4;
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
                    _worldpos.X = _pos.X / 32;
                    _worldpos.Y = _pos.Y / 32;
                    moving = false;
                    movingdir = Vector2.Zero;
                    
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _pos, Color.White);
        }
    }
}
