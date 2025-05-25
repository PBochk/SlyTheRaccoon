using SlyTheRaccoon.Models;
using System.Collections.Generic;

namespace SlyTheRaccoon.Models.Services
{
    public class EnemyMovementService
    {
        private readonly GameModel _model;

        public EnemyMovementService(GameModel model)
        {
            _model = model;
        }

        public void MoveEnemies()
        {
            for (int i = _model.Enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _model.Enemies[i];
                bool canSeePlayer = MovementService.HasLineOfSight(
                    _model.CurrentLevel,
                    enemy.X, enemy.Y,
                    _model.PlayerX, _model.PlayerY
                );

                if (enemy.IsSmart && canSeePlayer)
                {
                    enemy.HasDetectedPlayer = true;
                }

                int steps = enemy.IsFast ? 2 : 1;
                for (int j = 0; j < steps; j++)
                {
                    if (enemy.HasDetectedPlayer || (!enemy.IsSmart && canSeePlayer) ||
                        (enemy.X != enemy.StartX || enemy.Y != enemy.StartY))
                    {
                        MoveEnemyOnce(enemy);
                    }
                }
            }
        }

        private void MoveEnemyOnce(Enemy enemy)
        {
            if (ShouldChasePlayer(enemy))
            {
                ChasePlayer(enemy);
            }
            else if (ShouldReturnToStart(enemy))
            {
                ReturnToStartPosition(enemy);
            }
        }

        private bool ShouldChasePlayer(Enemy enemy)
        {
            bool canSeePlayer = MovementService.HasLineOfSight(
                _model.CurrentLevel,
                enemy.X, enemy.Y,
                _model.PlayerX, _model.PlayerY
            );
            return enemy.HasDetectedPlayer || canSeePlayer;
        }

        private bool ShouldReturnToStart(Enemy enemy)
        {
            return enemy.X != enemy.StartX || enemy.Y != enemy.StartY;
        }

        private void ChasePlayer(Enemy enemy)
        {
            if (enemy.IsSmart)
            {
                ChasePlayerSmart(enemy);
            }
            else
            {
                ChasePlayerSimple(enemy);
            }
        }

        private void ChasePlayerSmart(Enemy enemy)
        {
            var path = MovementService.FindPath(
                _model.CurrentLevel,
                enemy.X, enemy.Y,
                _model.PlayerX, _model.PlayerY
            );

            if (path.Count > 0)
            {
                var nextStep = path[0];
                MoveEnemyTo(enemy, nextStep.x, nextStep.y);
            }
        }

        private void ChasePlayerSimple(Enemy enemy)
        {
            var (dx, dy) = MovementService.GetDirectionTowardsTarget(
                enemy.X, enemy.Y,
                _model.PlayerX, _model.PlayerY
            );
            TryMoveEnemyInDirection(enemy, dx, dy);
        }

        private void ReturnToStartPosition(Enemy enemy)
        {
            var (dx, dy) = MovementService.GetDirectionTowardsTarget(
                enemy.X, enemy.Y,
                enemy.StartX, enemy.StartY
            );
            TryMoveEnemyInDirection(enemy, dx, dy);
        }

        private void TryMoveEnemyInDirection(Enemy enemy, int dx, int dy)
        {
            if (dx != 0 && MovementService.CanMoveTo(_model.CurrentLevel, enemy.X + dx, enemy.Y))
            {
                MoveEnemyTo(enemy, enemy.X + dx, enemy.Y);
            }
            else if (dy != 0 && MovementService.CanMoveTo(_model.CurrentLevel, enemy.X, enemy.Y + dy))
            {
                MoveEnemyTo(enemy, enemy.X, enemy.Y + dy);
            }
        }

        private void MoveEnemyTo(Enemy enemy, int newX, int newY)
        {
            if (newX == _model.PlayerX && newY == _model.PlayerY) return;

            if (_model.CurrentLevel.Grid[newX, newY] == CellType.Empty)
            {
                _model.CurrentLevel.Grid[enemy.X, enemy.Y] = CellType.Empty;
                _model.CurrentLevel.Grid[newX, newY] = enemy.IsSmart ? CellType.SmartEnemy :
                                                     enemy.IsFast ? CellType.FastEnemy :
                                                     CellType.Enemy;
                enemy.X = newX;
                enemy.Y = newY;
            }
        }
    }
}