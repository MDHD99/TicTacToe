using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameController : MonoBehaviour
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
    //private char[,] testBoard;
    private char[,] grid;
    private ArrayList finishedGrids = new ArrayList();
    private ArrayList unfinishedGrids = new ArrayList();
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
        //testBoard = new char[4, 4];
        grid = new char[4, 4];
        unfinishedGrids.Add(miniGrid1);
        unfinishedGrids.Add(miniGrid2);
        unfinishedGrids.Add(miniGrid3);
        unfinishedGrids.Add(miniGrid4);
        unfinishedGrids.Add(miniGrid5);
        unfinishedGrids.Add(miniGrid6);
        unfinishedGrids.Add(miniGrid7);
        unfinishedGrids.Add(miniGrid8);
        unfinishedGrids.Add(miniGrid9);
        //testBoard[1, 1] = 'o';
        //testBoard[1, 3] = 'x';
        //testBoard[3, 1] = 'x';
        //testBoard[3, 3] = 'o';

        //FindBestMove(testBoard);
        PlayAITurn();
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

        if (CheckMiniRow(row, miniGrid) || CheckMiniColumn(column, miniGrid) || CheckMiniDiagonals(miniGrid))
        {
            unfinishedGrids.Remove(miniGrid);
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
            if (turn == 1)
            {
                if (activeBoard == "null" && !(finishedGrids.Contains(miniBoard)))
                {
                    activeBoard = miniBoard;
                    Draw(gridCell, gridPosition, miniBoard);
                    string ab = "" + gridPosition[0] + gridPosition[1];
                    if (finishedGrids.Contains(ab))
                    {
                        ResetActiveColor(miniBoard);
                        activeBoard = "null";
                    }
                    else
                    {
                        activeBoard = ab;
                        ResetActiveColor(miniBoard);
                        if (GameState == "Ongoing")
                            SetActiveColor(activeBoard);
                    }


                }

                else if (miniBoard == activeBoard)
                {
                    Draw(gridCell, gridPosition, miniBoard);
                    string ab = "" + gridPosition[0] + gridPosition[1];
                    if (finishedGrids.Contains(ab))
                    {
                        ResetActiveColor(miniBoard);
                        activeBoard = "null";
                    }
                    else
                    {
                        activeBoard = ab;
                        ResetActiveColor(miniBoard);
                        if (GameState == "Ongoing")
                            SetActiveColor(activeBoard);
                    }

                   
                }

                else
                    print("Invalid Move");
                if(GameState == "Ongoing")
                    PlayAITurn();
            }

            else
                print("Not your turn");
        }

        else
            print("Game Ended");

    }

    ////For AI
    ////Get random avaiable grid position from board
    //private int[] GetAvailableGridPosition(char[,] gToPlay)
    //{
    //    ArrayList allAvPositions = new ArrayList();
    //    for (int i = 1; i <= 3; i++)
    //    {
    //        for (int j = 1; j <= 3; j++)
    //        {
    //            if (gToPlay[i, j] == '\0')
    //            {
    //                int[] gridPosition = { i, j };
    //                allAvPositions.Add(gridPosition);
    //            }
    //        }
    //    }
    //    float ct = (float)allAvPositions.Count;
    //    int cellToChoose = (int)Random.Range(0f, ct);
    //    return (int[])allAvPositions[cellToChoose];
    //}

    //For AI
    //Get tranfrom of avaiable cell game object
    private GameObject GetCellGameObject(string miniBoard, int[] smallCell)
    {
        string smallCellName = "" + smallCell[0] + smallCell[1];
        GameObject go = GameObject.Find("/" + miniBoard+"/"+smallCellName);
        return go;
    }
    private void PlayAITurn()
    {
        if(activeBoard == "null")
        {
            float unfinishedGridCount = unfinishedGrids.Count;
            int gridToPlay = (int) Random.Range(0f, unfinishedGridCount);
            char[,] gToPlay = (char[,]) unfinishedGrids[gridToPlay];
            int[] move = FindBestMove(gToPlay);
            string miniBoard = GetNameFromMiniGrid(gToPlay);
            GameObject cell = GetCellGameObject(miniBoard, move);
            print("Bot plays grid[" + miniBoard + "] in cell[" + move[0]+move[1] + "]");
            ResetActiveColor(miniBoard);
            
            Draw(cell, FindBestMove(gToPlay), miniBoard);
            activeBoard = miniBoard;
            string ab = "" + move[0] + move[1];
            if (finishedGrids.Contains(ab))
            {
                ResetActiveColor(miniBoard);
                activeBoard = "null";
            }
            else
            {
                activeBoard = ab;
                ResetActiveColor(miniBoard);
                if (GameState == "Ongoing")
                    SetActiveColor(activeBoard);
            }
        }

        else
        {
            int[] move = FindBestMove(GetMiniGridFromName(activeBoard));
            GameObject cell = GetCellGameObject(activeBoard, move);
            
            Draw(cell, move , activeBoard);
            print("Bot plays grid[" + activeBoard + "] in cell[" + move[0] + move[1] + "]");
            ResetActiveColor(activeBoard);
            string ab = "" + move[0] + move[1];
            if (finishedGrids.Contains(ab))
            {
                activeBoard = "null";
            }
            else
            {
                ResetActiveColor(activeBoard);
                activeBoard = ab;
                if (GameState == "Ongoing")
                    SetActiveColor(activeBoard);
            }
        }
    }

    private char[,] GetMiniGridFromName(string miniBoard)
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

    private string GetNameFromMiniGrid(char[,] mg)
    {
        if (mg == miniGrid1) return "11";
        else if (mg == miniGrid2) return "12";
        else if (mg == miniGrid3) return "13";
        else if (mg == miniGrid4) return "21";
        else if (mg == miniGrid5) return "22";
        else if (mg == miniGrid6) return "23";
        else if (mg == miniGrid7) return "31";
        else if (mg == miniGrid8) return "32";
        else  return "33";
    }

    private void Draw(GameObject gridCell, int[] gridPosition, string miniBoard)
    {
        char[,] miniGrid = GetMiniGridFromName(miniBoard);
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


    private int[] FindBestMove(char[,] board)
    {
        int bestVal = -1000;
        int[] bestMove = new int[2];
        bestMove[0] = -1;
        bestMove[1] = -1;
        
        // Traverse all cells, evaluate minimax function
        // for all empty cells. And return the cell
        // with optimal value.
        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                // Check if cell is empty
                if (board[i, j] == '\0')
                {
                    // Make the move
                    board[i, j] = 'x';

                    // compute evaluation function for this
                    // move.
                    int moveVal = MiniMax(board, 0, false);
                    print(moveVal);
                    // Undo the move
                    board[i, j] = '\0';

                    // If the value of the current move is
                    // more than the best value, then update
                    // best/
                    if (moveVal > bestVal)
                    {
                        bestMove[0] = i;
                        bestMove[1] = j;
                        bestVal = moveVal;

                    }
                }
            }
        }
        print("Best move is"+ bestMove[0] + bestMove[1]+" with value of "+ bestVal );

        return bestMove;
    }

        // This function returns true if there are moves
        // remaining on the board. It returns false if
        // there are no moves left to play.
        static bool isMovesLeft(char[,] board)
    {
        for (int i = 1; i <= 3; i++)
            for (int j = 1; j <= 3; j++)
                if (board[i, j] == '\0')
                    return true;
        return false;
    }

    private int MiniMax(char[,] board, int depth, bool isMax)
    {
        int score = EvaluateBoard(board);

        // If Maximizer has won the game
        // return his/her evaluated score
        if (score == 10)
            return score - depth;

        // If Minimizer has won the game
        // return his/her evaluated score
        if (score == -10)
            return score + depth;

        // If there are no more moves and
        // no winner then it is a tie
        if (isMovesLeft(board) == false)
            return 0;

        // If this maximizer's move
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == '\0')
                    {
                        // Make the move
                        board[i, j] = 'x';

                        // Call minimax recursively and choose
                        // the maximum value
                        best = Mathf.Max(best, MiniMax(board,
                                        depth + 1, !isMax));

                        // Undo the move
                        board[i, j] = '\0';
                    }
                }
            }
            return best;
        }

        // If this minimizer's move
        else
        {
            int best = 1000;

            // Traverse all cells
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == '\0')
                    {
                        // Make the move
                        board[i, j] = 'o';

                        // Call minimax recursively and choose
                        // the minimum value
                        best = Mathf.Min(best, MiniMax(board,
                                        depth + 1, !isMax));

                        // Undo the move
                        board[i, j] = '\0';
                    }
                }
            }
            return best;
        }
    }

    static int EvaluateBoard(char[,] b)
    {
        // Checking for Rows for X or O victory.
        for (int row = 1; row <= 3; row++)
        {
            if (b[row, 1] == b[row, 2] &&
                b[row, 2] == b[row, 3])
            {
                if (b[row, 1] == 'x')
                    return +10;
                else if (b[row, 1] == 'o')
                    return -10;
            }
        }

        // Checking for Columns for X or O victory.
        for (int col = 1; col <= 3; col++)
        {
            if (b[1, col] == b[2, col] &&
                b[2, col] == b[3, col])
            {
                if (b[1, col] == 'x')
                    return +10;

                else if (b[1, col] == 'o')
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory.
        if (b[1, 1] == b[2, 2] && b[2, 2] == b[3, 3])
        {
            if (b[1, 1] == 'x')
                return +10;
            else if (b[1, 1] == 'o')
                return -10;
        }

        if (b[1, 3] == b[2, 2] && b[2, 2] == b[3, 1])
        {
            if (b[1, 3] == 'x')
                return +10;
            else if (b[1, 3] == 'o')
                return -10;
        }

        // Else if none of them have won then return 0
        return 0;
    }
}
