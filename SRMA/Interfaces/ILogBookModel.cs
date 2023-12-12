using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface ILogBookModel
    {
        public int RegisterLogBook(LogBookEntity entity, long q);

    }
}
