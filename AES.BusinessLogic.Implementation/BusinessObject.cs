using AES.Infrastructure;

namespace AES.BusinessLogic.Implementation
{

    /// <summary>
    /// Базовый класс для всех объектов бизнесс-логики
    /// </summary>
    public abstract class BusinessObject
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        protected BusinessObject(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
    }
}
