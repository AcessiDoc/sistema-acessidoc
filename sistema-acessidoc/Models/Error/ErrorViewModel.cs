using sistema_acessidoc;
using sistema_acessidoc.Models;
using sistema_acessidoc.Models.Error;

namespace sistema_acessidoc.Models.Error
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
