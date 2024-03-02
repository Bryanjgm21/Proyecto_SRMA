using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IFidelityProModel
    {
        public FidelityProEntity? ConsultPoints(long q);
        public int InsertPoints(long q, int pQty);
        public FidelityProEntity? RedeemPoints(string Code, int pQty);
        public List<FidelityProEntity> GetAllFidelity();

    }
}
