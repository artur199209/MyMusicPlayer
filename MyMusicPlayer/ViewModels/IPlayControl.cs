namespace MyMusicPlayer
{
    public interface IPlayControl
    {
        void RepeatSong();
        void RandomSong();
        void PlayNextSongAfterPrevious();
        void PlayNextSong();
        void PlayPreviousSong();
        void PlaySong();
        void PlaySongWithSpecificIndex(int index);
        void PauseSong();
        void PlayOrPauseSong();
        void SetMediaPlayerVolume(double volumeValue);
        void SaveVolumeSetting(double volume);
        int GetSongSecondsCount();
        int GetActualSongSecond();
        int CalculateSliderValue();
        void SetMediaPlayerSongPosition();
        void StopSong();

    }
}