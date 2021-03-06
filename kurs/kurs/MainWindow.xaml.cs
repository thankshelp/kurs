﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player = new MediaPlayer();
        public Ellipse[,] map2;

        public int[,] map = {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,2,3,2,1,2,2,2,1,1,2,2,2,2,3,1},
                {1,2,1,2,2,2,1,2,1,1,2,1,2,1,2,1},
                {1,2,1,2,1,1,1,2,1,1,2,1,2,1,2,1},
                {1,2,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {1,2,1,1,1,1,2,1,1,1,1,1,2,1,2,1},
                {1,2,1,2,2,2,3,2,2,1,2,2,2,1,2,1},
                {1,2,1,2,1,1,1,1,2,1,2,1,2,1,2,1},
                {1,2,2,2,1,0,0,1,2,2,2,1,2,2,2,1},
                {1,1,1,0,1,0,0,0,2,1,1,1,2,1,1,1},
                {1,1,1,2,1,0,0,0,2,1,1,1,2,1,1,1},
                {1,2,2,2,1,0,0,1,2,2,2,1,2,2,2,1},
                {1,2,1,2,1,1,1,1,2,1,2,1,2,1,2,1},
                {1,2,1,2,2,2,3,2,2,1,2,2,2,1,2,1},
                {1,2,1,1,1,1,2,1,1,1,1,1,2,1,2,1},
                {1,2,1,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {1,2,1,2,1,1,1,2,1,1,2,1,2,1,2,1},
                {1,2,1,2,2,2,1,2,1,1,2,1,2,1,2,1},
                {1,2,3,2,1,2,2,2,1,1,2,2,2,2,3,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        }; 

    

            public class CChar
             {
            public int x, y;
            int px, py;
            Ellipse r;

            int h, w;
            Ellipse pm;
            ImageBrush ib = new ImageBrush();
            int currentFrame;
            int currentRow;



            public CChar(int x, int y)
            {
                this.x = x;
                this.y = y;

                w = h = 45;

                px = x * w;
                py = y * h;

                pm = new Ellipse();

                //pm.Stroke = Brushes.YellowGreen;
                //pm.Fill = Brushes.Yellow;
                //pm.StrokeThickness = 2;

                pm.Width = 45;
                pm.Height = 45;

                ib.AlignmentX = AlignmentX.Left;
                ib.AlignmentY = AlignmentY.Top;
                ib.Stretch = Stretch.None;

                try
                {
                    ib.Viewbox = new Rect(10, 10, 0, 0);
                ib.ViewboxUnits = BrushMappingMode.Absolute;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Try again");
                }
                currentFrame = 0;

                try
                {
                    ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/pm (1).png", UriKind.Absolute));
                }
                catch (ArgumentNullException)
                {
                    ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/pm (1).png", UriKind.Absolute));
                }

                pm.Fill = ib;

                pm.RenderTransform = new TranslateTransform(px, py);
                //pm.Margin = new Thickness(px, py, 0, 0);
            }

            public bool move(int x, int y, Ellipse[,] map2, ref Canvas scene, MediaPlayer player)
            {
                //if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1)) return true;

                //if (map[x, y] == 1) return true;
                

                if (player.Position == player.NaturalDuration)
                {
                    player.Stop();
                    player.Play();
                }


                this.x = x;
                this.y = y;

                var frameCount = 8;
                var frameW = 65;
                var frameH = 65;

                

                if (px > x * w)
                {
                    px -= 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 1;

                    player.Play();

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (pm.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop + 10, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }
                if (px < x * w)
                {
                    px += 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 0;

                    player.Play();


                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (pm.Fill as ImageBrush).Viewbox = new Rect(frameLeft + 10, frameTop + 10, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }

                //px = x * w;
                if (py > y * h)
                {
                    py -= 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 2;

                    player.Play();


                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (pm.Fill as ImageBrush).Viewbox = new Rect(frameLeft + 10, frameTop + 10, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }

                if (py < y * h)
                {
                    py += 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 3;

                    player.Play(); 


                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (pm.Fill as ImageBrush).Viewbox = new Rect(frameLeft + 10, frameTop + 20, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }

                //py = y * h;

                pm.RenderTransform = new TranslateTransform(px, py);

                if ((px == x * w) && (py == y * h)) {
                    player.Pause();
                    scene.Children.Remove(map2[x,y]);
                    return true; }
                else return false;

            }
          

            public void addToScene(ref Canvas scene)
                 {
                      scene.Children.Add(pm);
                 }
            }

        public class Enemy
        {
            public int x, y;
            int dx, dy;

            int h, w;
            Rectangle ghost1;
            ImageBrush ib1 = new ImageBrush();

            int currentFrame;
            int currentRow;

            public Enemy(int x, int y)
            {
                this.x = x;
                this.y = y;

                w = h = 45;

                dx = x * w;
                dy = y * h;

                ghost1 = new Rectangle();



                ghost1.Width = 45;
                ghost1.Height = 50;

                ib1.AlignmentX = AlignmentX.Left;
                ib1.AlignmentY = AlignmentY.Top;
                ib1.Stretch = Stretch.None;



                ib1.Viewbox = new Rect(0, 0, 0, 0);
                ib1.ViewboxUnits = BrushMappingMode.Absolute;

                currentFrame = 0;
                try
                {
                    ib1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/gh_r (1).png", UriKind.Absolute));
                }
                catch (ArgumentNullException)
                {
                    ib1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/gh_r (1).png", UriKind.Absolute));
                }

                    ghost1.Fill = ib1;


                ghost1.RenderTransform = new TranslateTransform(dx, dy);

            }
            public bool mode(int gx, int gy, int px, int py)
            {
                if ((((px-gx) <= 3) && ((px-gx)>= -3)) && (((py-gy) <=3) && ((py-gy) >= -3)))
                { 
                    
                    //преследование
                    return true;
                }
                else
                {
                    //рандом
                    return false;
                }
            }
            public void bonusMode(int x, int y)
            {            

                this.x = x;
                this.y = y;

                try
                {
                    ib1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/gh.png", UriKind.Absolute));
                }
                catch (ArgumentNullException)
                {
                    ib1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/gh.png", UriKind.Absolute));
                }
                    ghost1.Fill = ib1;

                var frameCount = 3;
                var frameW = 55;
                var frameH = 55;

                if (dx > x * w)
                {
                    dx -= 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 2;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }
                if (dx < x * w)
                {
                    dx += 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 1;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                }

                //px = x * w;
                if (dy > y * h)
                {
                    dy -= 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 0;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }

                if (dy < y * h)
                {
                    dy += 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 3;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }

                }

                //py = y * h;

                ghost1.RenderTransform = new TranslateTransform(dx, dy);

                if ((dx == x * w) && (dy == y * h)) return;
                
            }

        public bool move(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                try
                {
                    ib1.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/image/gh_r (1).png", UriKind.Absolute));
                }
                catch (ArgumentNullException)
                {
                    ib1.ImageSource = new BitmapImage(new Uri(@"C:\Users\Mvideo\Downloads\kurs2-master (2)\kurs2-master\kurs\kurs\image\gh_r (1).png", UriKind.Absolute));
                }
                ghost1.Fill = ib1;

                var frameCount = 3;
                var frameW = 55;
                var frameH = 55;

                if (dx > x * w)
                {
                    dx -= 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 2;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop , frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }
                if (dx < x * w)
                {
                    dx += 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 1;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }

                //px = x * w;
                if (dy > y * h)
                {
                    dy -= 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 0;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                }

                if (dy < y * h)
                {
                    dy += 1;
                    currentFrame = (currentFrame + 1 + frameCount) % frameCount;
                    currentRow = 3;

                    var frameLeft = currentFrame * frameW;
                    var frameTop = currentRow * frameH;
                    try
                    {
                        (ghost1.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Try again");
                    }
                }

                //py = y * h;

                ghost1.RenderTransform = new TranslateTransform(dx, dy);

                if ((dx == x * w) && (dy == y * h))
               
                    return true;
                
                else return false;

            }

            public void addToScene(ref Canvas scene)
            {
                scene.Children.Add(ghost1);
            }
        }

        public class GDir
        {
            public int x, y;
            int dx, dy;
            int d = 1;
            int xd = 0, yd = 0;



            public GDir(int x,int y)
            {
                this.x = x;
                this.y = y;
                dx = 1;
                dy = 1;
            }

            public void randomdir()
            {
                int s = new Random().Next(0, 4);
                
                while (((d == 0) && (s == 1)) || ((d == 2) && (s == 3)) || ((d == 1) && (s == 0)) || ((d == 3) && (s == 2)))
                {
                    s = new Random().Next(100);
                    s %= 4;
                }

                if (s == 0)
                {
                    up();
                    d = s;

                }
                if (s == 1)
                {
                    down();
                    d = s;
                    
                }
                if (s == 2)
                {
                    left();
                    d = s;
                }
                if (s == 3)
                {
                    right();
                    d = s;
                }
            }

            public void hunt(int px, int py, int gx, int gy, int[,] map)
            {
                int x0 = gx - px;
                int y0 = gy - py;
                

                if ((x0 >= 0) && (y0 >= 0))
                {
                    if ((map[x + 0, y + (-1)] != 1))
                    {
                        up();
                        //if (yd != 1)
                        //{
                        //    up();
                        //    xd = 0; yd = -1;
                        //}
                        //else
                        //{

                        //}
                    }
                    else
                    {
                        if (map[x - 1, y + 0] != 1)
                        { 
                            left();
                        }
                        else
                        {
                            if (map[x + 1, y + 0] != 1)
                            {
                                down();
                            }
                            else
                            {
                                right();
                            }
                        }
                    }                    
                }

                if ((x0 <= 0) && (y0 >= 0))
                {
                    if (map[x + 0, y + (-1)] != 1)
                    {
                        up();
                    }
                    else
                    {
                        if (map[x + 1, y + 0] != 1)
                        {
                            right();
                        }
                        else
                        {
                            if (map[x + 1, y + 0] != 1)
                            {
                                down();
                            }
                            else
                            {
                                left();
                            }
                        }
                    }
                }

                if ((x0 >= 0) && (y0 <= 0))
                {
                    if (map[x + 0, y + 1] != 1)
                    {
                        down();
                    }
                    else
                    {
                        if (map[x - 1, y + 0] != 1)
                        {
                            left();
                        }
                        else
                        {
                            if (map[x + 0, y + (-1)] != 1)
                            {
                                up();
                            }
                            else
                            {
                                right();
                            }
                        }
                    }
                }

                if ((x0<=0) && (y0 <= 0))
                {
                    if (map[x + 0, y + 1] != 1)
                    {
                        down();
                    }
                    else
                    {
                        if (map[x + 1, y + 0] != 1)
                        {
                            right();
                        }
                        else
                        {
                            if (map[x + 0, y + (-1)] != 1)
                            {
                                up();
                            }
                            else
                            {
                                left();
                            }
                        }
                    }
                }
            }

            public void Update(int[,] map)
            {
                if ((x + dx < 0) || (x + dx >= map.GetLength(0)) || (y + dy < 0) || (y + dy > map.GetLength(1))) return;

                if ((map[x + dx, y + dy] == 2) || (map[x + dx, y + dy] == 0) || (map[x + dx, y + dy] == 3))
                { 
                    x = x + dx;
                    y = y + dy;
                    
                }
                
                       
            }

           

            public void up()
                {
                    dy = -1;
                    dx = 0;
                }

                public void down()
                {
                    dy = 1;
                    dx = 0;
                }

                public void left()
                {
                    dx = -1;
                    dy = 0;
                }

                public void right()
                {
                    dx = 1;
                    dy = 0;
                }

            public bool moveTo(int x, int y)
            {
                //================
                return false;
            }
        }

         public class CDir
        {
            public int x, y;
            int dx, dy;
            


            public CDir(int x, int y)
            {
                this.x = x;
                this.y = y;
                dx = 1; 
                dy = 1;
            }

            public int k(int[,] map, int k)
            {
                if ((map[x, y] == 2) || (map[x, y] == 3))
                {
                    k--;
                }
                return k;
            }

            public int score(int[,] map, int s)
            {
                if ((map[x, y] == 2))
                {
                    s += 10;
                }

                if (map[x, y ] == 3)
                {
                    s += 20;
                }
                return s;
            }

            public void Update(int [,] map, DispatcherTimer Deathtimer, DispatcherTimer ghTimer)
            {
                if ((x + dx < 0) || (x + dx >= map.GetLength(0)) || (y + dy < 0) || (y + dy > map.GetLength(1))) return;

                if ((map[x + dx, y + dy] == 2) || (map[x + dx, y + dy] == 0) || (map[x + dx, y + dy] == 3))
                {
                    x = x + dx;
                    y = y + dy;

                    if ((map[x, y] == 2))
                    {
                        map[x, y] = 0;
                       
                    }
                    if (map[x, y] == 3)
                    {
                        map[x, y] = 0;
                        ghTimer.Stop();
                        Deathtimer.Start();
                        
                    }
                }


            }

            public void up()
            {
                dy = -1;
                dx = 0;
            }

            public void down()
            {
                dy = 1;
                dx = 0;
            }

            public void left()
            {
                dx = -1;
                dy = 0;
            }

            public void right()
            {
                dx = 1;
                dy = 0;
            }

          

        }

        CChar pakman;
        Enemy gh;
        CDir dir;
        GDir gir;
        int score = 0;
        int i = 0;
        int k = 141;
        System.Windows.Threading.DispatcherTimer pmTimer;
        System.Windows.Threading.DispatcherTimer ghTimer;
        System.Windows.Threading.DispatcherTimer DeathTimer;

        //16x20

        //{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        //{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
        //{ 1, 2, 1, 1, 2, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 2, 1, 1, 2, 1 },
        //{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
        //{ 1, 2, 1, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 2, 1 },
        //{ 1, 2, 2, 2, 2, 1, 2, 2, 2, 1, 1, 2, 2, 2, 1, 2, 2, 2, 2, 1 },
        //{ 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1 },
        //{ 1, 1, 1, 1, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 1, 1, 1 },
        //{ 1, 2, 2, 2, 2, 1, 2, 1, 1, 0, 0, 1, 1, 2, 1, 2, 2, 2, 2, 1 },
        //{ 1, 2, 1, 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 2, 2, 2, 1, 1, 2, 1 },
        //{ 1, 2, 2, 1, 2, 1, 2, 1, 0, 0, 0, 0, 1, 2, 1, 2, 1, 2, 2, 1 },
        //{ 1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 2, 1, 1 },
        //{ 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 1 },
        //{ 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1 },
        //{ 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
        //{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } }; 



        void fillmap2(int[,] map, ref Canvas scene)
        {
            map2 = new Ellipse[20, 16];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2 || map[i, j] == 3)
                    {

                        if (map[i, j] == 2)
                        {
                            map2[i, j] = new Ellipse();
                            map2[i, j].Fill = Brushes.Azure;
                            map2[i, j].Width = 10;
                            map2[i, j].Height = 10;
                            map2[i, j].StrokeThickness = 2;
                            map2[i, j].RenderTransform = new TranslateTransform(i * 45 + 15, j * 45 + 15);
                            scene.Children.Add(map2[i, j]);
                        }
                        
                        if (map[i, j] == 3)
                        {
                            map2[i, j] = new Ellipse();
                            map2[i, j].Fill = Brushes.Azure;
                            map2[i, j].Width = 20;
                            map2[i, j].Height = 20;
                            map2[i, j].StrokeThickness = 2;
                            map2[i, j].RenderTransform = new TranslateTransform(i * 45 + 11, j * 45 + 10);
                            scene.Children.Add(map2[i, j]);
                        }
                       // else map2[i, j] = null;
                    }
                    else map2[i, j] = null;
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            player.Open(new Uri("C:\\Users\\Admin\\Desktop\\kurs2-master\\kurs2-master\\kurs\\kurs\\sounds\\8-Bit Universe - Our House (8-Bit Version) (8-Bit Version).mp3", UriKind.Relative));
            player.Play();
            player.Volume = 50.0 / 100.0;

            pakman = new CChar(9, 3);
            
            gh = new Enemy(8, 5);

            gir = new GDir(8, 5);
            dir = new CDir(9, 3);
           

            pmTimer = new System.Windows.Threading.DispatcherTimer();
            pmTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            pmTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            ghTimer = new System.Windows.Threading.DispatcherTimer();
            ghTimer.Tick += new EventHandler(dispatcherGhostTimer_Tick);
            ghTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            DeathTimer = new System.Windows.Threading.DispatcherTimer();
            DeathTimer.Tick += new EventHandler(dispatcherDeathTimer_Tick);
            DeathTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);


            fillmap2(map, ref Game);
            pakman.addToScene(ref Game);
            gh.addToScene(ref Game);
         

        }
        private void dispatcherDeathTimer_Tick(object sender, EventArgs e)
        {
            if (i != 600)
            {
                gh.bonusMode(1, 1);
                i++;
            }
            else
            {
                DeathTimer.Stop();
                ghTimer.Start();
                i = 0;
            }
            
        }

        public bool iSeeYou()
        {
            if ((pakman.x == gh.x))// || (pakman.x == gh.x))
            {
                for (int i = Math.Min(pakman.y, gh.y); i < Math.Max(pakman.y, gh.y); i++)
                {
                    if (map[pakman.x, i] == 1) return false;
                }
                return true;
            }
            if ((pakman.y == gh.y))// || (pakman.x == gh.x))
            {
                for (int i = Math.Min(pakman.x, gh.x); i < Math.Max(pakman.x, gh.x); i++)
                {
                    if (map[i, pakman.y] == 1) return false;
                }
                return true;
            }

            return false;

        }

        private void dispatcherGhostTimer_Tick(object sender, EventArgs e)
            {
              if (gh.mode(gir.x, gir.y, dir.x, dir.y) == false)
              {
                if (gh.move(gir.x, gir.y) == true)
                {
                    gir.randomdir();
                    gir.Update(map);
                }
                     
              }
              else
              {
                if (gh.move(gir.x, gir.y) == true)
                {
                    gir.hunt(dir.x, dir.y, gir.x, gir.y, map);
                    gir.Update(map);
                }
              }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            if (pakman.move(dir.x, dir.y, map2, ref Game, player) == true)
            {
                k = dir.k(map, k);
                score = dir.score(map, score);

                dir.Update(map, DeathTimer, ghTimer);

                if (k == 0)
                {
                    pmTimer.Stop();
                    ghTimer.Stop();
                    DeathTimer.Stop();

                    Winner win = new Winner(score);

                    if(win.ShowDialog() == true)
                    {
                        string name = win.name.Text; 
                    }

                   
                }
            }
           
            sc.Content = score;
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            menu.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Visible;
            player.Open(new Uri("C:\\Users\\Admin\\Desktop\\kurs2-master\\kurs2-master\\kurs\\kurs\\sounds\\Body.mp3", UriKind.Relative));

            pmTimer.Start();
            ghTimer.Start();
        }

        private void Start_MouseEnter(object sender, MouseEventArgs e)
        {
            

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            if (e.Key == Key.Left) dir.left();
            //else
            if (e.Key == Key.Right) dir.right();
            //else
            if (e.Key == Key.Down) dir.down();
            //else
            if (e.Key == Key.Up) dir.up();

            //if (e.Key == Key.Escape) ;
            //}
            //catch(else)
            //{
            //    MessageBox.Show("Click the arrow button")
            //}
        }

    }

   
}
