using System;

namespace AES.Domain.Course;

public class BinaryData : DomainObject
{
    public byte[] Bits { get; set; }

    public static BinaryData CreateFromFile(Guid id, string fileName)
    {
        return new BinaryData()
        {
            Bits = System.IO.File.ReadAllBytes(fileName),
            Id = id
        };
    }
}