using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace svetofor
{
    public partial class Form1 : Form
    {
        private List<PointF> PointList;
        private Pen redPen = new Pen(Color.Red, 3);
        private int numberOfTurns = 1; // Начальное значение 1 виток
        private float scale = 10; // Начальное значение масштаба

        public Form1()
        {
            InitializeComponent();
            PointList = new List<PointF>();
            pictureBox1.Paint += PictureBox1_Paint;
            trackBar1.Scroll += TrackBar1_Scroll; // Подписка на событие прокрутки для TrackBar витков
            trackBar2.Scroll += TrackBar2_Scroll; // Подписка на событие прокрутки для TrackBar масштаба
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawSpiral(e.Graphics);
        }

        private void DrawSpiral(Graphics g)
        {
            PointList.Clear();
            CyclePointList();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2f, pictureBox1.ClientSize.Height / 2f);
            g.DrawCurve(redPen, PointList.ToArray());
        }

        private void CyclePointList()
        {
            // Проверка на количество витков и масштаб
            if (numberOfTurns <= 0 || scale <= 0)
            {
                return; // Если число витков 0 или меньше, или масштаб 0 или меньше, выходим из метода
            }

            for (float angle = 0; angle < numberOfTurns * 360; angle += 5)
            {
                float radian = angle * (float)Math.PI / 180;
                float x = (float)(radian * Math.Cos(radian) * scale);
                float y = (float)(radian * Math.Sin(radian) * scale);
                PointList.Add(new PointF(x, y));
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            
            if (trackBar1.Value == 0)
            {
                MessageBox.Show("Число витков не может быть равно нулю.");
                return; // Игнорируем изменение до нуля
            }

            numberOfTurns = trackBar1.Value;
            label3.Text = $"Число витков: {numberOfTurns}";
            pictureBox1.Invalidate(); // Перерисовываем PictureBox
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value == 0)
            {
                MessageBox.Show("Масштаб не может быть равен нулю.");
                return; // Игнорируем изменение до нуля
            }

            scale = trackBar2.Value;
            label4.Text = $"Масштаб: {scale}";
            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Minimum = 1; // Минимальное значение для числа витков
            trackBar1.Maximum = 10;
            trackBar1.Value = numberOfTurns; // Установка начального значения на 1

            trackBar2.Minimum = 1; // Минимальное значение для масштаба
            trackBar2.Maximum = 50;
            trackBar2.Value = (int)scale;

            label3.Text = $"Число витков: {numberOfTurns}";
            label4.Text = $"Масштаб: {scale}";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Label clicked!"); // Пример действия при клике на label
        }
    }
}