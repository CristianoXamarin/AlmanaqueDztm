using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1_ConsultarCep.Servico;
using App1_ConsultarCep.Servico.Modelo;

namespace App1_ConsultarCep
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            txtBOTAOCEP.Clicked += BuscarCep;
		}

        //Lógica de pesquisa do CEP, botão BUSCAR CEP.

        private void BuscarCep(object sender, EventArgs args)
        {
            string cep = txtCEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        txtRESULTADO.Text = string.Format("Endereço: {2} de {3} {0},{1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    } else
                    {
                        DisplayAlert("Erro", "O Endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch (Exception e)
                {
                    //Mensagem de Erro
                    DisplayAlert("Erro Crítico", e.Message, "OK");
                }
            } 
        }

        //Validações do botão BUSCAR CEP

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                //Mensagem de Erro
                DisplayAlert("Erro", "Cep Inválido, deve conter 8 caracteres!!!", "OK");

                valido = false;
            }

            int NovoCep = 0;

            if (!int.TryParse(cep, out NovoCep))
            {
                //Mensagem de Erro
                DisplayAlert("Erro", "Cep Inválido, O CEP deve conter somente números!!!", "OK");

                valido = false;
            }

            return valido;
        }
	}
}
