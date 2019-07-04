using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculadoraFinanceiraWPF
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        double valorInvestido = 0;
        int mesesPeriodo = 0;
        double porcentagemInvestidoPoupanca = 0;
        double porcentagemInvestidoRendaFixa = 0;
        double resultadoPoupanca = 0;
        double resultadoRendaFixa = 0;
        double resultado = 0;
        string mesPlural = string.Empty;
        string melhorInvestimento = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            valorInvestido = double.Parse(txtValorInvestir.Text);
            mesesPeriodo = int.Parse(txtNumeroMeses.Text);
            porcentagemInvestidoPoupanca = double.Parse(txtPorcentagemPoupanca.Text);

            mesPlural = Meses(mesesPeriodo);

            resultadoPoupanca = Calcular(valorInvestido, porcentagemInvestidoPoupanca, mesesPeriodo);

            lblResultadoPoupanca.Content = string.Format("{0} Total numa Aplicação de duração de {1} {2} Investido.", resultadoPoupanca.ToString("c"), mesesPeriodo, mesPlural);

            valorInvestido = double.Parse(txtValorInvestir.Text);
            mesesPeriodo = int.Parse(txtNumeroMeses.Text);
            porcentagemInvestidoRendaFixa = double.Parse(txtPorcentagemRendaFixa.Text);

            mesPlural = Meses(mesesPeriodo);

            resultadoRendaFixa = Calcular(valorInvestido, porcentagemInvestidoRendaFixa, mesesPeriodo);

            double imposto = Imposto(mesesPeriodo, valorInvestido, resultadoRendaFixa);
            double rendaFixaComDescontoDoImposto = resultadoRendaFixa - imposto;

            lblResultadoRendaFixa.Content = string.Format("{0} Total da Renda Fixa, sendo {1} a ser retirado no Periodo de {2} {3} \ne o valor do Imposto de Renda é: {4}", resultadoRendaFixa.ToString("c"), rendaFixaComDescontoDoImposto.ToString("c"), mesesPeriodo, mesPlural, imposto.ToString("c"));

            melhorInvestimento = MelhorRendimento(resultadoPoupanca, resultadoRendaFixa);

            lblResultadoRendimento.Content = melhorInvestimento;
        }

        private void RbRendaFixa_Checked(object sender, RoutedEventArgs e)
        {
            valorInvestido = double.Parse(txtValorInvestir.Text);
            mesesPeriodo = int.Parse(txtNumeroMeses.Text);
            porcentagemInvestidoRendaFixa = double.Parse(txtPorcentagemRendaFixa.Text);

            mesPlural = Meses(mesesPeriodo);

            resultadoRendaFixa = Calcular(valorInvestido, porcentagemInvestidoRendaFixa, mesesPeriodo);

            double imposto = Imposto(mesesPeriodo, valorInvestido, resultadoRendaFixa);
            double rendaFixaComDescontoDoImposto = resultadoRendaFixa - imposto;

            lblResultadoRendaFixa.Content = string.Format("{0} Total da Renda Fixa, sendo {1} a ser retirado no Periodo de {2} {3} \ne o valor do Imposto de Renda é: {4}", resultadoRendaFixa.ToString("c"), rendaFixaComDescontoDoImposto.ToString("c"), mesesPeriodo, mesPlural, imposto.ToString("c"));
        }

        private void RbPoupanca_Checked(object sender, RoutedEventArgs e)
        {
            valorInvestido = double.Parse(txtValorInvestir.Text);
            mesesPeriodo = int.Parse(txtNumeroMeses.Text);
            porcentagemInvestidoPoupanca = double.Parse(txtPorcentagemPoupanca.Text);

            mesPlural = Meses(mesesPeriodo);

            resultadoPoupanca = Calcular(valorInvestido, porcentagemInvestidoPoupanca, mesesPeriodo);

            lblResultadoPoupanca.Content = string.Format("{0} Total da Poupança numa Aplicação de duração de {1} {2} Investido.", resultadoPoupanca.ToString("c"), mesesPeriodo, mesPlural);
        }

        private double Calcular(double valorInvestido, double porcentagem, int mesesPeriodo)
        {
            porcentagem = porcentagem / 100;

            for (int i = 1; i <= mesesPeriodo; i++)
            {
                if (i == 1)
                {
                    resultado = (valorInvestido * porcentagem) + valorInvestido;
                }

                else
                {
                    resultado = (resultado * porcentagem) + resultado;
                }
            }

            return resultado;
        }

        private string Meses(int totalMes)
        {
            if (totalMes == 1)
            {
                mesPlural = "mês";
            }

            else
            {
                mesPlural = "meses";
            }

            return mesPlural;
        }

        private string MelhorRendimento(double poupanca, double rendaFixa)
        {
            if (poupanca > rendaFixa)
            {
                return "Poupança";
            }

            else
            {
                return "Renda Fixa";
            }
        }

        private double Imposto(int mesesPeriodo, double valorInvestido, double resultadoRendaFixa)
        {
            double ValorRendimento = resultadoRendaFixa - valorInvestido;
            double imposto = 0;

            if (mesesPeriodo <= 12)
            {
                imposto = ValorRendimento * 0.025;
            }

            else if (mesesPeriodo > 12 && mesesPeriodo <= 24)
            {
                imposto = ValorRendimento * 0.015;
            }

            else if (mesesPeriodo > 24 && mesesPeriodo <= 36)
            {
                imposto = ValorRendimento * 0.005;
            }

            else
            {
                imposto = ValorRendimento * 0.001;
            }

            return imposto;
        }
    }
}
