using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace AsotTagger
{
    public class CuenationHelper
    {
        private List<AsotTrack> _listAsotTracks = new List<AsotTrack>();

        public CuenationHelper(AsotTrack track)
        {
            this._listAsotTracks.Add(track);
        }

        public CuenationHelper(List<AsotTrack> tracks)
        {
            this._listAsotTracks = tracks;
        }

        public void DownloadCue()
        {
            if (_listAsotTracks.Count > 0)
            {
                string tempFile = Path.Combine(Path.GetTempPath(), string.Format("cuenation-{0}.html", DateTime.Now.ToString("yyyyMMdd")));

                if (!File.Exists(tempFile))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("referer", "http://cuenation.com/");
                        string htmlCode = client.DownloadString("http://cuenation.com/?page=cues&folder=asot");
                        File.WriteAllText(tempFile, htmlCode);
                    }
                }

                foreach (AsotTrack track in _listAsotTracks)
                {
                    using (StreamReader sr = new StreamReader(tempFile))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string line = sr.ReadLine();

                            if (line.IndexOf(track.EpisodeNumber) != -1)
                            {
                                // this limits to PS releases
                                // <a href=\"(?<Download>.+?ps\.cue)\".+<a href=\"(?<Page>.+?)\"
                                string regexp = "<a href=\"(?<Download>.+?)\".+<a href=\"(?<Page>.+?)\"";
                                Match match = Regex.Match(line, regexp);
                                if (match.Success)
                                {
                                    string www = "http://cuenation.com";
                                    string cuePath = www + "/" + HttpUtility.HtmlDecode(match.Groups["Download"].Value);
                                    string refererPage = www + HttpUtility.HtmlDecode(match.Groups["Page"].Value);

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Headers.Add("referer", refererPage);
                                        if (!Directory.Exists(track.Folder))
                                            Directory.CreateDirectory(track.Folder);

                                        client.DownloadFile(cuePath, track.CueFilePath);
                                        Console.WriteLine(string.Format("Downloaded {0}", track.CueFilePath));

                                        if (File.Exists(track.CueFilePath))
                                        {
                                            CueSharp.CueSheet cue = new CueSharp.CueSheet(track.CueFilePath);
                                            cue.Title = track.AlbumName;
                                            cue.Comments = new string[] { "Downloaded from " + cuePath };
                                            cue.Tracks[0].DataFile = new CueSharp.AudioFile(track.FileName, CueSharp.FileType.MP3);
                                            cue.SaveCue(track.CueFilePath);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}