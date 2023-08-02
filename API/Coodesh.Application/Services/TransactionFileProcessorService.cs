using Coodesh.Application.Interfaces;
using Coodesh.Domain.Entities.Transaction;
using Coodesh.Infrastructure.Models.Transaction;
using Coodesh.Infrastructure.Persistence.Transaction;
using ErrorOr;
using System.Text.RegularExpressions;

namespace Coodesh.Application.Services
{
    public class TransactionFileProcessorService : ITransactionFileProcessorService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionFileProcessorService(ITransactionRepository transactionRepository) => _transactionRepository = transactionRepository;

        public ErrorOr<List<TransactionModel>> ProcessFile(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);

            List<TransactionEntity> transactionLines = ParseTransactionLines(reader);

            if (!transactionLines.Any())
            {
                return Error.Validation("No valid transactions found.");
            }

            List<TransactionModel> transactions = ConvertToTransactionModels(transactionLines);
            return transactions;

        }

        private static List<TransactionEntity> ParseTransactionLines(StreamReader reader)
        {
            string LinePattern = @"^([1-4])(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}-\d{2}:\d{2})(.*?\S)\s+(\d{10})(.*)$";
            var regex = new Regex(LinePattern);

            var transactionLines = new List<TransactionEntity>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                    continue;

                var match = regex.Match(line);

                if (!match.Success)
                {
                    // You can handle invalid lines differently, for example, add to validation result or log errors.
                    continue;
                }

                TransactionEntity transactionLine = ParseTransactionLine(match);
                transactionLines.Add(transactionLine);
            }

            return transactionLines;
        }

        private static TransactionEntity ParseTransactionLine(Match match)
        {
            TransactionType type = (TransactionType)int.Parse(match.Groups[1].Value);
            DateTime date = DateTime.Parse(match.Groups[2].Value);
            string product = match.Groups[3].Value.Trim();
            decimal amount = int.Parse(match.Groups[4].Value);
            string seller = match.Groups[5].Value.Trim();

            return new TransactionEntity(type, date, product, amount, seller);
        }

        private static List<TransactionModel> ConvertToTransactionModels(List<TransactionEntity> transactionLines)
        {
            return transactionLines.Select(domain =>
                new TransactionModel(
                    Guid.NewGuid(),
                    (TransactionTypeModel)domain.Type,
                    domain.Date,
                    domain.Product,
                    domain.Amount,
                    domain.Seller
                )
            ).ToList();
        }
    }
}
