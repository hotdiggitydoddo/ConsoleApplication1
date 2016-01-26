using System;
using System.Collections.Generic;
using Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Rectangle> rects = new List<Rectangle>();
        DungeonMaker dm = new DungeonMaker();
        private Texture2D tex;
        private Random r = new Random();
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tex = Content.Load<Texture2D>("white_square");
            var rectlist = dm.Generate(150, 9, 30, 9, 30);
            separateRooms(rectlist);
            foreach (var rectangle in rectlist)
            {
                var c = new Color(
                (byte)r.Next(0, 255),
                (byte)r.Next(0, 255),
                (byte)r.Next(0, 255));

                rects.Add(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
            }
            //separateRooms();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            foreach (var rectangle in rects)
            {
                var c = new Color(
               (byte)r.Next(0, 255),
               (byte)r.Next(0, 255),
               (byte)r.Next(0, 255));

                spriteBatch.Draw(tex, rectangle, c);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // a Room is a simple rectangle (x, y, width, height)
        private void separateRooms(List<Lib.Rectangle> rects)
        {
            //Lib.Rectangle a, b; // to hold any two rooms that are over lapping
            int dx, dxa, dxb, dy, dya, dyb; // holds delta values of the overlap
            bool touching = false; // a boolean flag to keep track of touching rooms
            int padding = 1;
            var cnt = 0;
            do
            {
                touching = false;
                for (int i = 0; i < rects.Count; i++)
                {
                    for (int j = i + 1; j < rects.Count; j++)
                    { // for each pair of rooms (notice i+1)
                        if (rects[i].Intersects(rects[j]))
                        { // if the two rooms touch (allowed to overlap by 1)
                            touching = true; // update the touching flag so the loop iterates again
                                             // find the two smallest deltas required to stop the overlap
                            dx = Math.Min(rects[i].Right - rects[j].Left + padding, rects[i].Left - rects[j].Right - padding);
                            dy = Math.Min(rects[i].Bottom - rects[j].Top + padding, rects[i].Top - rects[j].Bottom - padding);
                            // only keep the smalled delta
                            if (Math.Abs(dx) < Math.Abs(dy)) dy = 0;
                            else dx = 0;
                            // create a delta for each rectangle as half the whole delta.
                            dxa = -dx / 2;
                            dxb = dx + dxa;
                            // same for y
                            dya = -dy / 2;
                            dyb = dy + dya;
                            // shift both rectangles
                           
                            rects[i].Offset(new Lib.Point(dxa, dya)); 
                            rects[j].Offset(new Lib.Point(dxb, dyb));
                            cnt++;
                        }
                    }
                }
            } while ( touching); // loop until no rectangles are touching
        }
    }
}
