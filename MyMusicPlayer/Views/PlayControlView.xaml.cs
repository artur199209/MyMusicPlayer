using System;
using System.Windows;
using System.Windows.Controls;
using MyMusicPlayer.ViewModels;
using MyMusicPlayer.Models;
using System.Timers;
using System.Windows.Input;

namespace MyMusicPlayer.Views
{
	/// <summary>
	/// Interaction logic for PlayControlView.xaml
	/// </summary>
	public partial class PlayControlView : UserControl
	{


		private PlayControlViewModel playControlViewModel;
		private Timer timer;
		private string time = "00:00";
		private int sliderNewValue = 0;

		public PlayControlView()
		{
			InitializeComponent();
			timer = new Timer();								
			playControlViewModel = new PlayControlViewModel();
			this.DataContext = playControlViewModel;

			playControlViewModel.mediaPlayer.MediaOpened += mediaPlayer_MediaOpened;
			playControlViewModel.mediaPlayer.MediaEnded += mediaPlayer_MediaEnded;
			durationSlider.AddHandler(MouseLeftButtonUpEvent,
					  new MouseButtonEventHandler(timeSlider_MouseLeftButtonUp),
					  true);
			durationSlider.AddHandler(MouseLeftButtonDownEvent,
					  new MouseButtonEventHandler(timeSlider_MouseLeftButtonDown),
					  true);
		}

		private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (playControlViewModel.GetSongSecondsCount() > 0)
			{
				Slider slider = sender as Slider;
				playControlViewModel.SetMediaPlayerSongPosition();
				ControlExtensions.InvokeIfRequired(this, (x) =>
				{
					durationSlider.Value = x;
					sliderNewValue = playControlViewModel.CalculateSliderValue();
				}, sliderNewValue);
				playControlViewModel.PlayOrPauseSong();
			}
		}

		private void timeSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			playControlViewModel.PauseSong();
		}

		private void InitTimer()
		{
			timer.Interval = 100;
			timer.Elapsed += timer_Elapsed;
			timer.Start();
		}

		void mediaPlayer_MediaEnded(object sender, EventArgs e)
		{
			if (sender.GetType().ToString() == "System.Windows.Media.MediaPlayer")
			{
				timer.Stop();

				ControlExtensions.InvokeIfRequired(this, (x) =>
				{
					elapsedTimeTextBlock.Text = x.ToString();
				}, "00:00");

				ControlExtensions.InvokeIfRequired(this, (x) =>
				{
					durationSlider.Value = x;
				}, 0);
			}

			playControlViewModel.PlayNextSongAfterPrevious();
			timer.Start();
		}

		void mediaPlayer_MediaOpened(object sender, EventArgs e)
		{
			InitTimer();
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (playControlViewModel.IsPlay)
			{
				ControlExtensions.InvokeIfRequired(this, (x) =>
					{
						elapsedTimeTextBlock.Text = x.ToString();
						time = playControlViewModel.mediaPlayer.Position.ToString(@"mm\:ss");
					}, time);

				ControlExtensions.InvokeIfRequired(this, (x) =>
				{
					durationSlider.Value = x;
					sliderNewValue = playControlViewModel.CalculateSliderValue();
				}, sliderNewValue);
			}								 
			
			if(playControlViewModel.IsStop)
			{
				ControlExtensions.InvokeIfRequired(this, (x) =>
				{
					elapsedTimeTextBlock.Text = x.ToString();
					playControlViewModel.DurationMP3 = "00:00";
				}, "00:00");
			}
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			playControlViewModel.SetMediaPlayerVolume(e.NewValue);
			playControlViewModel.SaveVolumeSetting(e.NewValue);
		}
	}
}