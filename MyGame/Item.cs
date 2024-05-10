using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyGame;
public class Item
{
    Texture2D cur_item;
    public Item(Texture2D texture)
    {
        cur_item = texture;
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 location, Direction direction, GameTime gameTime)
    {
        //ectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
        Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, cur_item.Width, cur_item.Height);

        //spriteBatch.Begin();
        spriteBatch.Draw(cur_item, destinationRectangle, Color.White);
        //spriteBatch.End();
    }
}