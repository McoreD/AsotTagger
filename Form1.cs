using AsotTagger.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AsotTagger
{
    public partial class Form1 : Form
    {
        private List<AsotTrack> _listAsotTracks = new List<AsotTrack>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ASOT 002 - (2001-06-08) - Part 1");
            sb.AppendLine("ASOT_000-Armin_Van_Buuren_-_A_State_Of_Trance_(2001-05-18).part2");
            sb.AppendLine("01-armin_van_buuren_-_a_state_of_trance_326_(di.fm)-11-15-2007-tt");
            sb.AppendLine("01-armin_van_Buuren_-_a_state_of_trance_episode_445-(di.fm)-net-25-02-2010-rtm");
            sb.AppendLine("01-armin_van_buuren_-_a_state_of_trance_episode_446-(di.fm)-net-2010-03-04-ps");
            sb.AppendLine("01-armin_van_buuren_-_a_state_of_trance_504-sbd-14-04-2011-tt");
            sb.AppendLine("01-armin_van_buuren_-_a_state_of_trance_509-sbd-05-19-2011");
            sb.AppendLine("01-armin_van_buuren_-_a_state_of_trance_745-sat-12-24-2015-talion");
            lblFileNames.Text = sb.ToString();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            List<AsotTrack> tracks = LoadFiles(paths);

            // 01-armin_van_buuren_-_a_state_of_trance_560_(di.fm)_10-05-2012-tt

            string[] regexps = new string[]
            {
                @".+ (?<Number>\d+) - \((?<Date>.+?)\)(?: - .*Part (?<Part>\d+))*",
                @"ASOT_(?<Number>\d+).+?\((?<Date>(?:\d|-)+?)\)(?:\.part(?<Part>\d+))*",
                @"01-armin_van_buuren_-_a_state_of_trance_(?<Number>\d+)_\(di\.fm\)_+(?<Day>\d+)-(?<Month>\d+)-(?<Year>\d+)-tt",
                @"01-armin_van_Buuren_-_a_state_of_trance_episode_(?<Number>\d+)-\(di.fm\)-net-(?<Day>\d+)-(?<Month>\d+)-(?<Year>\d+)-rtm",
                @"01-armin_van_buuren_-_a_state_of_trance_episode_(?<Number>\d+)-\(di.fm\)-net-(?<Year>\d+)-(?<Month>\d+)-(?<Day>\d+)-ps",
                @"01-armin_van_Buuren_-_a_state_of_trance_(?<Number>\d+)-sbd-(?<Day>\d+)-(?<Month>\d+)-(?<Year>\d+)-tt",
                @"01-armin_van_buuren_-_a_state_of_trance_(?<Number>\d+)-sat-(?<Month>\d+)-(?<Day>\d+)-(?<Year>\d+)-talion",
                @"01-armin_van_Buuren_-_a_state_of_trance_(?<Number>\d+)-sbd-(?<Month>\d+)-(?<Day>\d+)-(?<Year>\d+)"
            };

            foreach (AsotTrack track in tracks)
            {
                Match match = null;
                foreach (string regexp in regexps)
                {
                    match = Regex.Match(track.FileNameWithoutExtension, regexp, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                    if (match.Success)
                    {
                        track.EpisodeNumber = match.Groups["Number"].Value;

                        int month, day;
                        int.TryParse(match.Groups["Month"].Value, out month);
                        int.TryParse(match.Groups["Day"].Value, out day);
                        string epMonth = month > 12 ? day.ToString("00") : month.ToString("00");
                        string epDay = month > 12 ? month.ToString("00") : day.ToString("00");
                        string epYear = match.Groups["Year"].Value;
                        track.DateString = string.IsNullOrEmpty(epDay) ? match.Groups["Date"].Value : string.Format("{0}-{1}-{2}", epYear, epMonth, epDay);

                        uint discNumber = 1;
                        uint.TryParse(match.Groups["Part"].Value, out discNumber);
                        track.DiscNumber = discNumber > 0 ? discNumber : 1;

                        _listAsotTracks.Add(track);

                        break;
                    }
                }

                if (!match.Success)
                {
                    // attempt to read the details using file tags
                    track.ReadTagsFromFile();
                    _listAsotTracks.Add(track);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private List<AsotTrack> LoadFiles(string[] paths)
        {
            _listAsotTracks.Clear();
            lbFiles.Items.Clear();

            List<AsotTrack> tracks = new List<AsotTrack>();

            foreach (string dirOrFile in paths)
            {
                if (Directory.Exists(dirOrFile))
                {
                    foreach (string fp in Directory.GetFiles(dirOrFile, "*.mp3", SearchOption.AllDirectories))
                    {
                        tracks.Add(new AsotTrack(fp));
                    }
                }
                else if (File.Exists(dirOrFile) && Path.GetExtension(dirOrFile).ToLower() == ".mp3")
                {
                    tracks.Add(new AsotTrack(dirOrFile));
                }
            }

            foreach (AsotTrack track in tracks)
            {
                lbFiles.Items.Add(track);
            }

            return tracks;
        }

        private void AddToLibrary()
        {
            foreach (AsotTrack track in _listAsotTracks)
            {
                string dirDest = Path.Combine(txtDest.Text, track.AlbumName);
                track.ChangeDirectoryTo(dirDest);

                track.AddToLibrary();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            AddToLibrary();
        }

        private void chkCopyDest_CheckedChanged(object sender, EventArgs e)
        {
            // btnCopy.Text = chkCopyDest.Checked ? "&Copy and Update tags" : "&Update tags";
        }

        private void btnDownloadCue_Click(object sender, EventArgs e)
        {
            CuenationHelper scraper = new CuenationHelper(_listAsotTracks);
            scraper.DownloadCue();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex > -1)
            {
                pgTrack.SelectedObject = lbFiles.SelectedItem as AsotTrack;
            }
        }
    }
}