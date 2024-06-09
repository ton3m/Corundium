/*using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Runtime.Core.Inventory.View
{
    public class CellDraggerController
    {
        private readonly CellDragger _dragger;
        private readonly List<CellViewIndexed> _cells;
        
        public CellDraggerController(List<CellView> cells, CellDragger dragger)
        {
            _dragger = dragger;
            _cells = ToIndexed(cells);
            
            _cells.ForEach(cell =>
            {
                cell.Pressed += OnPressed;
            });
        }

        public void Destruct()
        {
            _cells.ForEach(cell =>
            {
                cell.Pressed -= OnPressed;
                cell.Destruct();
            });
        }

        private void OnPressed(Vector3 position, int index)
        {
            Debug.Log("Pressed");
            _dragger.Pick(position, index);
        }
        
        private List<CellViewIndexed> ToIndexed(List<CellView> cells)
        {
            List<CellViewIndexed> result = new(cells.Count);
            
            for (int i = 0; i < cells.Count; i++)
            {
                result.Add(new CellViewIndexed(cells[i], i));                    
            }

            return result;
        }
    }
}*/