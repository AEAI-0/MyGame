using System.Collections;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TextureAtlas;

namespace MyGame;
public enum Direction { direct_up = 0, direct_down = 1, direct_left = 2, direct_right = 3 };

public class Game1 : Game
{
    Texture2D Block;
    Texture2D Link;
    Texture2D linkAttack;
    Texture2D linkHurt;
    Texture2D itemtexture;
    Texture2D boomfire;
    int blocknumber = 0;
    int Monsternumber = 1;
    bool isAttack;
    bool isHurt;
    Direction link_direction;
    Vector2 linkPosition;
    Vector2 linkHurtPosition;
    float linkSpeed;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TextureAtlas.LinkMovement linkmovement;
    private TextureAtlas.fireAnimated fire;
    private Item linkitem;
    float itemtimer = 0f;
    float boomtimer = 0f;
    float firetimer = 0f;
    Texture2D monsterTexture;
    private Queue queue = new Queue();
    private Queue firequeue = new Queue();
    Monster monster1, monster2, monster3, monster4, monster5;
    Rectangle sourceRectangle = new Rectangle(0, 0, 16, 16);
    Rectangle MonsterEXsourceRectangle = new Rectangle(0, 0, 16, 16);
    Rectangle MonsterEXdestinationRectangle = new Rectangle(100, 100, 32, 32);
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
        linkHurtPosition = linkPosition;
        linkSpeed = 100f;
        link_direction = Direction.direct_down;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        monsterTexture = Content.Load<Texture2D>("En2");

        // 创建 Monster 实例
        MonsterEXsourceRectangle = new Rectangle(0, 0, 96, 32);
        MonsterEXdestinationRectangle = new Rectangle(200, 100, 96, 32);
        monster1 = new Monster(monsterTexture, MonsterEXsourceRectangle, MonsterEXdestinationRectangle);
        MonsterEXsourceRectangle = new Rectangle(0, 0, 96, 32);
        MonsterEXdestinationRectangle = new Rectangle(200, 100, 96, 32);
        monsterTexture = Content.Load<Texture2D>("En3");
        monster2 = new Monster(monsterTexture, MonsterEXsourceRectangle, MonsterEXdestinationRectangle);
        monsterTexture = Content.Load<Texture2D>("En4");
        MonsterEXsourceRectangle = new Rectangle(0, 128, 48, 64);
        MonsterEXdestinationRectangle = new Rectangle(200, 100, 48, 64);
        monster3 = new Monster(monsterTexture, MonsterEXsourceRectangle, MonsterEXdestinationRectangle);
        monsterTexture = Content.Load<Texture2D>("En1");
        MonsterEXsourceRectangle = new Rectangle(160, 96, 32, 32);
        MonsterEXdestinationRectangle = new Rectangle(200, 100, 32, 32);
        monster4 = new Monster(monsterTexture, MonsterEXsourceRectangle, MonsterEXdestinationRectangle);
        monsterTexture = Content.Load<Texture2D>("En1");
        MonsterEXsourceRectangle = new Rectangle(48, 96, 32, 32);
        MonsterEXdestinationRectangle = new Rectangle(200, 100, 32, 32);
        monster5 = new Monster(monsterTexture, MonsterEXsourceRectangle, MonsterEXdestinationRectangle);
        // TODO: use this.Content to load your game content here
        Block = Content.Load<Texture2D>("PriorityGraphicsDungeon1");
        // TODO: use this.Content to load your game content here
        Link = Content.Load<Texture2D>("LinkMovement");
        boomfire = Content.Load<Texture2D>("fire");
        fire = new TextureAtlas.fireAnimated(boomfire, 2, 1);
        linkmovement = new TextureAtlas.LinkMovement(Link, 8, 1);
        linkAttack = Content.Load<Texture2D>("ZeldaSpriteLinkSwingSwordFront");
        linkHurt = Content.Load<Texture2D>("ZeldaSpriteHeart");
        itemtexture = Content.Load<Texture2D>("ZeldaSpriteBomb");
        linkitem = new Item(itemtexture);

    }

    protected override void Update(GameTime gameTime)
    {
        linkHurtPosition = new Vector2(linkPosition.X + 5.0f, linkPosition.Y - 10.0f);
        fire.Update(gameTime);
        boomtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        firetimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        // TODO: Add your update logic here
        var kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.T))
        {
            if (blocknumber > 0)
                blocknumber--;
        }
        if (kstate.IsKeyDown(Keys.Y))
        {
            if (blocknumber < 5)
                blocknumber++;
        }
        if (kstate.IsKeyDown(Keys.O))
        {
            if (Monsternumber > 1)
                Monsternumber--;
        }
        if (kstate.IsKeyDown(Keys.P))
        {
            if (Monsternumber < 5)
                Monsternumber++;
        }
        if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
        {
            link_direction = Direction.direct_up;
            linkPosition.Y -= linkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
        if (kstate.IsKeyDown(Keys.E))//press E = hurt
        {
            isHurt = true;
            //link_direction = Direction.direct_right;
            //linkPosition.X += linkSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
        itemtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (kstate.IsKeyDown(Keys.D1) && itemtimer >= 1.0f)
        {
            // 在冷却时间后执行操作
            queue.Enqueue(linkPosition);

            // 重置计时器
            itemtimer = 0.0f;
        }
        if (boomtimer >= 2.0f)
        {
            boomtimer = 0.0f;
            if (queue.Count > 0)
            {
                firequeue.Enqueue(queue.Dequeue());
            }
        }
        if (firetimer >= 3.0f)
        {
            firetimer = 0.0f;
            if (firequeue.Count > 0)
            {
                firequeue.Dequeue();
            }
        }
        base.Update(gameTime);


        base.Update(gameTime);
    }

    public class Monster
    {
        // 怪物的纹理
        public Texture2D Texture { get; set; }

        // 用于剪裁纹理的源矩形
        public Rectangle SourceRectangle { get; set; }

        // 用于实际显示的目标矩形
        public Rectangle DestinationRectangle { get; set; }

        // 构造函数
        public Monster(Texture2D texture, Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
            DestinationRectangle = destinationRectangle;
        }

        // 设置源矩形
        public void SetSourceRectangle(int x, int y, int width, int height)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
        }

        // 设置显示的目标矩形
        public void SetDestinationRectangle(int x, int y, int width, int height)
        {
            DestinationRectangle = new Rectangle(x, y, width, height);
        }

        // 绘制怪物
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
        }
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        Rectangle destinationRectangle = new Rectangle(100, 100, 32, 32);
        switch (blocknumber)
        {
            case 0:
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
        switch (Monsternumber)
        {

            case 1:
                monster1.Draw(_spriteBatch);
                break;

            case 2:
                monster2.Draw(_spriteBatch);
                break;
            case 3:
                monster3.Draw(_spriteBatch);
                break;
            case 4:
                monster4.Draw(_spriteBatch);
                break;
            case 5:
                monster5.Draw(_spriteBatch);
                break;
        }

        // _spriteBatch.Draw(monsterTexture, destinationRectangle, sourceRectangle, Color.White);
        //  _spriteBatch.Draw(Block, blockPosition, sourceRectangle, Color.White);
        _spriteBatch.Draw(Block, destinationRectangle, sourceRectangle, Color.White);
        // TODO: Add your drawing code here
        if (isAttack)
        {
            // _spriteBatch.Begin();
            _spriteBatch.Draw(linkAttack, linkPosition, Color.White);
            //_spriteBatch.End();
            isAttack = false;
        }
        else if (isHurt)
        {
            // _spriteBatch.Begin();
            _spriteBatch.Draw(linkHurt, linkHurtPosition, Color.White);
            linkmovement.Draw(_spriteBatch, linkPosition);
            //_spriteBatch.End();
            isHurt = false;
        }
        else
        {
            linkmovement.Draw(_spriteBatch, linkPosition);
        }
        foreach (Vector2 pos in queue)
        {
            linkitem.Draw(_spriteBatch, pos, link_direction, gameTime);
        }
        foreach (Vector2 pos in firequeue)
        {
            fire.Draw(_spriteBatch, pos);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
