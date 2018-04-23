Imports Microsoft.VisualBasic
Imports System.Xml.Serialization
Imports Windows.Storage
Imports Windows.Storage.Streams
Imports Windows.UI.Xaml.Media.Imaging
Imports System.Runtime.InteropServices.WindowsRuntime
Imports System.Collections.Generic
Imports System
Imports System.IO

Namespace Data
	<XmlRoot("Writers")> _
	Public Class Writers
		Inherits List(Of Writer)
	End Class
	<XmlRoot("Writer")> _
	Public Class Writer
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateBorn As String
		Public Property Born() As String
			Get
				Return privateBorn
			End Get
			Set(ByVal value As String)
				privateBorn = value
			End Set
		End Property
		Private privateDied As String
		Public Property Died() As String
			Get
				Return privateDied
			End Get
			Set(ByVal value As String)
				privateDied = value
			End Set
		End Property
		Private privateOccupation As String
		Public Property Occupation() As String
			Get
				Return privateOccupation
			End Get
			Set(ByVal value As String)
				privateOccupation = value
			End Set
		End Property
		Private privateGenres As String
		Public Property Genres() As String
			Get
				Return privateGenres
			End Get
			Set(ByVal value As String)
				privateGenres = value
			End Set
		End Property
		Private privateNotes As String
		Public Property Notes() As String
			Get
				Return privateNotes
			End Get
			Set(ByVal value As String)
				privateNotes = value
			End Set
		End Property
		Private privateImageData As Byte()
		Public Property ImageData() As Byte()
			Get
				Return privateImageData
			End Get
			Set(ByVal value As Byte())
				privateImageData = value
			End Set
		End Property
		Private imageSource_Renamed As BitmapImage
		<XmlIgnore> _
		Public ReadOnly Property ImageSource() As BitmapImage
			Get
				If imageSource_Renamed Is Nothing Then
					SetImageSource()
				End If
				Return imageSource_Renamed
			End Get
		End Property
		Private async Sub SetImageSource()
			Dim stream As New InMemoryRandomAccessStream()
			await stream.WriteAsync(ImageData.AsBuffer())
			stream.Seek(0)

			imageSource_Renamed = New BitmapImage()
			imageSource_Renamed.SetSource(stream)
		End Sub
	End Class

	Public NotInheritable Class DataStorage
		Private Shared writers_Renamed As Writers
		Private Sub New()
		End Sub
		Public Shared ReadOnly Property Writers() As Writers
			Get
				If writers_Renamed Is Nothing Then
					Dim file As StorageFile = StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///Data/Writers.xml")).AsTask().Result

					Dim stream As Stream = file.OpenStreamForReadAsync().Result
					Dim serializier As New XmlSerializer(GetType(Writers))
					writers_Renamed = TryCast(serializier.Deserialize(stream), Writers)
				End If
				Return writers_Renamed
			End Get
		End Property
	End Class
	Public Class WritersData
		Public ReadOnly Property DataSource() As Writers
			Get
				Return DataStorage.Writers
			End Get
		End Property
	End Class
End Namespace