namespace DiagnosticoAI.Models
{
    public class Paciente
    {
        public string nombre { get; set; }
        public int edad { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string[] sintomas { get; set; }
    }
}
