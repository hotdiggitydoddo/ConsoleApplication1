using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace ConsoleApplication1
{
    class Program
    {
        private static int width = 101;
        private static int height = 101;
        private static Map map;
        private static int _playerX = 25;
        private static int _playerY = 25;
        static RLRootConsole rootConsole = new RLRootConsole("terminal8x8.png", 101, 101, 8, 8, 1f, "RLNET Sample");
        private static Tile[,] _map;
        static void Main(string[] args)
        {
            DungeonGenerator.Instance.GenerateHauberkDungeon(101, 101, 195000, 5, 5, 50, false, true);
            _map = DungeonGenerator._dungeon;
            map = new Map(101, 101);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    switch (_map[x, y])
                    {
                        case Tile.Floor: map.SetCellProperties(x, y, true, true); break;
                        case Tile.RoomFloor: map.SetCellProperties(x, y, true, true); break;
                        case Tile.Wall: map.SetCellProperties(x, y, false, false); break;
                        case Tile.Door: map.SetCellProperties(x, y, false, false); break;
                    }
                }
            }
            rootConsole.Update += rootConsole_Update;
            rootConsole.Render += rootConsole_Render;
            rootConsole.Run();
        }

        private static void rootConsole_Update(object sender, UpdateEventArgs e)
        {
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();
            if (keyPress != null)
            {
                // Check for the Up key press
                if (keyPress.Key == RLKey.Up)
                {
                    // Check the RogueSharp map to make sure the Cell is walkable before moving
                    if (map.GetCell(_playerX, _playerY - 1).IsWalkable)
                    {
                        // Update the player position
                        _playerY--;
                    }
                    else
                    {
                        if (_map[_playerX, _playerY - 1] == Tile.Door)
                            map.SetCellProperties(_playerX, _playerY - 1, true, true);
                    }
                }
                // Repeat for the other directions
                else if (keyPress.Key == RLKey.Down)
                {
                    if (map.GetCell(_playerX, _playerY + 1).IsWalkable)
                    {
                        _playerY++;
                    }
                    else
                    {
                        if (_map[_playerX, _playerY + 1] == Tile.Door)
                            map.SetCellProperties(_playerX, _playerY + 1, true, true);
                    }
                }
                else if (keyPress.Key == RLKey.Left)
                {
                    if (map.GetCell(_playerX - 1, _playerY).IsWalkable)
                    {
                        _playerX--;
                    }
                    else
                    {
                        if (_map[_playerX - 1, _playerY] == Tile.Door)
                            map.SetCellProperties(_playerX - 1, _playerY, true, true);
                    }
                }
                else if (keyPress.Key == RLKey.Right)
                {
                    if (map.GetCell(_playerX + 1, _playerY).IsWalkable)
                    {
                        _playerX++;
                    }
                    else
                    {
                        if (_map[_playerX + 1, _playerY] == Tile.Door)
                            map.SetCellProperties(_playerX + 1, _playerY, true, true);
                    }
                }
            }
        }

        private static void rootConsole_Render(object sender, UpdateEventArgs e)
        {
            rootConsole.Clear();
            map.ComputeFov(_playerX, _playerY, 50, true);

            foreach (var cell in map.GetAllCells())
            {
                if (!cell.IsInFov)
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                    switch (_map[cell.X, cell.Y])
                    {
                        case Tile.Floor: rootConsole.Print(cell.X, cell.Y, ".", RLColor.Gray); break;
                        case Tile.RoomFloor: rootConsole.Print(cell.X, cell.Y, ".", RLColor.White); break;
                        case Tile.Wall: rootConsole.Print(cell.X, cell.Y, "#", RLColor.LightGray); break;
                        case Tile.Door: rootConsole.Print(cell.X, cell.Y, "+", RLColor.Brown); break;
                    }
                }
                else if (cell.IsExplored)
                {
                    switch (_map[cell.X, cell.Y])
                    {
                        case Tile.Floor: rootConsole.Print(cell.X, cell.Y, ".", RLColor.Blend(RLColor.Gray, RLColor.Black)); break;
                        case Tile.RoomFloor: rootConsole.Print(cell.X, cell.Y, ".", RLColor.Blend(RLColor.White, RLColor.Black)); break;
                        case Tile.Wall: rootConsole.Print(cell.X, cell.Y, "#", RLColor.Blend(RLColor.LightGray, RLColor.Black)); break;
                        case Tile.Door: rootConsole.Print(cell.X, cell.Y, "+", RLColor.Blend(RLColor.Brown, RLColor.Black)); break;
                    }
                }
            }
            rootConsole.Print(_playerX, _playerY, "@", RLColor.LightGreen);
            rootConsole.Draw();
        }
    }
}
