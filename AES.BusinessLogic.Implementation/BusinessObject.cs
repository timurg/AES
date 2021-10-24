using AES.Infrastructure;

namespace AES.BusinessLogic.Implementation
{

    /// <summary>
    /// Базовый класс для всех объектов бизнесс-логики
    /// </summary>
    public abstract class BusinessObject
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        public BusinessObject(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
    }
}
