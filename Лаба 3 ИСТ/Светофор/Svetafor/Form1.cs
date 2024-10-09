using System;
using System.Drawing;
using System.Windows.Forms;

namespace Svetafor
{
    public partial class Form1 : Form
    {
        private CircleShape redCircle;
        private CircleShape yellowCircle;
        private CircleShape greenCircle;

        public Form1()
        {
            InitializeComponent();
            InitializeTrafficLight();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void InitializeTrafficLight()
        {
            redCircle = new CircleShape(this, 100, 50, Color.Red);
            yellowCircle = new CircleShape(this, 100, 100, Color.Yellow);
            greenCircle = new CircleShape(this, 100, 150, Color.Green);
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            redCircle.Toggle();
            this.Invalidate();
        }

        private void buttonYellow_Click(object sender, EventArgs e)
        {
            yellowCircle.Toggle();
            this.Invalidate();
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            greenCircle.Toggle();
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            redCircle.Draw(e.Graphics);
            yellowCircle.Draw(e.Graphics);
            greenCircle.Draw(e.Graphics);
        }

        private class CircleShape
        {
            private Form form;
            private int x;
            private int y;
            private Color color;
            private bool visible;

            public CircleShape(Form form, int x, int y, Color color)
            {
                this.form = form;
                this.x = x;
                this.y = y;
                this.color = color;
                this.visible = false;
            }

            public void Draw(Graphics g)
            {
                if (visible)
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        g.FillEllipse(brush, x - 25, y - 25, 50, 50);
                    }
                }
            }

            public void Show()
            {
                visible = true;
                form.Invalidate();
            }

            public void Hide()
            {
                visible = false;
                form.Invalidate();
            }

            public void Toggle()
            {
                visible = !visible; // Переключаем видимость
                form.Invalidate();
            }
        }
    }
}