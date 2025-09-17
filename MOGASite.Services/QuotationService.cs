using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;

namespace MOGASite.Services
{
    public class QuotationService(IUnitOfWork unitOfWork, IMailService mailService) : IQuotationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMailService _emailService = mailService;

        public async Task<QuotationResponse> AddQuotationAsync(AddQuotationRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. The body cannot be empty.");
            }

            var quotation = new Quotation
            {
                BusinessEmail = request.BusinessEmail,
                CompanyName = request.CompanyName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Notes = request.Notes,
                NumberOfEmployees = request.NumberOfEmployees,
                Phone = request.Phone,
                Service = request.Service,
            };

            _unitOfWork.Repository<Quotation>().Add(quotation);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add quotation.");
            }

            // **Send Email After Quotation is Saved**
            //await SendQuotationEmailAsync(quotation);

            return new QuotationResponse
            {
                Id = quotation.Id,
                BusinessEmail = quotation.BusinessEmail,
                CompanyName = quotation.CompanyName,
                FirstName = quotation.FirstName,
                LastName = quotation.LastName,
                Notes = quotation.Notes,
                NumberOfEmployees = quotation.NumberOfEmployees,
                Phone = quotation.Phone,
                Service = quotation.Service
            };

        }

        public async Task<bool> DeleteQuotationAsync(int id, CancellationToken cancellationToken = default)
        {
            var quotation = await _unitOfWork.Repository<Quotation>().GetByIdAsync(id);

            if (quotation == null)
            {
                throw new Exception("Quotation not found.");
            }

            _unitOfWork.Repository<Quotation>().Delete(quotation);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to delete quotation.");
            }

            return true;
        }

        public async Task<IReadOnlyList<QuotationResponse>> GetQuotationsAsync(CancellationToken cancellationToken = default)
        {
            var quotations = await _unitOfWork.Repository<Quotation>().GetAllAsync(cancellationToken);

            if (quotations == null)
            {
                throw new Exception("No quotations found.");
            }

            return quotations
                .OrderByDescending(x => x.Id)
                .Select(quotation => new QuotationResponse
                {
                    Id = quotation.Id,
                    BusinessEmail = quotation.BusinessEmail,
                    CompanyName = quotation.CompanyName,
                    FirstName = quotation.FirstName,
                    LastName = quotation.LastName,
                    Notes = quotation.Notes,
                    NumberOfEmployees = quotation.NumberOfEmployees,
                    Phone = quotation.Phone,
                    Service = quotation.Service,
                    Date = quotation.CreatedAt.ToShortDateString()

                }).ToList().AsReadOnly();

        }

        private async Task SendQuotationEmailAsync(Quotation quotation)
        {
            string subject = $"New Quotation Submission from {quotation.FirstName} {quotation.LastName}";
            string message = $@"
            <p><strong>First Name:</strong> {quotation.FirstName}</p>
            <p><strong>Last Name:</strong> {quotation.LastName}</p>
            <p><strong>Company Name:</strong> {quotation.CompanyName}</p>
            <p><strong>Number Of Employees:</strong> {quotation.NumberOfEmployees}</p>
            <p><strong>Business Email:</strong> {quotation.BusinessEmail}</p>
            <p><strong>Phone:</strong> {quotation.Phone}</p>
            <p><strong>Service:</strong> {quotation.Service}</p>
            <p><strong>Notes:</strong></p>
            <p>{quotation.Notes}</p>";

            string adminEmail = "sales@mogasoft.net"; // Change to your recipient email

            await _emailService.SendMailAsync(adminEmail, subject, message);
        }
    }
}
