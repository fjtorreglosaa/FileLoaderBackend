using ImagineApps.Infrastructure.ExternalResources.Contracts;
using System.IO;

namespace ImagineApps.Infrastructure.ExternalResources
{
    public class TXT : ITXT
    {
        public async Task<(List<string> Data, string ErrorMessage)> ReadFile(string path)
        {
			var lines = new List<string>();

			try
			{
				using (var fileReader = new StreamReader(path))
				{
					string line;

					while ((line = fileReader.ReadLine()) != null)
					{
						lines.Add(line);
					}
				}

				return (lines, null);
			}
			catch (Exception ex)
			{
				return (null, $"Unable to read data: {ex.ToString}");
			}
        }

        public async Task<(List<string> Data, string ErrorMessage)> ReadFile(StreamReader reader)
        {
            var lines = new List<string>();

            try
            {
                using (reader)
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                return (lines, null);
            }
            catch (Exception ex)
            {
                return (null, $"Unable to read data: {ex.ToString}");
            }
        }
    }
}
