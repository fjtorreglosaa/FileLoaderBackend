using ImagineApps.Application.Features.Bank.Queries.GetBankById;
using ImagineApps.Application.Features.FileHandler.Contracts;
using ImagineApps.Application.Utilities;
using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Application.Utilities.Helpers;
using ImagineApps.Infrastructure.ExternalResources.Contracts;
using MediatR;

namespace ImagineApps.Application.Features.FileHandler.Commands
{
    public class TXTHandler : ITXTHandler
    {
        protected IMediator _Mediator;
        protected ITXT _TXT;

        public TXTHandler(IMediator mediator, ITXT txt)
        {
            _Mediator = mediator;
            _TXT = txt;
        }

        public async Task<(FileInformationOutputDto Result, string ErrorMessage)> GetOutputs(BaseOutputDataDto output)
        {
            if (output.ErrorMessage == null)
            {
                var fileNormalizedData = TXTHelper.NormalizeInputData(output.Data);
                var bank = await _Mediator.Send(new GetBankByIdQuery { BankId = fileNormalizedData.Header.BankId });
                var outputData = TXTHelper.GetOutputData(fileNormalizedData, bank);

                if (outputData.ErrorMessage != null)
                {
                    return (null, outputData.ErrorMessage);
                }

                return (outputData.Result, null);
            }

            return (null, "Unexpected failure, the file could not be processed.");
        }

        public string OutputResult(FinalOutputDataDto data)
        {
            if (data.ErrorMessage != null) return data.ErrorMessage;

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            folder = Path.Combine(folder, "Downloads");
            var filename = $"{data.Result.FileName}{StringConstants.TXT_EXTENSION}";
            var detination = $"{folder}\\{filename}";

            if (File.Exists(detination))
            {
                string[] files = Directory.GetFiles(folder);
                var duplicates = files.Where(x => x.Contains(data.Result.FileName)).Count();
                filename = $"{data.Result.FileName} ({duplicates + 1}){StringConstants.TXT_EXTENSION}";
                detination = $"{folder}\\{filename}";
            }

            try
            {
                using (var file = new StreamWriter(detination))
                {
                    foreach (var line in data.Result.FileLines)
                    {
                        file.WriteLine(line);
                    }
                }

                return $"File created successfully. Check the downloads folder for the file named {filename}";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
