using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Chess_proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            string WhiteStalemate = "a2a4c7c5d2d4d7d6d1d2e7e5d2f4e5e4h2h3f8e7f4h2e7h4a1a3c8e6a3g3e6b3b1d2d8a5d4d5e4e3c2c4f7f5f2f3f5f4";
            string BlackStalemate = "e2e3a7a5d1h5a8a6h5a5h7h5h2h4a6h6a5c7f7f6c7d7e8f7d7b7d8d3b7b8d3h7b8c8f7g6c8e6";
            string castlechecker = "f2f3d7d5a2a4d8d6g2g4a7a6g4g5a6a5g5g6d6g6f3f4b7b6f1h3h7h6g1f3g6g5b2b3b6b5f4f5b5b4f3e5";
            string foolsmate = "e2e4e7e5f1c4b8c6d1h5g8f6h5f7 ";
            string carlsen = "e2e4e7e5g1f3b8c6f1b5a7a6b5a4b7b5a4b3c6d4f3d4e5d4e1g1c8b7d1f3f8c5f3f7 ";
            string kasparov = "e2e4e7e5g1f3b8c6d2d4e5d4f3d4g8f6d4c6b7c6e4e5d8e7d1e2f6d5c2c4e7b4b1d2d5f4e2e3f4g6f1d3f8c5e3g3e8g8e1g1d7d6d2b3g6e5a2a3b4b6b3c5b6c5c1e3c5a5b2b4a5a4e3d4f7f6d4e5f6e5f2f4c8f5f4e5f5d3g3d3d6e5d3d7a4b3d7c6b3e3g1h1g8h8f1e1e3c3c6c7a8c8c7a7c8c4h2h3c4f4a7c5c3b2c5e5b2b3e5e3b3c4a1c1c4f7e3g3h7h6b4b5f7d5a3a4f4a4c1b1f8f5b5b6f5g5b6b7d5b7g3g5";
            */
            ChessGame game1 = new ChessGame();
            game1.Play();
            
            //////////////
        }
    }
    class ChessPart
        {
        protected int row;
        protected int col;
        string name;
        bool isWhite;
        public bool moved = false;
        public ChessPart() { }
        public ChessPart(int row, int col, bool isWhite, string name)
            {
                this.row = row;
                this.col = col;
                this.isWhite = isWhite;
                this.name = name;
            }
        public string Print()
            {
                return this.name + " ";
            }
        public void ChangeCoordinates(int newRow, int newCol)
            {
                this.row = newRow;
                this.col = newCol;
            }
        public void GetLoc()
            {
                Console.WriteLine("" + row + " " + col);
            }
        public bool Iswhite()
         {
          return this.isWhite;
         }
        public string GetType()
            {
                return this.name;
            }
        public int GetRow()
            {
                return this.row;
            }
        public int GetCol()
            {
                return this.col;
            }
        public bool SetMoved()
        {
            this.moved = true;
            return true;
        }
        public bool GetMoved()
        {
            return moved;
        }
        public virtual int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {
                return 0;
            }
        }
    class Pawn : ChessPart
        {
            public int personalCounter;
            public bool twoJumpsHappened = false;
            public Pawn(int row, int col, bool isWhite, string name, bool firstTurn) : base(row, col, isWhite, name)
            {
            }
            public bool SetPersonalCounter(int globalCounter)
            {
                this.personalCounter = globalCounter;
                return true;
            }
            public int GetPersonalCounter()
            {
                return personalCounter;
            }
            public override int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {
                if (iswhite)
                {
                    //move 2 steps
                    if (destCol == col && row - destRow == 2 && board[destRow, destCol] == null && board[destRow + 1, destCol] == null && !this.moved)
                    {
                        this.SetPersonalCounter(globalCounter);
                        return 1;
                    }
                    //move 1 step forward
                    if (destCol == col && row - destRow == 1 && board[destRow, destCol] == null)
                        return 1;
                    if ((col - destCol == 1 || destCol - col == 1) && row - destRow == 1 && board[destRow, destCol] != null && (board[destRow, destCol].Iswhite() == false))
                        return 1;
                    if ((col - destCol == 1 || destCol - col == 1) && row - destRow == 1
                        && board[destRow, destCol] == null && (board[row, destCol] is Pawn) && !board[row,destCol].Iswhite())
                    {
                        Pawn pawn1 = (Pawn)board[row, destCol];
                        if (pawn1.GetPersonalCounter() + 1 == globalCounter)
                            return 11;
                    }
                }
                else
                {
                    if (destCol == col && destRow - row == 2 && !this.moved && board[destRow, destCol] == null && board[destRow - 1, destCol] == null)
                    {
                        this.SetPersonalCounter(globalCounter);
                        return 2;
                    }
                    if (destCol == col && destRow - row == 1 && board[destRow, destCol] == null)
                        return 2;
                    if ((col - destCol == 1 || destCol - col == 1) && destRow - row == 1 && board[destRow, destCol] != null && (board[destRow, destCol].Iswhite() == true))
                        return 2;
                    if ((col - destCol == 1 || destCol - col == 1) && destRow - row == 1
                            && board[destRow, destCol] == null && (board[row, destCol] is Pawn) && board[row, destCol].Iswhite())
                    {
                        Pawn pawn1 = (Pawn)board[row, destCol];
                        if (pawn1.GetPersonalCounter() + 1 == globalCounter)
                            return 22;
                    }
                }
                return 0;

            }
            public void Promotion(int destRow, int destCol, bool iswhite, ChessPart[,] board)
            {
                if ((iswhite && board[destRow, destCol] == board[0, destCol]) || (!iswhite && board[destRow, destCol] == board[7, destCol]))
                {
                    string ColorLetter = "b";
                    if (iswhite)
                        ColorLetter = "w";
                    bool badinput = true;
                    string ans = "0";
                    do
                    {
                        Console.WriteLine("----PROMOTION-----");
                        Console.WriteLine("convert from pawn to Q,R,B or N:");
                        string input;
                        input = Console.ReadLine();
                        input = input.Trim();
                        switch (input)
                        {
                            case "q":
                            case "Q":
                                board[destRow, destCol] = new Queen(destRow, destCol, iswhite, "Q" + ColorLetter);   
                                badinput = false;
                                break;
                            case "r":
                            case "R":
                                board[destRow, destCol] = new Rook(destRow, destCol, iswhite, "R" + ColorLetter);
                                badinput = false;
                                break;
                            case "b":
                            case "B":
                                board[destRow, destCol] = new Bishop(destRow, destCol, iswhite, "B" + ColorLetter);
                                badinput = false;
                                break;
                            case "n":
                            case "N":
                                board[destRow, destCol] = new Knight(destRow, destCol, iswhite, "N" + ColorLetter);
                                badinput = false;
                                break;
                            default:
                                badinput = true;
                                break;
                        }
                    } while (badinput);
                }
            }
        }
    class Rook : ChessPart
        {
        public Rook() { }
        public Rook(int row, int col, bool isWhite, string name) : base(row, col, isWhite, name){ }
        public override int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {
                int rowGap = row - destRow;
                int colGap = col - destCol;
                if ((row != destRow && col == destCol) || (row == destRow && col != destCol))
                {
                    //checking if there is something in between the locations
                    if (row != destRow && col == destCol)
                    {
                        if (rowGap < 0)
                        {
                            for (int i = row + 1; i < destRow; i++)
                            {
                                if (board[i, destCol] != null)
                                    return 0;
                            }
                        }
                        if (rowGap > 0)
                        {
                            for (int i = row - 1; i > destRow; i--)
                            {
                                if (board[i, destCol] != null)
                                    return 0;
                            }
                        }

                    }
                    if (row == destRow && col != destCol)
                    {
                        if (colGap < 0)
                        {
                            for (int j = col + 1; j < destCol; j++)
                            {
                                if (board[destRow, j] != null)
                                    return 0;
                            }
                        }
                        if (colGap > 0)
                        {
                            for (int j = col - 1; j > destCol; j--)
                            {
                                if (board[destRow, j] != null)
                                    return 0;
                            }
                        }
                    }
                    // checking if the location is empty (after the way to it is confiremd empty) 
                    if (board[destRow, destCol] == null)
                        return 1;
                    //checking if a location is of the same color
                    if (board[destRow, destCol].GetType()[1] == 'w' && iswhite || board[destRow, destCol].GetType()[1] == 'b' && (!iswhite))
                        return 0;
                    else
                        return 1;
                }
                return 0;
            }
        }
    class Bishop : ChessPart
        {
        public Bishop() { }
        public Bishop(int row, int col, bool isWhite, string name) : base(row, col, isWhite, name) {}
        public override int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {   
                int rowGap = row - destRow;
                int colGap = col - destCol;
                int absRowGap, absColGap;
                if (rowGap < 0)
                    absRowGap = -1 * rowGap;
                else
                    absRowGap = rowGap;
                if (colGap < 0)
                    absColGap = -1 * colGap;
                else
                    absColGap = colGap; 
                //check if there is something between the locations
                if (row != destRow && col != destCol && absColGap == absRowGap)
                {
                    if (rowGap > 0 && colGap > 0)
                    {
                        for (int i = row - 1, j = col - 1; i > destRow; i--, j--)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    if (rowGap > 0 && colGap < 0)
                    {
                        for (int i = row - 1, j = col + 1; i > destRow; i--, j++)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    if (rowGap < 0 && colGap > 0)
                    {
                        for (int i = row + 1, j = col - 1; i < destRow; i++, j--)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    if (rowGap < 0 && colGap < 0)
                    {
                        for (int i = row + 1, j = col + 1; i < destRow; i++, j++)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    //check if location is empty
                    if (board[destRow, destCol] == null)
                        return 1;

                    if (board[destRow, destCol].GetType()[1] == 'w' && iswhite || board[destRow, destCol].GetType()[1] == 'b' && (!iswhite))
                        return 0;
                    else
                        return 1;
                }
                return 0;
            }
        }
    class Knight : ChessPart
        {
        public Knight() { }
        public Knight(int row, int col, bool isWhite, string name) : base(row, col, isWhite, name){}
        public override int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {
                int rowGap = row - destRow;
                int colGap = col - destCol;
                int absRowGap, absColGap;
                if (rowGap < 0)
                    absRowGap = -1 * rowGap;
                else
                    absRowGap = rowGap;
                if (colGap < 0)
                    absColGap = -1 * colGap;
                else
                    absColGap = colGap;
                if (absRowGap == 2 && absColGap == 1 || absRowGap == 1 && absColGap == 2)
                {
                    if (board[destRow, destCol] == null)
                        return 1;
                    if (board[destRow, destCol].GetType()[1] == 'w' && iswhite || board[destRow, destCol].GetType()[1] == 'b' && (!iswhite))
                        return 0;
                    else
                        return 1;
                }
                return 0;
            }
        }
    class Queen : ChessPart
        {
            public Queen(int row, int col, bool isWhite, string name) : base(row, col, isWhite, name) { }
            public override int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {
                int rowGap = row - destRow;
                int colGap = col - destCol;
                int absRowGap, absColGap;
                if (rowGap < 0)
                    absRowGap = -1 * rowGap;
                else
                    absRowGap = rowGap;
                if (colGap < 0)
                    absColGap = -1 * colGap;
                else
                    absColGap = colGap;
                //check if there is something between the locations - moving like bishop
                if (row != destRow && col != destCol && absColGap == absRowGap)
                {
                    if (rowGap > 0 && colGap > 0)
                    {
                        for (int i = row - 1, j = col - 1; i > destRow; i--, j--)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    if (rowGap > 0 && colGap < 0)
                    {
                        for (int i = row - 1, j = col + 1; i > destRow; i--, j++)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    if (rowGap < 0 && colGap > 0)
                    {
                        for (int i = row + 1, j = col - 1; i < destRow; i++, j--)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                    if (rowGap < 0 && colGap < 0)
                    {
                        for (int i = row + 1, j = col + 1; i < destRow; i++, j++)
                        {
                            if (board[i, j] != null)
                                return 0;
                        }
                    }
                }
                //checking if there is something in between the locations - moving like rook
                if ((row != destRow && col == destCol) || (row == destRow && col != destCol))
                {
                    if (row != destRow && col == destCol)
                    {
                        if (rowGap < 0)
                        {
                            for (int i = row + 1; i < destRow; i++)
                            {
                                if (board[i, destCol] != null)
                                    return 0;
                            }
                        }
                        if (rowGap > 0)
                        {
                            for (int i = row - 1; i > destRow; i--)
                            {
                                if (board[i, destCol] != null)
                                    return 0;
                            }
                        }
                    }
                    if (row == destRow && col != destCol)
                    {
                        if (colGap < 0)
                        {
                            for (int j = col + 1; j < destCol; j++)
                            {
                                if (board[destRow, j] != null)
                                    return 0;
                            }
                        }
                        if (colGap > 0)
                        {
                            for (int j = col - 1; j > destCol; j--)
                            {
                                if (board[destRow, j] != null)
                                    return 0;
                            }
                        }
                    }
                }
                if (absRowGap == 0 && absColGap == 1 || absColGap == 0 && absRowGap == 1 || absRowGap == absColGap && absRowGap == 1)
                {
                    if (board[destRow, destCol] == null)
                        return 1;
                    //checking if a location is of the same color
                    if (board[destRow, destCol].GetType()[1] == 'w' && iswhite || board[destRow, destCol].GetType()[1] == 'b' && (!iswhite))
                        return 0;
                    else
                        return 1;
                }
                if (absRowGap >= 1 && absColGap > absRowGap || absColGap >= 1 && absRowGap > absColGap)
                {
                    return 0;
                }
                if (board[destRow, destCol] == null)
                    return 1;
                //checking if a location is of the same color
                if (board[destRow, destCol].GetType()[1] == 'w' && iswhite || board[destRow, destCol].GetType()[1] == 'b' && (!iswhite))
                    return 0;
                else
                    return 1;
            }
        }
    class King : ChessPart
        {
            public bool isIncheck;
            public bool WasinCheck;
            public bool casteled = false;
        public King(int row, int col, bool isWhite, string name) : base(row, col, isWhite, name)
        {
                this.isIncheck = false;
                this.WasinCheck = false;
                this.moved = false;
        }
        public bool setInCheck()
            {
                this.isIncheck = true;
                this.WasinCheck = true;
                return true;
            }
        public bool wasInCheck()
            {
                return WasinCheck;
            }
        public bool NotInCheck()
            {
                this.isIncheck = false;
                return true;
            }
        public bool inCheck()
            {
                return isIncheck;
            }
        public void reverseCheck()
            {
                this.isIncheck = !isIncheck;
            }
        public void reverseMoved()
            {
                this.moved = !moved;
            }
        public void setCasteled()
        {
            this.casteled = true;
        }
        public bool Casteled()
        {
            return this.casteled;
        }
        public override int CheckPossibleMove(int destRow, int destCol, bool iswhite, ChessPart[,] board, int globalCounter)
            {
                int rowGap = row - destRow;
                int colGap = col - destCol;
                int absRowGap, absColGap;
                if (rowGap < 0)
                    absRowGap = -1 * rowGap;
                else
                    absRowGap = rowGap;
                if (colGap < 0)
                    absColGap = -1 * colGap;
                else
                    absColGap = colGap;

                if (absRowGap == 0 && absColGap == 1 || absRowGap == 1 && absColGap == 0 || absColGap == absRowGap && absColGap == 1)
                {
                    if (board[destRow, destCol] == null)
                    {
                        SetMoved();
                        return 1;
                    }
                    if (board[destRow, destCol].GetType()[1] == 'w' && iswhite || board[destRow, destCol].GetType()[1] == 'b' && (!iswhite))
                    {
                        return 0;
                    }
                    else
                    {
                        SetMoved();
                        return 1;
                    }
                }
                if ((inCheck() == false && GetMoved() == false) && absColGap > 1 && (((destRow == 7 && destCol == 1 && board[7, 0] != null && board[7, 0].Iswhite() && board[7, 1] == null && board[7, 2] == null && board[7, 3] == null)) && row == 7 || ((destRow == 7 && destCol == 6 && board[7, 7] != null && board[7, 7].Iswhite()) && board[7, 6] == null && board[7, 5] == null) && row == 7 || row == 0 && ((destRow == 0 && destCol == 1 && board[0, 0] != null && !board[0, 0].Iswhite()) && board[0, 1] == null && board[0, 2] == null && board[0, 3] == null) || row == 0 && ((destRow == 0 && destCol == 6 && board[0, 7] != null && !board[0, 7].Iswhite() && board[0, 5] == null && board[0, 6] == null))))
                    return 2;
                return 0;
            }
        }
    class ChessGame
    {
        int turnCount = 1;
        bool isWhiteTurn;
        bool gameRunning = true;
        int lastTimePawnMoved;
        int lastTurnPieceWasTaken;
        int inputCounter = 0;
        string snapShots;
        ChessPart[,] board; 
        King Kw, Kb;
        public ChessGame()
        {
            Init();
            this.lastTimePawnMoved = 0;
            this.lastTurnPieceWasTaken = 0;
        }
        public void Init()
        {
            board = new ChessPart[8, 8];
            Kw = new King(7, 4, true, "Kw");
            Kb = new King(0, 4, false, "Kb");
            board[7, 4] = Kw;
            board[0,4] = Kb;
            board[0, 0] = new Rook(0, 0, false, "Rb");
            board[0, 7] = new Rook(0, 7, false, "Rb");
            board[7, 0] = new Rook(7, 0, true, "Rw");
            board[7, 7] = new Rook(7, 7, true, "Rw");
            board[0, 1] = new Knight(0, 1, false, "Nb");
            board[0, 6] = new Knight(0, 6, false, "Nb");
            board[7, 1] = new Knight(7, 1, true, "Nw");
            board[7, 6] = new Knight(7, 6, true, "Nw");
            board[0, 2] = new Bishop(0, 2, false, "Bb");
            board[0, 5] = new Bishop(0, 5, false, "Bb");
            board[7, 2] = new Bishop(7, 2, true, "Bw");
            board[7, 5] = new Bishop(7, 5, true, "Bw");
            board[0, 3] = new Queen(0, 3, false, "Qb");
            board[7, 3] = new Queen(7, 3, true, "Qw");
            for (int row = 0; row < 8; row++) 
            {
                for (int col = 0; col < 8; col++)
                {
                    if (row == 1)
                        board[row, col] = new Pawn(1, col, false, "Pb", true);
                    if (row == 6)
                        board[row, col] = new Pawn(6, col, true, "Pw", true);
                }
            }
            snapShots = SnapShotTheBoard();
        }
        public void Play()
        {
            PrintBoard();
            while (gameRunning)
            {
                string input;
                isWhiteTurn = turnCount % 2 != 0 ? true : false;
                string currentPlayerColor = isWhiteTurn ? "WHITE" : "BLACK";
                do
                { 
                    Console.WriteLine(currentPlayerColor + " - enter legal input to move, or write stalemate to ask for draw");
                    input = Console.ReadLine(); //------------************USER INPUT************-------------
                    //input = inputString.Substring(inputCounter, 4);
                    //Console.WriteLine(input);
                    if (IsStalemateRequasted(input,currentPlayerColor))
                    {
                        Console.WriteLine("GAME OVER");
                        gameRunning = false;
                        break;
                    }
                    //Console.ReadLine();
                } while (!(IsValidInput(input)) && gameRunning);

                if (!IsMovementPossible(DigitFromInput(input[1]), LetterFromInput(input[0]), DigitFromInput(input[3]), LetterFromInput(input[2])))
                    continue;
                BoardMovement(DigitFromInput(input[1]), LetterFromInput(input[0]), DigitFromInput(input[3]), LetterFromInput(input[2]));
                if (IsSelfChecked(DigitFromInput(input[1]), LetterFromInput(input[0]), DigitFromInput(input[3]), LetterFromInput(input[2]), currentPlayerColor))
                    continue;
                SpecialPostMovementActions(DigitFromInput(input[3]), LetterFromInput(input[2]));

                PrintBoard();
                inputCounter += 4; 
                King enemyKing = isWhiteTurn ? Kb : Kw;

                if(IsEnemyPlayerChecked(enemyKing))
                    if(IsCheckMate(enemyKing)) 
                        gameRunning = false; 
                
                if (Is3FoldStalemate(snapShots,SnapShotTheBoard()) || Is50MovesStalemate() || IsBaseStalemate(enemyKing) || IsInsufficientMaterial())
                {
                    Console.WriteLine("-----auto stalemate-----");
                    gameRunning = false;
                    break;
                }
                turnCount++;
                snapShots += SnapShotTheBoard();
            }
        }
        public bool IsEnemyPlayerChecked(King king)
        {
            if (IsCheck(king.GetRow(), king.GetCol(), !king.Iswhite(), false) == 1)
            {
                string checkedPlayerColor = king.Iswhite() ? "WHITE" : "BLACK";
                Console.WriteLine("----------" + checkedPlayerColor + " is checked----------");
                king.setInCheck();
                return true;
            }
            return false;
        }
        public void PrintBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                Console.Write("{0}  ", this.board.GetLength(0) - row);
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] != null)
                        Console.Write(this.board[row, col].Print());
                    else
                        Console.Write("-  ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("   A  B  C  D  E  F  G  H  ");
            Console.WriteLine("");
        }
        public bool IsValidInput(string input)
        {
            input = input.Trim();
            if (input.Length != 4 || input == "")
                return false;

            int SrcLetter, SrcDigit, DstLetter, DstDigit;
            SrcLetter = LetterFromInput(input[0]);
            SrcDigit = DigitFromInput(input[1]);
            DstLetter = LetterFromInput(input[2]);
            DstDigit = DigitFromInput(input[3]);
            if (SrcLetter == 99 || SrcDigit == 99 || DstLetter == 99 || DstDigit == 99)
            {
                Console.WriteLine("bad input");
                return false;
            }
            if (this.board[SrcDigit, SrcLetter] == null || (this.board[SrcDigit, SrcLetter].Iswhite() != isWhiteTurn))
            {
                Console.WriteLine("------move was not legal-----");
                return false;
            }
            return true;
        } 
        public bool IsMovementPossible(int srcDigit, int srcLetter, int dstDigit, int dstLetter)
        {  
            King insertedking = Kb;
            if (isWhiteTurn)
                insertedking = Kw;
              
                if (this.board[srcDigit, srcLetter] is Pawn || this.board[srcDigit, srcLetter] is King)
                {
                    if (!IsValidEnpassant(srcDigit, srcLetter, dstDigit, dstLetter))
                        return false;
                    if (!IsCastelingValid(srcDigit, srcLetter, dstDigit, dstLetter,insertedking))
                        return false;
                }
                else
                {
                    int res = this.board[srcDigit, srcLetter].CheckPossibleMove(dstDigit, dstLetter, board[srcDigit, srcLetter].Iswhite(), this.board, turnCount);
                    if (res == 0)
                    {
                        Console.WriteLine("move is not possible");
                        return false;
                    }
                }
                return true;
        }
        public void SpecialPostMovementActions(int dstDigit, int dstLetter)
        {
            if (this.board[dstDigit, dstLetter] is Pawn)
                PawnActions(dstDigit, dstLetter); 
        }
        public void PawnActions(int dstDigit, int dstLetter)
        {
            Pawn pawn = (Pawn)this.board[dstDigit, dstLetter];
            lastTimePawnMoved = turnCount;
            this.board[dstDigit, dstLetter] = pawn;
            pawn.Promotion(dstDigit, dstLetter, pawn.Iswhite(), board); 
        }
        public void BoardMovement(int srcDigit, int srcLetter, int dstDigit, int dstLetter)
        {
            this.board[dstDigit, dstLetter] = this.board[srcDigit, srcLetter];
            this.board[srcDigit, srcLetter] = null;
            this.board[dstDigit, dstLetter].ChangeCoordinates(dstDigit, dstLetter);
            this.board[dstDigit, dstLetter].SetMoved();
        }
        public bool IsSelfChecked(int srcDigit, int srcLetter, int dstDigit, int dstLetter, string currentPlayerColor)
        {
            King insertedKing = Kb;
            if (isWhiteTurn)
                insertedKing = Kw;
            ChessPart removedPart = CreateTempPart(this.board[dstDigit, dstLetter]); //incase the moving piece eats a part and king is in check while doing so
            int lastTurn = lastTurnPieceWasTaken; //storing the last time a piece was eaten in a variable before maybe reversing it
            if (this.board[dstDigit, dstLetter] != null)
                lastTurnPieceWasTaken = turnCount; //storing the current turn when a piece is being eaten.

            if (IsCheck(insertedKing.GetRow(), insertedKing.GetCol(), insertedKing.Iswhite(), true) != 0)
            {
                Console.WriteLine(currentPlayerColor + " king will be or is in check, move is not possible");
                this.board[srcDigit, srcLetter] = this.board[dstDigit, dstLetter];
                this.board[dstDigit, dstLetter] = removedPart;
                this.board[srcDigit, srcLetter].ChangeCoordinates(srcDigit, srcLetter);
                lastTurnPieceWasTaken = lastTurn; //reversing it 
                return true;
            }
            else
                insertedKing.NotInCheck();
            return false;
        }
        public bool IsCastelingValid(int srcDigit, int srcLetter, int dstDigit, int dstLetter, King insertedKing)
        {
            if (this.board[srcDigit, srcLetter] is King)
            {
                King king = (King)this.board[srcDigit, srcLetter];
                int KingRes = king.CheckPossibleMove(dstDigit, dstLetter, king.Iswhite(),board,turnCount);
                if (KingRes == 0)
                {
                    Console.WriteLine("move is not possible");
                    king.reverseMoved();
                    return false;
                }
                if (KingRes == 2)
                {
                    int Row = 7;
                    if (!king.Iswhite())
                        Row = 0;
                    int colOne = IsCheck(Row, 1,  king.Iswhite(), true);
                    int colTwo = IsCheck(Row, 2,  king.Iswhite(), true);
                    int colThree = IsCheck(Row, 3,  king.Iswhite(), true);
                    int colFive = IsCheck(Row, 5,  king.Iswhite(), true);
                    int colSix = IsCheck(Row, 6,  king.Iswhite(), true);
                    if (Casteling(king, dstDigit, dstLetter, insertedKing.Iswhite(), colOne, colTwo, colThree, colFive, colSix) != 1)
                        return false;
                    else
                        king.setCasteled();
                }
            }
            return true;
        }
        public bool IsValidEnpassant(int srcDigit, int srcLetter, int dstDigit, int dstLetter)
        {
            if (this.board[srcDigit, srcLetter] is Pawn)
            {
                Pawn pawn = (Pawn)this.board[srcDigit, srcLetter];
                int pawnRes = pawn.CheckPossibleMove(dstDigit, dstLetter, pawn.Iswhite(),board, turnCount);
                if (pawnRes == 0)
                {
                    Console.WriteLine("move is not possible");
                    return false;
                }
                //enpassnt remove pawn that was eaten
                if (pawnRes == 11)
                    this.board[dstDigit + 1, dstLetter] = null;
                if (pawnRes == 22)
                    this.board[dstDigit - 1, dstLetter] = null;
            }
            return true;
        }
        public int LetterFromInput(char input)
        {
            int outPut = 99;
            switch (input)
            {
                case 'a':
                case 'A':
                    outPut = 0;
                    break;
                case 'b':
                case 'B':
                    outPut = 1;
                    break;
                case 'c':
                case 'C':
                    outPut = 2;
                    break;
                case 'D':
                case 'd':
                    outPut = 3;
                    break;
                case 'E':
                case 'e':
                    outPut = 4;
                    break;
                case 'F':
                case 'f':
                    outPut = 5;
                    break;
                case 'G':
                case 'g':
                    outPut = 6;
                    break;
                case 'H':
                case 'h':
                    outPut = 7;
                    break;
                default:
                    return 99;
            }
            return outPut;
        }
        public int DigitFromInput(char input)
        {
            int outPut = 99;
            switch (input)
            {
                case '1':
                    outPut = 7;
                    break;
                case '2':
                    outPut = 6;
                    break;
                case '3':
                    outPut = 5;
                    break;
                case '4':
                    outPut = 4;
                    break;
                case '5':
                    outPut = 3;
                    break;
                case '6':
                    outPut = 2;
                    break;
                case '7':
                    outPut = 1;
                    break;
                case '8':
                    outPut = 0;
                    break;
                default:
                    return 99;
            }
            return outPut;
        }
        public int IsCheck(int destRow, int destCol, bool isWhite, bool selfThreatCheck)
        {
            int ans = 0;
                ans = IsThreatened( destRow, destCol, isWhite, selfThreatCheck);
            if (ans != 0)
                return 1;
            return 0;
        }
        public int IsThreatened( int destRow, int destCol, bool isWhite, bool selfThreatCheck)
        {
            int ans = 0;
            for (int srcRow = 0; srcRow < 8; srcRow++)
            {
                for (int srcCol = 0; srcCol < 8; srcCol++)
                {
                    if (this.board[srcRow, srcCol] != null && ((this.board[srcRow, srcCol].Iswhite() == !isWhite) == selfThreatCheck))
                    {
                        ans = this.board[srcRow, srcCol].CheckPossibleMove(destRow, destCol, !isWhite == selfThreatCheck,board, -1);
                        if (this.board[srcRow, srcCol] is Pawn)
                        {
                            if ((srcRow > 0 && srcRow < 7) && (srcCol > 0 && srcCol < 7) && (this.board[srcRow + 1, srcCol + 1] == null || this.board[srcRow + 1, srcCol - 1] == null) && (srcRow + 1 == destRow && ((srcCol + 1 == destCol) || srcCol - 1 == destCol)))
                                ans = 1;
                        }
                        if (selfThreatCheck)
                        {
                            if (this.board[srcRow, srcCol] is King)
                            {
                                King temp = (King)board[srcRow, srcCol];
                                ans = temp.CheckPossibleMove(destRow, destCol, !isWhite == selfThreatCheck,board, -1);
                                if (ans != 0)
                                    temp.reverseMoved();
                            }
                        }
                        if (ans != 0)
                            return 1;
                    }
                }
            }
            return 0;

        }
        public ChessPart CreateTempPart(ChessPart stored)
        {
            ChessPart temp;
            temp = null;
            if (stored is Pawn)
                temp = (Pawn)stored;
            if (stored is Knight)
                temp = (Knight)stored;
            if (stored is Bishop)
                temp = (Bishop)stored;
            if (stored is Rook)
                temp = (Rook)stored;
            if (stored is Queen)
                temp = (Queen)stored;
            if (stored is King)
                temp = (King)stored;
            return temp;
        }
        public int RookCastelingMove(King king,int RookRow,int rookCol,int oneCheck, int twoCheck, int threeCheck, int fiveCheck, int sixCheck)
        {
            int RookColDst=5;
            if (rookCol == 0)
                RookColDst = 2; 
            Rook temprook = (Rook)board[RookRow, rookCol];
            if (!temprook.GetMoved() && ((rookCol==0 && (oneCheck != 1 && twoCheck != 1 && threeCheck != 1))||(rookCol==7&& fiveCheck != 1 && sixCheck != 1)))
            {
                this.board[RookRow, RookColDst] = board[RookRow, rookCol];
                this.board[RookRow, rookCol] = null;
                this.board[RookRow, RookColDst].ChangeCoordinates(RookRow, RookColDst);
                king.SetMoved();
                return 1;
            }
            return 0;
        }
        public int Casteling(King king, int destRow, int destCol, bool isWhite, int oneChecked, int twoChecked, int threeChecked, int fiveChecked, int sixChecekd)
        {
            //casteling for white
            if (isWhite)
            {
                //left side casteling
                if (destRow == 7 && destCol == 1 && !king.inCheck() && !king.GetMoved() && this.board[7, 1] == null && this.board[7, 2] == null && this.board[7, 3] == null && (this.board[7, 0] != null))
                    return RookCastelingMove(king, 7, 0, oneChecked, twoChecked, threeChecked, fiveChecked, sixChecekd);
                //right side casteling
                if (destRow == 7 && destCol == 6 && !king.inCheck() && !king.GetMoved() && this.board[7, 5] == null && this.board[7, 6] == null && (this.board[7, 7] != null))
                    return RookCastelingMove(king, 7, 7, oneChecked, twoChecked, threeChecked, fiveChecked, sixChecekd);
            }
            if (!isWhite)
            {
                //left side casteling
                if (destRow == 0 && destCol == 1 && !king.inCheck() && !king.GetMoved() && this.board[0, 1] == null && this.board[0, 2] == null && this.board[0, 3] == null && (this.board[0, 0] != null))
                    return RookCastelingMove(king, 0, 0, oneChecked, twoChecked, threeChecked, fiveChecked, sixChecekd);
                //right side casteling
                if (destRow == 0 && destCol == 6 && !king.inCheck() && !king.GetMoved() && this.board[0, 5] == null && this.board[0, 6] == null && (this.board[0, 7] != null))
                    return RookCastelingMove(king, 0, 7, oneChecked, twoChecked, threeChecked, fiveChecked, sixChecekd);
            }
            return 0;
        }
        public bool IsStalemateRequasted(string input,string currentPlayerColor)
        {
            string OtherPlayerAnswer;
            input = input.Trim();
            if (input == "stalemate")
            {
                Console.WriteLine("--------------");
                Console.WriteLine(currentPlayerColor+" has asked for a stalemate, please enter Y to accept, N to reject the offer");
                Console.WriteLine("--------------");
                do
                {
                    Console.WriteLine("enter a valid input");
                    OtherPlayerAnswer = Console.ReadLine();
                    OtherPlayerAnswer = OtherPlayerAnswer.Trim();

                } while (OtherPlayerAnswer != "Y" && OtherPlayerAnswer != "N" && OtherPlayerAnswer != "y" && OtherPlayerAnswer != "n");
                if (OtherPlayerAnswer == "Y" || OtherPlayerAnswer == "y")
                    return true;
                if (OtherPlayerAnswer == "N" || OtherPlayerAnswer == "n")
                {
                    Console.WriteLine("-----The other player rejected the stalemate offer-----");
                    return false;
                }
            }
            return false;
        }
        public bool IsCheckMate(King king)
        {
            bool checkMate = true;
            if (king.inCheck())
            {
                int ans = 0;
                for (int srcRow = 0; srcRow < 8; srcRow++)
                {
                    for (int srcCol = 0; srcCol < 8; srcCol++)
                    {
                        if (this.board[srcRow, srcCol] != null && this.board[srcRow, srcCol].Iswhite() == king.Iswhite())
                        {
                            for (int dstRow = 0; dstRow < 8; dstRow++)
                            {
                                for (int dstCol = 0; dstCol < 8; dstCol++)
                                {
                                    if (!((dstRow == srcRow) && (dstCol == srcCol)))
                                    {
                                        ans = this.board[srcRow, srcCol].CheckPossibleMove(dstRow, dstCol, this.board[srcRow, srcCol].Iswhite(), this.board, -1);
                                        if (this.board[srcRow, srcCol] is King)
                                        {
                                            King temp = (King)this.board[srcRow, srcCol];
                                            ans = temp.CheckPossibleMove(dstRow, dstCol, temp.Iswhite(),board, -1);
                                            if (ans != 0)
                                                temp.reverseMoved();
                                        }
                                        if (ans != 0)
                                        {
                                            ChessPart tempSrcPart = CreateTempPart(this.board[srcRow, srcCol]);
                                            ChessPart tempDstPart = CreateTempPart(this.board[dstRow, dstCol]);
                                            this.board[dstRow, dstCol] = this.board[srcRow, srcCol];
                                            this.board[srcRow, srcCol] = null;
                                            this.board[dstRow, dstCol].ChangeCoordinates(dstRow, dstCol);
                                            if (IsCheck(king.GetRow(), king.GetCol(), king.Iswhite(), true) == 0)
                                            {
                                                checkMate = false;
                                            }
                                            this.board[srcRow, srcCol] = tempSrcPart;
                                            this.board[dstRow, dstCol] = tempDstPart;
                                            tempSrcPart.ChangeCoordinates(srcRow, srcCol);
                                            if (checkMate == false)
                                                return checkMate;
                                        }
                                    }

                                }
                            }
                        }
                    }

                }

            }
            Console.WriteLine("------MATE--------");
            return checkMate;
        }  
        public bool Is50MovesStalemate()
        {
            if (turnCount - lastTimePawnMoved == 51 || turnCount - lastTurnPieceWasTaken == 51)
                return true;
            return false;
        }
        public bool IsBaseStalemate(King king)
        {
            bool Stalemate;
            if (king.inCheck())
                return false;
            int ans = 0;
            for (int SrcRow = 0; SrcRow < 8; SrcRow++)
            {
                for (int SrcCol = 0; SrcCol < 8; SrcCol++)
                {
                    if (this.board[SrcRow, SrcCol] != null && this.board[SrcRow, SrcCol].Iswhite() == king.Iswhite())
                    {
                        for (int DstRow = 0; DstRow < 8; DstRow++)
                        {
                            for (int DstCol = 0; DstCol < 8; DstCol++)
                            {
                                if (DstRow != SrcRow && DstCol != SrcCol)
                                {
                                    ans = this.board[SrcRow, SrcCol].CheckPossibleMove(DstRow, DstCol, king.Iswhite(),board, -1);

                                    if (this.board[SrcRow, SrcCol] is King)
                                    {
                                        King temp = (King)board[SrcRow, SrcCol];
                                        if (IsCheck(DstRow, DstCol, king.Iswhite(), true) == 0)
                                        {
                                            ans = temp.CheckPossibleMove(DstRow, DstCol, king.Iswhite(),board, -1);
                                            if (ans != 0)
                                                temp.reverseMoved();
                                        }
                                        else
                                            ans = 0;
                                    }
                                    if (ans != 0)
                                    {
                                        Stalemate = false;
                                        return Stalemate;
                                    }

                                }

                            }
                        }
                    }
                }
            }
            return true;
        }
        public bool IsInsufficientMaterial()
        {
            int bishopcounter = 0;
            int otherPartsCounter = 0;
            foreach (ChessPart item in this.board)
            {
                if (item != null && item is Bishop)
                    bishopcounter += 1;
                if (item != null && !(item is Bishop))
                    otherPartsCounter++;
            }
            if (bishopcounter <= 1 && otherPartsCounter == 2 || otherPartsCounter == 2)
                return true;
            return false;
        }
        public string SnapShotTheBoard()
        {
            string currentBoard = "";
            foreach (ChessPart item in this.board)
            {
                if (item is King)
                {
                    King king = (King)item;
                    if (king.Casteled()) { currentBoard += "k" + king.GetType()[1]; }
                    else { currentBoard += king.GetType(); }
                }
                else
                {
                    if (item == null)
                        currentBoard += "--";
                    else currentBoard += item.GetType();
                }
            }
            return currentBoard;
        }
        public bool Is3FoldStalemate(string snapShots,string currentBoard)
        {      
            int IdenticalBoardsCounter = 0;
            for (int charIndex = 0; charIndex < snapShots.Length; charIndex += 128)
            {
                if (currentBoard.Equals(snapShots.Substring(charIndex, 128)))
                    IdenticalBoardsCounter++;
                if(IdenticalBoardsCounter==3)
                    return true;
            }
            return false;
        }
    } 
}