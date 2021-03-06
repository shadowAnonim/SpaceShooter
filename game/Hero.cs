﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace game
{
    // Класс для работы с кораблём игрока
    public class Hero
    {
        public Grid grid;
        public int gridRadius = 100;
        private int speed = 8;
        public static int shootDelay = 500;
        public DispatcherTimer shootTimer;

        // Движение игрока
        public void Update()
        {
            // Проверка ввода и изменение координат
            if (Keyboard.IsKeyDown(Key.W)) grid.Margin = new Thickness(grid.Margin.Left, grid.Margin.Top - speed, 0, 0);
            if (Keyboard.IsKeyDown(Key.A)) grid.Margin = new Thickness(grid.Margin.Left - speed, grid.Margin.Top, 0, 0);
            if (Keyboard.IsKeyDown(Key.S)) grid.Margin = new Thickness(grid.Margin.Left, grid.Margin.Top + speed, 0, 0);
            if (Keyboard.IsKeyDown(Key.D)) grid.Margin = new Thickness(grid.Margin.Left + speed, grid.Margin.Top, 0, 0);
            // И стрельба
            if (Keyboard.IsKeyDown(Key.Space)) Fire();
            //if (Keyboard.IsKeyDown(Key.R)) GameManager.SpawnEnemy();
            // Удерживание корабля на экране
            GameManager.CheckObjectOnScreen(grid, gridRadius, true);
        }

       /// <summary>
       /// Стрельба
       /// </summary>
        private void Fire()
        {
            //Проверка на скорострельность
            if (!shootTimer.IsEnabled)
            {
                //Создание снаряда
                Projectile proj = new Projectile();
                proj.grid = new Grid();
                proj.grid.HorizontalAlignment = HorizontalAlignment.Left;
                proj.grid.Height = 10;
                proj.grid.VerticalAlignment = VerticalAlignment.Top;
                proj.grid.Width = 100;

                // Создание визуального отображения снаряда
                Rectangle rect = new Rectangle();
                rect.Fill = Brushes.White;
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.Height = 10;
                rect.Stroke = Brushes.Black;
                rect.VerticalAlignment = VerticalAlignment.Top;
                rect.Width = 10;
                proj.grid.Children.Add(rect);

                
                GameManager.mainGrid.Children.Add(proj.grid);
                GameManager.projectiles.Add(proj);
                // Расположение снаряда
                proj.grid.Margin = new Thickness(this.grid.Margin.Left + gridRadius / 2,
                    this.grid.Margin.Top - gridRadius / 2, 0, 0);
                shootTimer.Start();
            }
        }
    }
}
