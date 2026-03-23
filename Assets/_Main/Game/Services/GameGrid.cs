using UnityEngine;

namespace Game
{
    public class GameGrid
    {
        private const float CELL_SIZE = 1.1f;
        
        public GameGrid(Utils.Cam gameCamera)
        {
            var camera = gameCamera.Get;
            var cameraSize = camera.orthographicSize;
            
            var height = cameraSize * 2;
            var width = height * camera.aspect;
            ColumnsCount = Mathf.Clamp(Mathf.FloorToInt(width / CELL_SIZE), 3, 4);
            RowsCount = Mathf.Clamp(Mathf.FloorToInt(height / CELL_SIZE), 3, 4);
        
            var gridWorldWidth = ColumnsCount * CELL_SIZE;
            var gridWorldHeight = RowsCount * CELL_SIZE;
        
            var offsetX = (width - gridWorldWidth) / 2f;
            var offsetY = ((height - gridWorldHeight) / 2f) - 1f;
        
            GridOrigin = new Vector2( -width / 2 + offsetX, -height / 2 + offsetY);
            
            GridPositions = new Vector2Int[ColumnsCount * RowsCount];
            for (var x = 0; x < ColumnsCount; x++)
            {
                for (var y = 0; y < RowsCount; y++)
                {
                    GridPositions[y * ColumnsCount + x] = new Vector2Int(x, y);
                }
                
            }
        }
        
        public Vector2Int WorldToGrid(Vector2 worldPos)
        {
            var x = Mathf.FloorToInt((worldPos.x - GridOrigin.x) / CELL_SIZE);
            var y = Mathf.FloorToInt((worldPos.y - GridOrigin.y) / CELL_SIZE);
            return new Vector2Int(x, y);
        }
        
        public Vector2 GridToWorld(Vector2Int gridPosition)
        {
            var x = GridOrigin.x + gridPosition.x * CELL_SIZE + CELL_SIZE * 0.5f;
            var y = GridOrigin.y + gridPosition.y * CELL_SIZE + CELL_SIZE * 0.5f;
            return new Vector2(x, y);
        }
        
        public Vector2 GridOrigin {get; }
        public int RowsCount { get; }
        public int ColumnsCount { get; }
        public Vector2Int[] GridPositions { get; }
    }
}