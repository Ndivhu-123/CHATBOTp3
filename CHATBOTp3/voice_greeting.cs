using System;
using System.IO;
using System.Media;
using System.Windows.Media;

namespace CHATBOTp3
{
    public class voice_greeting
    {
        // Constructor: automatically plays the greeting when an object is created
        public voice_greeting()
        {
            //creating an instance for the media class
            MediaPlayer voicegreet = new MediaPlayer();


            //get the path automatical
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;

            //then replace the \\bin\\Debug\\net8.0-windows
            string replaced = fullPath.Replace("\\bin\\Debug\\net8.0-windows", "");

            //combine paths once done replacing
            string combine_path = System.IO.Path.Combine(replaced, "voicegreet.wav");

            //combine the url as uri
            voicegreet.Open(new Uri(combine_path, UriKind.Relative));

            //play sound
            voicegreet.Play();

        }
    }
}
