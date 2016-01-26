using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class DungeonMaker
    {
        private int width = 100;
        private int height = 100;
        Random rng = new Random();
        public DungeonMaker()
        {
            
        }

        public List<Rectangle> Generate(int numCells, int minRoomWidth, int maxRoomWidth, int minRoomHeight, int maxRoomHeight)
        {
            var rects = new List<Rectangle>();

            while (rects.Count != numCells)
            {
                var widthOfCell = rng.Next(minRoomWidth, maxRoomWidth);
                var heightOfCell = rng.Next(minRoomHeight, maxRoomHeight);

                var start = new Point(rng.Next(0, width), rng.Next(0, height));
                if (start.X + widthOfCell >= width - 1 || start.Y + heightOfCell >= height - 1)
                    continue;
                rects.Add(new Rectangle(start.X, start.Y, widthOfCell, heightOfCell));
            }
            return rects;
        }
    }
}
