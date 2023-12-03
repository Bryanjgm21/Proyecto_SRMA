using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IFidelityProModel
    {
        public FidelityProEntity? ConsultPoints(long q);
        public FidelityProEntity? InsertP(long q, int pQty);
        public FidelityProEntity? RedeemP(string Code, int pQty);


    }
}
