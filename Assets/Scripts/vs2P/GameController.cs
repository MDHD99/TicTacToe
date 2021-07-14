using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int turn;
    public GameObject x, o;
    public GameObject XL, OL;
    private char[,] miniGrid1;
    private char[,] miniGrid2;
    private char[,] miniGrid3;
    private char[,] miniGrid4;
    private char[,] miniGrid5;
    private char[,] miniGrid6;
    private char[,] miniGrid7;
    private char[,] miniGrid8;
    private char[,] miniGrid9;
    private char[,] grid;
    private ArrayList finishedGrids = new ArrayList();
    private string activeBoard = "null";
    private string GameState = "Ongoing";
    public Material ActiveMat;
    public Material InactiveMat;


    private void Start()
    {
        turn = 0;
        miniGrid1 = new char[4, 4];
        miniGrid2 = new char[4, 4];
        miniGrid3 = new char[4, 4];
        miniGrid4 = new char[4, 4];
        miniGrid5 = new char[4, 4];
        miniGrid6 = new char[4, 4];
        miniGrid7 = new char[4, 4];
        miniGrid8 = new char[4, 4];
        miniGrid9 = new char[4, 4];
        grid = new char[4, 4];
    }

    private bool CheckMiniRow(int row, char[,] miniGrid)
    {
        if (miniGrid[row, 1] == miniGrid[row, 2] && miniGrid[row, 2] == miniGrid[row, 3] && (miniGrid[row, 3] == 'x' || miniGrid[row, 3] == 'o'))
            return true;

        return false;
    }

    private bool CheckRow(int row)
    {
        if (grid[row, 1] == grid[row, 2] && grid[row, 2] == grid[row, 3] && (grid[row, 3] == 'x' || grid[row, 3] == 'o'))
            return true;

        return false;
    }

    private bool CheckMiniColumn(int column, char[,] miniGrid)
    {
        if (miniGrid[1, column] == miniGrid[2, column] && miniGrid[2, column] == miniGrid[3, column] && (miniGrid[3, column] == 'x' || miniGrid[3, column] == 'o'))
            return true;

        return false;
    }

    private bool CheckColumn(int column)
    {
        if (grid[1, column] == grid[2, column] && grid[2, column] == grid[3, column] && (grid[3, column] == 'x' || grid[3, column] == 'o'))
            return true;

        return false;
    }

    private bool CheckMiniDiagonals(char[,] miniGrid)
    {
        if (miniGrid[1, 1] == miniGrid[2, 2] && miniGrid[2, 2] == miniGrid[3, 3] && (miniGrid[3, 3] == 'x' || miniGrid[3, 3] == 'o'))
            return true;

        if (miniGrid[1, 3] == miniGrid[2, 2] && miniGrid[2, 2] == miniGrid[3, 1] && (miniGrid[3, 1] == 'x' || miniGrid[3, 1] == 'o'))
            return true;

        return false;
    }

    private bool CheckDiagonals()
    {
        if (grid[1, 1] == grid[2, 2] && grid[2, 2] == grid[3, 3] && (grid[3, 3] == 'x' || grid[3, 3] == 'o'))
            return true;

        if (grid[1, 3] == grid[2, 2] && grid[2, 2] == grid[3, 1] && (grid[3, 1] == 'x' || grid[3, 1] == 'o'))
            return true;

        return false;
    }

    private bool CheckMiniWin(int row, int column, char[,] miniGrid)
    {
        print("Acitve board is " + activeBoard);
        if (CheckMiniRow(row, miniGrid) || CheckMiniColumn(column, miniGrid) || CheckMiniDiagonals(miniGrid))
        {
            finishedGrids.Add(activeBoard);
            if (turn == 0)
            {
                grid[int.Parse(activeBoard[0].ToString()), int.Parse(activeBoard[1].ToString())] = 'x';
                if (CheckWin(int.Parse(activeBoard[0].ToString()), int.Parse(activeBoard[1].ToString())))
                {
                    print("x wins whole game");
                    GameState = "Ended";
                }

                return true;
            }
            else
            {
                grid[int.Parse(activeBoard[0].ToString()), int.Parse(activeBoard[1].ToString())] = 'o';
                if (CheckWin(int.Parse(activeBoard[0].ToString()), int.Parse(activeBoard[1].ToString())))
                {
                    print("o wins whole game");
                    GameState = "Ended";
                }
                return true;
            }

        }

        else
            return false;
    }

    private bool CheckWin(int row, int column)
    {
        if (CheckRow(row) || CheckColumn(column) || CheckDiagonals())
        {
            return true;
        }

        else
            return false;
    }

    private void SetActiveColor(string miniGrid)
    {
        GameObject go = GameObject.Find("/"+miniGrid);
        Transform borders = go.transform.Find("Borders");
        foreach(Transform child in borders)
        {
            child.GetComponent<MeshRenderer>().material = ActiveMat;
        }

    }

    private void ResetActiveColor(string miniGrid)
    {
        GameObject go = GameObject.Find("/" + miniGrid);
        Transform borders = go.transform.Find("Borders");
        foreach (Transform child in borders)
        {
            child.GetComponent<MeshRenderer>().material = InactiveMat;
        }

    }

    public void PlayTurn(GameObject gridCell, int[] gridPosition, string miniBoard)
    {
        if (GameState == "Ongoing")
        {
            if (activeBoard == "null" && !(finishedGrids.Contains(miniBoard)))
            {
                activeBoard = miniBoard;
                print("Game Started");
                Draw(gridCell, gridPosition, miniBoard);
                string ab = "" + gridPosition[0] + gridPosition[1];
                if (finishedGrids.Contains(ab))
                {
                    activeBoard = "null";
                }
                else
                {
                    activeBoard = ab;
                    ResetActiveColor(miniBoard);
                    SetActiveColor(activeBoard);
                }
                
                
            }

            else if (miniBoard == activeBoard)
            {
                Draw(gridCell, gridPosition, miniBoard);
                string ab = "" + gridPosition[0] + gridPosition[1];
                if (finishedGrids.Contains(ab))
                {
                    activeBoard = "null";
                }
                else
                {
                    activeBoard = ab;
                    ResetActiveColor(miniBoard);
                    SetActiveColor(activeBoard);
                }

                
            }

            else
                print("Invalid Move");
        }

        else
            print("Game Ended");

    }

    private char[,] GetMiniGrid(string miniBoard)
    {
        switch (miniBoard)
        {
            case ("11"):
                return miniGrid1;
            case ("12"):
                return miniGrid2;
            case ("13"):
                return miniGrid3;
            case ("21"):
                return miniGrid4;
            case ("22"):
                return miniGrid5;
            case ("23"):
                return miniGrid6;
            case ("31"):
                return miniGrid7;
            case ("32"):
                return miniGrid8;
            case ("33"):
                return miniGrid9;
            default:
                return miniGrid1;
        }
    }



    private void Draw(GameObject gridCell, int[] gridPosition, string miniBoard)
    {
        char[,] miniGrid = GetMiniGrid(miniBoard);
        if (turn == 0)
        {
            Instantiate(x, gridCell.transform.position, Quaternion.identity);
            int column = gridPosition[1];
            int row = gridPosition[0];

            miniGrid[row, column] = 'x';
            if (CheckMiniWin(row, column, miniGrid))
            {
                print("x wins");
                Vector3 go = GameObject.Find("/" + miniBoard).transform.position;
                Instantiate(XL, new Vector3(go.x,go.y,1), Quaternion.identity);
            }
                
        }

        else
        {
            Instantiate(o, gridCell.transform.position, o.transform.rotation);
            int column = gridPosition[1];
            int row = gridPosition[0];

            miniGrid[row, column] = 'o';
            if (CheckMiniWin(row, column, miniGrid))
            {
                print("o wins");
                Vector3 go = GameObject.Find("/" + miniBoard).transform.position;
                Instantiate(OL, new Vector3(go.x, go.y, 1), OL.transform.rotation);
            }
                

        }
        Destroy(gridCell);

        turn = (turn + 1) % 2;
    }
}
