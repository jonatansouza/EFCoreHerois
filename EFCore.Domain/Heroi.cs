using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Domain {
    public class Heroi {
        public int Id{ get; set; }
        public string Nome { get; set; }

        public IdentidadeSecreta Identidade { get; set; }

        public IEnumerable<Arma> Armas { get; set; }
        public IEnumerable<HeroiBatalha> HeroisBatalhas { get; set; }
    }
}
