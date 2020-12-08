using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Карточки_памяти
{
    public partial class Form1 : Form
    {
        static Random rand = new Random();

        int[] cards = new int[16];
        bool[] opens = new bool[16];
        int done;
        int status;
        int card_a;
        int card_b;

        public Form1()
        {
            InitializeComponent();
            init_game();
        }

        private void menu_game_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menu_help_game_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Цель игры - открыть все карточки
за минимальное количество ходов.

На столе ледит 16 перевёрнутых карточек.
На них изображено 8 разных картинок.
Каждая картинка изображена дважды.
Необходимо найти парные картинки.

Щёлкайте по картинкам, чтобы их перевернуть.
Если пара подобрана верно - картинка остаётся.
Если ошибочно - карточки перевернуться назад.
Запоминайте картинки на карточках,
чтобы в следующий раз открыть их верно.", "Правила игры");
        }

        private void menu_help_about_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@"Игровая прогармма «Карточки памяти»
создана в обучающих целях
на практическом видеокурсе
«Изучаем язык C# с нуля».

http://videosharp.info", "О программе");
        }

        

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int nr = int.Parse(((PictureBox)sender).Tag.ToString());
            if (opens[nr]) return;
            switch (status)
            {
                case 0: status_0(nr); break;
                case 1: status_1(nr); break;
                case 2: status_2(nr); break;
                case 3: status_3(nr); break;
            }
        }

        private void init_game()
        {
            for (int j = 0; j < cards.Length; j++)
                cards[j] = j % (cards.Length / 2) + 1;
            for (int j = 0; j < 100; j++)
                shuffle_cards();
            for (int j = 0; j < cards.Length; j++)
                load_picrure(j, 0);
            for (int j = 0; j < cards.Length; j++)
                hide(j);
            for (int j = 0; j < cards.Length; j++)
                opens[j] = false;
            done = 0;
            status = 0;
        }

        private void shuffle_cards()
        {
            int a = rand.Next(0, cards.Length);
            int b = rand.Next(0, cards.Length);
            if (a == b) return;
            int x;
            x = cards[a];
            cards[a] = cards[b];
            cards[b] = x;
        }

        private void load_picrure(int picture, int image)
        {
            get_picture_box(picture).Image = get_image(image);
        }

        private PictureBox get_picture_box(int picture)
        {
            switch (picture)
            {
                case 0: return pictureBox0;
                case 1: return pictureBox1;
                case 2: return pictureBox2;
                case 3: return pictureBox3;
                case 4: return pictureBox4;
                case 5: return pictureBox5;
                case 6: return pictureBox6;
                case 7: return pictureBox7;
                case 8: return pictureBox8;
                case 9: return pictureBox9;
                case 10: return pictureBox10;
                case 11: return pictureBox11;
                case 12: return pictureBox12;
                case 13: return pictureBox13;
                case 14: return pictureBox14;
                case 15: return pictureBox15;
                default: return null;
            }
            
        }

        private Image get_image(int image)
        {
            switch (image)
            {
                case 0: return Properties.Resources._0;
                case 1: return Properties.Resources._1;
                case 2: return Properties.Resources._2;
                case 3: return Properties.Resources._3;
                case 4: return Properties.Resources._4;
                case 5: return Properties.Resources._5;
                case 6: return Properties.Resources._6;
                case 7: return Properties.Resources._7;
                case 8: return Properties.Resources._8;
                default: return null;
            }
        }

        private void menu_game_new_Click(object sender, EventArgs e)
        {
            init_game();
        }

        private void show(int picture)
        {
            load_picrure(picture, cards[picture]);
            get_picture_box(picture).Cursor = Cursors.Arrow; 
        }

        private void hide(int picture)
        {
            load_picrure(picture, 0);
            get_picture_box(picture).Cursor = Cursors.Hand;
        }

        private void open(int picture)
        {
            opens[picture] = true;
            show(picture);
        }

        private void status_0(int nr)
        {
            card_a=nr;
            show(card_a);
                status = 1;
        }

        private void status_1(int nr)
        {
            card_b = nr;
            if (card_a == card_b)
                return;
            show(card_b);
                status = 2;
            if (cards[card_a] == cards[card_b])
            {
                open(card_a);
                open(card_b);
                done += 2;
                if (done == 16)
                    you_win();
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
            hide(card_a);
            hide(card_b);
            status_0(nr);
        }

        private void you_win()
        {
            MessageBox.Show("Вы победили!", "Поздравляем");
        }
    }
}
