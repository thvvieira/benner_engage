using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Benner_Engage.PagamentosService;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Benner_Engage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                var binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 1073741824;
                binding.MaxBufferSize = 1073741824;

                PagamentosServiceClient client = new PagamentosServiceClient(binding, new EndpointAddress("http://testeengage.cloudapp.net/Engage/app_services/PagamentosService.svc"));

                LoginRequest loginRequest = new LoginRequest()
                {
                    BranchHandle = -1,
                    CompanyHandle = -1,
                    UserName = "thiago",
                    Password = "benner"
                };


                LoginResponse loginResponse = await client.LoginAsync(loginRequest);
                   
                //LoginResponse loginResponse = client.Login(getLoginRequest(textBoxUser.Text, textBoxPassword.Text, -1, -1));

                ObterFuncionariosRequest request = new ObterFuncionariosRequest()
                {
                    AuthenticationTokenId = loginResponse.AuthenticationTokenId,
                    Usuario = "thiago"

                };

                ObterFuncionariosResponse response = null;
                try
                {
                    response = await client.ObterFuncionariosAsync(request);
                    foreach (var funcionario in response.Funcionarios)
                    {
                        //if (funcionario == null)
                        //    throw new Exception("A");

                        if (funcionario.Foto != null)
                        {

                            var bytesImagem = new byte[funcionario.Foto.Length - 4];
                            Array.Copy(funcionario.Foto, 4, bytesImagem, 0, funcionario.Foto.Length - 4);
                            funcionario.Foto = bytesImagem;

                            //var fs = new FileStream("D:\\a" + funcionario.Handle.ToString() + ".jpg", FileMode.Create);
                            //fs.Write(funcionario.Foto, 0, funcionario.Foto.Count());
                            //fs.Close();

                            //var memoria = new MemoryStream(bytesImagem);
                            //var foto = Image

                            InMemoryRandomAccessStream ras = new InMemoryRandomAccessStream();
                            DataWriter dw = new DataWriter(ras.GetOutputStreamAt(0));
                            dw.WriteBytes(funcionario.Foto);
                            await dw.StoreAsync();

                            BitmapImage bImg = new BitmapImage();
                            bImg.SetSource(ras);
                            foto.Source = bImg;
                        }
                        else
                        {
                            foto.Source = new BitmapImage() { UriSource = new Uri("ms-appx:///Assets/img-login.png", UriKind.Absolute) };
                        }

                        txtNome.Text = funcionario.Nome.ToString();
                            dtAdmissao.Text = funcionario.DataAdmissao.ToString();
                            dtNascimento.Text = funcionario.DataNascimento.ToString();
                            tipoFunc.Text = funcionario.TipoColaborador.ToString();
                            viaja.IsChecked = (funcionario.Viaja == "S");
                        }
                    

                }
                finally
                {
                    client.LogoutAsync(getLogOutRequest(loginResponse.AuthenticationTokenId));
                }

                //infoFuncionarios.ItemsSource = response.Funcionarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public LoginRequest getLoginRequest(string pUserName, string pPassword, int pCompanyHandle, int pBranchHandle)
        {
            return new LoginRequest()
            {
                UserName = pUserName,
                Password = pPassword,
                CompanyHandle = pCompanyHandle,
                BranchHandle = pBranchHandle
            };
        }

        public LogoutRequest getLogOutRequest(string pAuthenticationTokenId)
        {
            return new LogoutRequest() { AuthenticationTokenId = pAuthenticationTokenId };
        }

        
    }


}
