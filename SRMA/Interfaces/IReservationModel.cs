using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IReservationModel
    {
        public List<ReservationEntity> GetReservationsByUser(long q);
        public int InsertReservationByClient(ReservationEntity entity, long q);
        public List<ReservationEntity> ListReservations();
        public ReservationEntity? GetReservationsById(long q);
        public ReservationEntity? UpdateReservation(ReservationEntity entity, long q);
        public ReservationEntity? DeleteReser(long IdReservation);
    }
}
