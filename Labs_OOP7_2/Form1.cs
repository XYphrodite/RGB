using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs_OOP7_2
{
    public partial class Form1 : Form
    {
        Graphics g;
        Point PreviousPoint = new Point();
        Pen redPen;
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.PNG) |*.bmp;*.jpg;*.gif;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(ofd.FileName);
                int width = image.Width;
                int height = image.Height;
                pictureBox1.Width = width;
                pictureBox1.Height = height;
                bmp = new Bitmap(image, width, height);
                pictureBox1.Image = bmp;
                g = Graphics.FromImage(pictureBox1.Image);


            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            PreviousPoint.X = e.X;
            PreviousPoint.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = new Point();
            if (e.Button == MouseButtons.Left)
            {
                point.X = e.X;
                point.Y = e.Y;
                g.DrawLine(redPen, PreviousPoint, point);
                PreviousPoint.X = point.X;
                PreviousPoint.Y = point.Y;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //g = CreateGraphics();
            //g.Clear(Color.Azure);
            //g.DrawEllipse(Pens.Black, 100, 100,300,200);
            //g.DrawRectangle(Pens.Black, 200, 200, 20, 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Сохранить картину как ...";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.Filter = "GIF File(*.gif)|*.gif|" + "JPEG File(*.jpg)|*.jpg|" + "PNG File (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                string strFillExtn = fileName.Remove(0, fileName.Length - 3);
                switch (strFillExtn)
                {
                    case "bmp":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            redPen = new Pen(Color.Red, 6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                pictureBox1.Image = RgbChannel((Bitmap)pictureBox1.Image, "R");
            if (radioButton2.Checked)
                pictureBox1.Image = RgbChannel((Bitmap)pictureBox1.Image, "G");
            if (radioButton3.Checked)
                pictureBox1.Image = RgbChannel((Bitmap)pictureBox1.Image, "B");
        }
        static Bitmap RgbChannel(Bitmap pict, string str)
        {
            Bitmap res = new Bitmap(pict.Width, pict.Height);
            for (int i = 0; i < pict.Width; i++)
            {
                for (int j = 0; j < pict.Height; j++)
                {
                    Color color = pict.GetPixel(i, j);
                    if (str == "R") res.SetPixel(i, j, Color.FromArgb(0, color.G, color.B));
                    if (str == "G") res.SetPixel(i, j, Color.FromArgb(color.R, 0, color.B));
                    if (str == "B") res.SetPixel(i, j, Color.FromArgb(color.R, color.G, 0));
                }
            }
            return res;
        }
    }
}
