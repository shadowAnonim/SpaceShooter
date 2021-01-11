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
    /// <summary>
    /// Главный класс, управлляющий игрой
    /// </summary>
    public static class GameManager
    {
        // Характеристики окна
        public static MainWindow mainWindow;
        public static Grid mainGrid;
        public static double windowHeight;
        public static double windowWidth;

        // Корабль игрока
        public static Hero hero;

        // Список врагов
        public static List<Enemy> enemies;
        public static List<Enemy> destroyedEnemies;

        // Список снарядов
        public static List<Projectile> projectiles;
        public static List<Projectile> destroyedProjectiles;

        // Количество сбитых врагов
        public static int points;

        public static bool gameOver = false;

        /// <summary>
        /// Метод, который вызывается в каждом кадре
        /// </summary>
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
        /// <summary>
        /// Проверяет, находится ли заданный объект на экране
        /// </summary>
        /// <param name="obj">Объект, который нужно проверить</param>
        /// <param name="radius">Радиус объекта </param>
        /// <param name="saveOnScreen">Если true, то удерживать объект на экране</param>
        /// <returns>Если объект находится на экране true, иначе false</returns>
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
        /// <summary>
        /// Уничтожает врага
        /// </summary>
        /// <param name="enemy"></param>
        public static void destroyEnemy(Enemy enemy)
        {
            mainGrid.Children.Remove(enemy.grid);
            destroyedEnemies.Add(enemy);
        }
        /// <summary>
        /// Уничтожает снаряд
        /// </summary>
        /// <param name="projectile"></param>
        public static void destroyProjectile(Projectile projectile)
        {
            mainGrid.Children.Remove(projectile.grid);
            destroyedProjectiles.Add(projectile);
        }
        /// <summary>
        /// Создает нового врага
        /// </summary>
        public static void SpawnEnemy()
        {
            Enemy enemy = new Enemy();

            // Настройка параметров Grid-а
            enemy.grid = new Grid();
            enemy.grid.HorizontalAlignment = HorizontalAlignment.Left;
            enemy.grid.Height = 100;
            enemy.grid.VerticalAlignment = VerticalAlignment.Top;
            enemy.grid.Width = 100;

            // Наастройка праметров кабины
            Ellipse cockpit = new Ellipse();
            cockpit.Fill = Brushes.White;
            cockpit.HorizontalAlignment = HorizontalAlignment.Left;
            cockpit.Height = 100;
            cockpit.Stroke = Brushes.Black;
            cockpit.VerticalAlignment = VerticalAlignment.Top;
            cockpit.Width = 100;

            // Расположение врага в случайной точке над экраном
            Random r = new Random();
            enemy.grid.Margin = new Thickness
                (r.Next(0, Convert.ToInt32(windowWidth - enemy.gridRadius)),
                -enemy.gridRadius, 0, 0);

            // Отображение врага на экране
            enemy.grid.Children.Add(cockpit);
            mainGrid.Children.Add(enemy.grid);
            enemies.Add(enemy); 
        }

        /// <summary>
        /// Перезапускает игру
        /// </summary>
        public static void ResetGame()
        {
            gameOver = false;
            // Обнуление очков
            points = 0;

            //Уничтожение всех врагов
            foreach(Enemy enemy in enemies) mainGrid.Children.Remove(enemy.grid);
            enemies.Clear();
            destroyedEnemies.Clear();

            //Уничтожение всех снарядов
            foreach (Projectile proj in projectiles) mainGrid.Children.Remove(proj.grid);
            projectiles.Clear();
            destroyedProjectiles.Clear();

            //Перемещение корабля игрока в начальную позицию
            hero.grid.Margin = new Thickness(250, 700, 0, 0);
        }

        public static void EndGame()
        {
            mainWindow.EndGame();
        }
    }
}
