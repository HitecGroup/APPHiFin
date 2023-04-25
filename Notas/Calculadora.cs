@page "/calculadora"

@using Microsoft.AspNetCore.Authorization
@using Microsoft.AspNetCore.Components.WebAssembly.Authentication
@using Microsoft.VisualBasic
@using APPHiFin.Shared
@using Services
@attribute [Authorize]
@inject HttpClient Http
@inject IMarcaService MarcaService
@inject IModeloService ModeloService
@inject IFinanciamientoService FinanciamientoService

<PageTitle>Calculadora</PageTitle>

@if (lstModelos == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <div class="container-fluid">
        <RadzenCard style="background-color:#f5f5f5;" class="mb-3 pb-2">
        <div class="row mb-2">
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-4 align-items-center d-flex">
                        <RadzenLabel Text="Marca" />
                    </div>
                    <div class="col-md-8">
                        <RadzenDropDown TValue="int" @bind-Value="idMar" Placeholder="Selecciona marca" Data=@(lstMarcas) TextProperty="Nombre" ValueProperty="idMarca" class="w-100" />
                        </div>                         
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-4 align-items-center d-flex">
                        <RadzenLabel Text="Modelo" />
                    </div>
                    <div class="col-md-8">
                        <RadzenDropDown TValue="int" @bind-Value="idMod" Data=@(lstModelos.Where(m => m.IdMarca == idMar)) Placeholder="Selecciona modelo" TextProperty="ModeloSAP" ValueProperty="IdModelo" class="w-100" FilterCaseSensitivity="FilterCaseSensitivity.CaseInsensitive" AllowFiltering="true" />                       
                    </div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-5 align-items-center d-flex">
                        <RadzenLabel Text="Precio sin IVA" />
                    </div>
                    <div class="col-md-7">
                            <RadzenNumeric TValue="double" @bind-Value="vPrecio" class="w-100"></RadzenNumeric>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-5 align-items-center d-flex">
                        <RadzenLabel Text="Renta Anticipada" />
                    </div>
                    <div class="col-md-7">
                            <RadzenNumeric TValue="double" @bind-Value="vRenta" class="w-100"></RadzenNumeric>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-md-6">
                <div class="row mb-2">
                    <div class="col-md-5 align-items-center d-flex">
                        <RadzenLabel Text="Meses" />
                    </div>
                    <div class="col-md-7">
                            <RadzenNumeric TValue="double" @bind-Value="vMeses" class="w-100"></RadzenNumeric>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row mb-2">
                    <div class="col-md-12 align-items-center d-flex">
                            <RadzenButton ButtonStyle="ButtonStyle.Secondary" Text="Calcular" Click=@Calcular />
                    </div>
                </div>
            </div>
        </div>
        </RadzenCard>
        <div class="container mb-4" style="height:60px;">
            <table class="table" >
                <tbody>
                    <tr>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th>&nbsp;</th>
                        <th style="text-align:right;">Sin Intereses</th>
                        <th style="text-align:right;">Con Intereses&nbsp;(@(vINTERES*100))</th>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Renta Mensual</td>
                        <td>&nbsp;</td>
                        <td style="text-align:right;">@vRentaMensualSinInteres.ToString("0,0.00")</td>
                        <td style="text-align:right;">@vRentaMensualConInteres.ToString("0,0.00")</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="container" style="font-size:calc(0.5em + 0.5vw);">
            <table class="table">
                <thead>
                    <tr>
                        <th style="width:5%;">&nbsp;</th>
                        <th style="width:35%;">Concepto</th>
                        <th style="width:20%;">Antes del <br> Beneficio Fiscal</th>
                        <th style="width:20%;">Beneficio Fiscal</th>
                        <th style="width:20%;">Después del <br> Beneficio Fiscal</th>
                    </tr>
                </thead>
                <tbody>
                     <tr>
                        <td>&nbsp;</td>
                        <td>Precio de Maquinaria</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vPrecio.ToString("0,0.00")</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vPrecio.ToString("0,0.00")</td>
                     </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Gastos Iniciales</td>
                        <td style="background-color:#ffe9d8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Renta Anticipada:</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vRentaAnt_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">@vRentaAnt_Out_BF</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vRentaAnt_Out_DBF</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Comisión por apertura (2.5%):</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vComApertura_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">@vComApertura_Out_BF</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vComApertura_Out_DBF</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Ratificación</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vRatificacion</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">&nbsp;</td>
                    </tr>
                    <tr style="font-weight:800; border:solid 2px #c3c3c3;">
                        <td>A</td>
                        <td>Total Gastos Iniciales</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vTotalGIniciales_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">@vTotalGIniciales_Out_BF</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vTotalGIniciales_Out_DBF</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Rentas Ordinarias</td>
                        <td style="background-color:#ffe9d8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Número de Rentas</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vMeses</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Monto de Renta</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vMontoRenta_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">@vMontoRenta_Out_BF</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vMontoRenta_Out_DBF</td>
                    </tr>
                    <tr style="font-weight:800; border:solid 2px #c3c3c3;">
                        <td>B</td>
                        <td>Total Rentas Ordinarias</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vTotalRentaOrd_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">@vTotalRentaOrd_Out_BF</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vTotalRentaOrd_Out_DBF</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Opción de Compra</td>
                        <td style="background-color:#ffe9d8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">&nbsp;</td>
                    </tr>
                    <tr style="font-weight:800; border:solid 2px #c3c3c3;">
                        <td>C</td>
                        <td>Valor de Opción de Compra</td>
                        <td style="background-color:#ffe9d8; text-align:right;">1</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">1.00</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="background-color:#ffe9d8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">&nbsp;</td>
                    </tr>
                    <tr style="font-weight:800; border:solid 2px #c3c3c3;">
                        <td>D</td>
                        <td>Flujo total de financiamiento</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vFlujoTFin_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">@vFlujoTFin_Out_BF</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vFlujoTFin_Out_DBF</td>
                    </tr>
                    <tr  style="font-weight:800;">
                        <td>&nbsp;</td>
                        <td>Precio de maquinaria</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vPrecio.ToString("0,0.00")</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vPrecio.ToString("0,0.00")</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Costo/ Beneficio del financiamiento</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vCostoBeneficio_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vCostoBeneficio_Out_DBF</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Número de años</td>
                        <td style="background-color:#ffe9d8; text-align:right;">3</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">3</td>
                    </tr>
                    <tr style="font-weight:800; border:solid 2px #c3c3c3;">
                        <td>&nbsp;</td>
                        <td>Costo/ Beneficio por año</td>
                        <td style="background-color:#ffe9d8; text-align:right;">@vCostoBeneficioAnual_Out_ABF</td>
                        <td style="background-color:#efffe8; text-align:right;">&nbsp;</td>
                        <td style="background-color:#ddebf7; text-align:right;">@vCostoBeneficioAnual_Out_DBF</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="font-weight:bold;">
                        <th></th>
                        <th>Tasa Anual Global</th>
                        <th style="text-align:right;">@vTasaAnual_Out_ABF</th>
                        <th style="text-align:right;">&nbsp;</th>
                        <th style="text-align:right;">@vTasaAnual_Out_DBF</th>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
}

@code {
        public Financiamiento financiamiento;

    string? IdMarca;

    public IEnumerable<Marca> lstMarcas { get; set; }
    public IEnumerable<Modelo> lstModelos { get; set; }
    public string? Mensaje { get; set; }
    public int idMar;
    public int idMod;
    double vINTERES = 0;
    public double vPrecio = 78020.93;
    public double vRenta = 20;
    public double vMeses = 36;
    public double vResidual = 1;
    public double vTasaReinversion = 0.0;
    public double vInteresPrestamo = 10.0;    
    public double @vRentaMensualSinInteres = 0;
    public double @vRentaMensualConInteres = 0;
    // Renta Anticipada
    public string? vRentaAnt_Out_ABF = "0.0";
    public string? vRentaAnt_Out_BF = "0.0";
    public string? vRentaAnt_Out_DBF = "0.0";
    // Renta Anticipada
    public string? vComApertura_Out_ABF = "0.0";
    public string? vComApertura_Out_BF = "0.0";
    public string? vComApertura_Out_DBF = "0.0";
    // Renta Anticipada
    public string? vRatificacion = "200";

    // Total de gastos iniciales
    public string? vTotalGIniciales_Out_ABF = "0.0";
    public string? vTotalGIniciales_Out_BF = "0.0";
    public string? vTotalGIniciales_Out_DBF = "0.0";

    // Monto de Renta
    public string? vMontoRenta_Out_ABF = "0.0";
    public string? vMontoRenta_Out_BF = "0.0";
    public string? vMontoRenta_Out_DBF = "0.0";

    // Monto de Renta
    public string? vTotalRentaOrd_Out_ABF = "0.0";
    public string? vTotalRentaOrd_Out_BF = "0.0";
    public string? vTotalRentaOrd_Out_DBF = "0.0";

    // Flujo total de financiamiento
    public string? vFlujoTFin_Out_ABF = "0.0";
    public string? vFlujoTFin_Out_BF = "0.0";
    public string? vFlujoTFin_Out_DBF = "0.0";

    // Costo-Beneficio del financiamiento
    public string? vCostoBeneficio_Out_ABF = "0.0";
    public string? vCostoBeneficio_Out_DBF = "0.0";
    public string? vCostoBeneficioAnual_Out_ABF = "0.0";
    public string? vCostoBeneficioAnual_Out_DBF = "0.0";

    // Tasa anual global
    public string? vTasaAnual_Out_ABF = "0.0";
    public string? vTasaAnual_Out_DBF = "0.0";

    protected async override Task OnInitializedAsync()
    {
        try
        {
            lstMarcas = await MarcaService.GetAllMarca();
            lstModelos = await ModeloService.GetAllModelos();         
        }
        catch (Exception e)
        {
            Mensaje = "Error:" + e.Message;
        }
    }

    void Calcular()
    {      

        if (idMar == 5) {
            vINTERES = 3.9 / 100;
        } else
        {
            vINTERES = 5.9 / 100;
        }


        // Renta Anticipada
        double vOpe01_ABF;
        double vOpe01_BF;
        double vOpe01_DBF;
        vOpe01_ABF = (vPrecio * (vRenta / 100));
        vOpe01_BF = vOpe01_ABF * 0.3;
        vOpe01_DBF = vOpe01_ABF - vOpe01_BF;
        vRentaAnt_Out_ABF = vOpe01_ABF.ToString("0,0.00");
        vRentaAnt_Out_BF = vOpe01_BF.ToString("0,0.00");
        vRentaAnt_Out_DBF = vOpe01_DBF.ToString("0,0.00");

        // Comisión por apertura
        double vOpe02_ABF;
        double vOpe02_BF;
        double vOpe02_DBF;
        vOpe02_ABF = (vPrecio * (2.5 / 100));
        vOpe02_BF = vOpe02_ABF * 0.3;
        vOpe02_DBF = vOpe02_ABF - vOpe02_BF;
        vComApertura_Out_ABF = vOpe02_ABF.ToString("0,0.00");
        vComApertura_Out_BF = vOpe02_BF.ToString("0,0.00");
        vComApertura_Out_DBF = vOpe02_DBF.ToString("0,0.00");

        // Total gastos totales
        double vOpe03_ABF;
        double vOpe03_BF;
        double vOpe03_DBF;

        vOpe03_ABF = (vOpe01_ABF + vOpe02_ABF + 200);
        vOpe03_BF = (vOpe01_BF + vOpe02_BF);
        vOpe03_DBF = (vOpe03_ABF - vOpe03_BF);

        vTotalGIniciales_Out_ABF = vOpe03_ABF.ToString("0,0.00");
        vTotalGIniciales_Out_BF = vOpe03_BF.ToString("0,0.00");
        vTotalGIniciales_Out_DBF = vOpe03_DBF.ToString("0,0.00");

        // Monto de Renta
        double vOpe04_ABF = 0.0;
        double vOpe04_BF = 0.0;
        double vOpe04_DBF = 0.0;
        double monto;
        DueDate PayType = DueDate.BegOfPeriod;
        monto = vPrecio * (1 - (vRenta / 100));
        double FVal = 1;

    //@vRentaMensualSinInteres
        //@vRentaMensualConInteres
        vRentaMensualSinInteres = (vPrecio - vOpe01_ABF) / vMeses;
        vRentaMensualConInteres = Financial.Pmt(vINTERES / 12, vMeses, -monto);

        vOpe04_ABF = Financial.Pmt(vINTERES / 12, vMeses, -monto);
        vOpe04_BF = vOpe04_ABF * 0.3;
        vOpe04_DBF = vOpe04_ABF - vOpe04_BF;

        vMontoRenta_Out_ABF = vOpe04_ABF.ToString("0,0.00");
        vMontoRenta_Out_BF = vOpe04_BF.ToString("0,0.00");
        vMontoRenta_Out_DBF = vOpe04_DBF.ToString("0,0.00");

        // Total Rentas Ordinarias
        double vOpe05_ABF = 0.0;
        double vOpe05_BF = 0.0;
        double vOpe05_DBF = 0.0;

        vOpe05_ABF = (vOpe04_ABF * vMeses);
        vOpe05_BF = vOpe05_ABF * 0.3;
        vOpe05_DBF = vOpe05_ABF - vOpe05_BF;

        vTotalRentaOrd_Out_ABF = vOpe05_ABF.ToString("0,0.00");
        vTotalRentaOrd_Out_BF = vOpe05_BF.ToString("0,0.00");
        vTotalRentaOrd_Out_DBF = vOpe05_DBF.ToString("0,0.00");

        // Flujo total de financiamiento
        double vOpe06_ABF = 0.0;
        double vOpe06_BF = 0.0;
        double vOpe06_DBF = 0.0;

        vOpe06_ABF = vOpe03_ABF + vOpe05_ABF + 1;
        vOpe06_BF = vOpe03_BF + vOpe05_BF;
        vOpe06_DBF = vOpe06_ABF - vOpe06_BF;

        vFlujoTFin_Out_ABF = vOpe06_ABF.ToString("0,0.00");
        vFlujoTFin_Out_BF = vOpe06_BF.ToString("0,0.00");
        vFlujoTFin_Out_DBF = vOpe06_DBF.ToString("0,0.00");

        // Costo/ Beneficio del financiamiento
        double vOpe07_ABF = 0.0;
        double vOpe07_DBF = 0.0;

        vOpe07_ABF = vOpe06_ABF - vPrecio;
        vOpe07_DBF = vOpe06_DBF - vPrecio;
        
        vCostoBeneficio_Out_ABF = vOpe07_ABF.ToString("0,0.00");
        vCostoBeneficio_Out_DBF = vOpe07_DBF.ToString("0,0.00");
        
        // Costo/ Beneficio por año
        double vOpe08_ABF = 0.0;
        double vOpe08_DBF = 0.0;
        vOpe08_ABF = vOpe07_ABF / 3;
        vOpe08_DBF = vOpe07_DBF / 3;
        vCostoBeneficioAnual_Out_ABF = vOpe08_ABF.ToString("0,0.00");
        vCostoBeneficioAnual_Out_DBF = vOpe08_DBF.ToString("0,0.00");
        
        // Tasa Anual Global
        double vOpe09_ABF = 0.0;
        double vOpe09_DBF = 0.0;
        vOpe09_ABF = vOpe08_ABF / vPrecio;
        vOpe09_DBF = vOpe08_DBF / vPrecio;
        vTasaAnual_Out_ABF = vOpe09_ABF.ToString("p02");
        vTasaAnual_Out_DBF = vOpe09_DBF.ToString("p02");

    //Console.WriteLine("Marca:" + idMar + ", Modelo:" + idMod + ", Precio:" + vPrecio + ", Renta:" + vRenta + ", Meses:" + vMeses);
    }
}
