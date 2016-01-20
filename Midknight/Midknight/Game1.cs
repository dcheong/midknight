using System;
using System.IO;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera2d cam = new Camera2d();
        Player player = new Player();
        Texture2D playerTexture;
        List<Texture2D> textureList = new List<Texture2D>();
        SpriteFont sFont;
        int[,] world1;
        int[,] world1top;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {

            cam.Pos = new Vector2(0.0f, 00.0f);
            player.Pos = new Vector2(64.0f, 64.0f);
            base.Initialize();


            //Create world
            string levelpath = "world1.txt";
            string levelpath2 = "world1top.txt";
            world1 = readWorld(levelpath);
            world1top = readWorld(levelpath2);
            string texturepath = "textures.txt";
            readTextures(texturepath);

            
        }
        public void readTextures(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >=0)
                {
                    string line = sr.ReadLine();
                    textureList.Add(this.Content.Load<Texture2D>(line));
                }
            }
        }
        public int[,] readWorld(string path)
        {
            int[,] world;
            using (StreamReader sr = new StreamReader(path))
            {
                int count = 0;
                int worldsize = int.Parse(sr.ReadLine().ToString());
                Console.Out.Write("WORLDSIZE::" + worldsize);
                world = new int[worldsize, worldsize];
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    for (int i = 0; i < worldsize; i++)
                    {
                        world[i, count] = int.Parse(line[i].ToString());
                    }
                    count++;
                }
            }
            return world;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
/*            genTile = Content.Load<Texture2D>("tile");
            
            rockTexture = Content.Load<Texture2D>("rock");
 * */
            playerTexture = Content.Load<Texture2D>("player");
            player.Tex = playerTexture;

            sFont = Content.Load<SpriteFont>("SpriteFont1");
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
 
            player.Update(world1);
            cam.Pos = player._pos;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                cam.Zoom = cam.Zoom + 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                cam.Zoom = cam.Zoom - 0.1f;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            String playerpos = player.Pos.X.ToString() + " " + player.Pos.Y.ToString();
            String worldplayerpos = player._worldpos.X.ToString() + " " + player._worldpos.Y.ToString();

            //Draw tilemap
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(graphics.GraphicsDevice));
            drawWorld(spriteBatch, world1, textureList);
            drawWorld(spriteBatch, world1top, textureList);
            spriteBatch.End();

            //Draw Player
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(graphics.GraphicsDevice));
            player.Draw(spriteBatch);
            spriteBatch.End();

            //Draw HUD or overlays
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null);
            //spriteBatch.DrawString(sFont,playerpos,Vector2.Zero,Color.White);
            spriteBatch.DrawString(sFont, worldplayerpos, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void drawWorld(SpriteBatch sb, int[,] w, List<Texture2D> texList)
        {
            for (int i = 0; i<w.GetLength(0);i++)
            {
                for(int j = 0; j<w.GetLength(1);j++)
                {
                    Vector2 pos = new Vector2(i*32,j*32);
                    if (w[i, j] != 0)
                    {
                        sb.Draw(texList[w[i, j]], pos, Color.White);
                    }
                }
            }
        }
    }
}
