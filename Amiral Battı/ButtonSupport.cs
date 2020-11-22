using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Amiral_Battı
{
    class ButtonSupport
    {
        public static Button[] CreateShip(int Length, Form frm)
        {
            Button[] MyShip = new Button[Length];
            CreateNewButton(MyShip, 10, 520, frm);
            return MyShip;
        }
        public static void CreateNewButton(Button[] NewButton, int x, int y, Form frm)
        {
            for (int i = 0; i < NewButton.Length; i++)
            {
                NewButton[i] = new Button();
                NewButton[i].Size = new Size(50, 50);
                NewButton[i].Location = new Point(x, y);
                NewButton[i].Name = "X";
                NewButton[i].TabStop = false;
                x += 50;
                frm.Controls.Add(NewButton[i]);
            }

        }
        public static void CreateANewButton(int RangeX, int RangeY, int x, int y, Form frm)
        {
            int previuosX = x;
            Button[,] Myfield = new Button[RangeX, RangeY];
            for (int i = 0; i < RangeX; i++)
            {
                for (int j = 0; j < RangeY; j++)
                {
                    Myfield[i, j] = new Button();
                    Myfield[i, j].Size = new Size(50, 50);
                    Myfield[i, j].Location = new Point(x, y);
                    Myfield[i, j].TabStop = false;
                    frm.Controls.Add(Myfield[i, j]);
                    x += 50;
                }
                x = previuosX;
                y += 50;
            }
        }
        public static void ResetShip(Button[] Ship, Form frm)
        {
            int x = 10;
            int y = 520;
            for (int i = 0; i < Ship.Length; i++)
            {
                Ship[i].Location = new Point(x, y);
                x += Ship[i].Size.Width;
            }
        }
        public static void RemoveAllShips(Form frm)
        {
            ArrayList Remove = new ArrayList();
            foreach (Control item in frm.Controls)
            {
                if (item is Button && item.Name == "X")
                {
                    Remove.Add(item);
                }
            }
            foreach (Control item in Remove)
            {
                frm.Controls.Remove(item);
            }
        }

    }
}