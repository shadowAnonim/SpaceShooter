using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace game
{
    public static class GameManager
    {
        public static MainWindow mainWindow;
        public static double windowHeight;
        public static double windowWidth;
        public static Hero hero;
        public static List<Enemy> enemies;
        public static List<Enemy> destroyedEnemies;
        public static List<Projectile> projectiles;
        public static List<Projectile> destroyedProjectiles;
        public static int shootDelay = 1;
        public static Grid mainGrid;
        public static int points;
        public static bool gameOver = false;
        public static void Update()
        {
            hero.Update();
            foreach (Enemy enemy in enemies) enemy.Move();
            foreach (Enemy enemy in destroyedEnemies) enemies.Remove(enemy);
            destroyedEnemies.Clear();
            foreach (Projectile proj in projectiles) proj.Move();
            foreach (Projectile proj in destroyedProjectiles) projectiles.Remove(proj);
            destroyedProjectiles.Clear();
            if (gameOver)
            {
                gameOver = false;
                EndGame();
            }
        }
        public static bool CheckObjectOnScreen(Grid obj, int radius, bool saveOnScreen = false)
        {
            bool onScreenLeft, onScreenTop, onScreenRight, onScreenBotttom;
            onScreenLeft = onScreenTop = onScreenRight = onScreenBotttom = true;
            if (obj.Margin.Left < 0) onScreenLeft = false;
            if (obj.Margin.Top  < 0) onScreenTop = false;
            if (obj.Margin.Left + radius > windowWidth) onScreenRight = false;
            if (obj.Margin.Top + radius >  windowHeight) onScreenBotttom = false;
            if (saveOnScreen)
            {
                if (!onScreenLeft) obj.Margin = new Thickness(0, obj.Margin.Top, 0, 0);
                if (!onScreenTop) obj.Margin = new Thickness(obj.Margin.Left, 0, 0, 0);
                if (!onScreenRight) obj.Margin = new Thickness(windowWidth - radius, obj.Margin.Top, 0, 0);
                if (!onScreenBotttom) obj.Margin = new Thickness(obj.Margin.Left, windowHeight - radius, 0, 0);
            }
            return onScreenLeft && onScreenTop && onScreenRight && onScreenBotttom;
        }

        public static void destroyEnemy(Enemy enemy)
        {
            mainGrid.Children.Remove(enemy.grid);
            destroyedEnemies.Add(enemy);
        }

        public static void destroyProjectile(Projectile projectile)
        {
            mainGrid.Children.Remove(projectile.grid);
            destroyedProjectiles.Add(projectile);
        }
        public static void SpawnEnemy()
        {
            Enemy enemy = new Enemy();
            enemy.grid = new Grid();
            enemy.grid.HorizontalAlignment = HorizontalAlignment.Left;
            enemy.grid.Height = 100;
            enemy.grid.VerticalAlignment = VerticalAlignment.Top;
            enemy.grid.Width = 100;
            Ellipse cockpit = new Ellipse();
            cockpit.Fill = Brushes.White;
            cockpit.HorizontalAlignment = HorizontalAlignment.Left;
            cockpit.Height = 100;
            cockpit.Stroke = Brushes.Black;
            cockpit.VerticalAlignment = VerticalAlignment.Top;
            cockpit.Width = 100;
            enemy.grid.Children.Add(cockpit);
            mainGrid.Children.Add(enemy.grid);
            enemies.Add(enemy);
            Random r = new Random();
            enemy.grid.Margin = new Thickness
                (r.Next(0, Convert.ToInt32(windowWidth - enemy.gridRadius)),
                -enemy.gridRadius, 0, 0);
            
        }

        public static void ResetGame()
        {
            gameOver = false;
            points = 0;
            foreach(Enemy enemy in enemies) mainGrid.Children.Remove(enemy.grid);
            enemies.Clear();
            destroyedEnemies.Clear();
            foreach(Projectile proj in projectiles) mainGrid.Children.Remove(proj.grid);
            projectiles.Clear();
            destroyedProjectiles.Clear();
            hero.grid.Margin = new Thickness(250, 700, 0, 0);
        }

        public static void EndGame()
        {
            mainWindow.EndGame();
        }
    }
}
