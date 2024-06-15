using System ;
using System.ComponentModel;

namespace T1 {

    internal class Program {
        struct stGame {

            public enChoice PlayerChoice ;
            public enChoice PcChoice ;

        } ;
        struct stResult {

            public byte GameRounds ;
            public byte PlayerWinTimes ;
            public byte PcWinTimes ;
            public byte DrawTimes ;
            public enWinner Winner ;

        } ;
        enum enWinner {

            Player = 1 , Pc = 2 , NoWinner = 3
        }
        enum enChoice {

            Stone = 1 , Paper , Scissor
        } ;
        static byte ReadRounds() {

            byte Rounds ;

            do {

                Console.WriteLine("How many Rounds 1 to 10 ?") ;
                Rounds = Convert.ToByte(Console.ReadLine() ?? "") ;

            } while (Rounds > 10 || Rounds <= 0) ;

            return Rounds ;
        }
        static byte ReadChoice() {

            byte Choice ;

            do {

                Console.Write("\nYour Choice: [1]:Stone, [2]:Paper , [3]:Scissor ? ") ;
                Choice = Convert.ToByte(Console.ReadLine() ?? "") ;

            } while (Choice > 3 || Choice <= 0) ;

            return Choice ;
        }
        static int RandomNumber() {

            Random RandNum = new Random() ;

            return RandNum.Next(1 , 4) ;
        }
        static enWinner RoundWinner(enChoice Player , enChoice Pc) {

            if (Player == Pc) {

                Console.BackgroundColor = ConsoleColor.Yellow ;
                return enWinner.NoWinner ;
            }
            
            if ((Player == enChoice.Stone && Pc == enChoice.Scissor) || 
            (Player == enChoice.Paper && Pc == enChoice.Stone) || 
            (Player == enChoice.Scissor && Pc == enChoice.Paper)) {

                Console.BackgroundColor = ConsoleColor.Green ;
                return enWinner.Player ;

            }

            Console.Write("\a") ;
            Console.BackgroundColor = ConsoleColor.Red ;
            return enWinner.Pc ;
        }
        static string WinnerToString(enWinner Winner) {

            string [] arr = {"No Winner" , "Pc" , "Player"} ;

            return arr[3 - ((byte)Winner)] ;
        }
        static void AddWins(enWinner Winner , ref byte Player , ref byte Pc , ref byte Draw) {

            switch (Winner) {

                case enWinner.Player :
                    Player++ ;
                    break ;
                case enWinner.Pc :
                    Pc++ ;
                    break ;
                case enWinner.NoWinner :
                    Draw++ ;
                    break ;
            }
        }
        static enWinner FinalWinner(byte Player , byte Pc , byte Draw) {

            if (Player == Pc) {

                Console.BackgroundColor = ConsoleColor.Yellow ;
                return enWinner.NoWinner ;
            }
            
            if (Player > Pc && Player > Draw) {

                Console.BackgroundColor = ConsoleColor.Green ;
                return enWinner.Player ;
            }

            if (Pc > Player && Pc > Draw) {

                Console.Write("\a") ;
                Console.BackgroundColor = ConsoleColor.Red ;
                return enWinner.Pc ;
            }

            Console.BackgroundColor = ConsoleColor.Yellow ;
            return enWinner.NoWinner ;
                
        }
        static stResult GameResult(byte Rounds , byte Player , byte Pc , byte Draw) {

            stResult Result ;

            Result.GameRounds = Rounds ;
            Result.PlayerWinTimes = Player ;
            Result.PcWinTimes = Pc ;
            Result.DrawTimes = Draw ;
            Result.Winner =  FinalWinner(Player , Pc , Draw) ;

            return Result ;
        }
        static void PrintScreenEndGame() {

            Console.WriteLine("\t\t___________________________________________________________________________\n") ;
            Console.WriteLine("\t\t\t\t\t+++ G a m e O v e r +++") ;
            Console.WriteLine("\t\t___________________________________________________________________________") ;

        }
        static void EndGame(stResult Result) {

            PrintScreenEndGame() ;

            Console.WriteLine("\n\t\t______________________________[ Game Results ]_____________________________") ;
            Console.WriteLine("\n\t\tGame Rounds        : " + Result.GameRounds) ;
            Console.WriteLine("\t\tPlayer1 won times  : " + Result.PlayerWinTimes) ;
            Console.WriteLine("\t\tPc won times       : " + Result.PcWinTimes) ;
            Console.WriteLine("\t\tDraw times         : " + Result.DrawTimes) ;
            Console.WriteLine("\t\tFinal Winner       : " + WinnerToString(Result.Winner)) ;
            Console.WriteLine("\t\t___________________________________________________________________________") ;

        }
        static void PlayGame() {

            stGame Game ;
            byte Player = 0 , Pc = 0 , Draw = 0 ;
            enWinner Winner ;
            stResult Result ;

            byte Rounds = ReadRounds() ;

            for (byte i = 1 ; i <= Rounds ; i++) {

                Console.WriteLine("\nRound [" + i + "] begins: ") ;
                Game.PlayerChoice = (enChoice) ReadChoice() ;
                Game.PcChoice = (enChoice) RandomNumber() ;

                Console.WriteLine("\n____________________________Round [" + i + "]____________________________\n") ;
                Console.WriteLine("Player1 Choice : " + Game.PlayerChoice) ;
                Console.WriteLine("Pc      Choice : " + Game.PcChoice) ;
                Winner = RoundWinner(Game.PlayerChoice , Game.PcChoice) ;
                AddWins(Winner , ref Player , ref Pc , ref Draw) ;
                Console.WriteLine("Round Winner   : [" + WinnerToString(Winner) + "]") ;
                Console.WriteLine("_________________________________________________________________\n") ;

                
            }

            Result = GameResult(Rounds , Player , Pc , Draw) ;
            
            EndGame(Result) ;
            
        }
        static void StartGame() {

            char Answer = 'Y' ;

            do {

                Console.Clear() ;

                PlayGame() ;

                Console.Write("\t\tDo you want to play again? [y/n] ? ") ;
                Answer = Convert.ToChar(Console.ReadLine() ?? "") ;

            } while (Char.ToUpper(Answer) == 'Y') ;
        }

        static void Main(string[] args) {

            StartGame() ;
        }
    }
}