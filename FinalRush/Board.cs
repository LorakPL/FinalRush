using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalRush
{
    class Board
    {
        public static string board;
        public static int boardLength;
        public Color color;
        public int coordinateX = 0;
        public int coordinateY = 0;

        public static void readBoard()
        {
            board = System.IO.File.ReadAllText(@"C:\Users\Karol\Desktop\GitHub\Final-Rush\FinalRush\FinalRush\board.txt");
            boardLength = board.Length;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Rectangle)
        {
            Board.readBoard();

            for (int i = 0; i < boardLength; i++)
            {
                if (board[i] == 'A')
                {
                    color = Color.Yellow;
                }
                if (board[i] == 'B')
                {
                    color = Color.Red;
                }
                if (board[i] == '.')
                {
                    color = Color.Transparent;
                }
                if (i % 37 == 0 && i > 0)
                {
                    coordinateX = 0;
                    coordinateY += 30;
                }

                if (board[i] == 'A' || board[i] == '.' || board[i] == 'B')
                {
                    spriteBatch.Draw(Rectangle, new Rectangle(coordinateX, coordinateY, 30, 30), color);
                    coordinateX += 30;
                }
            }
            coordinateX = 0;
            coordinateY = 0;
        }
    }
}
