using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IReservationModel
    {
        List<ReservationEntity> GetReservationsByUser(long q);
        public int InsertReservationByClient(ReservationEntity entity, long q);
    }
}
