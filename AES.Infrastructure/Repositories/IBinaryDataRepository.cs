using System;
using System.Linq;
using AES.Domain.Course;

namespace AES.Infrastructure;

public interface IBinaryDataRepository
{
    BinaryData Get(Guid id);
    void Save(BinaryData entity);
    void Delete(BinaryData entity);
    IQueryable<BinaryData> GetQuery();
}