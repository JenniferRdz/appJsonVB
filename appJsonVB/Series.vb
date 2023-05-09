Imports System.Runtime.Serialization


<DataContract>
Class Serie
    <DataMember(Name:="titulo")>
    Public Property Title As String
    <DataMember(Name:="idSerie")>
    Public Property IdSerie As String
    <DataMember(Name:="datos")>
    Public Property Data As DataSerie()
End Class

<DataContract>
Class DataSerie
    <DataMember(Name:="fecha")>
    Public Property Fecha As String
    <DataMember(Name:="dato")>
    Public Property Data As String
End Class

<DataContract>
Class SeriesResponse
    <DataMember(Name:="series")>
    Public Property Series As Serie()
End Class

<DataContract>
Class Response
    <DataMember(Name:="bmx")>
    Public Property seriesResponse As SeriesResponse
End Class