using System;

namespace BatalhaNaval
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> opcoesValidas = new List<string>()
            {
                "0",
                "1",
                "2"
            };
            string opcao;
            bool control = true;

            Console.Write("===========================\nOlá!\nBem-vindo ao Batalha do Naval\ndesenvolvido por Marcelo Oliveira\n===========================\n");

            while (true)
            {
                Console.WriteLine("\nEntre com uma opção abaixo:");
                Console.WriteLine("1 - Jogue contra o computador");
                Console.WriteLine("2 - Jogue multiplayer");
                Console.WriteLine("0 - Sair");
                Console.Write("Digite: ");
                opcao = Console.ReadLine();

                while (!opcoesValidas.Contains(opcao))
                {

                    Console.WriteLine("\nOpção inválida.\n\nEntre com uma das opões abaixo:");
                    Console.WriteLine("1 - Jogue contra o computador");
                    Console.WriteLine("2 - Jogue multiplayer");
                    Console.WriteLine("0 - Sair");
                    Console.Write("Digite: ");
                    opcao = Console.ReadLine();
                }


                if (opcao.Equals("0"))
                {
                    Console.WriteLine("\nEntre com qualquer tecla para fechar...");
                    break;
                }
                else if (opcao.Equals("1"))
                {

                    string player1, player2;
                    string[,] player1Board = new string[10, 10];
                    string[,] player1OpponentBoard = new string[10, 10];
                    string[,] cpuBoard = new string[10, 10];
                    string[,] cpuOpponentBoard = new string[10, 10];
                    Dictionary<string, int> shipsType = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };
                    Dictionary<string, int> ships1Amount = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };
                    Dictionary<string, int> shipsCpuAmount = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };
                    Dictionary<string, string> shipsAmount1 = new Dictionary<string, string>()
                        {
                            {"PS","PS - Porta-Aviões (5 quadrantes)"},
                            {"NT","NT - Navio-Tanque (4 quadrantes)"},
                            {"DS","DS - Destroyers (3 quadrantes)"},
                            {"SB","SB - Submarinos (2 quadrantes)"}
                        };

                    Console.Write("Qual é o nome do jogador 1? ");
                    player1 = Console.ReadLine();

                    Console.Clear();

                    /*======================== INSERCAO DE EMBARCACOES - JOGADOR 1 =========================*/

                    do
                    {
                        Console.WriteLine($"\n==== Tabuleiro do {player1} =====\n");
                        ShowBoard(player1Board);

                        Console.WriteLine($"\n==== Posicionamento dos navios - {player1} =====\n");
                        Console.WriteLine("====  Opções de embarcações =====\n");

                        foreach (string value in shipsAmount1.Values)
                        {
                            Console.WriteLine(value);
                        }

                        Console.Write("Qual o tipo de embarcação? ");
                        var embarcacao = Console.ReadLine();
                        var embarcacaoUpper = embarcacao.ToUpper();

                        if (!shipsAmount1.ContainsKey(embarcacaoUpper))
                        {
                            Console.WriteLine("\nOpção inválida.\nEntre com uma das opões abaixo.\n");
                            continue;
                        }

                        string embarcacaoPosicaoUpper;

                        do
                        {
                            Console.Write("Qual é a posição da embarcação selecionada?\n" +
                                "Informe o campo de ínicio e fim da posição. Por exemplo, H1H2 (2 quadrantes).\n" +
                                "Digite: ");
                            string embarcacaoPosicao = Console.ReadLine();
                            embarcacaoPosicaoUpper = embarcacaoPosicao.ToUpper() + "XX"; //XX: para equalizar o tamanho de todas as entradas para terem no mínimo 6 
                        } while (embarcacaoPosicaoUpper.Length < 6);


                        int coluna1Int = 0, coluna2Int = 0, linha1Int = 0, linha2Int = 0;

                        int[] arrayColumn = ConversionTypeColumn(embarcacaoPosicaoUpper);

                        coluna1Int = arrayColumn[0];
                        coluna2Int = arrayColumn[1];

                        int[] arrayLine = ConversionTypeLine(embarcacaoPosicaoUpper);

                        linha1Int = arrayLine[0];
                        linha2Int = arrayLine[1];

                        if (!(TestPosition(embarcacaoUpper, linha1Int, coluna1Int, linha2Int, coluna2Int)))
                        {
                            continue;
                        };

                        coluna1Int--;
                        coluna2Int--;

                        if (InsertBoard(player1Board, embarcacaoUpper, linha1Int, coluna1Int, linha2Int, coluna2Int))
                        {
                            shipsAmount1.Remove(embarcacaoUpper);
                            Console.WriteLine($"\nSUCESSO! A embarcação {embarcacaoUpper} foi inserida no seu tabuleiro.");
                            Console.WriteLine($"Aperte qualquer tecla para prosseguir");
                        }
                        else
                        {
                            Console.WriteLine($"\nERRO! Última posição indicada sobrepõe alguma outra embarcação. Por favor, {player1} verifique o seu tabuleiro.");
                            Console.WriteLine($"Aperte qualquer tecla para prosseguir");
                        }

                        Console.ReadKey();
                        Console.Clear();

                        if (shipsAmount1.Count == 0)
                        {
                            break;
                        }


                    } while (control);

                    /*======================== INSERCAO DE EMBARCACOES - CPU =========================*/

                    cpuBoard[9, 0] = "SB";
                    cpuBoard[9, 1] = "SB";

                    cpuBoard[5, 3] = "DS";
                    cpuBoard[6, 3] = "DS";
                    cpuBoard[7, 3] = "DS";

                    cpuBoard[2, 8] = "NT";
                    cpuBoard[3, 8] = "NT";
                    cpuBoard[4, 8] = "NT";
                    cpuBoard[5, 8] = "NT";

                    cpuBoard[1, 5] = "PS";
                    cpuBoard[1, 6] = "PS";
                    cpuBoard[1, 7] = "PS";
                    cpuBoard[1, 8] = "PS";
                    cpuBoard[1, 9] = "PS";

                    //ShowBoard(cpuBoard);

                    //Console.ReadKey();
                    //Console.Clear();

                    /*======================== INICIO DO JOGO =========================*/

                    bool fimDeJogo = false;
                    int numeroDeRodadas = 0;
                    for (int i = 0; fimDeJogo == false; i++)
                    {
                        if (i % 2 == 0)
                        {
                            Console.WriteLine($"{player1}, PRESSIONE QUALQUER TECLA, QUANDO ESTIVER PRONTO!");

                            Console.ReadKey();
                            Console.Clear();

                            Console.Write($"\n==== Seu tabuleiro (suas embarcações) - {player1} =====\n");
                            ShowBoard(player1Board);

                            Console.Write("\n");

                            Console.Write($"==== Tabuleiro do adversário - =====\n");
                            ShowBoard(player1OpponentBoard);

                            var disparoUpper = "teste";
                            int coluna = 0, linha = 0;
                            char repetido = 'N';
                            do
                            {
                                if (repetido == 'S') { Console.Write("Disparo fora da grade ou disparo repetido.\nEfetue um disparo corretamente (digite uma linha e coluna): "); }
                                else { Console.Write("Efetue um disparo (digite uma linha e coluna): "); }
                                var disparo = Console.ReadLine();
                                disparoUpper = disparo.ToUpper() + "XX";
                                if (string.IsNullOrEmpty(disparo))
                                {
                                    repetido = 'S';
                                }
                                else if (ConversionTypeColumnShot(disparoUpper) == 11)
                                {
                                    repetido = 'S';
                                }
                                else if (disparoUpper.Count() == 3)
                                {
                                    repetido = 'S';
                                }
                                else if (char.IsLetter(char.Parse(disparoUpper.Substring(1, 1))))
                                {
                                    repetido = 'S';
                                }
                                else
                                {
                                    coluna = ConversionTypeColumnShot(disparoUpper);
                                    linha = ConversionTypeLineShot(disparoUpper);
                                    coluna--;
                                    repetido = string.IsNullOrEmpty(player1OpponentBoard[linha, coluna]) ? 'N' : 'S';
                                }
                            } while (repetido == 'S' || coluna > 9 || linha > 9 || coluna < 0 || linha < 0);


                            if (AttackPlayer1(linha, coluna, shipsCpuAmount, cpuBoard, player1OpponentBoard, fimDeJogo))
                            {
                                fimDeJogo = true;
                            }

                            if (fimDeJogo == true)
                            {
                                numeroDeRodadas = i;
                            }

                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            char repetido = 'N';
                            int[] arrayPosicao = new int[2];
                            Random randNum = new Random();

                            do
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    arrayPosicao[j] = randNum.Next(9);
                                }
                                repetido = (player1Board[arrayPosicao[0], arrayPosicao[1]] == "A " || player1Board[arrayPosicao[0], arrayPosicao[1]] == "X ") ? 'S' : 'N';

                            } while (repetido == 'S');

                            if (AttackPlayer2(arrayPosicao[0], arrayPosicao[1], ships1Amount, player1Board, cpuOpponentBoard, fimDeJogo))
                            {
                                fimDeJogo = true;
                            }

                            Console.ReadKey();
                            Console.Clear();


                            if (fimDeJogo == true)
                            {
                                numeroDeRodadas = i;
                            }

                        }
                        Console.Clear();
                    }
                        if (numeroDeRodadas % 2 == 0)
                        {
                            Console.WriteLine($"PARABÉNSSSSSSSSSSSSSS! GANHADOR : {player1}");
                        }
                        else
                        {
                            Console.WriteLine($"PARABÉNSSSSSSSSSSSSSS! GANHADOR : CPU");
                        }


                        Console.WriteLine("Fim de jogo! Digite qualquer tecla pra reiniciar no menu principal.");

                        Console.ReadKey();

                    // FUNCÕES

                    static void ShowBoard(string[,] board)
                    {

                        Console.Write("  ");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write($"{i + 1}  ");
                        }
                        Console.WriteLine();

                        for (int i = 0; i < board.GetLength(0); i++)
                        {

                            Console.Write($"{Convert.ToChar(65 + i)} ");


                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (string.IsNullOrEmpty(board[i, j]))
                                {
                                    Console.Write($"{board[i, j]}  |");
                                }
                                else
                                {
                                    Console.Write($"{board[i, j]}|");
                                }
                            }
                            Console.WriteLine();
                        }

                        Console.WriteLine();

                    }


                    static int[] ConversionTypeColumn(string embarcacaoPosicaoUpper)
                    {
                        int coluna1Int = 0, coluna2Int = 0;
                        int[] array = new int[2];
                        bool control = true;

                        if (embarcacaoPosicaoUpper.Substring(1, 2) == "10" && embarcacaoPosicaoUpper.Substring(4, 2) == "10")
                        {
                            if (!(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 2), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(4, 2), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;

                            }

                        }
                        else if (control && embarcacaoPosicaoUpper.Substring(1, 2) == "10")
                        {
                            if (!(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 2), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(4, 1), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;
                            }
                        }

                        else if (control && embarcacaoPosicaoUpper.Substring(3, 2) == "10")
                        {
                            if (!(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 1), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(3, 2), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;
                            }
                        }
                        else
                        {
                            if (control && !(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 1), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(3, 1), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;
                            }
                        }

                        array[0] = coluna1Int;
                        array[1] = coluna2Int;

                        return array;

                    }


                    static int[] ConversionTypeLine(string embarcacaoPosicaoUpper)
                    {
                        int linha1Int = 0, linha2Int = 0;
                        int[] array = new int[2];
                        bool control = true;

                        if (embarcacaoPosicaoUpper.Substring(1, 2) == "10")
                        {
                            char linha1 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(0, 1).ToUpper());
                            char linha2 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(3, 1).ToUpper());
                            linha1Int = Convert.ToInt32(linha1) - 65;
                            linha2Int = Convert.ToInt32(linha2) - 65;
                        }

                        else
                        {
                            char linha1 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(0, 1).ToUpper());
                            char linha2 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(2, 1).ToUpper());
                            linha1Int = Convert.ToInt32(linha1) - 65;
                            linha2Int = Convert.ToInt32(linha2) - 65;
                        }

                        array[0] = linha1Int;
                        array[1] = linha2Int;

                        return array;
                    }


                    static bool TestPosition(string embarcacaoPosicaoUpper, int linha1Int, int coluna1Int, int linha2Int, int coluna2Int)
                    {
                        bool control = true;
                        Dictionary<string, int> shipsType = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };

                        Dictionary<string, string> shipsAmount = new Dictionary<string, string>()
                    {
                        {"PS","PS - Porta-Aviões (5 quadrantes)"},
                        {"NT","NT - Navio-Tanque (4 quadrantes)"},
                        {"DS","DS - Destroyers (3 quadrantes)"},
                        {"SB","SB - Submarinos (2 quadrantes)"}
                    };

                        if (control && (linha1Int < 0 || linha1Int > 10 || linha2Int < 0 || linha2Int > 10))
                        {
                            Console.WriteLine("\nOpção inválida.\nLinha(s) inválida(s)!\n");
                            control = false;
                        }

                        else if (control && (coluna1Int < 0 || coluna1Int > 10 || coluna2Int < 0 || coluna2Int > 10))
                        {
                            Console.WriteLine("\nOpção inválida.\nColuna(s) inválida(s)!\n");
                            control = false;
                        }

                        else if (control && (linha1Int == linha2Int))
                        {

                            int max = Math.Max(coluna1Int, coluna2Int);
                            int min = Math.Min(coluna1Int, coluna2Int);
                            int tamanho = (max - min) + 1;

                            if (control && (!shipsType.Contains((new KeyValuePair<string, int>(embarcacaoPosicaoUpper, tamanho)))))
                            {
                                Console.WriteLine("\nOpção inválida.\nQuantidade de quadrantes inválidos para embarcação escolhida.\n");
                                control = false;
                            }
                        }
                        else if (control && (coluna1Int == coluna2Int))
                        {
                            int max = Math.Max(linha1Int, linha2Int);
                            int min = Math.Min(linha1Int, linha2Int);
                            int tamanho = (max - min) + 1;

                            if (control && (!shipsType.Contains((new KeyValuePair<string, int>(embarcacaoPosicaoUpper, tamanho)))))
                            {
                                Console.WriteLine("\nOpção inválida.\nQuantidade de quadrantes inválidos para embarcação escolhida.\n");
                                control = false;
                            }
                        }
                        else if (control && (coluna1Int != coluna2Int) && (linha1Int != linha2Int))
                        {
                            Console.WriteLine("\nOpção inválida.\nQuadrantes inválidos, eles devem ser sequenciais.\n");
                            control = false;
                        }

                        return control;

                    }




                    static bool InsertBoard(string[,] tabuleiro, string embarcacaoUpper, int linha1Int, int coluna1Int, int linha2Int, int coluna2Int)
                    {

                        bool controle = true;
                        if (linha1Int == linha2Int)
                        {
                            int max = Math.Max(coluna1Int, coluna2Int);
                            int min = Math.Min(coluna1Int, coluna2Int);

                            for (int i = min; i <= max; i++)
                            {
                                if (string.IsNullOrEmpty(tabuleiro[linha1Int, i]))
                                {
                                    continue;
                                }
                                controle = false;
                                break;
                            }
                            if (controle)
                            {
                                for (int i = min; i <= max; i++)
                                {
                                    tabuleiro[linha1Int, i] = embarcacaoUpper;

                                }
                            }
                        }
                        else
                        {
                            int max = Math.Max(linha1Int, linha2Int);
                            int min = Math.Min(linha1Int, linha2Int);

                            for (int i = min; i <= max; i++)
                            {
                                if (string.IsNullOrEmpty(tabuleiro[i, coluna1Int]))
                                {
                                    continue;
                                }
                                controle = false;
                                break;
                            }

                            if (controle)
                            {
                                for (int i = min; i <= max; i++)
                                {
                                    tabuleiro[i, coluna1Int] = embarcacaoUpper;
                                }
                            }
                        }

                        return controle;

                    }

                    static int ConversionTypeColumnShot(string disparoUpper)
                    {
                        int coluna = 0;
                        bool control = true;

                        if (disparoUpper.Substring(1, 2) == "10")
                        {
                            if (!(int.TryParse(disparoUpper.Substring(1, 2), out coluna)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna válida.\n");
                                control = false;

                            }

                        }

                        else if (disparoUpper.Substring(2, 1) == "X")
                        {
                            if (!(int.TryParse(disparoUpper.Substring(1, 1), out coluna)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite uma coluna válida.\n");
                                control = false;
                            }
                        }

                        else
                        {
                            coluna = 11;
                        }

                        return coluna;
                    }


                    static int ConversionTypeLineShot(string disparoUpper)
                    {
                        int linha = 0;

                        if (string.IsNullOrEmpty(disparoUpper.Substring(0, 1)))
                        {
                            linha = 11;
                        }
                        else
                        {
                            linha = 0;
                            bool control = true;
                            char linha1 = Convert.ToChar(disparoUpper.Substring(0, 1).ToUpper());
                            linha = Convert.ToInt32(linha1) - 65;
                        }

                        return linha;
                    }


                    static bool AttackPlayer1(int linha, int coluna, Dictionary<string, int> ships2Amount, string[,] player2Board, string[,] player1OpponentBoard, bool fimDeJogo)
                    {
                        bool control = false;
                        if (ships2Amount.ContainsKey(string.IsNullOrEmpty(player2Board[linha, coluna]) ? "A" : player2Board[linha, coluna]))
                        {

                            var embarcacaoAtingida = player2Board[linha, coluna];
                            int retornoRestante = ships2Amount[embarcacaoAtingida] - 1;
                            ships2Amount.Remove(embarcacaoAtingida);
                            ships2Amount.Add(embarcacaoAtingida, retornoRestante);
                            player1OpponentBoard[linha, coluna] = "X ";
                            player2Board[linha, coluna] = "X ";
                            Console.WriteLine("** FOGO **!\nVocê acertou um alvo! :)");

                            if (ships2Amount[embarcacaoAtingida] == 0)
                            {
                                ships2Amount.Remove(embarcacaoAtingida);
                                Console.WriteLine($"Parabéns! Você destrui a embarcação {embarcacaoAtingida}");
                                Console.WriteLine($"Restam {ships2Amount.Count} embarcações para você ganhar o jogo");
                            }

                            if (ships2Amount.Count == 0)
                            {
                                Console.WriteLine($"Pode comemorar, você ganhou o jogo!");
                                control = true;
                            }
                        }
                        else
                        {
                            player1OpponentBoard[linha, coluna] = "A ";
                            Console.WriteLine("** ÁGUA **!\nNão foi dessa vez! :(");
                        }
                        return control;

                    }


                    static bool AttackPlayer2(int linha, int coluna, Dictionary<string, int> ships1Amount, string[,] player1Board, string[,] player2OpponentBoard, bool fimDeJogo)
                    {
                        bool control = false;

                        if (ships1Amount.ContainsKey(string.IsNullOrEmpty(player1Board[linha, coluna]) ? "A" : player1Board[linha, coluna]))
                        {
                            var embarcacaoAtingida = player1Board[linha, coluna];
                            int retornoRestante = ships1Amount[embarcacaoAtingida] - 1;
                            ships1Amount.Remove(embarcacaoAtingida);
                            ships1Amount.Add(embarcacaoAtingida, retornoRestante);
                            player2OpponentBoard[linha, coluna] = "X ";
                            player1Board[linha, coluna] = "X ";
                            Console.WriteLine("** FOGO **!\nO CPU acertou um alvo! :(");

                            if (ships1Amount[embarcacaoAtingida] == 0)
                            {
                                ships1Amount.Remove(embarcacaoAtingida);
                                Console.WriteLine($"Nada bom! O CPU destrui a embarcação {embarcacaoAtingida}");
                                Console.WriteLine($"Restam {ships1Amount.Count} embarcações para CPU ganhar o jogo");
                            }
                            if (ships1Amount.Count == 0)
                            {
                                Console.WriteLine($"Que triste, a CPU ganhou o jogo!");
                                control = true;
                            }
                        }
                        else
                        {
                            player1Board[linha, coluna] = "A ";
                            Console.WriteLine("** ÁGUA **!\n Ufa! A CPU não acertou nada! :)");
                        }

                        return control;
                    }

                }

                else if (opcao.Equals("2"))
                {
                    string player1, player2;
                    string[,] player1Board = new string[10, 10];
                    string[,] player1OpponentBoard = new string[10, 10];
                    string[,] player2Board = new string[10, 10];
                    string[,] player2OpponentBoard = new string[10, 10];
                    Dictionary<string, int> shipsType = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };
                    Dictionary<string, int> ships1Amount = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };
                    Dictionary<string, int> ships2Amount = new Dictionary<string, int>()
                        {
                            {"PS",5},
                            {"NT",4},
                            {"DS",3},
                            {"SB",2}
                        };
                    Dictionary<string, string> shipsAmount1 = new Dictionary<string, string>()
                        {
                            {"PS","PS - Porta-Aviões (5 quadrantes)"},
                            {"NT","NT - Navio-Tanque (4 quadrantes)"},
                            {"DS","DS - Destroyers (3 quadrantes)"},
                            {"SB","SB - Submarinos (2 quadrantes)"}
                        };

                    Dictionary<string, string> shipsAmount2 = new Dictionary<string, string>()
                        {
                            {"PS","PS - Porta-Aviões (5 quadrantes)"},
                            {"NT","NT - Navio-Tanque (4 quadrantes)"},
                            {"DS","DS - Destroyers (3 quadrantes)"},
                            {"SB","SB - Submarinos (2 quadrantes)"}
                        };

                    Console.Write("Qual é o nome do jogador 1? ");
                    player1 = Console.ReadLine();

                    Console.Write("Qual é o nome do jogador 2? ");
                    player2 = Console.ReadLine();

                    Console.Clear();

                    /*======================== INSERCAO DE EMBARCACOES - JOGADOR 1 =========================*/

                    Console.WriteLine($"\n== Jogador(a) {player2}, AGUARDE SUA VEZ, NÃO OLHE A ESTRATÉGIA DO SEU ADVERSÁRIO! ==\n");

                    do
                    {
                        Console.WriteLine($"\n==== Tabuleiro do {player1} =====\n");
                        ShowBoard(player1Board);

                        Console.WriteLine($"\n==== Posicionamento dos navios - {player1} =====\n");
                        Console.WriteLine("====  Opções de embarcações =====\n");

                        foreach (string value in shipsAmount1.Values)
                        {
                            Console.WriteLine(value);
                        }

                        Console.Write("Qual o tipo de embarcação? ");
                        var embarcacao = Console.ReadLine();
                        var embarcacaoUpper = embarcacao.ToUpper();

                        if (!shipsAmount1.ContainsKey(embarcacaoUpper))
                        {
                            Console.WriteLine("\nOpção inválida.\nEntre com uma das opões abaixo.\n");
                            continue;
                        }

                        string embarcacaoPosicaoUpper;

                        do
                        {
                            Console.Write("Qual é a posição da embarcação selecionada?\n" +
                                "Informe o campo de ínicio e fim da posição. Por exemplo, H1H2 (2 quadrantes).\n" +
                                "Digite: ");
                            string embarcacaoPosicao = Console.ReadLine();
                            embarcacaoPosicaoUpper = embarcacaoPosicao.ToUpper() + "XX"; //XX: para equalizar o tamanho de todas as entradas para terem no mínimo 6 
                        } while (embarcacaoPosicaoUpper.Length < 6);


                        int coluna1Int = 0, coluna2Int = 0, linha1Int = 0, linha2Int = 0;

                        int[] arrayColumn = ConversionTypeColumn(embarcacaoPosicaoUpper);

                        coluna1Int = arrayColumn[0];
                        coluna2Int = arrayColumn[1];

                        int[] arrayLine = ConversionTypeLine(embarcacaoPosicaoUpper);

                        linha1Int = arrayLine[0];
                        linha2Int = arrayLine[1];

                        if (!(TestPosition(embarcacaoUpper, linha1Int, coluna1Int, linha2Int, coluna2Int)))
                        {
                            continue;
                        };

                        coluna1Int--;
                        coluna2Int--;

                        if (InsertBoard(player1Board, embarcacaoUpper, linha1Int, coluna1Int, linha2Int, coluna2Int))
                        {
                            shipsAmount1.Remove(embarcacaoUpper);
                            Console.WriteLine($"\nSUCESSO! A embarcação {embarcacaoUpper} foi inserida no seu tabuleiro.");
                            Console.WriteLine($"Aperte qualquer tecla para prosseguir");
                        }
                        else
                        {
                            Console.WriteLine($"\nERRO! Última posição indicada sobrepõe alguma outra embarcação. Por favor, {player1} verifique o seu tabuleiro.");
                            Console.WriteLine($"Aperte qualquer tecla para prosseguir");
                        }

                        Console.ReadKey();
                        Console.Clear();

                        if (shipsAmount1.Count == 0)
                        {
                            break;
                        }


                    } while (control);

                    /*======================== INSERCAO DE EMBARCACOES - JOGADOR 2 =========================*/

                    Console.WriteLine($"\n== Jogador(a) {player1}, AGUARDE SUA VEZ, NÃO OLHE A ESTRATÉGIA DO SEU ADVERSÁRIO! ==\n");

                    do
                    {
                        Console.WriteLine($"\n==== Tabuleiro do {player2} =====\n");
                        ShowBoard(player2Board);

                        Console.WriteLine($"\n==== Posicionamento dos navios - {player2} =====\n");
                        Console.WriteLine("====  Opções de embarcações =====\n");

                        foreach (string value in shipsAmount2.Values)
                        {
                            Console.WriteLine(value);
                        }

                        Console.Write("Qual o tipo de embarcação? ");
                        var embarcacao = Console.ReadLine();
                        var embarcacaoUpper = embarcacao.ToUpper();

                        if (!shipsAmount2.ContainsKey(embarcacaoUpper))
                        {
                            Console.WriteLine("\nOpção inválida.\nEntre com uma das opões abaixo.\n");
                            continue;
                        }

                        string embarcacaoPosicaoUpper;

                        do
                        {
                            Console.Write("Qual é a posição da embarcação selecionada?\n" +
                                "Informe o campo de ínicio e fim da posição. Por exemplo, H1H2 (2 quadrantes).\n" +
                                "Digite: ");
                            string embarcacaoPosicao = Console.ReadLine();
                            embarcacaoPosicaoUpper = embarcacaoPosicao.ToUpper() + "XX"; //XX: para equalizar o tamanho de todas as entradas para terem no mínimo 6 
                        } while (embarcacaoPosicaoUpper.Length < 6);


                        int coluna1Int = 0, coluna2Int = 0, linha1Int = 0, linha2Int = 0;

                        int[] arrayColumn = ConversionTypeColumn(embarcacaoPosicaoUpper);

                        coluna1Int = arrayColumn[0];
                        coluna2Int = arrayColumn[1];

                        int[] arrayLine = ConversionTypeLine(embarcacaoPosicaoUpper);

                        linha1Int = arrayLine[0];
                        linha2Int = arrayLine[1];

                        if (!(TestPosition(embarcacaoUpper, linha1Int, coluna1Int, linha2Int, coluna2Int)))
                        {
                            continue;
                        };

                        coluna1Int--;
                        coluna2Int--;

                        if (InsertBoard(player2Board, embarcacaoUpper, linha1Int, coluna1Int, linha2Int, coluna2Int))
                        {
                            shipsAmount2.Remove(embarcacaoUpper);
                            Console.WriteLine($"\nSUCESSO! A embarcação {embarcacaoUpper} foi inserida no seu tabuleiro.");
                            Console.WriteLine($"Aperte qualquer tecla para prosseguir");
                        }
                        else
                        {
                            Console.WriteLine($"\nERRO! Última posição indicada sobrepõe alguma outra embarcação. Por favor, {player2} verifique o seu tabuleiro.");
                            Console.WriteLine($"Aperte qualquer tecla para prosseguir");
                        }

                        Console.ReadKey();
                        Console.Clear();

                        if (shipsAmount2.Count == 0)
                        {
                            break;
                        }


                    } while (control);

                    /*======================== INICIO DO JOGO =========================*/

                    bool fimDeJogo = false;
                    int numeroDeRodadas = 0;
                    for (int i = 0; fimDeJogo == false; i++)
                    {

                        if (i % 2 == 0)
                        {
                            Console.WriteLine($"\n== Jogador(a) {player2}, AGUARDE SUA VEZ! " +
                            "NÃO OLHE OS TABULEIROS DO SEU ADVERSÁRIO! ==\n");
                            Console.WriteLine($"{player1}, PRESSIONE QUALQUER TECLA, QUANDO ESTIVER PRONTO!");

                            Console.ReadKey();
                            Console.Clear();

                            Console.Write($"\n==== Seu tabuleiro (suas embarcações) - {player1} =====\n");
                            ShowBoard(player1Board);

                            Console.Write("\n");

                            Console.Write($"==== Tabuleiro do adversário - =====\n");
                            ShowBoard(player1OpponentBoard);

                            var disparoUpper = "teste";
                            int coluna = 0, linha = 0;
                            char repetido = 'N';
                            do
                            {
                                if (repetido == 'S') { Console.Write("Disparo fora da grade ou disparo repetido.\nEfetue um disparo corretamente (digite uma linha e coluna): "); }
                                else { Console.Write("Efetue um disparo (digite uma linha e coluna): "); }
                                var disparo = Console.ReadLine();
                                disparoUpper = disparo.ToUpper() + "XX";
                                if (string.IsNullOrEmpty(disparo))
                                {
                                    repetido = 'S';
                                }
                                else if (ConversionTypeColumnShot(disparoUpper) == 11)
                                {
                                    repetido = 'S';
                                }
                                else if (disparoUpper.Count() == 3)
                                {
                                    repetido = 'S';
                                }
                                else if (char.IsLetter(char.Parse(disparoUpper.Substring(1, 1))))
                                {
                                    repetido = 'S';
                                }
                                else
                                {
                                    coluna = ConversionTypeColumnShot(disparoUpper);
                                    linha = ConversionTypeLineShot(disparoUpper);
                                    coluna--;
                                    repetido = string.IsNullOrEmpty(player1OpponentBoard[linha, coluna]) ? 'N' : 'S';
                                }
                            } while (repetido == 'S' || coluna > 9 || linha > 9 || coluna < 0 || linha < 0);


                            if (AttackPlayer1(linha, coluna, ships2Amount, player2Board, player1OpponentBoard, fimDeJogo))
                            {
                                fimDeJogo = true;
                            }

                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine($"\n== Jogador(a) {player1}, AGUARDE SUA VEZ! " +
                            "NÃO OLHE OS TABULEIROS DO SEU ADVERSÁRIO! ==\n");
                            Console.WriteLine($"{player2}, PRESSIONE QUALQUER TECLA, QUANDO ESTIVER PRONTO!");

                            Console.ReadKey();
                            Console.Clear();

                            Console.Write($"\n==== Seu tabuleiro (suas embarcações) - {player2} =====\n");
                            ShowBoard(player2Board);

                            Console.Write("\n");

                            Console.Write($"==== Tabuleiro do adversário - =====\n");
                            ShowBoard(player2OpponentBoard);

                            var disparoUpper = "teste";
                            int coluna = 0, linha = 0;
                            char repetido = 'N';
                            do
                            {
                                if (repetido == 'S') { Console.Write("Disparo fora da grade ou disparo repetido.\nEfetue um disparo corretamente (digite uma linha e coluna): "); }
                                else { Console.Write("Efetue um disparo (digite uma linha e coluna): "); }
                                var disparo = Console.ReadLine();
                                disparoUpper = disparo.ToUpper() + "XX";
                                if (string.IsNullOrEmpty(disparo))
                                {
                                    repetido = 'S';
                                }
                                else if (ConversionTypeColumnShot(disparoUpper) == 11)
                                {
                                    repetido = 'S';
                                }
                                else if (disparoUpper.Count() == 3)
                                {
                                    repetido = 'S';
                                }
                                else if (char.IsLetter(char.Parse(disparoUpper.Substring(1, 1))))
                                {
                                    repetido = 'S';
                                }
                                else
                                {
                                    coluna = ConversionTypeColumnShot(disparoUpper);
                                    linha = ConversionTypeLineShot(disparoUpper);
                                    coluna--;
                                    repetido = string.IsNullOrEmpty(player2OpponentBoard[linha, coluna]) ? 'N' : 'S';
                                }
                            } while (repetido == 'S' || coluna > 9 || linha > 9 || coluna < 0 || linha < 0);

                            if (AttackPlayer2(linha, coluna, ships1Amount, player1Board, player2OpponentBoard, fimDeJogo))
                            {
                                fimDeJogo = true;
                            }


                            Console.ReadKey();
                            Console.Clear();
                        }

                        if (fimDeJogo == true)
                        {
                            numeroDeRodadas = i;
                        }
                    }

                    Console.Clear();
                    if (numeroDeRodadas % 2 == 0)
                    {
                        Console.WriteLine($"PARABÉNSSSSSSSSSSSSSS! GANHADOR : {player1}");
                    }
                    else
                    {
                        Console.WriteLine($"PARABÉNSSSSSSSSSSSSSS! GANHADOR : {player2}");
                    }


                    Console.WriteLine("Fim de jogo! Digite qualquer tecla pra reiniciar no menu principal.");

                    Console.ReadKey();


                    // FUNCÕES

                    static void ShowBoard(string[,] board)
                    {

                        Console.Write("  ");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write($"{i + 1}  ");
                        }
                        Console.WriteLine();

                        for (int i = 0; i < board.GetLength(0); i++)
                        {

                            Console.Write($"{Convert.ToChar(65 + i)} ");


                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (string.IsNullOrEmpty(board[i, j]))
                                {
                                    Console.Write($"{board[i, j]}  |");
                                }
                                else
                                {
                                    Console.Write($"{board[i, j]}|");
                                }
                            }
                            Console.WriteLine();
                        }

                        Console.WriteLine();

                    }


                    static int[] ConversionTypeColumn(string embarcacaoPosicaoUpper)
                    {
                        int coluna1Int = 0, coluna2Int = 0;
                        int[] array = new int[2];
                        bool control = true;

                        if (embarcacaoPosicaoUpper.Substring(1, 2) == "10" && embarcacaoPosicaoUpper.Substring(4, 2) == "10")
                        {
                            if (!(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 2), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(4, 2), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;

                            }

                        }
                        else if (control && embarcacaoPosicaoUpper.Substring(1, 2) == "10")
                        {
                            if (!(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 2), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(4, 1), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;
                            }
                        }

                        else if (control && embarcacaoPosicaoUpper.Substring(3, 2) == "10")
                        {
                            if (!(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 1), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(3, 2), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;
                            }
                        }
                        else
                        {
                            if (control && !(int.TryParse(embarcacaoPosicaoUpper.Substring(1, 1), out coluna1Int)) | !(int.TryParse(embarcacaoPosicaoUpper.Substring(3, 1), out coluna2Int)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna(s) válida(s).\n");
                                control = false;
                            }
                        }

                        array[0] = coluna1Int;
                        array[1] = coluna2Int;

                        return array;

                    }


                    static int[] ConversionTypeLine(string embarcacaoPosicaoUpper)
                    {
                        int linha1Int = 0, linha2Int = 0;
                        int[] array = new int[2];
                        bool control = true;

                        if (embarcacaoPosicaoUpper.Substring(1, 2) == "10")
                        {
                            char linha1 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(0, 1).ToUpper());
                            char linha2 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(3, 1).ToUpper());
                            linha1Int = Convert.ToInt32(linha1) - 65;
                            linha2Int = Convert.ToInt32(linha2) - 65;
                        }

                        else
                        {
                            char linha1 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(0, 1).ToUpper());
                            char linha2 = Convert.ToChar(embarcacaoPosicaoUpper.Substring(2, 1).ToUpper());
                            linha1Int = Convert.ToInt32(linha1) - 65;
                            linha2Int = Convert.ToInt32(linha2) - 65;
                        }

                        array[0] = linha1Int;
                        array[1] = linha2Int;

                        return array;
                    }


                    static bool TestPosition(string embarcacaoPosicaoUpper, int linha1Int, int coluna1Int, int linha2Int, int coluna2Int)
                    {
                        bool control = true;
                        Dictionary<string, int> shipsType = new Dictionary<string, int>()
                    {
                        {"PS",5},
                        {"NT",4},
                        {"DS",3},
                        {"SB",2}
                    };

                        Dictionary<string, string> shipsAmount = new Dictionary<string, string>()
                    {
                        {"PS","PS - Porta-Aviões (5 quadrantes)"},
                        {"NT","NT - Navio-Tanque (4 quadrantes)"},
                        {"DS","DS - Destroyers (3 quadrantes)"},
                        {"SB","SB - Submarinos (2 quadrantes)"}
                    };

                        if (control && (linha1Int < 0 || linha1Int > 10 || linha2Int < 0 || linha2Int > 10))
                        {
                            Console.WriteLine("\nOpção inválida.\nLinha(s) inválida(s)!\n");
                            control = false;
                        }

                        else if (control && (coluna1Int < 0 || coluna1Int > 10 || coluna2Int < 0 || coluna2Int > 10))
                        {
                            Console.WriteLine("\nOpção inválida.\nColuna(s) inválida(s)!\n");
                            control = false;
                        }

                        else if (control && (linha1Int == linha2Int))
                        {

                            int max = Math.Max(coluna1Int, coluna2Int);
                            int min = Math.Min(coluna1Int, coluna2Int);
                            int tamanho = (max - min) + 1;

                            if (control && (!shipsType.Contains((new KeyValuePair<string, int>(embarcacaoPosicaoUpper, tamanho)))))
                            {
                                Console.WriteLine("\nOpção inválida.\nQuantidade de quadrantes inválidos para embarcação escolhida.\n");
                                control = false;
                            }
                        }
                        else if (control && (coluna1Int == coluna2Int))
                        {
                            int max = Math.Max(linha1Int, linha2Int);
                            int min = Math.Min(linha1Int, linha2Int);
                            int tamanho = (max - min) + 1;

                            if (control && (!shipsType.Contains((new KeyValuePair<string, int>(embarcacaoPosicaoUpper, tamanho)))))
                            {
                                Console.WriteLine("\nOpção inválida.\nQuantidade de quadrantes inválidos para embarcação escolhida.\n");
                                control = false;
                            }
                        }
                        else if (control && (coluna1Int != coluna2Int) && (linha1Int != linha2Int))
                        {
                            Console.WriteLine("\nOpção inválida.\nQuadrantes inválidos, eles devem ser sequenciais.\n");
                            control = false;
                        }

                        return control;

                    }




                    static bool InsertBoard(string[,] tabuleiro, string embarcacaoUpper, int linha1Int, int coluna1Int, int linha2Int, int coluna2Int)
                    {

                        bool controle = true;
                        if (linha1Int == linha2Int)
                        {
                            int max = Math.Max(coluna1Int, coluna2Int);
                            int min = Math.Min(coluna1Int, coluna2Int);

                            for (int i = min; i <= max; i++)
                            {
                                if (string.IsNullOrEmpty(tabuleiro[linha1Int, i]))
                                {
                                    continue;
                                }
                                controle = false;
                                break;
                            }
                            if (controle)
                            {
                                for (int i = min; i <= max; i++)
                                {
                                    tabuleiro[linha1Int, i] = embarcacaoUpper;

                                }
                            }
                        }
                        else
                        {
                            int max = Math.Max(linha1Int, linha2Int);
                            int min = Math.Min(linha1Int, linha2Int);

                            for (int i = min; i <= max; i++)
                            {
                                if (string.IsNullOrEmpty(tabuleiro[i, coluna1Int]))
                                {
                                    continue;
                                }
                                controle = false;
                                break;
                            }

                            if (controle)
                            {
                                for (int i = min; i <= max; i++)
                                {
                                    tabuleiro[i, coluna1Int] = embarcacaoUpper;
                                }
                            }
                        }

                        return controle;

                    }




                    static int ConversionTypeColumnShot(string disparoUpper)
                    {
                        int coluna = 0;
                        bool control = true;

                        if (disparoUpper.Substring(1, 2) == "10")
                        {
                            if (!(int.TryParse(disparoUpper.Substring(1, 2), out coluna)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite coluna válida.\n");
                                control = false;

                            }

                        }

                        else if (disparoUpper.Substring(2, 1) == "X")
                        {
                            if (!(int.TryParse(disparoUpper.Substring(1, 1), out coluna)))
                            {
                                Console.WriteLine("\nOpção inválida.\nDigite uma coluna válida.\n");
                                control = false;
                            }
                        }

                        else
                        {
                            coluna = 11;
                        }

                        return coluna;
                    }


                    static int ConversionTypeLineShot(string disparoUpper)
                    {
                        int linha = 0;

                        if (string.IsNullOrEmpty(disparoUpper.Substring(0, 1)))
                        {
                            linha = 11;
                        }
                        else
                        {
                            linha = 0;
                            bool control = true;
                            char linha1 = Convert.ToChar(disparoUpper.Substring(0, 1).ToUpper());
                            linha = Convert.ToInt32(linha1) - 65;
                        }

                        return linha;
                    }


                    static bool AttackPlayer1(int linha, int coluna, Dictionary<string, int> ships2Amount, string[,] player2Board, string[,] player1OpponentBoard, bool fimDeJogo)
                    {
                        bool control = false;
                        if (ships2Amount.ContainsKey(string.IsNullOrEmpty(player2Board[linha, coluna]) ? "A" : player2Board[linha, coluna]))
                        {

                            var embarcacaoAtingida = player2Board[linha, coluna];
                            int retornoRestante = ships2Amount[embarcacaoAtingida] - 1;
                            ships2Amount.Remove(embarcacaoAtingida);
                            ships2Amount.Add(embarcacaoAtingida, retornoRestante);
                            player1OpponentBoard[linha, coluna] = "X ";
                            player2Board[linha, coluna] = "X ";
                            Console.WriteLine("** FOGO **!\nVocê acertou um alvo! :)");

                            if (ships2Amount[embarcacaoAtingida] == 0)
                            {
                                ships2Amount.Remove(embarcacaoAtingida);
                                Console.WriteLine($"Parabéns! Você destrui a embarcação {embarcacaoAtingida}");
                                Console.WriteLine($"Restam {ships2Amount.Count} embarcações para você ganhar o jogo");
                            }

                            if (ships2Amount.Count == 0)
                            {
                                Console.WriteLine($"Pode comemorar, você ganhou o jogo!");
                                control = true;
                            }
                        }
                        else
                        {
                            player1OpponentBoard[linha, coluna] = "A ";
                            Console.WriteLine("** ÁGUA **!\nNão foi dessa vez! :(");
                        }
                        return control;

                    }


                    static bool AttackPlayer2(int linha, int coluna, Dictionary<string, int> ships1Amount, string[,] player1Board, string[,] player2OpponentBoard, bool fimDeJogo)
                    {
                        bool control = false;

                        if (ships1Amount.ContainsKey(string.IsNullOrEmpty(player1Board[linha, coluna]) ? "A" : player1Board[linha, coluna]))
                        {
                            var embarcacaoAtingida = player1Board[linha, coluna];
                            int retornoRestante = ships1Amount[embarcacaoAtingida] - 1;
                            ships1Amount.Remove(embarcacaoAtingida);
                            ships1Amount.Add(embarcacaoAtingida, retornoRestante);
                            player2OpponentBoard[linha, coluna] = "X ";
                            player1Board[linha, coluna] = "X ";
                            Console.WriteLine("** FOGO **!\nVocê acertou um alvo! :)");

                            if (ships1Amount[embarcacaoAtingida] == 0)
                            {
                                ships1Amount.Remove(embarcacaoAtingida);
                                Console.WriteLine($"Parabéns! Você destrui a embarcação {embarcacaoAtingida}");
                                Console.WriteLine($"Restam {ships1Amount.Count} embarcações para você ganhar o jogo");
                            }
                            if (ships1Amount.Count == 0)
                            {
                                Console.WriteLine($"Pode comemorar, você ganhou o jogo!");
                                control = true;
                            }
                        }
                        else
                        {
                            player2OpponentBoard[linha, coluna] = "A ";
                            Console.WriteLine("** ÁGUA **!\n Não foi dessa vez! :(");
                        }

                        return control;
                    }


                }
            }
        }
    }
}