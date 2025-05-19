using SlyTheRaccoon.Models;
using SlyTheRaccoon.ViewModels;
using SlyTheRaccoon.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

public class GameViewModel : ObservableObject
{
    private readonly GameModel _model = new GameModel();
    private ObservableCollection<CellViewModel> _cells = new ObservableCollection<CellViewModel>();

    public ObservableCollection<CellViewModel> Cells => _cells;
    public int GridRows => _model.CurrentLevel?.Height ?? 0;
    public int GridColumns => _model.CurrentLevel?.Width ?? 0;
    public ICommand MoveCommand { get; }

    private int _currentLevelIndex = 0;
    private readonly List<Level> _levels = new List<Level>();

    public GameViewModel()
    {
        _levels.Add(GameLevels.Level1);
        _levels.Add(GameLevels.Level2);
        _levels.Add(GameLevels.Level3);
        MoveCommand = new RelayCommand<Key>(HandleMove);
        InitializeLevel();
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= _levels.Count)
        {
            HandleGameComplete();
            return;
        }

        Level freshLevel = GetFreshLevel(levelIndex);
        _model.LoadLevel(freshLevel);
        UpdateAllCells();
        OnPropertyChanged(nameof(GridRows));
        OnPropertyChanged(nameof(GridColumns));
    }

    private void InitializeLevel()
    {
        _model.LoadLevel(GameLevels.Level1);
        UpdateAllCells();
    }

    private void UpdateAllCells()
    {
        _cells.Clear();
        for (int y = 0; y < _model.CurrentLevel.Height; y++)
        {
            for (int x = 0; x < _model.CurrentLevel.Width; x++)
            {
                _cells.Add(new CellViewModel(_model.CurrentLevel.Grid[x, y], x, y));
            }
        }
    }
    private Level GetFreshLevel(int levelIndex)
    {
        switch (levelIndex)
        {
            case 0: return GameLevels.Level1;
            case 1: return GameLevels.Level2;
            case 2: return GameLevels.Level3;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleMove(Key key)
    {
        int dx = 0, dy = 0;
        switch (key)
        {
            case Key.Left: dx = -1; break;
            case Key.Right: dx = 1; break;
            case Key.Up: dy = -1; break;
            case Key.Down: dy = 1; break;
            default: return;
        }

        int newX = _model.PlayerX + dx;
        int newY = _model.PlayerY + dy;

        if (!CanMoveTo(newX, newY)) return;

        switch (_model.CurrentLevel.Grid[newX, newY])
        {
            case CellType.Wall:
                return;

            case CellType.Enemy:
                HandleEnemyCollision();
                return;

            case CellType.Food:
                _model.HasFood = true;
                _model.CurrentLevel.Grid[newX, newY] = CellType.Empty;
                MovePlayer(newX, newY);
                break;

            case CellType.Exit:
                if (_model.HasFood)
                {
                    MovePlayer(newX, newY);
                    HandleLevelComplete();
                }
                return;

            default:
                MovePlayer(newX, newY);
                MoveEnemies();
                return;
        }
    }

    private bool CanMoveTo(int x, int y)
    {
        if (x < 0 || x >= _model.CurrentLevel.Width ||
            y < 0 || y >= _model.CurrentLevel.Height)
            return false;

        CellType cell = _model.CurrentLevel.Grid[x, y];
        return cell != CellType.Wall;
    }

    private void MovePlayer(int newX, int newY)
    {
        _model.CurrentLevel.Grid[_model.PlayerX, _model.PlayerY] = CellType.Empty;
        _model.CurrentLevel.Grid[newX, newY] = CellType.Player;
        _model.PlayerX = newX;
        _model.PlayerY = newY;
        UpdateAllCells();
    }

    private bool HasLineOfSight(int enemyX, int enemyY, int playerX, int playerY)
    {
        if (!IsValidCoordinate(enemyX, enemyY) || !IsValidCoordinate(playerX, playerY))
            return false;

        if (enemyX != playerX && enemyY != playerY)
            return false;

        int dx = 0, dy = 0;
        if (enemyX == playerX)
            dy = enemyY < playerY ? 1 : -1;
        else
            dx = enemyX < playerX ? 1 : -1;

        int x = enemyX + dx;
        int y = enemyY + dy;

        while (x != playerX || y != playerY)
        {
            if (!IsValidCoordinate(x, y))
                return false;

            if (_model.CurrentLevel.Grid[x, y] == CellType.Wall)
                return false;

            x += dx;
            y += dy;
        }

        return true;
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < _model.CurrentLevel.Width &&
               y >= 0 && y < _model.CurrentLevel.Height;
    }

    private void MoveEnemies()
    {
        foreach (var enemy in _model.Enemies)
        {
            bool canSeePlayer = HasLineOfSight(enemy.X, enemy.Y, _model.PlayerX, _model.PlayerY);

            if (enemy.IsSmart && canSeePlayer)
            {
                enemy.HasDetectedPlayer = true;
            }

            if ((enemy.IsSmart && enemy.HasDetectedPlayer) || (!enemy.IsSmart && canSeePlayer))
            {
                MoveEnemyOnce(enemy);
                if (enemy.IsFast)
                {
                    MoveEnemyOnce(enemy);
                }
            }
            else if (enemy.X != enemy.StartX || enemy.Y != enemy.StartY)
            {
                MoveEnemyOnce(enemy);
                if (enemy.IsFast)
                {
                    MoveEnemyOnce(enemy);
                }
            }
        }

        CheckEnemyCollisions();
        UpdateAllCells();
    }

    private void MoveEnemyOnce(Enemy enemy)
    {
        bool canSeePlayer = HasLineOfSight(enemy.X, enemy.Y, _model.PlayerX, _model.PlayerY);

        if ((enemy.IsSmart && enemy.HasDetectedPlayer) || (!enemy.IsSmart && canSeePlayer))
        {
            if (enemy.IsSmart)
            {
                var path = FindPath(enemy.X, enemy.Y, _model.PlayerX, _model.PlayerY);
                if (path.Count > 0)
                {
                    var nextStep = path[0];
                    if (nextStep.x == _model.PlayerX && nextStep.y == _model.PlayerY)
                    {
                        HandleEnemyCollision();
                        return;
                    }
                    MoveEnemyTo(enemy, nextStep.x, nextStep.y);
                }
            }
            else
            {
                int dx = Math.Sign(_model.PlayerX - enemy.X);
                int dy = Math.Sign(_model.PlayerY - enemy.Y);

                if (dx != 0 && CanMoveTo(enemy.X + dx, enemy.Y))
                {
                    MoveEnemyTo(enemy, enemy.X + dx, enemy.Y);
                }
                else if (dy != 0 && CanMoveTo(enemy.X, enemy.Y + dy))
                {
                    MoveEnemyTo(enemy, enemy.X, enemy.Y + dy);
                }
            }
        }
        else if (enemy.X != enemy.StartX || enemy.Y != enemy.StartY)
        {
            int dx = Math.Sign(enemy.StartX - enemy.X);
            int dy = Math.Sign(enemy.StartY - enemy.Y);

            if (dx != 0 && CanMoveTo(enemy.X + dx, enemy.Y))
            {
                MoveEnemyTo(enemy, enemy.X + dx, enemy.Y);
            }
            else if (dy != 0 && CanMoveTo(enemy.X, enemy.Y + dy))
            {
                MoveEnemyTo(enemy, enemy.X, enemy.Y + dy);
            }
        }
    }

    private void MoveEnemyTo(Enemy enemy, int newX, int newY)
    {
        if (newX == _model.PlayerX && newY == _model.PlayerY)
        {
            HandleEnemyCollision();
            return;
        }

        if (_model.CurrentLevel.Grid[newX, newY] == CellType.Empty)
        {
            _model.CurrentLevel.Grid[enemy.X, enemy.Y] = CellType.Empty;
            if (enemy.IsSmart)
            {
                _model.CurrentLevel.Grid[newX, newY] = CellType.SmartEnemy;
            }
            else if (enemy.IsFast)
            {
                _model.CurrentLevel.Grid[newX, newY] = CellType.FastEnemy;
            }
            else
            {
                _model.CurrentLevel.Grid[newX, newY] = CellType.Enemy;
            }
            enemy.X = newX;
            enemy.Y = newY;
        }
    }

    private List<(int x, int y)> FindPath(int startX, int startY, int targetX, int targetY)
    {
        var grid = _model.CurrentLevel.Grid;
        if (!IsValidCoordinate(startX, startY) || !IsValidCoordinate(targetX, targetY))
            return new List<(int x, int y)>();

        if (startX == targetX && startY == targetY)
            return new List<(int x, int y)> { (startX, startY) };

        var queue = new Queue<(int x, int y)>();
        var visited = new Dictionary<(int x, int y), (int x, int y)>();
        var directions = new (int dx, int dy)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        queue.Enqueue((startX, startY));
        visited[(startX, startY)] = (-1, -1);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var dir in directions)
            {
                int nx = current.x + dir.dx;
                int ny = current.y + dir.dy;
                if (nx == targetX && ny == targetY)
                {
                    var path = new List<(int x, int y)> { (targetX, targetY) };
                    var temp = current;

                    while (temp.x != startX || temp.y != startY)
                    {
                        path.Add(temp);
                        temp = visited[temp];
                    }
                    path.Reverse();
                    return path;
                }

                if (IsValidCoordinate(nx, ny) 
                    && grid[nx, ny] != CellType.Wall 
                    && !visited.ContainsKey((nx, ny)))
                {
                    visited[(nx, ny)] = current;
                    queue.Enqueue((nx, ny));
                }
            }
        }

        return new List<(int x, int y)>();
    }


    private void CheckEnemyCollisions()
    {
        foreach (var enemy in _model.Enemies)
        {
            if (Math.Abs(enemy.X - _model.PlayerX) <= 1 &&
                Math.Abs(enemy.Y - _model.PlayerY) <= 1)
            {
                HandleEnemyCollision();
                return;
            }
        }
    }

    private void HandleEnemyCollision()
    {
        MessageBox.Show("Вы попались! Начать уровень заново?");
        LoadLevel(_currentLevelIndex);
    }

    private void HandleLevelComplete()
    {
        _model.HasFood = false;
        _currentLevelIndex++;
        LoadLevel(_currentLevelIndex);
    }

    private void HandleGameComplete()
    {
        MessageBox.Show("Поздравляем! Вы прошли все уровни!");
    }
}