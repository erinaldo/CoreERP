using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public class GOPERITEMPRODRELAC
    {
        public GOPERITEMPRODRELAC() { }

        public int CODEMPRESAORIGEM { get; set; }
        public string CODPRODUTOORIGEM { get; set; }
        public int CODOPERORIGEM { get; set; }
        public int NSEQITEMORIGEM { get; set; }
        public int CODEMPRESADESTINO { get; set; }
        public string CODPRODUTODESTINO { get; set; }
        public int CODOPERDESTINO { get; set; }
        public int NSEQITEMDESTINO { get; set; }

        /// <summary>
        /// Inseri o relacionamento na tabela GOPERITEMPRODRELAC
        /// </summary>
        /// <param name="conn">Conexão da AppLib</param>
        /// <param name="gOperItemProdRelac">Objeto</param>
        /// <returns></returns>
        public bool setRelacionamento(AppLib.Data.Connection conn, GOPERITEMPRODRELAC gOperItemProdRelac)
        {
            try
            {
                conn.ExecTransaction("INSERT INTO GOPERITEMPRODRELAC (CODEMPRESAORIGEM, CODPRODUTOORIGEM, CODOPERORIGEM, NSEQITEMORIGEM, CODEMPRESADESTINO, CODPRODUTODESTINO, CODOPERDESTINO, NSEQITEMDESTINO) VALUES (?, ?, ?, ?, ?, ?, ?, ?)", new object[] { gOperItemProdRelac.CODEMPRESAORIGEM, gOperItemProdRelac.CODPRODUTOORIGEM, gOperItemProdRelac.CODOPERORIGEM, gOperItemProdRelac.NSEQITEMORIGEM, gOperItemProdRelac.CODEMPRESADESTINO, gOperItemProdRelac.CODPRODUTODESTINO, gOperItemProdRelac.CODOPERDESTINO, gOperItemProdRelac.NSEQITEMDESTINO });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
