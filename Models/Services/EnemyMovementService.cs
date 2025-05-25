namespace SlyTheRaccoon.Models.Services
{
    public class EnemyMovementService
    {
        private readonly GameModel _model;

        public EnemyMovementService(GameModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Двигает всех врагов, которые засекли игрока
        /// </summary>
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
                    if (enemy.HasDetectedPlayer 
                        || canSeePlayer 
                        || enemy.X != enemy.StartX 
                        || enemy.Y != enemy.StartY)
                    {
                        MoveEnemyOnce(enemy);
                    }
                }
            }
        }

        /// <summary>
        /// Один раз двигает врага в зависимости от условий
        /// </summary>
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

        /// <summary>
        /// Нужно ли врагу преследовать игрока
        /// </summary>
        private bool ShouldChasePlayer(Enemy enemy)
        {
            bool canSeePlayer = MovementService.HasLineOfSight(
                _model.CurrentLevel,
                enemy.X, enemy.Y,
                _model.PlayerX, _model.PlayerY
            );
            return enemy.HasDetectedPlayer || canSeePlayer;
        }

        /// <summary>
        /// Нужно ли врагу возвращаться на исходную позицию
        /// </summary>
        private bool ShouldReturnToStart(Enemy enemy)
        {
            return enemy.X != enemy.StartX || enemy.Y != enemy.StartY;
        }

        /// <summary>
        /// Определение способа преследования
        /// </summary>
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

        /// <summary>
        /// Преследование с поиском пути
        /// </summary>
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

        /// <summary>
        /// Преследование по прямой
        /// </summary>
        private void ChasePlayerSimple(Enemy enemy)
        {
            var (dx, dy) = MovementService.GetDirectionTowardsTarget(
                enemy.X, enemy.Y,
                _model.PlayerX, _model.PlayerY
            );
            TryMoveEnemyInDirection(enemy, dx, dy);
        }

        /// <summary>
        /// Возврат врага на исходную позицию
        /// </summary>
        private void ReturnToStartPosition(Enemy enemy)
        {
            var (dx, dy) = MovementService.GetDirectionTowardsTarget(
                enemy.X, enemy.Y,
                enemy.StartX, enemy.StartY
            );
            TryMoveEnemyInDirection(enemy, dx, dy);
        }

        /// <summary>
        /// Определение направления движения врага
        /// </summary>
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

        /// <summary>
        /// Обновление сетки после движения врага
        /// </summary>
        private void MoveEnemyTo(Enemy enemy, int newX, int newY)
        {
            if (newX == _model.PlayerX && newY == _model.PlayerY) return;

            if (_model.CurrentLevel.Grid[newX, newY] == CellType.Empty)
            {
                _model.CurrentLevel.Grid[enemy.X, enemy.Y] = CellType.Empty;
                _model.CurrentLevel.Grid[newX, newY] = enemy.IsSmart ? CellType.SmartEnemy 
                                                        : enemy.IsFast ? CellType.FastEnemy 
                                                        : CellType.Enemy;
                enemy.X = newX;
                enemy.Y = newY;
            }
        }
    }
}