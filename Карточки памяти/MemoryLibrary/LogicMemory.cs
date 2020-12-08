using System;

namespace MemoryLibrary
{
    public class LogicMemory
    {
        IPlayable play;

        static Random rand = new Random();

        int[] cards = new int[16];
        bool[] opens = new bool[16];
        int done;
        int status;
        int card_a;
        int card_b;

        public LogicMemory(IPlayable play)
        {
            this.play = play;
        }

        public void CreateNewGame()
        {
            for (int j = 0; j < cards.Length; j++)
                cards[j] = j % (cards.Length / 2) + 1;
            for (int j = 0; j < 100; j++)
                ShuffleCards();
            for (int j = 0; j < cards.Length; j++)
                play.HideCard(j);
            for (int j = 0; j < cards.Length; j++)
                opens[j] = false;
            done = 0;
            status = 0;
        }

        public void ClickPicture(int nr)
        {
            if (opens[nr]) return;
            switch (status)
            {
                case 0: status_0(nr); break;
                case 1: status_1(nr); break;
                case 2: status_2(nr); break;
                case 3: status_3(nr); break;
            }
        }

        private void ShuffleCards()
        {
            int a = rand.Next(0, cards.Length);
            int b = rand.Next(0, cards.Length);
            if (a == b) return;
            int x;
            x = cards[a];
            cards[a] = cards[b];
            cards[b] = x;
        }

        private void open(int picture)
        {
            opens[picture] = true;
            play.ShowCard(picture, cards[picture]);
        }

        private void status_0(int nr)
        {
            card_a = nr;
            play.ShowCard(card_a, cards[card_a]);
            status = 1;
        }

        private void status_1(int nr)
        {
            card_b = nr;
            if (card_a == card_b)
                return;
            play.ShowCard(card_b, cards[card_b]);
            status = 2;
            if (cards[card_a] == cards[card_b])
            {
                open(card_a);
                open(card_b);
                done += 2;
                if (done == 16)
                    play.ShowWinner();
                else
                    status = 0;
            }
            else
                status = 3;

        }

        private void status_2(int nr)
        {

        }

        private void status_3(int nr)
        {
            play.HideCard(card_a);
            play.HideCard(card_b);
            status_0(nr);
        }
    }
}
