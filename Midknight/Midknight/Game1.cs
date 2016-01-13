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
        Texture2D genTile;
        Texture2D playerTexture;
        Texture2D rockTexture;
        int[,] world;
        List<Texture2D> textureList = new List<Texture2D>();
        SpriteFont sFont;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cam.Pos = new Vector2(0.0f, 00.0f);
            player.Pos = new Vector2(0.0f, 0.0f);
            base.Initialize();


            //Create world
            world = new int[11, 11];
            string path = "world1.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                int count = 0;
                while (sr.Peek() >=0)
                {
                    string line = sr.ReadLine();
                    Console.Out.WriteLine(line);
                    for (int i = 0; i < 11; i++)
                    {
                        world[i, count] = int.Parse(line[i].ToString());
                    }
                    count++;
                }
            }
            Console.Out.Write(world[0, 0]);
            
            textureList.Add(genTile);
            textureList.Add(genTile);
            textureList.Add(genTile);
            textureList.Add(rockTexture);

            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            genTile = Content.Load<Texture2D>("tile");
            playerTexture = Content.Load<Texture2D>("player");
            rockTexture = Content.Load<Texture2D>("rock");
            player.Tex = playerTexture;

            sFont = Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
 
            // TODO: Add your update logic here
            player.Update(world);
            cam.Pos = player._pos;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                cam.Zoom = cam.Zoom + 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                cam.Zoom = cam.Zoom - 0.1f;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            String playerpos = player.Pos.X.ToString() + " " + player.Pos.Y.ToString();
            String worldplayerpos = player._worldpos.X.ToString() + " " + player._worldpos.Y.ToString();

            //Draw tilemap
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, cam.get_transformation(graphics.GraphicsDevice));
            drawWorld(spriteBatch, world, textureList);
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
                    sb.Draw(texList[w[i,j]],pos,Color.White);
                }
            }
        }
    }
}
