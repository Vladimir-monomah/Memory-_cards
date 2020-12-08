namespace MemoryLibrary
{
    public interface IPlayable
    {
        void HideCard(int nr);
        void ShowCard(int nr, int card);
        void ShowWinner();
    }
}
