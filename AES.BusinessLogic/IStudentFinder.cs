using AES.Domain;

namespace AES.BusinessLogic;

public interface IStudentFinder
{
    Student FindByAgreementNumber(string agreementNumber);
}