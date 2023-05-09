Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Microsoft.SqlServer
Public Class Form1
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fechaDesde As String = dtpFechaInicio.Value.ToString("yyyy-MM-dd")
        Dim fechaHasta As String = dtpFechaFin.Value.ToString("yyyy-MM-dd")
        Dim response As Response = read(fechaDesde, fechaHasta)
        consultarDatos(response)
    End Sub

    Private Sub consultarDatos(response As Response)
        Dim serie As Serie = response.seriesResponse.Series(0)
        lbSerie.Text = "Serie: " & serie.Title

        For Each dataSerie As DataSerie In serie.Data
            If dataSerie.Data.Equals("N/E") Then Continue For
            lbFecha.Text = "Fecha: " & dataSerie.Fecha
            lbPrecio.Text = "Precio: " & dataSerie.Data
        Next

        Console.ReadLine()
    End Sub
    Private Shared Function read(fechaDesde As String, fechaHasta As String) As Response
        Try
            Dim url As String = "https://www.banxico.org.mx/SieAPIRest/service/v1/series/SF43718/datos/" & fechaDesde & "/" & fechaHasta
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(url), HttpWebRequest)
            request.Accept = "application/json"
            request.Headers("Bmx-Token") = "69260904c3e398685c78e54928e7129fb21c7f79443e3c8b59e5c91f8319ef47"
            Dim response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim jsonSerializer As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(Response))
            Dim objResponse As Object = jsonSerializer.ReadObject(response.GetResponseStream())
            Dim jsonResponse As Response = TryCast(objResponse, Response)
            Return jsonResponse
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Return Nothing
    End Function

End Class
