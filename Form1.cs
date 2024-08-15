using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Form1 : Form
    {
        private List<string> musicFiles;
        private string currentSong;
        private bool isPaused;
        private bool isChangingPosition;
        public Form1()
        {
            InitializeComponent();
            musicFiles = new List<string>();
            isPaused = false;
            isChangingPosition = false;
        }

        private void siticoneGradientButton3_Click(object sender, EventArgs e)
        {
            musicPlayer.Ctlcontrols.stop();
            isPaused = false;
            timer2.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex >= 0)
            {
                musicPlayer.Ctlcontrols.play();
                isPaused = false;
            }
            else
            {
                currentSong = musicFiles[listBox1.SelectedIndex];
                musicPlayer.URL = currentSong;
                musicPlayer.Ctlcontrols.play();
            }
            //timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!isChangingPosition)
            {
                //label1.Text = "Length: " + FormatTime(musicPlayer.Ctlcontrols.currentPosition) + " / " + FormatTime(musicPlayer.currentMedia.duration);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "MP3 Files | *.mp3";
            if(dialog.ShowDialog() == DialogResult.OK )
            {
                foreach (var item in dialog.FileNames)
                {
                    musicFiles.Add(item);
                    listBox1.Items.Add(Path.GetFileName(item));
                }
                if(musicFiles.Count > 0)
                {
                    btnStart.Enabled = true;
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(isPaused)
            {
                musicPlayer.Ctlcontrols.pause();
                isPaused = true;
            }
            else
            {
                musicPlayer.Ctlcontrols.play();
                isPaused = false;
            }
        }
        private string FormatTime(double seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"mm\:ss");
        }

        

        private void volumeBar_Scroll(object sender, ScrollEventArgs e)
        {
            musicPlayer.settings.volume = volumeBar.Value;
        }

        private void musicPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                int nextIndex = listBox1.SelectedIndex += 1;
                if (nextIndex < musicFiles.Count)
                {
                    listBox1.SelectedIndex = nextIndex;
                    currentSong = musicFiles[nextIndex];
                    musicPlayer.URL = currentSong;
                    musicPlayer.Ctlcontrols.play();
                    isPaused = false;
                }
                else
                {
                    musicPlayer.Ctlcontrols.stop();
                    isPaused = false;
                }
            }
        }
    }
}
