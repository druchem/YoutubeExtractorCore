using System;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeExtractorCore.Example
{
    public class Program
    {
        public static void Main()
        {
            RunExample().GetAwaiter().GetResult();
        }

        private static async Task RunExample()
        {
            const string videoUrl = "https://www.youtube.com/watch?v=6tjHBbvKd3M";
            try
            {
                var videoInfos = await DownloadUrlResolver.GetDownloadUrlsAsync(videoUrl, false);
                var videoWithAudio =
                    videoInfos.FirstOrDefault(video => video.Resolution > 0 && video.AudioBitrate > 0);

                if (videoWithAudio != null)
                {
                    Console.WriteLine($"Video title:{videoWithAudio.Title}");
                    Console.WriteLine($"Video url:{videoWithAudio.DownloadUrl}");
                }
                else
                {
                    Console.WriteLine("Video with audio not found");
                }
            }
            catch (YoutubeVideoNotAvailableException)
            {
                Console.WriteLine("Video is not available");
            }
            catch (YoutubeParseException)
            {
                Console.WriteLine("Error while trying to parse youtube data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}