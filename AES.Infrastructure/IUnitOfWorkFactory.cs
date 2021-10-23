namespace AES.Infrastructure
{
    public interface IUnitOfWorkFactory
    {
        ///<summary>
        /// Создает uow, если у uow не будет вызван метод <see cref="IUnitOfWork.Commit"/>, то автоматически будет выполнен rollback
        ///</summary>
        ///<returns></returns>
        IUnitOfWork Create();
    }
}
