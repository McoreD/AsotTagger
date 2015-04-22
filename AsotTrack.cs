using ShareX.HelpersLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AsotTagger
{
    public class AsotTrack
    {
        public string EpisodeNumber { get; set; }

        [DefaultValue(1)]
        public uint DiscNumber { get; set; }

        public string DateString { get; set; }

        public string Location { get; set; }

        public string OriginalLocation { get; private set; }

        public string AlbumArtist { get; set; }

        public AsotTrack()
        {
            this.ApplyDefaultPropertyValues();
        }

        public AsotTrack(string location)
        {
            this.AlbumArtist = "Armin van Buuren";
            this.Location = location;
            this.OriginalLocation = this.Location;
        }

        public void ChangeDirectoryTo(string dir)
        {
            this.Location = Path.Combine(dir, FileName);
        }

        public void AddToLibrary()
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            ThreadPool.QueueUserWorkItem(state => CopyFileAndUpdateTags());
            ThreadPool.QueueUserWorkItem(state => DownloadCue());
        }

        private void CopyFileAndUpdateTags()
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            if (OriginalLocation != Location)
                File.Copy(OriginalLocation, Location, true);

            UpdateTags();
        }

        /// <summary>
        /// Update the mp3 files based on the DiscCount, Episode Number and Episode Date
        /// </summary>
        /// <param name="fp">File path of the mp3 file</param>
        /// <param name="discCount">DiscCount determined by regex</param>
        /// <param name="epNum">Episode number determined by regex</param>
        /// <param name="epDate">Episode date determined by regex</param>
        private void UpdateTags()
        {
            TagLib.File f = TagLib.File.Create(this.Location);

            if (this.AlbumName != f.Tag.Album)
            {
                f.Tag.Track = 1;
                f.Tag.Title = this.AlbumName;
                f.Tag.Performers = new string[] { "Armin van Buuren" };
                f.Tag.AlbumArtists = new string[] { "Armin van Buuren" };
                f.Tag.Genres = new string[] { "Trance/ASOT" };
                f.Tag.Album = this.AlbumName;
                f.Tag.Comment = this.DateString; // use foobar2000 to fill this in Date as yyyy-MM-dd
                f.Tag.Disc = this.DiscNumber;
            }
            f.Save();
        }

        private void DownloadCue()
        {
            new CuenationHelper(this).DownloadCue();
        }

        public string Folder
        {
            get
            {
                return Path.GetDirectoryName(this.Location);
            }
        }

        public string AlbumName
        {
            get
            {
                if (string.IsNullOrEmpty(EpisodeNumber))
                    throw new Exception("EpisodeNumber is empty!");

                return string.Format("A State of Trance {0}", EpisodeNumber);
            }
        }

        public string CueFilePath
        {
            get
            {
                return Path.Combine(Folder, FileNameWithoutExtension + ".cue");
            }
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(this.Location);
            }
        }

        public string FileNameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.Location);
            }
        }

        internal void ReadTagsFromFile()
        {
            TagLib.File f = TagLib.File.Create(this.Location);
            this.EpisodeNumber = Regex.Match(f.Tag.Album, "([1-9][0-9]*)").Value.PadLeft(3, '0');
            this.DateString = f.Tag.Year.ToString();
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}