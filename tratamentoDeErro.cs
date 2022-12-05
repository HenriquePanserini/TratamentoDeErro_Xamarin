DataTable dbpbar = Cl_gestor.EXE_QUERY("SELECT * FROM vendas");
int qtdProgress = dbpbar.Rows.Count;

pbar = new Android.App.ProgressDialog();
pbar.setMessage("sincronizando...");
pbar.SetCancelable(false);
pbar.SetProgressStyle(ProgressDialogStyle.Horizontal);
pbar.Progress = 0;
pbar.Max = qtdProgress;
pbar.Show();

int statusBarr = 0;

Cl_gestor.NOM_QUERY("DELETE FROM nvenda");
Cl_gestor.NOM_QUERY("DELETE FROM vendasprd_BKP");
Cl_gestor.NOM_QUERY("DELETE FROM vendas_BKP");

DataTable dados;
DataTable dados1;

DateTime data1 = DateTime.MinValue;

if(vars.web_local){
    dados =  Cl_gestor.EXE_QUERY("SELECT * FROM parametro");
    enderecows = dados.Rows[0]["endereco"].ToString() + "/insert5.php";
    enderecowsporta = dados.Rows[0]["endereco"].ToString();
    enderecowslinha = dados.Rows[0]["endereco"].ToString() + "/insertp5.php";
 }else{
    dados = Cl_gestor.EXE_QUERY("SELECT * FROM parametro");
    enderecows = dados.Rows[0]["caminholocal"].ToString() + "/insert5.php";
    enderecowsporta  = dados.Rows[0]["caminholocal"].ToString();
    enderecowsplinha = dados.Rows[0]["caminholocal"].ToString() + "/insertp5.php";
 }

 List<SQLparametro> parametrox = new List<SQLparametro>();
 parametrox.Add(new SQLparametro("@data", DateTime.MinValue));

 int cv1 = 0;

dados =  Cl_gestor.EXE_QUERY("SELECT * FROM vendas WHERE datasincro == @datax", parametrox);
 + 
Console.WriteLine(" linhas header ------> " + dados.Rows.Count);

int cv = dados.Rows.Count;
int totall = 0;
string codappx = "";
int seqx = 0;

    for(totall = 0; totall < cv;totall++){

        try{
            
            DataTable dadosyx;
            dadosyx = Cl_gestor.EXE_QUERY("SELECT SUM(id_seq) AS id_seqx FROM sincronizar");

            int clientez = 0;
            int32 nnvovo = 1;

            List<SQLparametro> parametroxy = new List<SQLparametro>();
            parametroxy.Add(new SQLparametro("@cli", dados.Rows[totall]["id_cliente"]));

            clientez = Convert.ToInt32(dados.Rows[totall]["id_cliente"]);

            string c = dados.Rows[totall]["nrovendabco"].ToString();

            statusBarra += 1;
            pbar.Progress += statusBarra;
            Thread.Sleep(1000);

            if(string.IsNullOrEmpty(dados.Rows[totall]["nrovendabco"].ToString()) || c == "0"){

                if(clientez != 0){

                    codappx = Convert.ToString(dados.Rows[totall]["id_pedido"]);

                    FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                        
                        new KeyValuePair<String, String>("data", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                        new KeyValuePair<String, String>("codcli", clientez.ToString()),
                        new KeyValuePair<String, String>("bruto", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                        new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados.Rows[totall]["desconto"]).ToString().Replace(",",".")),
                        new KeyValuePair<String, String>("liquido", Convert.ToDecimal(dados.Rows[totall]["total"]).ToString().Replace(",",".")),
                        new KeyValuePair<String, String>("codven", vars.numerovendedor.ToString()),
                        new KeyValuePair<String, String>("codform", dados.Rows[totall]["id_pgto"].ToString()),
                        new KeyValuePair<String, String>("codapp", dados.Rows[totall]["id_pedido"].ToString()),
                        new KeyValuePair<String, String>("dataincapp", Convert.ToDateTime(dados.Rows[totall]["data"]).ToString("yyyy-MM-dd")),
                        new KeyValuePair<String, String>("datasincapp", DateTime.Now.ToString("yyyy-MM-dd")),
                        new KeyValuePair<String, String>("tipopgto", Convert.ToString(dados.Rows[totall]["tipopagto"]))

                    });

                    int pedido = Convert.ToInt32(dados.Rows[totall]["id_pedido"]);

                    Cl_gestor.NOM_QUERY("INSERT INTO nvenda (id_venda) VALUES ( " + pedido + ")");
                    Console.WriteLine("Vendas inseridas -----------------------> " + pedido);

                    HttpClient http = new HttpClient();
                    HttpResponseMessage resposta = http.PostAsync(enderecows, param).GetAwaiter().Getresult();

                    if(respostas.StatusCode == HttpStatusCode.OK){
                        x = true;

                        List<tslv020> dois;
                        List<tslv020> Users;

                        using(var client = new HttpClient())
                            try{

                                string url = enderecowsporta + "/selectultped.php" + "?par=" + vars.numerovendedor.ToString();

                                var response =  await client.GetAsync(url);
                                response.EnsuranceSucessStatusCode();

                                var stringResult = await response.Content.ReadAsStringAsync();

                                tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                vars.numeropedidosalvo = 0;
                                vars.numeropedidosalvo = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                Cl_gestor.NOM_QUERY("UPDATE vendas SET nrovendabco = " + vars.numeropedidosalvo + "WHERE id_pedido = " + pedido);
                            
                            }catch(Excepetion ex){
                                Toast.MakeToast("");
                                return Users = null;
                            }
                        
                        DataTable dados2 = Cl_gestor.EXE_QUERY("SELECT * FROM vendasprd WHERE id_pedido = " + pedido);
                        DataTable dados3 = Cl_gestor.EXE_QUERY("SELECT * FROM Produtos");

                        Console.WriteLine("Lista de linhas --------> " + dados1.Rows.Count);

                        int Count = Convert.ToInt32(dados2.Rows.Count);

                        try{

                            for(int ii = 0; ii < count; ii++){

                                if(string.IsNullOrEmpty(dados2.Rows[ii]["nrovendabco"].ToString()) || dados2.Rows[ii]["nrovendabco"].ToString() == "0"){
                                        
                                        FormUrlEncodedContent param3 = new FormUrlEncodedContent(new[] {
                                               
                                             new KeyValuePair<String, String>("cupom", vars.numeropedidosalvo.ToString()),
                                             new KeyValuePair<String, String>("codprod", Convert.ToInt32(dados2.Rows[ii]["id_produto"]).ToString()),
                                             new KeyValuePair<String, String>("descricao", Convert.ToString(dados2.Rows[ii]["descricao"])),
                                             new KeyValuePair<String, String>("quant", Convert.ToDecimal(dados2.Rows[ii]["quantidade"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("total", Convert.ToDecimal(dados2.Rows[ii]["total"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("desconto", Convert.ToDecimal(dados2.Rows[ii]["desconto"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("uni", dados3.Rows[ii]["uni"].ToString()),
                                             new KeyValuePair<String, String>("unit", Convert.ToDecimal(dados2.Rows[ii]["precodesconto"]).ToString().Replace(",",".")),
                                             new KeyValuePair<String, String>("preco1",Convert.ToDecimal(dados2.Rows[ii]["preco"]).ToStgring().Replace(",",".")),
                                             new KeyValuePair<String, String>("codappp1",dados3.Rows[ii]["codbar"].ToString())

                                        });

                                    int seqx = Convert.ToInt32(dados2.Rows.[ii]["id_pedido"]);

                                    HttpClient http1 = new HttpClient();
                                    HttpResponseMessage resposta1 = http1.PostAsync(enderecowslinha, param3).GetAwaiter().Getresult();

                                    if(response1.StatusCode == HttpStatusCode.OK){

                                        int id_prod = Convert.ToInt32(dados2.Rows[ii]["id_produto"]);

                                        List<SQLparametro> parametro1 = new List<SQLparametro>();

                                        parametro1.Add(new SQLparametro("@pedido", pedido1));
                                        parametro1.Add(new SQLparametro("@data", DateTime.Now));

                                        Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1);

                                        using(var client = new HttpClient())
                                            try{

                                                string url = enderecowsporta + "/selectultped.php" + "?par=" + vars.numeropedidosalvo;
                                                var response = await client.GetAsync(url);
                                                response.EnsuranceSucessStatusCode();

                                                var stringResult = await response.Content.ReadAsStringAsync();

                                                tslv020[] colecaoPegarid =  JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                                vars.numeropedidosalvol = 0;
                                                vars.numeropedidosalvol =  Convert.ToInt32(colecaoPegarid[0].seqmax);

                                                Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = " + vars.numeropedidosalvol + "WHERE seq = " + seqx);
                                            }catch(Exception ex){
                                                Toast.MakeToast("");
                                                Users = null;
                                            }

                                        vars.nbarra += 5;
                                        int q = 1;

                                    }


                                }
                            }

                            string xpedbco =  vars.numeropedidosalvo.ToString();

                            int contalinha = dados2.Rows.Count;

                            using(var cliemt = new HttpClient())
                                try{
                                    
                                    string url = enderecowsporta + "/selectpedido" + "?par=" + xpedbco;
                                    var response = await clientez.GetAsync(url);
                                    response.EnsuranceSucessStatusCode(); 

                                    var stringResult = await response.Content.ReadAsStringAsync();

                                    linped[] colecaoPegarid = JsonConvert.DeserializeObject<linped[]>(stringResult);

                                    vars.maxlinhas = 0;
                                    vars.maxlinhas = Convert.ToInt32(colecaoPegarid[0].totmax);

                                    List<SQLparametro> parametroy = new List<SQLparametro>();
                                    parametroy.Add(new SQLparametro("@datax", DateTime.Minvalue));
                                    parametroy.Add(new SQLparametro("@ped1", pedido));

                                    if(contalinha != vars.maxlinhas){

                                        Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro == @datax, nrovendabco = 0 WHERE id_pedido = @ped1", parametroy);
                                        Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = 0 WHERE id_pedido = @ped1", parametroy);

                                        string url2 = enderecowsporta + "/deletapedido.php?par=" + xpedbco;
                                        var response2 = await clientez.GetAsync(url2);
                                        response2.EnsuranceSucessStatusCode();

                                        var stringResult2 = await response2.Content.ReadAsStringAsync();


                                    }else{

                                        Cl_gestor.NOM_QUERY("UPDATE vendas SET checar =1 WHERE id_pedido = @ped1", parametroy);

                                    }

                                }catch(Exception ex){
                                    Toast.MakeToast("");
                                    Users = null;
                                }
                            

                        }catch(Exception ex){
                             
                             int conta = dados2.Rows.Count;

                             using(var client = new HttpClient())
                                try{

                                   string url = enderecowsporta + "/selectcontalinha.php" + "?par=" + vars.numeropedidosalvo;
                                   var response = await client.GetAsync(url);
                                   response.EnsuranceSucessStatusCode();

                                   var stringResult = await response.Content.ReadAsStringAsync();
                                    tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                                    var contagem = 0;
                                    contagem = Convert.ToInt32(colecaoPegarid[0].seqmax);

                                    if(conta != contagem){
                                        string urlx = enderecowsporta + "/excluir" + "?par=" + vars.numeropedidosalvo; 
                                        var responsex = await client.GetAsync(urlx);
                                        responsex.EnsuranceSucessStatusCode();

                                        var stringResultx = await responsex.Content.ReadAsStringAsync(stringResultx);

                                        var zero = 0;
                                        Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = " + zero + "WHERE seq = " + seqx);

                                        List<SQLparametro> parametro1x = new List<SQLparametro>();

                                        parametro1x.Add(new SQLparametro("@pedido", pedido1));
                                        parametro1x.Add(new SQLparametro("@data", DateTime.MinValue));

                                        CL_gestor.NOM_QUERY("UPDATE vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1x);

                                        totall = cv;

                                    }


                                }catch(HttpRequestExcpetion httpRequestExcpetion){

                                    List<SQLparametros> parametro1xx = new List<SQLparametro>();

                                    parametro1xx.Add(new SQLparametro("@pedido", pedido1));
                                    parametro1xx.Add(new SQLparametro("@data", DateTime.MinValue));

                                    CL_gestor.NOM_QUERY("UPDATEA vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1xx);

                                    string urlx = enderecowsporta + "/exlcuir.php" + "?par=" + vars.numeropedidosalvo;
                                    var responsex = await client.GetAsync(urlx);
                                    responsex.EnsuranceSucessStatusCode();

                                    var stringResultx = await responsex.Content.ReadAsStringAsync();

                                    Users = null; 
                                }

                        }
                    }   

                }

                dados1 = Cl_gestor.EXE_QUERY("select * from nvenda ");
                int ped1 = 0, xi = 0;

            }

        }catch(Exception ex){
            Toast.MakeToast();
            int conta = dados2.Rows.Count;

            using(var client = new HttpClient())
                try{

                     string url = endereciwsporta + "/selectcontalinha.php" + "?par=" + vars.numeropedidosalvo;
                     var response = await client.GetAsync(url);
                     response.EnsuranceSucessStatusCode();
                    
                     var stringResult = await response.Content.ReadAsStringAsync();

                     tslv020[] colecaoPegarid = JsonConvert.DeserializeObject<tslv020[]>(stringResult);

                     int contagem = 0;
                     contagem = Convert.ToInt32(colecaoPegarid[0].seqmax);

                     if(conta != contagem)
                     {
                        
                        string urlx = enderecowsporta + "/exclui.php" + "?par=" + vars.numeropedidosalvo;
                        var responsex = await client.GetAsync(urlx);
                        responsex.EnsuranceSucessStatusCode();

                        var stringResultx = await response.Content.ReadAsStringAsync();

                        int zero = 0;
                        Cl_gestor.NOM_QUERY("UPDATE vendasprd SET nrovendabco = " + zero + "WHERE seq = " + seqx);

                        List<SQLparametro> parametro1x = new List<SQLparametro>();

                        parametro1x.Add(new SQLparametro("@pedido", pedido1));
                        parametro1x.Add(new SQLparametro("@data", DateTime.MinValue));

                        Cl_gestor.NOM_QUERY("UDPDATE vendas SET  datasincro = @data WHERE id_pedido == @pedido",parametro1x);

                        totall = cv;
                     }

                }catch(HttpRequestException httpRequestException){

                    List<SQLparametro> parametro1xx = new List<SQLparametro>();

                    parametro1xx.Add(new SQLparametro("@pedido", pedido1));
                    parametro1xx.Add(new SQLparametro("@data", DateTime.MinValue));

                    Cl_gestor.NOM_QUERY("UPDATE vendas SET datasincro = @data WHERE id_pedido == @pedido", parametro1xx);

                    string urlx = endrecowsporta + "/exclui.php" + "?par=" + vars.numeropedidosalvo;
                    var responsex = await client.GetAsync(urlx);
                    responsex.EnsuranceSucessStatusCode();

                    var stringresultx = await responsex.Content.ReadAsStringAsync();

                    Users = null;

                }
        }

       

    }
        
    pbar.Dismiss();
  