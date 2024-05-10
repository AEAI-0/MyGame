using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TextureAtlas;

namespace MyGame;
public enum Direction { direct_up = 0, direct_down = 1, direct_left = 2, direct_right = 3 };

public class Game1 : Game
{

    Texture2D Link;
    Texture2D Block;
    Texture2D linkAttack;
    bool isAttack;
    int blocknumber=0;
    Direction link_direction;
    Vector2 linkPosition;
    Vector2 blockPosition;
    float linkSpeed;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TextureAtlas.LinkMovement linkmovement;
    Rectangle sourceRectangle = new Rectangle(0, 0, 16, 16);
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        linkPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
        _graphics.PreferredBackBufferHeight / 2);
        blockPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
     _graphics.PreferredBackBufferHeight / 2);
        linkSpeed = 100f;
        link_direction = Direction.direct_down;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
      
        // TODO: use this.Content to load your game content here
        Block = Content.Load<Texture2D>("PriorityGraphicsDungeon1");
        Link = Content.Load<Texture2D>("LinkMovement");
        linkmovement = new TextureAtlas.LinkMovement(Link, 8, 1);
        linkAttack = Content.Load<Texture2D>("ZeldaSpriteLinkSwingSwordFront");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        // TODO: Add your update logic here
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
        {
            link_direction = Direction.direct_up;
            linkPosition.Y -= linkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.T))
        {   if (blocknumber > 0)
                blocknumber--;  
        }
        if (kstate.IsKeyDown(Keys.Y))
        {
            if (blocknumber < 5)
                blocknumber++;
        }
        if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
        {
            link_direction = Direction.direct_down;
            linkPosition.Y += linkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
        {
            link_direction = Direction.direct_left;
            linkPosition.X -= linkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
        {
            link_direction = Direction.direct_right;
            linkPosition.X += linkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (kstate.IsKeyDown(Keys.Z))
        {
            isAttack = true;
            switch (link_direction)
            {
                case Direction.direct_up:
                    linkAttack = Content.Load<Texture2D>("ZeldaSpriteLinkSwingSwordBack");
                    break;

                case Direction.direct_down:
                    linkAttack = Content.Load<Texture2D>("ZeldaSpriteLinkSwingSwordFront");
                    break;

                case Direction.direct_left:
                    linkAttack = Content.Load<Texture2D>("ZeldaSpriteLinkSwingSwordLeft");
                    break;

                case Direction.direct_right:
                    linkAttack = Content.Load<Texture2D>("ZeldaSpriteLinkSwingSwordRight");
                    break;

            }
        }
        if (linkPosition.X > _graphics.PreferredBackBufferWidth - linkAttack.Width / 2)
        {
            linkPosition.X = _graphics.PreferredBackBufferWidth - linkAttack.Width / 2;
        }
        else if (linkPosition.X < linkAttack.Width / 2)
        {
            linkPosition.X = linkAttack.Width / 2;
        }

        if (linkPosition.Y > _graphics.PreferredBackBufferHeight - linkAttack.Height / 2)
        {
            linkPosition.Y = _graphics.PreferredBackBufferHeight - linkAttack.Height / 2;
        }
        else if (linkPosition.Y < linkAttack.Height / 2)
        {
            linkPosition.Y = linkAttack.Height / 2;
        }
        switch (link_direction)
        {
            case Direction.direct_up:
                linkmovement.MoveUp(gameTime);
                break;

            case Direction.direct_down:
                linkmovement.MoveDown(gameTime);
                break;

            case Direction.direct_left:
                linkmovement.MoveLeft(gameTime);
                break;

            case Direction.direct_right:
                linkmovement.MoveRight(gameTime);
                break;

        }
        base.Update(gameTime);


        base.Update(gameTime);
    }

    /*  protected override void Draw(GameTime gameTime)
     {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         // TODO: Add your drawing code here
         _spriteBatch.Begin();
         _spriteBatch.Draw(Link,
     linkPosition,
     null,
     Color.White,
     0f,
     new Vector2(Link.Width / 2, Link.Height / 2),
     Vector2.One,
     SpriteEffects.None,
     0f);
         _spriteBatch.End();

         base.Draw(gameTime);
     } */
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        float timer = 0f;
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        _spriteBatch.Begin();
        Rectangle destinationRectangle = new Rectangle(100, 100, 32, 32);
        switch (blocknumber)
        {  case 0:
        sourceRectangle = new Rectangle(64, 64, 32, 32);
        break;
            case 1:
                sourceRectangle = new Rectangle(32, 64, 32, 32);
                break;

            case 2:
                sourceRectangle = new Rectangle(0, 64, 32, 32);
                break;
            case 3:
                sourceRectangle = new Rectangle(96, 64, 32, 32);
                break;
            case 4:
                sourceRectangle = new Rectangle(128, 64, 32, 32);
                break;
            case 5:
                sourceRectangle = new Rectangle(156, 64, 32, 32);
                break;
        }
         

        //  _spriteBatch.Draw(Block, blockPosition, sourceRectangle, Color.White);
        _spriteBatch.Draw(Block, destinationRectangle, sourceRectangle, Color.White);
        // TODO: Add your drawing code here
        if (isAttack)
        {
           
            _spriteBatch.Draw(linkAttack, linkPosition, Color.White);
 
            isAttack = false;
        }
        else
        {
            linkmovement.Draw(_spriteBatch, linkPosition);
        }
   
        base.Draw(gameTime);
        _spriteBatch.End();
    }
}
