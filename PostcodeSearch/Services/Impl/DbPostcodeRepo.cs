using PostcodeSearch.Db;
using PostcodeSearch.Db.Models;
using System.Device.Location;
using System.IO.Compression;
using TinyCsvParser;
using System.Globalization;
using PostcodeSearch.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PostcodeSearch.Services.Impl
{
    public class DbPostcodeRepo : IPostcodeRepo
    {
        private readonly PostcodeDbContext db;

        private const string ContentDirectory = "C:\\WordshopTest\\";

        public DbPostcodeRepo(PostcodeDbContext db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public List<string> Search(string partialPostcode)
        {
            var sanitisedPostcode = partialPostcode.ToUpper().Replace(" ", string.Empty);
            return this.db.Postcodes.Where(p => (p.Postcode.Replace(" ", string.Empty) ?? string.Empty).ToUpper().Contains(sanitisedPostcode)).Select(p => p.Postcode).ToList();
        }

        /// <inheritdoc/>
        public List<string> Search(double latitude, double longitude, double distanceKM)
        {
            var geoCoord = new GeoCoordinate(latitude, longitude);
            return this.db.Postcodes.Where(p => p.Latitude < 90).ToList().Where(p => geoCoord.GetDistanceTo(new GeoCoordinate(p.Latitude.Value, p.Longitude.Value)) / 1000 < distanceKM).Select(p => p.Postcode).ToList();
        }

        /// <inheritdoc/>
        public void Import()
        {
            //CreateOrCleanDirectory();
            //var zipFile = DownloadZip();

            var zipFile = ContentDirectory + "2021-08.zip";
            //ExtractZipFile(zipFile);
            ImportToDatabase();
        }

        /// <summary>
        /// Creates directory if required to download postcodes to, and deletes any existing files in that directory
        /// </summary>
        private void CreateOrCleanDirectory()
        {
            if (!Directory.Exists(ContentDirectory))
            {
                Directory.CreateDirectory(ContentDirectory);
            }

            var directory = new DirectoryInfo(ContentDirectory);
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Downloads postcode zip file from web
        /// </summary>
        /// <returns>File path of downloaded zip file</returns>
        private string DownloadZip()
        {
            {
                var postcodesZip = "https://parlvid.mysociety.org/os/ONSPD/2021-08.zip";
                var fileName = "postcodes.zip";

                using (HttpClient client = new HttpClient())
                {
                    using (var response = client.GetAsync(postcodesZip).Result)
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            using (var file = File.OpenWrite(fileName))
                            {
                                stream.CopyTo(file);
                            }
                        }
                    }
                    return ContentDirectory + fileName;
                }
            }
        }

        /// <summary>
        /// Extracts zip file
        /// </summary>
        /// <param name="filename">Zip file to extract</param>
        private void ExtractZipFile(string filename)
        {
            ZipFile.ExtractToDirectory(filename, ContentDirectory + "2021-08");
        }

        /// <summary>
        /// Imports postcodes into database
        /// </summary>
        private void ImportToDatabase()
        {
            this.db.Database.ExecuteSqlRaw("Delete From Postcodes");
            
            foreach (var postcodeAreaFile in Directory.GetFiles(ContentDirectory + "2021-08\\Data\\multi_csv"))
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
                PostcodeMapping csvMapper = new PostcodeMapping();
                CsvParser<PostcodeEntry> csvParser = new CsvParser<PostcodeEntry>(csvParserOptions, csvMapper);

                var postcodes = csvParser.ReadFromFile(postcodeAreaFile, Encoding.ASCII).ToList();
                this.db.AddRange(postcodes.Select(p => p.Result));
                this.db.SaveChanges();
            }
        }
    }
}
