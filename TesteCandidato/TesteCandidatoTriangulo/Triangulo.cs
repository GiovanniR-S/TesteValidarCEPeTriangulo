using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCandidatoTriangulo
{
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// Um elemento somente pode ser somado com um dos dois elementos da próxima linha. Como o elemento 5 na Linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// Neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// Seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo (string dadosTriangulo) {
            
            string[] linhas = dadosTriangulo.Split(new string[] { "\r\n", "\n", "[]" }, StringSplitOptions.RemoveEmptyEntries);
            int[][] triangulo = new int[linhas.Length][];
            for(int i = 0; i < linhas.Length; i++) {
                string[] elementos = linhas[i].Split(' ');
                triangulo[i] = new int[elementos.Length];
                for(int j = 0; j < elementos.Length; j++) {
                    triangulo[i][j] = int.Parse(elementos[j]);
                }
            }

            int[][] tabela = new int[triangulo.Length][];
            for(int i = 0; i < triangulo.Length; i++) {
                tabela[i] = new int[triangulo[i].Length];
            }

            tabela[0][0] = triangulo[0][0];
  
            for(int i = 1; i < triangulo.Length; i++) {
                for(int j = 0; j < triangulo[i].Length; j++) {
                    int valorEsquerda = (j > 0) ? tabela[i - 1][j - 1] : 0;
                    int valorDireita = (j < triangulo[i - 1].Length) ? tabela[i - 1][j] : 0;
                    tabela[i][j] = triangulo[i][j] + Math.Max(valorEsquerda, valorDireita);
                }
            }

            int valorMaximo = 0;
            for(int j = 0; j < tabela[triangulo.Length - 1].Length; j++) {
                valorMaximo = Math.Max(valorMaximo, tabela[triangulo.Length - 1][j]);
            }

            return valorMaximo;
        }
    }
}
