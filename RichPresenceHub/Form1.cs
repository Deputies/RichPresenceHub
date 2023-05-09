using System;
using DiscordRPC;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Button = DiscordRPC.Button;

namespace RichPresenceHub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void StartBtn_Click(object sender, EventArgs e)
        {
            // Initialize the Discord rich presence client.
            var client = new DiscordRpcClient(IDTxt.Text);
            client.Initialize();

            // Set the initial rich presence information to display on Discord.
            var presence = new RichPresence
            {
                Details = details.Text,
                State = state.Text,
                Party = new Party()
                {
                    ID = PartyIDTxt.Text,
                    Size = Convert.ToInt16(MinPlayers.Text),
                    Max = Convert.ToInt16(MaxPlayers.Text)
                },
                Timestamps = Timestamps.Now,
                Assets = new Assets
                {
                    LargeImageKey = LargeImg.Text,
                    LargeImageText = LargeImgTxt.Text,
                    SmallImageKey = SmallImg.Text,
                    SmallImageText = SmallImgTxt.Text,

                },
                Buttons = new Button[]
                {
                    new Button
                    {
                        Label = Btn1Txt.Text,
                        Url = Btn1URL.Text,
                    },
                    new Button
                    {
                        Label = Btn2Txt.Text,
                        Url = Btn2URL.Text,
                    }

                }
            };
            client.SetPresence(presence);

            // Start the timer to track elapsed time.
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Update the rich presence information every 10 seconds.
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                presence.Timestamps = new Timestamps
                {
                    Start = DateTime.UtcNow - stopwatch.Elapsed,
                    End = DateTime.Parse(TimeTxt.Text)
                };
                client.SetPresence(presence);
            }
        }
    }
}
