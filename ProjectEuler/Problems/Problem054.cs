namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;

    /// <summary>
    /// In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:
    /// 
    ///     * High Card: Highest value card.
    ///     * One Pair: Two cards of the same value.
    ///     * Two Pairs: Two different pairs.
    ///     * Three of a Kind: Three cards of the same value.
    ///     * Straight: All cards are consecutive values.
    ///     * Flush: All cards of the same suit.
    ///     * Full House: Three of a kind and a pair.
    ///     * Four of a Kind: Four cards of the same value.
    ///     * Straight Flush: All cards are consecutive values of same suit.
    ///     * Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
    /// 
    /// The cards are valued in the order:
    /// 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.
    /// 
    /// If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below); if the highest cards tie then the next highest cards are compared, and so on.
    /// 
    /// Consider the following five hands dealt to two players:
    /// 
    /// Hand    Player 1           Player 2             Winner
    /// 
    /// 1       5H 5C 6S 7S KD     2C 3S 8S 8D TD       Player 2
    ///         Pair of Fives      Pair of Eights
    ///         
    /// 2       5D 8C 9S JS AC     2C 5C 7D 8S QH       Player 1
    ///         Highest card Ace   Highest card Queen
    ///         
    /// 3       2D 9C AS AH AC     3D 6D 7D TD QD       Player 2
    ///         Three Aces         Flush with Diamonds
    ///         
    /// 4	 	4D 6S 9H QH QC     3D 6D 7H QD QS       Player 1
    ///         Pair of Queens     Pair of Queens
    ///         Highest card Nine  Highest card Seven 
    ///         
    /// 5	 	2H 2D 4C 4D 4S     3C 3D 3S 9S 9D       Player 1
    ///         Full House         Full House
    ///         With Three Fours   With Three Threes
    /// 
    /// The file, poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): the first five are Player 1's cards and the last five are Player 2's cards. You can assume that all hands are valid (no invalid characters or repeated cards), each player's hand is in no specific order, and in each hand there is a clear winner.
    /// 
    /// How many hands does Player 1 win?
    /// </summary>
    [ProblemResource("poker")]
    [Result(Name = "count", Expected = "376")]
    public class Problem054 : Problem
    {
        [DebuggerDisplay("{Value} of {Suit}")]
        private class Card
        {
            public char Suit
            {
                get;
                set;
            }
            public int Value
            {
                get;
                set;
            }
        }

        public override string Solve(string resource)
        {
            var deals = (from l in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                         let cards = l.Split(' ')
                         let values = from c in cards
                                      let value = c.Substring(0, c.Length - 1)
                                      select new Card
                                      {
                                          Suit = c[c.Length - 1],
                                          Value = value == "A" ? 14 : (value == "K" ? 13 : (value == "Q" ? 12 : (value == "J" ? 11 : (value == "T" ? 10 : int.Parse(value))))),
                                      }
                         select new
                         {
                             Player1 = values.Take(5).OrderByDescending(c => c.Value).ToList(),
                             Player2 = values.Skip(5).OrderByDescending(c => c.Value).ToList(),
                         }).ToList();

            Func<IList<Card>, string> score = hand =>
            {
                var isFlush = hand.Select(s => s.Suit).Distinct().Count() == 1;

                var isStraight = hand[0].Value == hand[1].Value + 1 &&
                                 hand[1].Value == hand[2].Value + 1 &&
                                 hand[2].Value == hand[3].Value + 1 &&
                                 hand[3].Value == hand[4].Value + 1;

                Func<int, string> pad = value => "." + value.ToString().PadLeft(2, '0');

                if (isStraight)
                {
                    return (isFlush ? "08" : "04") + pad(hand[0].Value);
                }
                else if (isFlush)
                {
                    return "05" + pad(hand[0].Value) + pad(hand[1].Value) + pad(hand[2].Value) + pad(hand[3].Value) + pad(hand[4].Value);
                }

                var groups = (from c in hand
                              group hand by c.Value into g
                              orderby g.Key descending
                              orderby g.Count() descending
                              select new
                              {
                                  Value = g.Key,
                                  Count = g.Count(),
                              }).ToList();

                if (groups[0].Count == 4)
                {
                    return "07" + pad(groups[0].Value) + pad(groups[1].Value);
                }
                else if (groups[0].Count == 3)
                {
                    if (groups[1].Count == 2)
                    {
                        return "06" + pad(groups[0].Value) + pad(groups[1].Value);
                    }
                    else
                    {
                        return "03" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value);
                    }
                }
                else if (groups[0].Count == 2)
                {
                    if (groups[1].Count == 2)
                    {
                        return "02" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value);
                    }
                    else
                    {
                        return "01" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value) + pad(groups[3].Value);
                    }
                }

                return "00" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value) + pad(groups[3].Value) + pad(groups[4].Value);
            };

            /// High Card = 00.AA.BB.CC.DD.EE
            /// One Pair = 01.AA.CC.DD.EE
            /// Two Pairs = 02.AA.BB.EE
            /// Three of a Kind = 03.AA.DD.EE
            /// Straight = 04.AA
            /// Flush = 05.AA.BB.CC.DD.EE
            /// Full House = 06.AA.BB
            /// Four of a Kind = 07.AA.EE
            /// Straight\Royal Flush = 08.AA

            var count = 0;
            foreach (var deal in deals)
            {
                var p1 = score(deal.Player1);
                var p2 = score(deal.Player2);

                if (p1.CompareTo(p2) > 0)
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
