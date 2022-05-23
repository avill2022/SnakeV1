using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private char ctecla;
        private Random rdn;
        private Cuadro comida;
        private Bitmap bmp;
        private Graphics pagina;
        private int tipo_Juego;
        private List<Cuadro> Snake;


        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            iniciaComponentes();
            timer1.Tick += new EventHandler(timer1_Tick);
           
            
        }

        public void iniciaComponentes()
        { 
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            pagina = Graphics.FromImage(bmp);
            Snake = new List<Cuadro>();
            Snake.Add(new Cuadro());
            this.Size = new Size(16*32,16*28);
            comida = new Cuadro();
            rdn = new Random();
            ctecla = 'z';
            Snake[0].X = 12;
            Snake[0].Y = 12;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            moverJugador();
            choque();
            SnakeCome();



            if (tipo_Juego == 1)
                transporta();
            else
                limites();

            if (tipo_Juego == 2)
                generaObstaculos();


            Form1_Paint(this,null);
        }

        public void generaComida()
        {
            comida.X = rdn.Next(30); 
            comida.Y = rdn.Next(23);
            
        }

        public void generaObstaculos()
        { 
        
        
        }

        public void fondo(Graphics g)
        { 
            for(int i=0;i<31;i++)
            {
                for(int j=0;j<24;j++)
                {
                    g.DrawRectangle(new Pen(Color.White), i*16, j*16, 16, 16);
                }
            }
        }
        public void SnakeCome()
        {
            if (Snake[0].X == comida.X && Snake[0].Y == comida.Y)
            {
                generaComida();
                Snake.Add(new Cuadro());
            }
            
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pagina.SmoothingMode = SmoothingMode.AntiAlias;
            pagina.Clear(Color.Black);


            fondo(pagina);
           
            for (int i = 0; i < Snake.Count;i++ )
            {
                if(i==0)
                    pagina.FillRectangle(Brushes.Red, Snake[i].X * 16, Snake[i].Y * 16, 16, 16);
                else
                    pagina.FillRectangle(Brushes.White, Snake[i].X * 16, Snake[i].Y * 16, 16, 16);
            }
            pagina.FillRectangle(Brushes.WhiteSmoke, comida.X*16, comida.Y*16, 16, 16);
            
            
            g.DrawImage(bmp, 0, 0);
           
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a' && ctecla != 'd')
            { ctecla = e.KeyChar; }
            if (e.KeyChar == 'd' && ctecla != 'a')
            { ctecla = e.KeyChar; }
            if (e.KeyChar == 's' && ctecla != 'w')
            { ctecla = e.KeyChar; }
            if (e.KeyChar == 'w' && ctecla != 's')
            { ctecla = e.KeyChar; }
        }

        private void Iniciar_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        public void moverJugador()
        { 
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch(ctecla)
                    {
                        case 'a':
                            Snake[i].X--;
                            break;
                        case 'd':
                            Snake[i].X++;
                            break;
                        case 'w':
                            Snake[i].Y--;
                            break;
                        case 's':
                            Snake[i].Y++;
                            break;
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        
        }
        private void transporta()
        {
            if (Snake[0].X <0)
            {
                Snake[0].X = 30;
            }
            else
            if (Snake[0].X > 30)
            {
                Snake[0].X = 0;
            }

            if (Snake[0].Y < 0)
            {
                Snake[0].Y = 23;
            }
            else
                if (Snake[0].Y > 23)
                {
                    Snake[0].Y = 0;
                }
            
        }
        public void limites()
        {
            if (Snake[0].X < 0)
            {
                timer1.Stop();
            }
            else
                if (Snake[0].X > 30)
                {
                    timer1.Stop();
                }

            if (Snake[0].Y < 0)
            {
                timer1.Stop();
            }
            else
                if (Snake[0].Y > 23)
                {
                    timer1.Stop();
                }
        
        
        }
        public void choque()
        {
            for (int i = 1; i < Snake.Count;i++ )
            {
                if (Snake[0].X == Snake[i].X && Snake[0].Y == Snake[i].Y)
                {
                    timer1.Stop();
                }
            }
        }

        private void Reiniciar_Click(object sender, EventArgs e)
        {
            iniciaComponentes();
            generaComida();
            timer1.Start();
        }

        private void Entrenamiento_Click(object sender, EventArgs e)
        {
            tipo_Juego = 1;
        }

        private void Aventura_Click(object sender, EventArgs e)
        {
            tipo_Juego = 2;
        }

        private void Infinito_Click(object sender, EventArgs e)
        {
            tipo_Juego = 3;
        }

    }
}
