using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IUtilities
    {
        public string ArmarHTML(UserEntity datos, string claveTemporal);
        public string GenerarCodigo();
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
        public string Encrypt(string texto);
        public string Decrypt(string texto);

    }
}
