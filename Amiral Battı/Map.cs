using System;
using System.Collections;
using System.Windows.Forms;

namespace Amiral_Battı
{
    class Map
    {
        public bool[,] Sea;
        public int[] ShipCountANDLength = new int[] { 4, 4, 3, 3, 2 };
        public Map(int Height, int Width)
        {
            Sea = new bool[Height, Width];

            for (int i = 0; i < Sea.GetLength(0); i++)
            {
                for (int j = 0; j < Sea.GetLength(1); j++)
                {
                    Sea[i, j] = false;
                }
            }
        }
        public bool IsItTrue(int i, int j)
        {
            if ((i >= 0 && i <= 9) && (j >= 0 && j <= 9))
            {
                return Sea[i, j];
            }
            else
            {
                return true;
            }
        }

        public void SetTrue(int i, int j)
        {
            Sea[i, j] = true;
        }
    }

    class EnemyField : Map
    {
        public EnemyField(int Height, int Width) : base(Height, Width) { }
        public void PickShips()
        {
            Random Random = new Random();

            for (int i = 0; i < ShipCountANDLength.Length; i++)
            {
                int x = 0, y = 0;
                bool verticalORhorizontal = Random.Next(100) < 50;
            GOBack:
                int[,] Temp = new int[ShipCountANDLength[i], 2];

                if (verticalORhorizontal) //Vertical or Horizontal
                {
                    x = Random.Next(10 - ShipCountANDLength[i]);
                    y = Random.Next(9);
                }
                else
                {
                    x = Random.Next(9);
                    y = Random.Next(10 - ShipCountANDLength[i]);
                }
                if (IsItTrue(x, y)) goto GOBack;
                Temp[0, 0] = x;
                Temp[0, 1] = y;

                for (int j = 1; j < ShipCountANDLength[i]; j++)
                {
                    if (verticalORhorizontal)
                    {
                        x += 1;
                    }
                    else
                    {
                        y += 1;
                    }
                    if (IsItTrue(x, y)) goto GOBack;
                    Temp[j, 0] = x;
                    Temp[j, 1] = y;
                }
                TemptoSea(Temp);
            }
        }
        private void TemptoSea(int[,] Temp)
        {
            for (int i = 0; i < Temp.GetLength(0); i++)
            {
                SetTrue(Temp[i, 0], Temp[i, 1]);
            }
        }

        public static (int i, int j) FindIndex(int PositionX, int PositionY)
        {
            int i = (PositionY - 20) / 50;
            int j = (PositionX - 640) / 50;
            return (i, j);
        }
    }
    class MyField : Map
    {
        public MyField(int Height, int Width) : base(Height, Width) { }
        public static (int i, int j) FindIndex(int PositionX, int PositionY)
        {
            int i = (PositionY - 20) / 50;
            int j = (PositionX - 10) / 50;
            return (i, j);
        }
        public void PositionTOsea(Button[] Ship)
        {
            foreach (Button PerShip in Ship)
            {
                (int i, int j) = FindIndex(PerShip.Location.X, PerShip.Location.Y);
                SetTrue(i, j);
            }
        }
        public bool IsItSuitable(Button[] Ship)
        {
            foreach (Button MyShip in Ship)
            {
                (int i, int j) = MyField.FindIndex(MyShip.Location.X, MyShip.Location.Y);
                if (IsItTrue(i, j))
                {
                    return false;
                }
            }
            return true;
        }
    }

    class AI
    {
        ArrayList PickedCells = new ArrayList();
        MyField EF;
        public AI(MyField EF)
        {
            this.EF = EF;
        }

        public (int i, int j) RandomMove()
        {
            int x, y;
            do
            {
                Random random = new Random();
                x = random.Next(10);
                y = random.Next(10);
            } while (IsItPicked(x,y));
            AddCell(x, y);
            return (x, y);
        }
        public (int i, int j) FindBestMove(int LastX, int LastY) //X is column(i), Y is Row(j)
        {
            int ProbablyX1, ProbablyX2;
            int ProbablyY1, ProbablyY2;

            ProbablyX1 = LastX - 1; //With LastY
            ProbablyX2 = LastX + 1;

            ProbablyY1 = LastY - 1; //With LastX
            ProbablyY2 = LastY + 1;



            //X is column(i), Y is Row(j)
            if (LastX == 0 && LastY == 0) //letf Top
            {

            }
            else if (LastX == EF.Sea.GetLength(0) && LastY == 0) //Left Bottom
            {

            }
            else if (LastX == 0 && LastY == EF.Sea.GetLength(1)) //Rigt Top
            {

            }
            else if (LastX == EF.Sea.GetLength(0) && LastY == EF.Sea.GetLength(1)) //Right Bottom
            {

            }
            // Half Middle
            else if (LastY > 0 && LastY < EF.Sea.GetLength(1) && LastX == 0) //Top Middle
            {

            }
            else if (LastX > 0 && LastX < EF.Sea.GetLength(0) && LastY == 0) //Left Middle 
            {

            }
            else if (LastY > 0 && LastY < EF.Sea.GetLength(1) && LastX == EF.Sea.GetLength(0)) //Bottom Middle 
            {

            }
            else if (LastX > 0 && LastX < EF.Sea.GetLength(0) && LastY == EF.Sea.GetLength(1)) //Right Middle 
            {

            }
            else //Middle Center
            {

            }
            return (0, 0);
        }
        public bool IsItOkey(int index)
        {
            if (index<0)
            {

            }
            else if (index>EF.Sea.GetLength(0))
            {

            }
            return true;
        }
        public void AddCell(int i, int j)
        {
            PickedCells.Add(new Cell(i, j));
        }

        public bool IsItPicked(int X,int Y)
        {
            foreach (Cell Cells in PickedCells)
            {
                if (Cells.X == X && Cells.Y == Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
    struct Cell
    {
        public int X, Y;
        public Cell(int X,int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
