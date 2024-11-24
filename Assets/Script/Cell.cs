using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell Right;
    public Cell RightUp;
    public Cell RightDown;
    public Cell Left;
    public Cell LeftUp;
    public Cell LeftDown;

    public BLock Block;


    private void OnEnable()
    {
        GameManager.slide += OnSlide;
    }

    private void OnDisable()
    {
        GameManager.slide -= OnSlide;
    }

    private void OnSlide(string key_board)
    {
        Cell currentCell = this;

        switch (key_board)
        {
            case "W":
                if (LeftUp != null) return;
                SlideLeftUp(currentCell);
                break;
            case "E":
                if (RightUp != null) return;
                SlideRightUp(currentCell);
                break;
            case "A":
                if (Left != null) return;
                SlideLeft(currentCell);
                break;
            case "D":
                if (Right != null) return;
                SlideRight(currentCell);
                break;
            case "X":
                if (RightDown != null) return;
                SlideRightDown(currentCell);
                break;
            case "Z":
                if (LeftDown != null) return;
                SlideLeftDown(currentCell);
                break;
            default:
                break;
        }

        //GameManager.ticker++;
        //if (GameManager.ticker >= 5)
        //{

        //    GameManager.Instance.Spawn(GameManager.Instance.Cell);
        //}
    }

    private IEnumerator SlideAndSpawn(string key_board)
    {
        Cell currentCell = this;

        // Xử lý hành động quẹt
        switch (key_board)
        {
            case "W":
                if (LeftUp == null) SlideLeftUp(currentCell);
                break;
            case "E":
                if (RightUp == null) SlideRightUp(currentCell);
                break;
            case "A":
                if (Left == null) SlideLeft(currentCell);
                break;
            case "D":
                if (Right == null) SlideRight(currentCell);
                break;
            case "X":
                if (RightDown == null) SlideRightDown(currentCell);
                break;
            case "Z":
                if (LeftDown == null) SlideLeftDown(currentCell);
                break;
            default:
                yield break; // Không làm gì nếu không có phím hợp lệ
        }

        // Chờ xử lý trong khung hình hiện tại
        yield return null;

        // Tăng ticker và spawn nếu đủ điều kiện
       

    }

    //private void MoveRight(Cell currentCell)
    //{
    //    print(currentCell.name);
    //    // Kiểm tra nếu ô bên phải null
    //    if (currentCell.Right == null)
    //    {
    //        Cell next = currentCell.Left;
    //        bool isDone = false; // Thêm điều kiện thoát
    //        while (!isDone)
    //        {


    //            // Nếu không có ô bên cạnh
    //            if (next == null)
    //            {
    //                isDone = true;
    //                return;
    //            }
    //            // Nếu ô tiếp theo không có Block
    //            else if (next.Block == null)
    //            {
    //                next = next.Left;

    //            }
    //            else if (currentCell.Block == null)
    //            {
    //                next.Block.transform.parent = currentCell.transform;

    //                currentCell.Block = next.Block;
    //                next.Block = null;
    //                next = next.Left;
    //            }
    //            // Nếu 2 ô giống nhau
    //            else if (next.Block.currentLevel == currentCell.Block.currentLevel)
    //            {
    //                next.Block.transform.parent = currentCell.transform;

    //                currentCell.Block = next.Block;
    //                currentCell.Block.Double();
    //                next.Block = null;
    //                next = next.Left;


    //            }
    //            else if (next.Block.currentLevel != currentCell.Block.currentLevel)
    //            {
    //                currentCell = currentCell.Left;
    //            }
    //        }
    //    }
    //    return;
    //}


    private void SlideRight(Cell currentCell)
    {
        // Dừng nếu không còn ô bên trái
        if (currentCell.Left == null) return;

        // Nếu currentCell đã có block
        if (currentCell.Block != null)
        {
            // Tìm ô xa nhất có thể hợp nhất hoặc di chuyển
            Cell nextCell = currentCell.Left;
            while (nextCell.Left != null && nextCell.Block == null)
            {
                nextCell = nextCell.Left;
            }

            if (nextCell.Block != null) // Nếu có block trong ô cuối cùng
            {
                if (currentCell.Block.currentLevel == nextCell.Block.currentLevel) // Hợp nhất
                {
                    // Thực hiện hợp nhất
                    nextCell.Block.Double();
                    nextCell.Block.transform.parent = currentCell.transform;
                    currentCell.Block = nextCell.Block;
                    nextCell.Block = null;
                }
                else if (currentCell.Left.Block == null) // Di chuyển block nếu không hợp nhất được
                {
                    currentCell.Left.Block = nextCell.Block;
                    nextCell.Block.transform.parent = currentCell.Left.transform;
                    nextCell.Block = null;
                }
            }
        }
        else // Nếu currentCell chưa có block
        {
            Cell nextCell = currentCell.Left;
            while (nextCell.Left != null && nextCell.Block == null)
            {
                nextCell = nextCell.Left;
            }

            if (nextCell.Block != null) // Nếu tìm được block cần di chuyển
            {
                // Di chuyển block về currentCell
                currentCell.Block = nextCell.Block;
                nextCell.Block.transform.parent = currentCell.transform;
                nextCell.Block = null;

                // Gọi lại để xử lý trạng thái mới của currentCell
                SlideRight(currentCell);
            }
        }

        // Tiếp tục xử lý cho ô bên trái
        SlideRight(currentCell.Left);
    }



    private void SlideLeft(Cell currentCell)
    {
        if (currentCell.Right == null) return;

        if (currentCell.Block != null)
        {
            Cell nextCell = currentCell.Right;
            while (nextCell.Right != null && nextCell.Block == null)
            {
                nextCell = nextCell.Right;
            }

            if (nextCell.Block != null) 
            {
                if (currentCell.Block.currentLevel == nextCell.Block.currentLevel) 
                {
                    
                    nextCell.Block.Double();
                    nextCell.Block.transform.parent = currentCell.transform;
                    currentCell.Block = nextCell.Block;
                    nextCell.Block = null;
                }
                else if (currentCell.Right.Block == null) 
                {
                    currentCell.Right.Block = nextCell.Block;
                    nextCell.Block.transform.parent = currentCell.Right.transform;
                    nextCell.Block = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.Right;
            while (nextCell.Right != null && nextCell.Block == null)
            {
                nextCell = nextCell.Right;
            }

            if (nextCell.Block != null) 
            {
                
                currentCell.Block = nextCell.Block;
                nextCell.Block.transform.parent = currentCell.transform;
                nextCell.Block = null;
                SlideLeft(currentCell);
            }
        }
        SlideLeft(currentCell.Right);
    }

    private void SlideLeftUp(Cell currentCell)
    {
        if (currentCell.RightDown == null) return;

        if (currentCell.Block != null)
        {
            Cell nextCell = currentCell.RightDown;
            while (nextCell.RightDown != null && nextCell.Block == null)
            {
                nextCell = nextCell.RightDown;
            }

            if (nextCell.Block != null)
            {
                if (currentCell.Block.currentLevel == nextCell.Block.currentLevel)
                {

                    nextCell.Block.Double();
                    nextCell.Block.transform.parent = currentCell.transform;
                    currentCell.Block = nextCell.Block;
                    nextCell.Block = null;
                }
                else if (currentCell.RightDown.Block == null)
                {
                    currentCell.RightDown.Block = nextCell.Block;
                    nextCell.Block.transform.parent = currentCell.RightDown.transform;
                    nextCell.Block = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.RightDown;
            while (nextCell.RightDown != null && nextCell.Block == null)
            {
                nextCell = nextCell.RightDown;
            }

            if (nextCell.Block != null)
            {

                currentCell.Block = nextCell.Block;
                nextCell.Block.transform.parent = currentCell.transform;
                nextCell.Block = null;
                SlideLeftUp(currentCell);
            }
        }
        SlideLeftUp(currentCell.RightDown);
    }


    private void SlideLeftDown(Cell currentCell)
    {
        if (currentCell.RightUp == null) return;

        if (currentCell.Block != null)
        {
            Cell nextCell = currentCell.RightUp;
            while (nextCell.RightUp != null && nextCell.Block == null)
            {
                nextCell = nextCell.RightUp;
            }

            if (nextCell.Block != null) 
            {
                if (currentCell.Block.currentLevel == nextCell.Block.currentLevel) 
                {
                    
                    nextCell.Block.Double();
                    nextCell.Block.transform.parent = currentCell.transform;
                    currentCell.Block = nextCell.Block;
                    nextCell.Block = null;
                }
                else if (currentCell.RightUp.Block == null) 
                {
                    currentCell.RightUp.Block = nextCell.Block;
                    nextCell.Block.transform.parent = currentCell.RightUp.transform;
                    nextCell.Block = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.RightUp;
            while (nextCell.RightUp != null && nextCell.Block == null)
            {
                nextCell = nextCell.RightUp;
            }

            if (nextCell.Block != null) 
            {
                
                currentCell.Block = nextCell.Block;
                nextCell.Block.transform.parent = currentCell.transform;
                nextCell.Block = null;
                SlideLeftDown(currentCell);
            }
        }
        SlideLeftDown(currentCell.RightUp);
    }

    private void SlideRightUp(Cell currentCell)
    {
        if (currentCell.LeftDown == null) return;

        if (currentCell.Block != null)
        {
            Cell nextCell = currentCell.LeftDown;
            while (nextCell.LeftDown != null && nextCell.Block == null)
            {
                nextCell = nextCell.LeftDown;
            }

            if (nextCell.Block != null)
            {
                if (currentCell.Block.currentLevel == nextCell.Block.currentLevel)
                {

                    nextCell.Block.Double();
                    nextCell.Block.transform.parent = currentCell.transform;
                    currentCell.Block = nextCell.Block;
                    nextCell.Block = null;
                }
                else if (currentCell.LeftDown.Block == null)
                {
                    currentCell.LeftDown.Block = nextCell.Block;
                    nextCell.Block.transform.parent = currentCell.LeftDown.transform;
                    nextCell.Block = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.LeftDown;
            while (nextCell.LeftDown != null && nextCell.Block == null)
            {
                nextCell = nextCell.LeftDown;
            }

            if (nextCell.Block != null)
            {

                currentCell.Block = nextCell.Block;
                nextCell.Block.transform.parent = currentCell.transform;
                nextCell.Block = null;
                SlideRightUp(currentCell);
            }
        }
        SlideRightUp(currentCell.LeftDown);
    }

    private void SlideRightDown(Cell currentCell)
    {
        if (currentCell.LeftUp == null) return;

        if (currentCell.Block != null)
        {
            Cell nextCell = currentCell.LeftUp;
            while (nextCell.LeftUp != null && nextCell.Block == null)
            {
                nextCell = nextCell.LeftUp;
            }

            if (nextCell.Block != null)
            {
                if (currentCell.Block.currentLevel == nextCell.Block.currentLevel)
                {

                    nextCell.Block.Double();
                    nextCell.Block.transform.parent = currentCell.transform;
                    currentCell.Block = nextCell.Block;
                    nextCell.Block = null;
                }
                else if (currentCell.LeftUp.Block == null)
                {
                    currentCell.LeftUp.Block = nextCell.Block;
                    nextCell.Block.transform.parent = currentCell.LeftUp.transform;
                    nextCell.Block = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.LeftUp;
            while (nextCell.LeftUp != null && nextCell.Block == null)
            {
                nextCell = nextCell.LeftUp;
            }

            if (nextCell.Block != null)
            {

                currentCell.Block = nextCell.Block;
                nextCell.Block.transform.parent = currentCell.transform;
                nextCell.Block = null;
                SlideRightDown(currentCell);
            }
        }
        SlideRightDown(currentCell.LeftUp);
    }
}
