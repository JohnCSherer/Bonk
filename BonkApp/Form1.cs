using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Resources;
using System.Timers;
using System.Windows.Forms;
using System.Diagnostics;

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BonkApp
{
    public partial class Form1 : Form
    {
        private SoundPlayer[] sound;
        private SoundPlayer bonk;
        private double screenSize;
        private int minSize;
        private String[] names;
        private bool displayPitch;

        public static int numForms;
        public static Form1 parent;
        public Form1()
        {

            InitializeComponent();
            sound = new SoundPlayer[13];
            sound[0] = new SoundPlayer(Properties.Resources.BonkC3);
            sound[0].LoadAsync();
            sound[1] = new SoundPlayer(Properties.Resources.BonkCSharp3);
            sound[1].LoadAsync();
            sound[2] = new SoundPlayer(Properties.Resources.BonkD3);
            sound[2].LoadAsync();
            sound[3] = new SoundPlayer(Properties.Resources.BonkDSharp3);
            sound[3].LoadAsync();
            sound[4] = new SoundPlayer(Properties.Resources.BonkE3);
            sound[4].LoadAsync();
            sound[5] = new SoundPlayer(Properties.Resources.BonkF3);
            sound[5].LoadAsync();
            sound[6] = new SoundPlayer(Properties.Resources.BonkFSharp3);
            sound[6].LoadAsync();
            sound[7] = new SoundPlayer(Properties.Resources.BonkG3);
            sound[7].LoadAsync();
            sound[8] = new SoundPlayer(Properties.Resources.BonkGSharp3);
            sound[8].LoadAsync();
            sound[9] = new SoundPlayer(Properties.Resources.BonkA3);
            sound[9].LoadAsync();
            sound[10] = new SoundPlayer(Properties.Resources.BonkASharp3);
            sound[10].LoadAsync();
            sound[11] = new SoundPlayer(Properties.Resources.BonkB3);
            sound[11].LoadAsync();
            sound[12] = new SoundPlayer(Properties.Resources.BonkC4);
            sound[12].LoadAsync();

            names = new String[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", "C" };
            bonk = sound[0];
            screenSize = Screen.PrimaryScreen.Bounds.Width * Screen.PrimaryScreen.Bounds.Height / 2;
            minSize = (this.MinimumSize.Width * this.MinimumSize.Height);
            displayPitch = false;

            if (numForms < 1)
            {
                numForms = 1;
            }

            if (parent is null)
            {
                parent = this;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Text = "" + bonk.IsLoadCompleted;
            bonk.Play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width / Math.Sqrt(4)), 
                                 Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height / Math.Sqrt(4)));
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            int windowSize = (this.Size.Width * this.Size.Height);
            double percent = Math.Sqrt(windowSize / screenSize);
            percent = percent - Math.Sqrt(minSize / screenSize);
            percent = percent / (1 - Math.Sqrt(minSize / screenSize));
            int index = Convert.ToInt32(percent * 13);
            if(index < 0)
            {
                index = 0;
            }else if(index > 12)
            {
                index = 12;
            }
            bonk = sound[12 - index];
            if (displayPitch)
            {
                this.Text = names[12 - index];
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button + "" == "Right")
            {
                Form1 f = new Form1();
                f.Visible = true;
                f.setDisplayPitch(displayPitch);
                f.Size = Size;
                f.Text = Text;
                numForms++;
            }else if (e.Button + "" == "Middle")
            {
                if (displayPitch)
                {
                    displayPitch = false;
                    Text = "";
                }
                else
                {
                    displayPitch = true;
                    Form1_ResizeEnd(sender, e);
                }
            }
        }

        public void setDisplayPitch(bool val)
        {
            displayPitch = val;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.Equals(parent))
            {
                numForms--;
            }
            else
            {
                numForms--;
            }
            if(numForms > 0)
            {
                this.Visible = false;
                this.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                base.OnFormClosing(e);
                Application.Exit();
            }
        }
    }
}
