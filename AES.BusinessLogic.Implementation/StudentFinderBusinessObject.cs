using AES.Domain;
using AES.Infrastructure;

namespace AES.BusinessLogic.Implementation;

public class StudentFinderBusinessObject : BusinessObject, IStudentFinder
{
    public Student FindByAgreementNumber(string agreementNumber)
    {
        throw new System.NotImplementedException();
    }

    public StudentFinderBusinessObject(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}